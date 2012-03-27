using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Utilities.ExtensionMethods.Primitives;
using NUnit.Framework;

namespace Shared.Utilities.Tests.ExtensionMethods.Primitives
{
    [TestFixture]
    public class MemberInfoExtensionsTests
    {
        #region Test Classes

        [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
        public class Stub1Attribute : Attribute { }

        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
        public class Stub2Attribute : Attribute { }

        [Stub1]
        public class StubClass1
        {
        }

        public class InheritStubClass1 : StubClass1
        {
        }

        [Stub1]
        [Stub1]
        [Stub2]
        [Stub2]
        public class StubClass2
        {
        }

        #endregion

        #region GetCustomAttribute Tests

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetCustomAttribute_MemberInfoNull()
        {
            Type t = null;
            t.GetCustomAttribute<Stub1Attribute>();
        }

        [Test]
        public void GetCustomAttribute_AttributeSpecified()
        {
            Stub1Attribute attr = typeof(StubClass1).GetCustomAttribute<Stub1Attribute>();

            Assert.NotNull(attr);
        }

        [Test]
        public void GetCustomAttribute_AttributeSpecified_WithInherit()
        {
            Stub1Attribute attr = typeof(InheritStubClass1).GetCustomAttribute<Stub1Attribute>();

            Assert.NotNull(attr);
        }

        [Test]
        public void GetCustomAttribute_AttributeSpecified_WithoutInherit()
        {
            Stub1Attribute attr = typeof(InheritStubClass1).GetCustomAttribute<Stub1Attribute>(false);

            Assert.IsNull(attr);
        }

        #endregion

        #region GetCustomAttributes Tests

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetCustomAttributes_MemberInfoNull()
        {
            Type t = null;
            t.GetCustomAttributes<Stub1Attribute>();
        }

        [Test]
        public void GetCustomAttributes_AttributeSpecified()
        {
            IList<Stub1Attribute> attrs = typeof(StubClass1).GetCustomAttributes<Stub1Attribute>();

            Assert.NotNull(attrs);
            Assert.AreEqual(1, attrs.Count);
            Assert.NotNull(attrs[0]);
        }

        [Test]
        public void GetCustomAttributes_AttributeSpecified_WithInherit()
        {
            IList<Stub1Attribute> attrs = typeof(InheritStubClass1).GetCustomAttributes<Stub1Attribute>();

            Assert.NotNull(attrs);
            Assert.AreEqual(1, attrs.Count);
            Assert.NotNull(attrs[0]);
        }

        [Test]
        public void GetCustomAttributes_AttributeSpecified_WithoutInherit()
        {
            IList<Stub1Attribute> attrs = typeof(InheritStubClass1).GetCustomAttributes<Stub1Attribute>(false);

            Assert.NotNull(attrs);
            Assert.AreEqual(0, attrs.Count);
        }

        [Test]
        public void GetCustomAttributes_AttributeSpecifiedMoreThanOnce()
        {
            IList<Stub1Attribute> attrs = typeof(StubClass2).GetCustomAttributes<Stub1Attribute>();

            Assert.NotNull(attrs);
            Assert.AreEqual(2, attrs.Count);
            Assert.NotNull(attrs[0]);
            Assert.NotNull(attrs[1]);
        }

        #endregion
    }
}
