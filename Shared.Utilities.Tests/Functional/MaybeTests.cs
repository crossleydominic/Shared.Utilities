using System;
using NUnit.Framework;
using Shared.Utilities.Functional;

namespace Shared.Utilities.Tests.Functional
{
    [TestFixture]
    public class MaybeTests
    {
        #region Constructor tests

        [Test]
        public void Constructor_NullArgumentShouldHaveNoValue()
        {
            Maybe<string> str = new Maybe<string>(null);

            Assert.IsFalse(str.HasValue);
        }

        [Test]
        public void Constructor_NotNullArgumentShouldHaveValue()
        {
            Maybe<string> str = new Maybe<string>("abc");

            Assert.IsTrue(str.HasValue);
            Assert.AreEqual("abc", str.Value);
        }

        #endregion

        #region Type conversion tests

        [Test]
        public void Conversion_NonNullValueConversionHasValue()
        {
            Maybe<string> str = "abc";

            Assert.IsTrue(str.HasValue);
            Assert.AreEqual("abc", str.Value);
        }
        
        #endregion

        #region Apply tests

        [Test]
        public void Apply_NullFunctionReturnsEmptyMaybe()
        {
            Maybe<string> str = new Maybe<string>("abc");

            Maybe<object> obj = str.Apply<object>(null);

            Assert.IsNotNull(obj);
            Assert.IsFalse(obj.HasValue);
        }

        [Test]
        public void Apply_EmptyMaybeReturnsEmptyMaybe()
        {
            Maybe<string> str = new Maybe<string>(null);

            Maybe<object> obj = str.Apply(x=>(Object)x);

            Assert.IsNotNull(obj);
            Assert.IsFalse(obj.HasValue);
        }

        [Test]
        public void Apply_NonEmptyMaybeReturnsNonEmptyMaybe()
        {
            Maybe<string> str = new Maybe<string>("abc");

            Maybe<string> obj = str.Apply(x => x + "123");

            Assert.IsNotNull(obj);
            Assert.IsTrue(obj.HasValue);
            Assert.AreEqual("abc123", obj.Value);
        }

        #endregion

        #region Value tests

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Value_GettingNoValue_Throws()
        {
            Maybe<string> str = new Maybe<string>(null);

            string str2 = str.Value;
        }

        #endregion
    }
}
