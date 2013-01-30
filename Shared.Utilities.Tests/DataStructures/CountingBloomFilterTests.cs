using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.DataStructures;

namespace Shared.Utilities.Tests.DataStructures
{
    [TestFixture]
    public class CountingBloomFilterTests
    {
        #region Constructor Tests

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ExpectedNumberOfItemsLessThanZero_Throws()
        {
            CountingBloomFilter bf = new CountingBloomFilter(-1, 0.5d);
        }

        [Test]
        public void Constructor_ExpectedNumberOfItemsGreaterThanZero_DoesNotThrow()
        {
            CountingBloomFilter bf = new CountingBloomFilter(1, 0.5d);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ErrorRateLessThanZero_Throws()
        {
            CountingBloomFilter bf = new CountingBloomFilter(1, -1.0d);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ErrorRateGreaterThanOne_Throws()
        {
            CountingBloomFilter bf = new CountingBloomFilter(1, 2.0d);
        }

        [Test]
        public void Constructor_ErrorRateIsValid_DoesNotThrow()
        {
            CountingBloomFilter bf = new CountingBloomFilter(1, 0.5d);
        }

        #endregion

        #region Add Tests

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullData_Throws()
        {
            CountingBloomFilter bf = new CountingBloomFilter(10, 0.01d);
            CommonBloomFilterTests.Add_NullData_Throws(bf);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_EmptyData_Throws()
        {
            CountingBloomFilter bf = new CountingBloomFilter(10, 0.01d);
            CommonBloomFilterTests.Add_EmptyData_Throws(bf);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullString_Throws()
        {
            CountingBloomFilter bf = new CountingBloomFilter(10, 0.01d);
            CommonBloomFilterTests.Add_NullString_Throws(bf);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_EmptyString_Throws()
        {
            CountingBloomFilter bf = new CountingBloomFilter(10, 0.01d);
            CommonBloomFilterTests.Add_EmptyString_Throws(bf);
        }

        [Test]
        public void Add_AddedItemIsPresent()
        {
            CountingBloomFilter bf = new CountingBloomFilter(10, 0.001);
            CommonBloomFilterTests.Add_AddedItemIsPresent(bf);
        }

        [Test]
        public void Add_NonAddedItemIsNotPresent()
        {
            CountingBloomFilter bf = new CountingBloomFilter(10, 0.001);
            CommonBloomFilterTests.Add_NonAddedItemIsNotPresent(bf);
        }

        #endregion

        #region Clear Tests

        [Test]
        public void Clear_AddedItemNoLongerPresent()
        {
            CountingBloomFilter bf = new CountingBloomFilter(10, 0.001);
            CommonBloomFilterTests.Clear_AddedItemNoLongerPresent(bf);
        }

        #endregion

        #region Remove Tests

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Remove_NullData_Throws()
        {
            CountingBloomFilter bf = new CountingBloomFilter(10, 0.01d);
            bf.Remove((byte[])null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Remove_EmptyData_Throws()
        {
            CountingBloomFilter bf = new CountingBloomFilter(10, 0.01d);
            bf.Remove(new byte[0]);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Remove_NullString_Throws()
        {
            CountingBloomFilter bf = new CountingBloomFilter(10, 0.01d);
            bf.Remove((string)null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Remove_EmptyString_Throws()
        {
            CountingBloomFilter bf = new CountingBloomFilter(10, 0.01d);
            bf.Remove(string.Empty);
        }

        [Test]
        public void Remove_RemovedItemIsNotPresent()
        {
            CountingBloomFilter bf = new CountingBloomFilter(10, 0.001);
            bf.Add("a");
            bf.Add("a");

            Assert.IsTrue(bf.IsPresent("a"));

            bf.Remove("a");

            Assert.IsFalse(bf.IsPresent("a"));
        }

        #endregion

        #region Other Tests

        /// <summary>
        /// Tests to make sure the that the requested error rate is achieved
        /// when inserting random strings.
        /// </summary>
        [Test]
        public void ErrorRateWithinExpectedBounds()
        {
            CountingBloomFilter bf = new CountingBloomFilter(10000, 0.01d);
            CommonBloomFilterTests.ErrorRateWithinExpectedBounds(bf);
        }

        #endregion
    }
}
