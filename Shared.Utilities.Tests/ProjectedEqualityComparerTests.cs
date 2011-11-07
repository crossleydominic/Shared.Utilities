using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Shared.Utilities.Tests
{
    [TestFixture]
    public class ProjectedEqualityComparerTests
    {
        #region Test Classes

        private class IntWrapper
        {
            public int Value { get; set; }
        }

        #endregion

        #region Constructor Tests

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullComparison_ThrowsException()
        {
            ProjectedEqualityComparer<IntWrapper, int> c = new ProjectedEqualityComparer<IntWrapper, int>(null, (x) => { return x.Value; });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullProjection_ThrowsException()
        {
            ProjectedEqualityComparer<IntWrapper, int> c = new ProjectedEqualityComparer<IntWrapper, int>((x, y) => true, null);
        }

        [Test]
        public void Constructor_NonNullComparisonAndNonNullProjection_DoesNotThrowException()
        {
            ProjectedEqualityComparer<IntWrapper, int> c = new ProjectedEqualityComparer<IntWrapper, int>((x, y) => true, x => x.Value);

        }

        #endregion

        #region CompareTo Tests

        [Test]
        public void Equals_EqualityComparisonApplied()
        {
            List<IntWrapper> source1 = new List<IntWrapper>() {                 
                new IntWrapper(){ Value=5},
                new IntWrapper(){ Value=4},
                new IntWrapper(){ Value=3}
            };
            List<IntWrapper> source2 = new List<IntWrapper>() {                 
                new IntWrapper(){ Value=3},
                new IntWrapper(){ Value=2},
                new IntWrapper(){ Value=1},
                new IntWrapper(){ Value=0}
            };
            List<IntWrapper> result = source1.Union(source2, ProjectedEqualityComparer.Create<IntWrapper, int>(
                (x, y) => { return x == y; },
                s=>s.Value)).ToList();

            for (int i = 0; i < result.Count; i++)
            {
                Assert.IsTrue(result.Where(x => x.Value == i).Count() == 1);
            }

            Assert.IsTrue(result.Count == 6);
            Assert.IsTrue(result.Where(x => x.Value == 3).Count() == 1);

        }

        #endregion
    }
}
