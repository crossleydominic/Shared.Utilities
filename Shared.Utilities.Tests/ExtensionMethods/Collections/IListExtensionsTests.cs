using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.ExtensionMethods.Collections;

namespace Shared.Utilities.Tests.ExtensionMethods.Collections
{
    [TestFixture]
    public class IListExtensionsTests
    {
        #region FromIndexes Tests

        [Test]
        [ExpectedException(typeof (ArgumentNullException), ExpectedMessage = "list", MatchType = MessageMatch.Contains)]
        public void FromIndexes_NullList_Throws()
        {
            IList<int> subject = null;

            IList<int> result = subject.FromIndexes(2, 3, 2);
        }

        [Test]
        public void FromIndexes_EmptyList_DoesNotThrows()
        {
            IList<int> subject = new List<int>();

            IList<int> result = subject.FromIndexes(IndexOutOfRangeBehaviour.Ignore, 2, 3, 2);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "indexes", MatchType = MessageMatch.Contains)]
        public void FromIndexes_NullIndexes_Throws()
        {
            IList<int> subject = new List<int>() { 1, 2, 3, 4, 5 };

            IList<int> result = subject.FromIndexes(null);
        }

        [Test]
        [ExpectedException(typeof (ArgumentException), ExpectedMessage = "outOfRange", MatchType = MessageMatch.Contains)]
        public void FromIndexes_UndefinedOutOfRangeBehaviour_Throws()
        {
            IList<int> subject = new List<int>() { 1, 2, 3, 4, 5 };

            IList<int> result = subject.FromIndexes((IndexOutOfRangeBehaviour)5, 1, 2);
        }

        [Test]
        public void FromIndexes_EmptyIndexes_DoesNotThrows()
        {
            IList<int> subject = new List<int>() { 1, 2, 3, 4, 5 };

            IList<int> result = subject.FromIndexes();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void FromIndexes_OutOfRangeWhenIgnoring_DoesNotThrow()
        {
            IList<int> subject = new List<int>() { 1, 2, 3, 4, 5 };

            IList<int> result = subject.FromIndexes(IndexOutOfRangeBehaviour.Ignore, 2, 6, 3);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(3, result[0]);
            Assert.AreEqual(4, result[1]);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void FromIndexes_OutOfRangeWhenNotIgnoring_Throws()
        {
            IList<int> subject = new List<int>() { 1, 2, 3, 4, 5 };

            IList<int> result = subject.FromIndexes(IndexOutOfRangeBehaviour.ThrowException, 2, 6, 3);
        }

        [Test]
        public void FromIndexes_ValuesCollated()
        {
            IList<int> subject = new List<int>() { 1, 2, 3, 4, 5 };

            IList<int> result = subject.FromIndexes(2, 3, 2);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(3, result[0]);
            Assert.AreEqual(4, result[1]);
            Assert.AreEqual(3, result[2]);
        }

        [Test]
        public void FromIndex_DefaultBehaviourIsIgnore()
        {
            IList<int> subject = new List<int>() { 1, 2, 3, 4, 5 };

            IList<int> result = subject.FromIndexes(2, 6, 3);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(3, result[0]);
            Assert.AreEqual(4, result[1]);
        }

        #endregion
    }
}
