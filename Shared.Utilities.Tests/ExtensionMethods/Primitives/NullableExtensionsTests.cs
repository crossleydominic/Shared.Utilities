using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.ExtensionMethods.Primitives;

namespace Shared.Utilities.Tests.ExtensionMethods.Primitives
{
    [TestFixture]
    public class NullableExtensionsTests
    {
        #region SelectValueOrDefault Tests

        [Test]
        public void SelectValueOrDefault_ValueNull()
        {
            Nullable<int> subject = null;

            string result = subject.SelectValueOrDefault(v=>v.ToString(), string.Empty);

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void SelectValueOrDefault_ValueNotNull()
        {
            Nullable<int> subject = 1;

            string result = subject.SelectValueOrDefault(v => v.ToString(), string.Empty);

            Assert.AreEqual("1", result);
        }

        [Test]
        public void SelectValueOrDefault_DefaultNull()
        {
            Nullable<int> subject = null;

            string result = subject.SelectValueOrDefault(v => v.ToString(), null);

            Assert.AreEqual(null, result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "projection", MatchType = MessageMatch.Contains)]
        public void SelectValueOrDefault_ValueNull_Throws()
        {
            Nullable<int> subject = null;

            string result = subject.SelectValueOrDefault(null, string.Empty);
        }

        #endregion

        #region GetValueAsStringOrEmpty Tests

        [Test]
        public void GetValueAsStringOrEmpty_ValueNull()
        {
            Nullable<int> subject = null;

            string result = subject.GetValueAsStringOrEmpty();

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void GetValueAsStringOrEmpty_ValueNotNull()
        {
            Nullable<int> subject = 1;

            string result = subject.GetValueAsStringOrEmpty();

            Assert.AreEqual("1", result);
        }

        [Test]
        public void GetValueAsStringOrEmpty_WithConversion_ValueNull()
        {
            Nullable<int> subject = null;

            string result = subject.GetValueAsStringOrEmpty(v=>"converted");

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public void GetValueAsStringOrEmpty_WithConversion_ValueNotNull()
        {
            Nullable<int> subject = 1;

            string result = subject.GetValueAsStringOrEmpty(v=>"converted");

            Assert.AreEqual("converted", result);
        }

        #endregion
    }
}
