using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Utilities.ExtensionMethods.Object;
using NUnit.Framework;

namespace Shared.Utilities.Tests.ExtensionMethods.Object
{
    [TestFixture]
    public class ObjectExtensionsTests
    {
        #region SafeDeRef tests

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SafeDeRef_DeRefFuncIsNull_Throws()
        {
            string str = "abcdefg";
            int length = str.SafeDeRef<string,int>(null);
        }

        [Test]
        public void SafeDeRef_NonNullObjectDeRefsCorrectly_ValueType()
        {
            string str = "abcdefg";
            int length = str.SafeDeRef(s=>s.Length);

            Assert.AreEqual(7, length);
        }

        [Test]
        public void SafeDeRef_NullObjectDeRefsCorrectly_ValueType()
        {
            string str = null;
            int length = str.SafeDeRef(s => s.Length);

            Assert.AreEqual(0, length);
        }

        [Test]
        public void SafeDeRef_NonNullObjectDeRefsCorrectly_ReferenceType()
        {
            string str = "abcdefg";
            string result = str.SafeDeRef(s => s.Substring(1));

            Assert.AreEqual("bcdefg", result);
        }

        [Test]
        public void SafeDeRef_NullObjectDeRefsCorrectly_ReferenceType()
        {
            string str = null;
            string result = str.SafeDeRef(s => s.Substring(1));

            Assert.AreEqual(null, result);
        }


        #endregion
    }
}
