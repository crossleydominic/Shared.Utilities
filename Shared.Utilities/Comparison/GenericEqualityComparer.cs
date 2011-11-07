using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.Comparison
{
    /// <summary>
    /// Class that can use a delegate/lambda comparison function
    /// which can be combined with LINQ.
    /// </summary>
    public class GenericEqualityComparer<T> : IEqualityComparer<T>
    {
        #region Private members

        /// <summary>
        /// The delegate that will perform the comparison
        /// </summary>
        Func<T,T, bool> _comparison;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if comparison is null.
        /// </exception>
        public GenericEqualityComparer(Func<T, T, bool> comparison)
        {
            #region Input Validation

            Insist.IsNotNull(comparison, "comparison");

            #endregion

            _comparison = comparison;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Tests the two objects for equality using the supplied comparison function
        /// </summary>
        public bool Equals(T x, T y)
        {
            return _comparison(x, y);
        }

        /// <summary>
        /// Gets the hashcode from the supplied object. Returns 0 if the object is null.
        /// </summary>
        public int GetHashCode(T obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return obj.GetHashCode();
            }
        }

        #endregion
    }

    /// <summary>
    /// Static class that makes construction of a GenericEqualityComparer nicer
    /// </summary>
    public static class GenericEqualityComparer
    {
        #region Public static methods

        /// <summary>
        /// Creates a new GenericEqualityComparer
        /// </summary>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if comparison is null.
        /// </exception>
        public static GenericEqualityComparer<T> Create<T>(Func<T, T, bool> comparison)
        {
            return new GenericEqualityComparer<T>(comparison);
        }

        #endregion
    }
}
