using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Shared.Utilities.DataStructures
{
    /// <summary>
    /// Implementation of the Bloom Filter data structure
    /// </summary>
    public sealed class BloomFilter : BaseBloomFilter<BitArray>
    {
        #region Constructors

        /// <summary>
        /// Construct a new bloom filter that tracks the specified number of
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
        public BloomFilter(int expectedNumberOfElements, double errorRate) :
            base(expectedNumberOfElements, errorRate) { }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the storage for the bloom filter
        /// </summary>
        protected override BitArray GetBucketStorage(int size)
        {
            return new BitArray(size);
        }

        /// <summary>
        /// Sets a bucket as in-use
        /// </summary>
        protected override void SetBucketAsInUse(int index)
        {
            _bucketStorage[index] = true;
        }

        /// <summary>
        /// Checks to see if a bucket is in-use.
        /// </summary>
        protected override bool IsBucketInUse(int index)
        {
            return _bucketStorage[index] == true;
        }

        /// <summary>
        /// Clears all buckets
        /// </summary>
        protected override void ClearInternal()
        {
            for (int i = 0; i < _bucketStorage.Length; i++)
            {
                _bucketStorage[i] = false;
            }
        }

        #endregion
    }
}
