using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Shared.Utilities.ExtensionMethods.Primitives;

namespace Shared.Utilities.Security
{
    public static class TimeBasedOneTimePassword
    {
        #region Public constants

        /// <summary>
        /// Default time step of 30 seconds
        /// </summary>
        public static readonly TimeSpan DEFAULT_TIME_STEP = TimeSpan.FromSeconds(30);

        /// <summary>
        /// Default generated code length of 6 digits.
        /// </summary>
        public const OneTimePasswordLength DEFAULT_CODE_LENGTH = OneTimePasswordLength.SixDigits;

        #endregion

        /// <summary>
        /// Generates a new one time password using the current datetime, hmacsha1 algorith,
        /// default timestep, default code length.
        /// </summary>
        /// <param name="secretKey">The secret key to use in the HMAC</param>
        /// <returns>A one time password code of the default length</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if hmac or secret key is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if secret key is empty.</exception>
        public static string Generate(byte[] secretKey)
        {
            return Generate(secretKey, new HMACSHA1(), DateTime.Now, TimeSpan.Zero, DEFAULT_TIME_STEP, OneTimePasswordLength.SixDigits);
        }

        /// <summary>
        /// Generates a new one time password using the current datetime, defualt timestep, default code length
        /// </summary>
        /// <param name="secretKey">The secret key to use in the HMAC</param>
        /// <param name="hmac">The hmac algorithm to use</param>
        /// <returns>A one time password code of the default length</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if hmac or secret key is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if secret key is empty</exception>
        public static string Generate(byte[] secretKey, HMAC hmac)
        {
            return Generate(secretKey, hmac, DateTime.Now, TimeSpan.Zero, DEFAULT_TIME_STEP, OneTimePasswordLength.SixDigits);
        }

        /// <summary>
        /// Generates a new one time password using the current datetime and default timestep
        /// </summary>
        /// <param name="secretKey">The secret key to use in the HMAC</param>
        /// <param name="hmac">The hmac algorithm to use</param>
        /// <param name="otpLength">The required legnth of the returned passcode</param>
        /// <returns>A one time password code of the required size</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if hmac or secret key is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if secret key is empty, optLength is
        /// not defined value.</exception>
        public static string Generate(byte[] secretKey, HMAC hmac, OneTimePasswordLength otpLength)
        {
            return Generate(secretKey, hmac, DateTime.Now, TimeSpan.Zero, DEFAULT_TIME_STEP, otpLength);
        }

        /// <summary>
        /// Generates a new one time password using the current datetime, hmacsha1 algorithm,
        /// default code length
        /// </summary>
        /// <param name="secretKey">The secret key to use in the HMAC</param>
        /// <param name="timeStep">The timestep value to use to calculate the current step</param>
        /// <returns>A one time password code of the default length</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if secret key is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if secret key is empty, timeStep is less than 1 second.</exception>
        public static string Generate(byte[] secretKey, TimeSpan timeStep)
        {
            return Generate(secretKey, new HMACSHA1(), DateTime.Now, TimeSpan.Zero, timeStep, OneTimePasswordLength.SixDigits);
        }

        /// <summary>
        /// Generates a new one time password from the supplied datetime
        /// using the default timestep and code length
        /// </summary>
        /// <param name="secretKey">The secret key to use in the HMAC</param>
        /// <param name="hmac">The hmac algorithm to use</param>
        /// <param name="dt">The datetime to generate a code for</param>
        /// <returns>A one time password code of the required length</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if hmac or secret key is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if secret key is empty, optLength is
        /// not defined value or timeStep is less than 1 second.</exception>
        public static string Generate(byte[] secretKey, HMAC hmac, DateTime dt)
        {
            return Generate(secretKey, hmac, DateTime.Now, TimeSpan.Zero, DEFAULT_TIME_STEP, DEFAULT_CODE_LENGTH);
        }

        /// <summary>
        /// Generates a new one time password from the current datetime
        /// </summary>
        /// <param name="secretKey">The secret key to use in the HMAC</param>
        /// <param name="hmac">The hmac algorithm to use</param>
        /// <param name="timeStep">The timestep value to use to calculate the current step</param>
        /// <param name="otpLength">The required legnth of the returned passcode</param>
        /// <returns>A one time password code of the required length</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if hmac or secret key is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if secret key is empty, optLength is
        /// not defined value or timeStep is less than 1 second.</exception>
        public static string Generate(byte[] secretKey, HMAC hmac, TimeSpan timeStep, OneTimePasswordLength otpLength)
        {
            return Generate(secretKey, hmac, DateTime.Now, TimeSpan.Zero, timeStep, otpLength);
        }

        /// <summary>
        /// Generates a new one time password from the supplied datetime.
        /// </summary>
        /// <param name="secretKey">The secret key to use in the HMAC</param>
        /// <param name="hmac">The hmac algorithm to use</param>
        /// <param name="dt">The date and time to generate a code for</param>
        /// <param name="timeStep">The timestep value to use to calculate the current step</param>
        /// <param name="otpLength">The required legnth of the returned passcode</param>
        /// <returns>A one time password code of the required length</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if hmac or secret key is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if secret key is empty, optLength is
        /// not defined value or timeStep is less than 1 second.</exception>
        public static string Generate(byte[] secretKey, HMAC hmac, DateTime dt, TimeSpan timeStep, OneTimePasswordLength otpLength)
        {
            return Generate(secretKey, hmac, dt, DEFAULT_TIME_STEP, otpLength);
        }

        /// <summary>
        /// Generates a new one time password from the supplied data.
        /// </summary>
        /// <param name="secretKey">The secret key to use in the HMAC</param>
        /// <param name="hmac">The hmac algorithm to use</param>
        /// <param name="dt">The date and time to generate a code for</param>
        /// <param name="offset">Any offsets that should be applied to the supplie date time</param>
        /// <param name="timeStep">The timestep value to use to calculate the current step</param>
        /// <param name="otpLength">The required legnth of the returned passcode</param>
        /// <returns>A one time password code</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if hmac or secret key is null</exception>
        /// <exception cref="System.ArgumentException">Thrown if secret key is empty, optLength is
        /// not defined value or timeStep is less than 1 second.</exception>
        public static string Generate(byte[] secretKey, HMAC hmac, DateTime dt, TimeSpan offset, TimeSpan timeStep, OneTimePasswordLength otpLength)
        {
            #region Input validation

            Insist.IsNotNull(hmac, "hmac");
            Insist.IsNotNull(secretKey, "secretKey");
            Insist.IsNotEmpty(secretKey, "secretKey");
            Insist.IsDefined<OneTimePasswordLength>(otpLength, "optLength");
            Insist.IsAtLeast(timeStep.TotalSeconds, 1, "timeStep");

            #endregion

            dt = dt + offset;

            ulong stepNumber = (ulong)Math.Floor((double)dt.ToUnixTime() / (double)timeStep.TotalSeconds);

            return HmacOneTimePassword.Generate(secretKey, stepNumber, hmac, otpLength);
        }
    }
}
