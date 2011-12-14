using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.DataStructures;
using System.Threading;

namespace Shared.Utilities.Tests.DataStructures
{
    [TestFixture]
    public class BloomFilterTests
    {
        #region Constructor Tests

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ExpectedNumberOfItemsLessThanZero_Throws()
        {
            BloomFilter bf = new BloomFilter(-1, 0.5d);
        }

        [Test]
        public void Constructor_ExpectedNumberOfItemsGreaterThanZero_DoesNotThrow()
        {
            BloomFilter bf = new BloomFilter(1, 0.5d);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ErrorRateLessThanZero_Throws()
        {
            BloomFilter bf = new BloomFilter(1, -1.0d);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ErrorRateGreaterThanOne_Throws()
        {
            BloomFilter bf = new BloomFilter(1, 2.0d);
        }

        [Test]
        public void Constructor_ErrorRateIsValid_DoesNotThrow()
        {
            BloomFilter bf = new BloomFilter(1, 0.5d);
        }

        #endregion

        #region Add Tests

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullData_Throws()
        {
            BloomFilter bf = new BloomFilter(10, 0.01d);
            CommonBloomFilterTests.Add_NullData_Throws(bf);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_EmptyData_Throws()
        {
            BloomFilter bf = new BloomFilter(10, 0.01d);
            CommonBloomFilterTests.Add_EmptyData_Throws(bf);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullString_Throws()
        {
            BloomFilter bf = new BloomFilter(10, 0.01d);
            CommonBloomFilterTests.Add_NullString_Throws(bf);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_EmptyString_Throws()
        {
            BloomFilter bf = new BloomFilter(10, 0.01d);
            CommonBloomFilterTests.Add_EmptyString_Throws(bf);
        }

        [Test]
        public void Add_AddedItemIsPresent()
        {
            BloomFilter bf = new BloomFilter(10, 0.001);
            CommonBloomFilterTests.Add_AddedItemIsPresent(bf);
        }

        [Test]
        public void Add_NonAddedItemIsNotPresent()
        {
            BloomFilter bf = new BloomFilter(10, 0.001);
            CommonBloomFilterTests.Add_NonAddedItemIsNotPresent(bf);
        }

        #endregion

        #region Clear Tests

        [Test]
        public void Clear_AddedItemNoLongerPresent()
        {
            BloomFilter bf = new BloomFilter(10, 0.001);
            CommonBloomFilterTests.Clear_AddedItemNoLongerPresent(bf);
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
            BloomFilter bf = new BloomFilter(10000, 0.01d);
            CommonBloomFilterTests.ErrorRateWithinExpectedBounds(bf);
        }

        #endregion
    }
}
