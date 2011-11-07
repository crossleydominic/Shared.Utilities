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
    public class ProjectedEqualityComparer<TSource, TProjected> : IEqualityComparer<TSource>
    {
        #region Private members

        /// <summary>
        /// The delegate that will perform the comparison
        /// </summary>
        Func<TProjected, TProjected, bool> _comparison;

        /// <summary>
        /// Function to transform the source type into a target type
        /// </summary>
        Func<TSource, TProjected> _projection;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if comparison or projection is null.
        /// </exception>
        public ProjectedEqualityComparer(
            Func<TProjected, TProjected, bool> comparison,
            Func<TSource, TProjected> projection)
        {
            #region Input Validation

            Insist.IsNotNull(comparison, "comparison");
            Insist.IsNotNull(projection, "projection");

            #endregion

            _comparison = comparison;
            _projection = projection;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Tests the two objects for equality using the supplied comparison function
        /// after applying the projection
        /// </summary>
        public bool Equals(TSource x, TSource y)
        {
            return _comparison(_projection(x), _projection(y));
        }

        /// <summary>
        /// Gets the hashcode from the supplied object. Returns 0 if the object is null.
        /// </summary>
        public int GetHashCode(TSource obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return _projection(obj).GetHashCode();
            }
        }

        #endregion
    }

    /// <summary>
    /// Static class that makes construction of a ProjectedEqualityComparer nicer
    /// </summary>
    public static class ProjectedEqualityComparer
    {
        /// <summary>
        /// Creates a new ProjectedEqualityComparer
        /// </summary>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if comparison or projection is null.
        /// </exception>
        public static ProjectedEqualityComparer<TSource, TProjected> Create<TSource, TProjected>(
            Func<TProjected, TProjected, bool> comparison,
            Func<TSource, TProjected> projection)
        {
            return new ProjectedEqualityComparer<TSource, TProjected>(comparison, projection);
        }
    }
}
