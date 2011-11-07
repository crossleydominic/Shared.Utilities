using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Shared.Utilities.Tests
{
    [TestFixture]
    public class GenericComparerTests
    {
        #region Constructor Tests

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullComparison_ThrowsException()
        {
            GenericComparer<string> c = new GenericComparer<string>(null);
        }

        [Test]
        public void Constructor_NonNullComparison_DoesNotThrowException()
        {
            GenericComparer<string> c = new GenericComparer<string>((x,y) => { return 0; });
        }

        #endregion

        #region CompareTo Tests

        [Test]
        public void CompareTo_ComparisonApplied()
        {
            List<int> source = new List<int>() { 5, 4, 3, 2, 1, 0 };
            source.Sort(GenericComparer.Create<int>((x, y) => { return x.CompareTo(y); }));

            for (int i = 0; i < source.Count; i++ )
            {
                Assert.AreEqual(i, source[i]);
            }
        }

        #endregion
    }
}
