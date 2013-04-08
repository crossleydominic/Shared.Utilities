using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.Tests
{
    [TestFixture]
    public class EitherTests
    {
        #region CreateLeft Tests

        [Test]
        public void CreateLeft_EitherHasLeftValue()
        {
            var result = Either<string, bool>.CreateLeft("value");

            Assert.IsFalse(result.HasRight);
            Assert.AreEqual("value", result.LeftValue);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateLeft_EitherDoesNotHaveRightValue()
        {
            var result = Either<string, bool>.CreateLeft("value");

            Assert.IsFalse(result.HasRight);

            var temp = result.RightValue;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), MatchType = MessageMatch.Contains, ExpectedMessage = "leftValue")]
        public void CreateLeft_NullValueThrows()
        {
            var result = Either<string, bool>.CreateLeft(null);
        }

        #endregion

        #region CreateRight Tests

        [Test]
        public void CreateRight_EitherHasRightValue()
        {
            var result = Either<string, bool>.CreateRight(true);

            Assert.IsTrue(result.HasRight);
            Assert.AreEqual(true, result.RightValue);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateRight_EitherDoesNotHaveLeftValue()
        {
            var result = Either<string, bool>.CreateRight(true);

            Assert.IsTrue(result.HasRight);

            var temp = result.LeftValue;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), MatchType = MessageMatch.Contains, ExpectedMessage = "rightValue")]
        public void CreateRight_NullValueThrows()
        {
            var result = Either<string, object>.CreateRight(null);
        }

        #endregion

        #region Apply Tests

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "leftAction", MatchType = MessageMatch.Contains)]
        public void Apply_NoReturn_NullLeftActionThrows()
        {
            var result = Either<string, int>.CreateLeft("value");

            result.Apply(null, (r) => { });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "rightAction", MatchType = MessageMatch.Contains)]
        public void Apply_NoReturn_NullRightActionThrows()
        {
            var result = Either<string, int>.CreateRight(1);

            result.Apply(l => { }, null);
        }

        [Test]
        public void Apply_NoReturn_LeftActionRun()
        {
            var result = Either<int, int>.CreateLeft(1);

            bool leftBranchTaken = false;
            bool rightBranchTaken = false;
            result.Apply(l => { leftBranchTaken = true; }, r => { rightBranchTaken = false; });

            Assert.IsTrue(leftBranchTaken);
            Assert.IsFalse(rightBranchTaken);
        }

        [Test]
        public void Apply_NoReturn_RightActionRun()
        {
            var result = Either<int, int>.CreateRight(1);

            bool leftBranchTaken = false;
            bool rightBranchTaken = false;
            result.Apply(l => { leftBranchTaken = false; }, r => { rightBranchTaken = true; });

            Assert.IsFalse(leftBranchTaken);
            Assert.IsTrue(rightBranchTaken);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "leftFunc", MatchType = MessageMatch.Contains)]
        public void Apply_WithReturn_NullLeftActionThrows()
        {
            var result = Either<string, int>.CreateLeft("value");

            result.Apply(null, (r) => 1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "rightFunc", MatchType = MessageMatch.Contains)]
        public void Apply_WithReturn_NullRightActionThrows()
        {
            var result = Either<string, int>.CreateRight(1);

            result.Apply(l => 1, null);
        }

        [Test]
        public void Apply_WithReturn_LeftFuncRun()
        {
            var result = Either<int, int>.CreateLeft(1);

            int branchOutput = -1;
            branchOutput = result.Apply(l => 1, r => 2);

            Assert.AreEqual(1, branchOutput);
        }

        [Test]
        public void Apply_WithReturn_RightFuncRun()
        {
            var result = Either<int, int>.CreateRight(1);

            int branchOutput = -1;
            branchOutput = result.Apply(l => 1, r => 2);

            Assert.AreEqual(2, branchOutput);
        }

        #endregion

        #region HasRight Tests

        [Test]
        public void HasRight_TrueWhenCreatedWithRight()
        {
            var result = Either<int, int>.CreateRight(1);

            Assert.IsTrue(result.HasRight);
        }

        [Test]
        public void HasRight_FalseWhenCreatedWithLeft()
        {
            var result = Either<int, int>.CreateLeft(1);

            Assert.IsFalse(result.HasRight);
        }

        #endregion

        #region LeftValue Tests

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LeftValue_ThrowsWhenCreatedAsRight()
        {
            var result = Either<int, int>.CreateRight(1);

            var temp = result.LeftValue;
        }

        [Test]
        public void LeftValue_DoesNotThrowWhenCreatedAsLeft()
        {
            var result = Either<int, int>.CreateLeft(1);

            Assert.AreEqual(1, result.LeftValue);
        }

        #endregion

        #region RightValue Tests

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RightValue_ThrowsWhenCreatedAsLeft()
        {
            var result = Either<int, int>.CreateLeft(1);

            var temp = result.RightValue;
        }

        [Test]
        public void RightValue_DoesNotThrowWhenCreatedAsRight()
        {
            var result = Either<int, int>.CreateRight(1);

            Assert.AreEqual(1, result.RightValue);
        }

        #endregion

        #region Lefts Tests

        [Test]
        [ExpectedException(typeof (ArgumentNullException), MatchType = MessageMatch.Contains, ExpectedMessage = "eithers")]
        public void Lefts_NullCollectionThrows()
        {
            Either.Lefts<string,int>(null);
        }

        [Test]
        public void Lefts_EmptyCollectionDoesNotThrow()
        {
            var result = Either.Lefts<string, int>(new List<Either<string, int>>());

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [Test]
        public void Lefts_LeftValuesCollated()
        {
            var result = Either.Lefts<string, int>(new List<Either<string, int>>()
            {
                Either<string,int>.CreateLeft("1"),
                Either<string,int>.CreateRight(2),
                Either<string,int>.CreateLeft("3"),
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(i=>i == "1"));
            Assert.IsTrue(result.Any(i => i == "3"));
        }

        #endregion

        #region Rights Tests

        [Test]
        [ExpectedException(typeof(ArgumentNullException), MatchType = MessageMatch.Contains, ExpectedMessage = "eithers")]
        public void Rights_NullCollectionThrows()
        {
            Either.Rights<string, int>(null);
        }

        [Test]
        public void Rights_EmptyCollectionDoesNotThrow()
        {
            var result = Either.Rights<string, int>(new List<Either<string, int>>());

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }

        [Test]
        public void Rights_RightValuesCollated()
        {
            var result = Either.Rights<string, int>(new List<Either<string, int>>()
            {
                Either<string,int>.CreateRight(1),
                Either<string,int>.CreateLeft("2"),
                Either<string,int>.CreateRight(3),
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(i => i == 1));
            Assert.IsTrue(result.Any(i => i == 3));
        }

        #endregion

        #region Rights Tests

        [Test]
        [ExpectedException(typeof(ArgumentNullException), MatchType = MessageMatch.Contains, ExpectedMessage = "eithers")]
        public void Partition_NullCollectionThrows()
        {
            Either.Partition<string, int>(null);
        }

        [Test]
        public void Partition_EmptyCollectionDoesNotThrow()
        {
            var result = Either.Partition<string, int>(new List<Either<string, int>>());

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Item1.Any());
            Assert.IsFalse(result.Item2.Any());
        }

        [Test]
        public void Partition_ValuesCollated()
        {
            var result = Either.Partition<string, int>(new List<Either<string, int>>()
            {
                Either<string,int>.CreateRight(1),
                Either<string,int>.CreateLeft("2"),
                Either<string,int>.CreateRight(3),
            });

            Assert.IsNotNull(result);

            Assert.AreEqual(1, result.Item1.Count());
            Assert.IsTrue(result.Item1.Any(i => i == "2"));

            Assert.AreEqual(2, result.Item2.Count());
            Assert.IsTrue(result.Item2.Any(i => i == 1));
            Assert.IsTrue(result.Item2.Any(i => i == 3));
        }

        #endregion
    }
}
