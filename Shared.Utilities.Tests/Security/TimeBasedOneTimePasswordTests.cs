using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Security.Cryptography;
using Shared.Utilities.Security;

namespace Shared.Utilities.Tests.Security
{
    [TestFixture]
    public class TimeBasedOneTimePasswordTests
    {
        #region Generate Tests


        /// <summary>
        /// Test to make sure an exception is thrown if using a null hmac
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Generate_NullHmacThrowsException()
        {
            TimeBasedOneTimePassword.Generate(new byte[] { 0 }, null);
        }

        /// <summary>
        /// Test to make sure that an exception is thrown if using a null secretkey
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Generate_NullSecretKeyThrowsException()
        {
            TimeBasedOneTimePassword.Generate(null, new HMACSHA1());
        }

        /// <summary>
        /// Test to make sure that an exception is thrown if using an empty secretkey
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Generate_EmptySecretKeyThrowsException()
        {
            TimeBasedOneTimePassword.Generate(new byte[0], new HMACSHA1());
        }

        /// <summary>
        /// Test to make sur ethat an exception is thrown if using a non-defined OTP length
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Generate_UndefinedHOTPLengthThrowsException()
        {
            TimeBasedOneTimePassword.Generate(new byte[] { 0 }, new HMACSHA1(), 0);
        }

        /// <summary>
        /// Test to make sure that an exception is thrown if using a negative timestep
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Generate_ZeroTimeStepThrowsException()
        {
            TimeBasedOneTimePassword.Generate(new byte[] { 0 }, new HMACSHA1(), TimeSpan.FromSeconds(-1), OneTimePasswordLength.SixDigits);
        }

        /// <summary>
        /// Test to make sure that an exception is thrown if using a zero timestep
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Generate_NegativeTimeStepThrowsException()
        {
            TimeBasedOneTimePassword.Generate(new byte[] { 0 }, new HMACSHA1(), TimeSpan.FromSeconds(0), OneTimePasswordLength.SixDigits);
        }

        [Test]
        public void Generate_YieldsExpectedResults()
        {
            //Values taken from RFC specification

            byte[] secret = Encoding.ASCII.GetBytes("12345678901234567890");

            //Item1 is the datetime to generate password for
            //Item2 is the expected result
            //item3 is the hmac algorithm to use.
            List<Tuple<DateTime, string, HMAC>> testValues = new List<Tuple<DateTime, string, HMAC>>()
            {
                new Tuple<DateTime, string, HMAC>(new DateTime(1970,1,1,0,0,59), "94287082", new HMACSHA1()),
                new Tuple<DateTime, string, HMAC>(new DateTime(1970,1,1,0,0,59), "32247374", new HMACSHA256()),
                new Tuple<DateTime, string, HMAC>(new DateTime(1970,1,1,0,0,59), "69342147", new HMACSHA512()),

                new Tuple<DateTime, string, HMAC>(new DateTime(2005,3,18,1,58,29), "07081804", new HMACSHA1()),
                new Tuple<DateTime, string, HMAC>(new DateTime(2005,3,18,1,58,29), "34756375", new HMACSHA256()),
                new Tuple<DateTime, string, HMAC>(new DateTime(2005,3,18,1,58,29), "63049338", new HMACSHA512()),

                new Tuple<DateTime, string, HMAC>(new DateTime(2005,3,18,1,58,31), "14050471", new HMACSHA1()),
                new Tuple<DateTime, string, HMAC>(new DateTime(2005,3,18,1,58,31), "74584430", new HMACSHA256()),
                new Tuple<DateTime, string, HMAC>(new DateTime(2005,3,18,1,58,31), "54380122", new HMACSHA512()),

                new Tuple<DateTime, string, HMAC>(new DateTime(2009,2,13,23,31,30), "89005924", new HMACSHA1()),
                new Tuple<DateTime, string, HMAC>(new DateTime(2009,2,13,23,31,30), "42829826", new HMACSHA256()),
                new Tuple<DateTime, string, HMAC>(new DateTime(2009,2,13,23,31,30), "76671578", new HMACSHA512()),

                new Tuple<DateTime, string, HMAC>(new DateTime(2033,5,18,3,33,20), "69279037", new HMACSHA1()),
                new Tuple<DateTime, string, HMAC>(new DateTime(2033,5,18,3,33,20), "78428693", new HMACSHA256()),
                new Tuple<DateTime, string, HMAC>(new DateTime(2033,5,18,3,33,20), "56464532", new HMACSHA512())
            };

            foreach (var testValue in testValues)
            {
                string result = TimeBasedOneTimePassword.Generate(secret, testValue.Item3, testValue.Item1, TimeSpan.Zero, TimeBasedOneTimePassword.DEFAULT_TIME_STEP, OneTimePasswordLength.EightDigits);
                
                Assert.AreEqual(result, testValue.Item2.ToString());
            }
        }


        /// <summary>
        /// Test to make sure that the default algorithm is HMACSHA1
        /// </summary>
        [Test]
        public void Generate_DefaultsToHmacSha1Algorithm()
        {
            byte[] secretKey = new byte[] { 0 };
            DateTime dt = DateTime.Now;
            string result1 = TimeBasedOneTimePassword.Generate(secretKey, new HMACSHA1());
            string result2 = TimeBasedOneTimePassword.Generate(secretKey);

            Assert.AreEqual(result1, result2);
        }

        /// <summary>
        /// Test to make sure that the defualt length for generated codes is 6 digits.
        /// </summary>
        [Test]
        public void Generate_DefaultsToSixDigitCode()
        {
            string result = TimeBasedOneTimePassword.Generate(new byte[] { 0 });
            Assert.IsTrue(result.Length == 6);
        }

        /// <summary>
        /// Test to make sure that the algorithm can produce 7 digit codes
        /// </summary>
        [Test]
        public void Generate_CanGenerateSevenDigitCodes()
        {
            string result = TimeBasedOneTimePassword.Generate(new byte[] { 0 },new HMACSHA1(), OneTimePasswordLength.SevenDigits);
            Assert.IsTrue(result.Length == 7);
        }

        /// <summary>
        /// Test to make sure that the algorithm can produce 8 digit codes
        /// </summary>
        [Test]
        public void Generate_CanGenerateEightDigitCodes()
        {
            string result = TimeBasedOneTimePassword.Generate(new byte[] { 0 }, new HMACSHA1(), OneTimePasswordLength.EightDigits);
            Assert.IsTrue(result.Length == 8);
        }


        #endregion
    }
}
