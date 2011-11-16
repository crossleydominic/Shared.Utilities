using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.ExtensionMethods.Primitives;
using System.Globalization;
using System.IO;

namespace Shared.Utilities.Tests.ExtensionMethods.Primitives
{
	/// <summary>
	/// A set of tests for testing the DateTime ExtensionMethods
	/// </summary>
	[TestFixture]
	public class DateTimeExtensionsTests
	{
		#region Static members

		/// <summary>
		/// The culture that we'll use to do all our tests against.
		/// Don't just use the current threads culture.
		/// </summary>
		public static CultureInfo _enGbCulture = System.Globalization.CultureInfo.GetCultureInfo("en-gb");

		#endregion

		#region MoveToStartOfDay Tests

		/// <summary>
		/// Tests when the date is right at the end of the day
		/// </summary>
		[Test]
		public void MoveToStartOfDay_DateIsAtEndOfDay()
		{
			DateTime testDate = DateTime.Parse("01/01/2011 23:59:59", _enGbCulture.DateTimeFormat);
			testDate = testDate.AddMilliseconds(999);

			DateTime resultDate = testDate.MoveToStartOfDay();

			Assert.IsTrue(resultDate.ToString(_enGbCulture) == "01/01/2011 00:00:00");
		}

		/// <summary>
		/// Tests when the date is right at the start of the day
		/// </summary>
		[Test]
		public void MoveToStartOfDay_DateIsAtStartOfDay()
		{
			DateTime testDate = DateTime.Parse("01/01/2011 00:00:00", _enGbCulture.DateTimeFormat);

			DateTime resultDate = testDate.MoveToStartOfDay();

			Assert.IsTrue(resultDate.ToString(_enGbCulture) == "01/01/2011 00:00:00");
		}

		/// <summary>
		/// Tests when the date is somewhere in the middle of the day
		/// </summary>
		[Test]
		public void MoveToStartOfDay_DateIsInMiddleOfDay()
		{
			//Just some random time in the middle of the day
			DateTime testDate = DateTime.Parse("01/01/2011 10:01:53", _enGbCulture.DateTimeFormat);

			DateTime resultDate = testDate.MoveToStartOfDay();

			Assert.IsTrue(resultDate.ToString(_enGbCulture) == "01/01/2011 00:00:00");
		}

		#endregion

		#region MoveToEndOfDay Tests

		/// <summary>
		/// Tests when the date is right at the end of the day
		/// </summary>
		[Test]
		public void MoveToEndOfDay_DateIsAtEndOfDay()
		{
			DateTime testDate = DateTime.Parse("01/01/2011 23:59:59", _enGbCulture.DateTimeFormat);
			testDate = testDate.AddMilliseconds(999);

			DateTime resultDate = testDate.MoveToEndOfDay();

			Assert.IsTrue(resultDate.ToString(_enGbCulture) == "01/01/2011 23:59:59");
		}

		/// <summary>
		/// Tests when the date is right at the start of the day
		/// </summary>
		[Test]
		public void MoveToEndOfDay_DateIsAtStartOfDay()
		{
			DateTime testDate = DateTime.Parse("01/01/2011 00:00:00", _enGbCulture.DateTimeFormat);

			DateTime resultDate = testDate.MoveToEndOfDay();

			Assert.IsTrue(resultDate.ToString(_enGbCulture) == "01/01/2011 23:59:59");
		}

		/// <summary>
		/// Tests when the date is somewhere in the middle of the day
		/// </summary>
		[Test]
		public void MoveToEndOfDay_DateIsInMiddleOfDay()
		{
			//Just some random time in the middle of the day
			DateTime testDate = DateTime.Parse("01/01/2011 10:01:53", _enGbCulture.DateTimeFormat);

			DateTime resultDate = testDate.MoveToEndOfDay();

			Assert.IsTrue(resultDate.ToString(_enGbCulture) == "01/01/2011 23:59:59");
		}

		#endregion

        #region ToFileSystemSafeString

        [Test]
        public void ToFileSystemSafeString_DoesNotContainInvalidCharacters()
        {
            DateTime dt = DateTime.Now;

            string fsString = dt.ToFileSystemSafeString();

            Assert.IsTrue(fsString.Contains(dt.Day.ToString()));
            Assert.IsTrue(fsString.Contains(dt.Month.ToString()));
            Assert.IsTrue(fsString.Contains(dt.Year.ToString()));
            Assert.IsTrue(fsString.Contains(dt.Hour.ToString()));
            Assert.IsTrue(fsString.Contains(dt.Minute.ToString()));
            Assert.IsTrue(fsString.Contains(dt.Second.ToString()));

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                Assert.IsFalse(fsString.Contains(c));
            }

            foreach (char c in Path.GetInvalidPathChars())
            {
                Assert.IsFalse(fsString.Contains(c));
            }
        }

        #endregion

        #region ToUnixTime Tests

        /// <summary>
        /// Test to make sure that ToUnixTime yields a set of expected results.
        /// </summary>
        [Test]
        public void ToUnixTime_YieldsExpectedResults()
        {
            List<Tuple<DateTime, long>> testValues = new List<Tuple<DateTime, long>>()
            {
                new Tuple<DateTime, long>(new DateTime(1970,1,1,0,0,0), 0),
                new Tuple<DateTime, long>(new DateTime(1970,1,1,0,0,1), 1),
                new Tuple<DateTime, long>(new DateTime(2011,11,16,11,30,17), 1321443017)
            };

            foreach (var testValue in testValues)
            {
                Assert.AreEqual(testValue.Item1.ToUnixTime(), testValue.Item2);
            }
        }
        
        #endregion
    }
}
