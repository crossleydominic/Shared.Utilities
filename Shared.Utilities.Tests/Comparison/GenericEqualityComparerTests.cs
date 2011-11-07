using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Shared.Utilities.Tests.Comparison
{
    [TestFixture]
    public class GenericEqualityComparerTests
    {
        #region Constructor Tests

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullComparison_ThrowsException()
        {
            GenericEqualityComparer<string> c = new GenericEqualityComparer<string>(null);
        }

        [Test]
        public void Constructor_NonNullComparison_DoesNotThrowException()
        {
            GenericEqualityComparer<string> c = new GenericEqualityComparer<string>((x, y) => { return false; });
        }

        #endregion

        #region CompareTo Tests

        [Test]
        public void Equals_EqualityComparisonApplied()
        {
            List<int> source1 = new List<int>() { 5, 4, 3 };
            List<int> source2 = new List<int>() { 3, 2, 1, 0 };
            List<int> result = source1.Union(source2, GenericEqualityComparer.Create<int>((x, y) => { return x == y; })).ToList();

            for (int i = 0; i < result.Count; i++)
            {
                Assert.IsTrue(result.Where(x => x == i).Count() == 1);
            }

            Assert.IsTrue(result.Count == 6);
            Assert.IsTrue(result.Where(x => x == 3).Count() == 1);
        }

        #endregion
    }
}
