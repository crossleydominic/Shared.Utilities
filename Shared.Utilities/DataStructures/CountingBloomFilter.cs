using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.DataStructures
{
    /// <summary>
    /// Implementation of the Counting Bloom Filter data structure.
    /// Allows removing as well as adding elements
    /// </summary>
    public sealed class CountingBloomFilter : BaseBloomFilter<byte[]>
    {
        #region Constructors

        /// <summary>
        /// Construct a new counting bloom filter that tracks the specified number of
        /// elements with the desired error rate.
        /// </summary>
        /// <param name="expectedNumberOfElements">
        /// The number of items that this bloom filter is expected to track
        /// </param>
        /// <param name="errorRate">
        /// The desired error rate for generating false positives.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if expectedNumberOfElements is less than 0, errorRate is less than 0 or
        /// errorRate is greater than 1.
        /// </exception>
        public CountingBloomFilter(int expectedNumberOfElements, double errorRate) :
            base(expectedNumberOfElements, errorRate) { }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the storage for the filter
        /// </summary>
        protected override byte[] GetBucketStorage(int size)
        {
            return new byte[size];
        }

        /// <summary>
        /// Sets a bucket as in-use
        /// </summary>
        protected override void SetBucketAsInUse(int index)
        {
            //Don't overflow
            if (_bucketStorage[index] == byte.MaxValue)
            {
                throw new ArithmeticException("Cannot add item to filter due to arithmetic overflow.");
            }

            _bucketStorage[index]++;
        }

        /// <summary>
        /// Checks to see if a bucket is in-use
        /// </summary>
        protected override bool IsBucketInUse(int index)
        {
            return _bucketStorage[index] > 0;
        }

        /// <summary>
        /// Clears all storage buckets
        /// </summary>
        protected override void ClearInternal()
        {
            for (int i = 0; i < _bucketStorage.Length; i++)
            {
                _bucketStorage[i] = 0;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Removes the specified data from the counting bloom filter.
        /// </summary>
        /// <param name="data">The data to remove from this counting bloom filter.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if data is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrownf if data is empty.
        /// </exception>
        public void Remove(byte[] data)
        {
            #region Input validation

            Insist.IsNotNull(data, "data");
            Insist.IsNotEmpty(data, "data");

            #endregion

            int[] computedIndeces = GetIndeces(data);

            _lock.EnterWriteLock();
            try
            {
                foreach (int index in computedIndeces)
                {
                    if (_bucketStorage[index] > 0)
                    {
                        _bucketStorage[index]--;
                    }
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Removes the specified string from the counting bloom filter.
        /// </summary>
        /// <param name="data">The string to remove from this counting bloom filter.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if string is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrownf if string is empty.
        /// </exception>
        public void Remove(string data)
        {
            Remove(System.Text.Encoding.UTF8.GetBytes(data));
        }

        #endregion
    }
}
