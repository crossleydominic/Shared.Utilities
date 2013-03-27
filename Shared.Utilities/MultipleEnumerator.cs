using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities
{
    /// <summary>
    /// Class that allows a single foreach to enumerate through a set of lists, enumerating each in turn
    /// </summary>
    public class MultipleEnumerator<T> : IEnumerable<T>
    {
        #region Private members

        /// <summary>
        /// The set of enumerables to enumerate through
        /// </summary>
        private IEnumerable<T>[] _enumerables;

        #endregion

        #region Constructors

        /// <summary>
        /// Create new instance that will enumerate through the supplied set of enumerables
        /// </summary>
        public MultipleEnumerator(params IEnumerable<T>[] enumerables)
        {
            #region Input validation

            Insist.IsNotNull(enumerables, "enumerables");
            Insist.IsNotEmpty(enumerables, "enumerables");

            #endregion

            _enumerables = enumerables;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get an enumerator to loop through each of the items in all of the enumerables in turn
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (IEnumerable<T> enumerable in _enumerables)
            {
                foreach (T value in enumerable)
                {
                    yield return value;
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
