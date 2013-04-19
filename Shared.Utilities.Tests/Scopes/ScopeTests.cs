using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.Scopes;

namespace Shared.Utilities.Tests.Scopes
{
    public class StubScope : Scope<string>
    {
        public StubScope(string v) : base(v) {}
    }

    [TestFixture]
    public class ScopeTests
    {
        #region Constructor tests

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullValueThrows()
        {
            new StubScope(null);
        }

        [Test]
        public void Constructor_ValueSet()
        {
            StubScope subject = new StubScope("abc");
            Assert.AreEqual("abc", subject.Value);
        }

        #endregion

    }
}
