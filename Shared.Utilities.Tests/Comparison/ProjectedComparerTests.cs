using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Shared.Utilities.Tests.Comparison
{
    [TestFixture]
    public class ProjectedComparerTests
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
            ProjectedComparer<IntWrapper, int> c = new ProjectedComparer<IntWrapper, int>(null, (x) => { return x.Value; });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullProjection_ThrowsException()
        {
            ProjectedComparer<IntWrapper, int> c = new ProjectedComparer<IntWrapper, int>((x, y) => 0, null);
        }

        [Test]
        public void Constructor_NonNullComparisonAndNonNullProjection_DoesNotThrowException()
        {
            ProjectedComparer<IntWrapper, int> c = new ProjectedComparer<IntWrapper, int>((x, y) => 0, x => x.Value);
        
        }

        #endregion

        #region CompareTo Tests

        [Test]
        public void CompareTo_ComparisonAndProjectionApplied()
        {
            List<IntWrapper> source = new List<IntWrapper>() { 
                new IntWrapper(){ Value=5},
                new IntWrapper(){ Value=4},
                new IntWrapper(){ Value=3},
                new IntWrapper(){ Value=2},
                new IntWrapper(){ Value=1},
                new IntWrapper(){ Value=0}
            };
            source.Sort(ProjectedComparer.Create<IntWrapper, int>(
                (x, y) => x.CompareTo(y),
                s => s.Value
                ));

            for (int i = 0; i < source.Count; i++)
            {
                Assert.AreEqual(i, source[i].Value);
            }
        }

        #endregion
    }
}
