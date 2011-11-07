using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities
{
    /// <summary>
    /// Class that can use a delegate/lambda comparison function
    /// with a projection function to transform the source
    /// which can be combined with LINQ.
    /// </summary>
    public class ProjectedComparer<TSource, TProjected> : IComparer<TSource>
    {
        #region Private members

        /// <summary>
        /// The delegate that will perform the comparison
        /// </summary>
        Func<TProjected, TProjected, int> _comparison;

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
        public ProjectedComparer(
            Func<TProjected, TProjected, int> comparison,
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
        /// Compares elements using the comparison function aftera applying the 
        /// projection
        /// </summary>
        public int Compare(TSource x, TSource y)
        {
            return _comparison(_projection(x), _projection(y));
        }

        #endregion
    }

    /// <summary>
    /// Static class that makes construction of a GenericComparer nicer
    /// </summary>
    public static class ProjectedComparer
    {
        /// <summary>
        /// Creates a new ProjectedComparer
        /// </summary>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if comparison is null.
        /// </exception>
        public static ProjectedComparer<TSource, TProjected> Create<TSource, TProjected>(
            Func<TProjected, TProjected, int> comparison,
            Func<TSource, TProjected> projection)
        {
            return new ProjectedComparer<TSource, TProjected>(comparison, projection);
        }
    }
}
