using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.Security;
using System.Security.Cryptography;

namespace Shared.Utilities.Tests.Security
{
    /// <summary>
    /// A set of tests for generating HOTP codes
    /// </summary>
    [TestFixture]
    public class HmacOneTimePasswordTests
    {
        #region Generate Tests

        /// <summary>
        /// Test to make sure an exception is thrown if using a null hmac
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Generate_NullHmacThrowsException()
        {
            HmacOneTimePassword.Generate(new byte[] { 0 }, 0, null);
        }

        /// <summary>
        /// Test to make sure that an exception is thrown if using a null secretkey
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Generate_NullSecretKeyThrowsException()
        {
            HmacOneTimePassword.Generate(null, 0, new HMACSHA1());
        }

        /// <summary>
        /// Test to make sure that an exception is thrown if using an empty secretkey
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Generate_EmptySecretKeyThrowsException()
        {
            HmacOneTimePassword.Generate(new byte[0], 0, new HMACSHA1());
        }

        /// <summary>
        /// Test to make sur ethat an exception is thrown if using a non-defined HOTP length
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Generate_UndefinedHOTPLengthThrowsException()
        {
            HmacOneTimePassword.Generate(new byte[] { 0 }, 0, new HMACSHA1(), 0);
        }

        /// <summary>
        /// Test to ensure that our algorithm yields the same results as the specifications examples
        /// </summary>
        [Test]
        public void Generate_YieldsExpectedResults()
        {
            //Values taken from RFC specification

            byte[] secret = Encoding.ASCII.GetBytes("12345678901234567890");

            //Item1 = secretKey,
            //Item2 = counter,
            //Item3 = expected result.
            List<Tuple<byte[], uint, uint>> testValues = new List<Tuple<byte[], uint, uint>>(){
                    new Tuple<byte[], uint, uint>(secret, 0, 755224),
                    new Tuple<byte[], uint, uint>(secret, 1, 287082),
                    new Tuple<byte[], uint, uint>(secret, 2, 359152),
                    new Tuple<byte[], uint, uint>(secret, 3, 969429),
                    new Tuple<byte[], uint, uint>(secret, 4, 338314),
                    new Tuple<byte[], uint, uint>(secret, 5, 254676),
                    new Tuple<byte[], uint, uint>(secret, 6, 287922),
                    new Tuple<byte[], uint, uint>(secret, 7, 162583),
                    new Tuple<byte[], uint, uint>(secret, 8, 399871),
                    new Tuple<byte[], uint, uint>(secret, 9, 520489),
                };

            foreach (var testValue in testValues)
            {
                string result = HmacOneTimePassword.Generate(testValue.Item1, testValue.Item2, new HMACSHA1(), OneTimePasswordLength.SixDigits );

                Assert.AreEqual(result, testValue.Item3.ToString());
            }
        }

        /// <summary>
        /// Test to make sure that the default algorithm is HMACSHA1
        /// </summary>
        [Test]
        public void Generate_DefaultsToHmacSha1Algorithm()
        {
            byte[] secretKey = new byte[] { 0 };
            string result1 = HmacOneTimePassword.Generate(secretKey, 1, new HMACSHA1());
            string result2 = HmacOneTimePassword.Generate(secretKey, 1);

            Assert.AreEqual(result1, result2);
        }

        /// <summary>
        /// Test to make sure that the defualt length for generated codes is 6 digits.
        /// </summary>
        [Test]
        public void Generate_DefaultsToSixDigitCode()
        {
            string result = HmacOneTimePassword.Generate(new byte[] { 0 }, 1);
            Assert.IsTrue(result.Length == 6);
        }

        /// <summary>
        /// Test to make sure that the algorithm can produce 7 digit codes
        /// </summary>
        [Test]
        public void Generate_CanGenerateSevenDigitCodes()
        {
            string result = HmacOneTimePassword.Generate(new byte[] { 0 }, 1, new HMACSHA1(), OneTimePasswordLength.SevenDigits);
            Assert.IsTrue(result.Length == 7);
        }

        /// <summary>
        /// Test to make sure that the algorithm can produce 8 digit codes
        /// </summary>
        [Test]
        public void Generate_CanGenerateEightDigitCodes()
        {
            string result = HmacOneTimePassword.Generate(new byte[] { 0 }, 1, new HMACSHA1(), OneTimePasswordLength.EightDigits);
            Assert.IsTrue(result.Length == 8);
        }

        #endregion
    }
}
