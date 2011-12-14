using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Shared.Utilities.DataStructures
{
    /// <summary>
    /// Base class for bloomFilter and CountingBloomFilter.
    /// Not designed to be extended outside of this assembly.
    /// Contains common code for adding items to the bloom filter.
    /// </summary>
    /// <typeparam name="T">The storage type</typeparam>
    public abstract class BaseBloomFilter<T>
    {
        #region Private members

        /// <summary>
        /// The expected numbers of elements that the bloom filter
        /// will need to track
        /// </summary>
        private int _expectedNumberOfElements;

        /// <summary>
        /// The desired rate at which we generate false positives.
        /// Expressed as a value between 0 and 1.
        /// </summary>
        private double _errorRate;

        /// <summary>
        /// The number of hashes each item added to the bloom filter must undergo
        /// </summary>
        private int _numberOfHashes;

        /// <summary>
        /// The algorithm used to hash the data before adding to the filter
        /// </summary>
        private MurmurHash2 _hashAlgorithm;

        #endregion

        #region Protected members

        /// <summary>
        /// The number of buckets that this bloom filter needs so 
        /// that it maintains it's error rate for the expected number of
        /// items
        /// </summary>
        protected int _numberOfBuckets;

        /// <summary>
        /// The storage buckets 
        /// </summary>
        protected T _bucketStorage;

        /// <summary>
        /// Lock for thread safety.
        /// </summary>
        protected ReaderWriterLockSlim _lock;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevent direct usage outside this assembly.
        /// (no way to hide it though).
        /// </summary>
        private BaseBloomFilter() { }

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
        public BaseBloomFilter(int expectedNumberOfElements, double errorRate)
        {
            #region Input validation

            Insist.IsAtLeast(expectedNumberOfElements, 0, "expectedNumberOfElements");
            Insist.IsAtLeast(errorRate, 0, "errorRate");
            Insist.IsAtMost(errorRate, 1.0d, "errorRate");

            #endregion

            _expectedNumberOfElements = expectedNumberOfElements;
            _errorRate = errorRate;
            _hashAlgorithm = new MurmurHash2();

            _numberOfBuckets = (int)Math.Ceiling(1.1d * Math.Abs((((double)_expectedNumberOfElements) * Math.Log(errorRate)) / (Math.Pow(Math.Log(2), 2))));
            _numberOfHashes = (int)(Math.Ceiling(0.7d * Math.Abs((double)(_numberOfBuckets / _expectedNumberOfElements))));

            _bucketStorage = GetBucketStorage(_numberOfBuckets);
            _lock = new ReaderWriterLockSlim();
        }

        #endregion

        #region Abstract methods

        /// <summary>
        /// Inherited classes provides the storage for the buckets
        /// </summary>
        protected abstract T GetBucketStorage(int size);

        /// <summary>
        /// Allows inherited classes to set a bucket to "in use"
        /// </summary>
        /// <param name="index"></param>
        protected abstract void SetBucketAsInUse(int index);

        /// <summary>
        /// Allows inherited classes to see if a particular bucket is in use.
        /// </summary>
        protected abstract bool IsBucketInUse(int index);

        /// <summary>
        /// Allows inherited classes to reset the storage buckets.
        /// </summary>
        protected abstract void ClearInternal();

        #endregion

        #region Public methods

        /// <summary>
        /// Adds the specified string to the bloom filter.
        /// </summary>
        /// <param name="data">The data to track in this bloom filter.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if data is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrownf if data is empty.
        /// </exception>
        public void Add(string data)
        {
            Add(System.Text.Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        /// Adds the specified data to the bloom filter.
        /// </summary>
        /// <param name="data">The data to track in this bloom filter.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if data is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrownf if data is empty.
        /// </exception>
        public void Add(byte[] data)
        {
            #region Input validation

            Insist.IsNotNull(data, "data");
            Insist.IsNotEmpty(data, "data");

            #endregion

            int[] computedIndeces = GetIndeces(data);

            _lock.EnterWriteLock();
            try
            {
                bool itemAlreadyPresent = true;
                foreach (int index in computedIndeces)
                {
                    if (IsBucketInUse(index) == false)
                    {
                        itemAlreadyPresent = false;
                        break;
                    }
                }

                if (itemAlreadyPresent == false)
                {
                    foreach (int index in computedIndeces)
                    {
                        SetBucketAsInUse(index);
                    }
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Clears all storage buckets. 
        /// </summary>
        public void Clear()
        {
            _lock.EnterWriteLock();
            try
            {
                ClearInternal();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Checks to see if the supplied strign has been added to this filter.
        /// </summary>
        /// <param name="data">
        /// The string to check for presence in the filter.
        /// </param>
        /// <returns>
        /// True if the item *might* have been added to the filter. False if it 
        /// definitely has not.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if data is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrownf if data is empty.
        /// </exception>
        public bool IsPresent(string data)
        {
            return IsPresent(System.Text.Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        /// Checks to see if the supplied data has been added to this filter.
        /// </summary>
        /// <param name="data">
        /// The data to check for presence in the filter.
        /// </param>
        /// <returns>
        /// True if the item *might* have been added to the filter. False if it 
        /// definitely has not.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if data is null.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrownf if data is empty.
        /// </exception>
        public bool IsPresent(byte[] data)
        {
            #region Input validation

            Insist.IsNotNull(data, "data");
            Insist.IsNotEmpty(data, "data");

            #endregion

            int[] computedIndeces = GetIndeces(data);

            _lock.EnterReadLock();
            try
            {
                foreach (int index in computedIndeces)
                {
                    if (IsBucketInUse(index) == false)
                    {
                        return false;
                    }
                }
            }
            finally
            {
                _lock.ExitReadLock();
            }

            return true;
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Computes a set of hashes for the supplied set of data
        /// </summary>
        protected int[] GetIndeces(byte[] data)
        {
            int[] computedIndeces = new int[_numberOfHashes];

            for (uint i = 0; i < _numberOfHashes; i++)
            {
                computedIndeces[i] = Math.Abs(((int)_hashAlgorithm.Hash(data, i)) % _numberOfBuckets);
            }

            return computedIndeces;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the expected number of elements
        /// </summary>
        public int ExpectedNumberOfElements
        {
            get { return _expectedNumberOfElements; }
        }

        /// <summary>
        /// Gets the error rate.
        /// </summary>
        public double ErrorRate
        {
            get { return _errorRate; }
        }

        #endregion
    }
}
