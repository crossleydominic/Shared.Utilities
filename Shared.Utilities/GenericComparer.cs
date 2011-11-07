using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities
{
    /// <summary>
    /// Class that can use a delegate/lambda comparison function
    /// which can be combined with LINQ.
    /// </summary>
    public class GenericComparer<T> : IComparer<T>
    {
        #region Private members

        /// <summary>
        /// The delegate that will perform the comparison
        /// </summary>
        Func<T, T, int> _comparison;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if comparison is null.
        /// </exception>
        public GenericComparer(Func<T, T, int> comparison)
        {
            #region Input Validation

            Insist.IsNotNull(comparison, "comparison");

            #endregion

            _comparison = comparison;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Uses the supplied delegate to perform the comparison.
        /// </summary>
        public int Compare(T x, T y)
        {
            return _comparison(x, y);
        }

        #endregion
    }

    /// <summary>
    /// Static class that makes construction of a GenericComparer nicer
    /// </summary>
    public static class GenericComparer
    {
        #region Public static methods

        /// <summary>
        /// Creates a new GenericComparer
        /// </summary>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if comparison is null.
        /// </exception>
        public static GenericComparer<T> Create<T>(Func<T, T, int> comparison)
        {
            return new GenericComparer<T>(comparison);
        }

        #endregion
    }
}
