using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Shared.Utilities.Security
{
    /// <summary>
    /// A class to generate HOTP (HMAC-One-Time-Password) codes.
    /// An implementation of RFC 4226.
    /// </summary>
    public static class HmacOneTimePassword
    {
        #region Public constants

        /// <summary>
        /// The default length of the generated codes
        /// </summary>
        public const HmacOneTimePasswordLength DEFAULT_CODE_LENGTH = HmacOneTimePasswordLength.SixDigits;

        #endregion

        #region Private constants

        /// <summary>
        /// The spec defined length of the counter array to be HMAC'd
        /// </summary>
        private const int COUNTER_LEGNTH = 8;

        #endregion

        #region Public static methods

        /// <summary>
        /// Generates a new 6 digit HOTP code using the HMAC-SHA1 algorithm
        /// </summary>
        /// <param name="secretKey">
        /// The secret key used to generate the HMAC value
        /// </param>
        /// <param name="counter">
        /// The counter value to generate a HOTP code from
        /// </param>
        /// <returns>
        /// The generated HOTP code.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">Thrown if secretKey is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if secretKey is empty</exception>
        public static string Generate(byte[] secretKey, ulong counter)
        {
            return Generate(new HMACSHA1(), secretKey, counter, DEFAULT_CODE_LENGTH);
        }

        /// <summary>
        /// Generates a new HOTP code using the supplied HMAC generater, key, and counter.
        /// Generates a code of the default length.
        /// </summary>
        /// <param name="hmac">The HMAC algorithm to use</param>
        /// <param name="secretKey">The secret key used in HMAC-ing the counter</param>
        /// <param name="counter">The counter to use to generate a HOTP code for</param>
        /// <returns>The generated HOTP code</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if hmac or secretKey is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if secretKey is empty</exception>
        public static string Generate(HMAC hmac, byte[] secretKey, ulong counter)
        {
            return Generate(hmac, secretKey, counter, DEFAULT_CODE_LENGTH);
        }

        /// <summary>
        /// Generates a new HOTP code using the supplied HMAC generater, key, counter and 
        /// HOTP length.
        /// </summary>
        /// <param name="hmac">The HMAC algorithm to use</param>
        /// <param name="secretKey">The secret key used in HMAC-ing the counter</param>
        /// <param name="counter">The counter to use to generate a HOTP code for</param>
        /// <param name="hotpLength">The required length of the generated HOTP code</param>
        /// <returns>The generated HOTP code</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if hmac or secretKey is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if secretKey is empty or hotpLength is not a valid value</exception>
        public static string Generate(HMAC hmac, byte[] secretKey, ulong counter, HmacOneTimePasswordLength hotpLength)
        {
            #region Input validation

            Insist.IsNotNull(hmac, "hmac");
            Insist.IsNotNull(secretKey, "secretKey");
            Insist.IsNotEmpty(secretKey, "secretKey");
            Insist.IsDefined<HmacOneTimePasswordLength>(hotpLength, "hotpLength");

            #endregion

            byte[] hashedCounter = HashCounter(hmac, secretKey, counter);

            uint truncatedHash = DynamicTruncate(hashedCounter);

            //Convert to string and preserve leading 0s
            return GenerateHotpValue(truncatedHash, hotpLength).ToString("D" + (int)hotpLength);
        }

        #endregion

        #region Private static methods

        /// <summary>
        /// Hashes the counter using the hmac and secretkey
        /// </summary>
        private static byte[] HashCounter(HMAC hmacGenerator, byte[] secretKey, ulong counter)
        {
            hmacGenerator.Key = secretKey;

            //Spec says 8byte, array
            byte[] counterBytes = new byte[COUNTER_LEGNTH];

            //Bit converter may return an arry with less than 8 bytes
            byte[] intermediateBytes = BitConverter.GetBytes(counter);

            //Copy number-bytes into padded buffer.
            Array.Copy(intermediateBytes, counterBytes, counterBytes.Length);

            if (BitConverter.IsLittleEndian)
            {
                counterBytes = counterBytes.Reverse().ToArray();
            }

            return hmacGenerator.ComputeHash(counterBytes);
        }

        /// <summary>
        /// Truncates the hashed counter according to the spec
        /// </summary>
        private static uint DynamicTruncate(byte[] hashedCounter)
        {
            //Let OffsetBits be the low-order 4 bits of String[19]
            byte offsetBits = (byte)(hashedCounter[19] & 0x0f);

            //Offset = StToNum(OffsetBits) // 0 <= OffSet <= 15
            uint offset = (uint)offsetBits;

            //Let P = String[OffSet]...String[OffSet+3]
            byte[] p = new byte[4]{ 
                hashedCounter[offset],
                hashedCounter[offset+1],
                hashedCounter[offset+2],
                hashedCounter[offset+3]
            };

            //clear highest bit
            p[0] = (byte)(p[0] & 0x7F);

            //Return the Last 31 bits of P
            if (BitConverter.IsLittleEndian)
            {
                return BitConverter.ToUInt32(new byte[] { p[3], p[2], p[1], p[0] }, 0);
            }
            else
            {
                return BitConverter.ToUInt32(p, 0);
            }
        }

        /// <summary>
        /// Generates the final HOTP value from the truncated value
        /// </summary>
        private static uint GenerateHotpValue(UInt32 truncatedHash, HmacOneTimePasswordLength htopLength)
        {
            //calculate 10^(htopLength-1)
            int calculatedModulo = 10;

            htopLength = htopLength - 1;
            for (int i = 0; i < (int)htopLength; i++)
            {
                calculatedModulo *= 10;
            }

            return (uint)(truncatedHash % calculatedModulo);
        }

        #endregion
    }
}
