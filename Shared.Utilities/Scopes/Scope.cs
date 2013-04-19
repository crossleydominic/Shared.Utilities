using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.Scopes
{
    /// <summary>
    /// Base class providing minimal functionality for created scope 
    /// based helper classes. Scopes are designed to be used in Using blocks.
    /// </summary>
    public abstract class Scope<T>
    {
        #region Private members

        /// <summary>
        /// An instance of the type over which scoping functionality is
        /// being placed.
        /// </summary>
        private T _value;

        #endregion

        #region Constructors

        /// <summary>
        /// Create new scope instace around the underlying value
        /// </summary>
        protected Scope(T value)
        {
            #region Input validation

            Insist.IsNotNull(value, "value");

            #endregion

            _value = value;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets access to the underlying value object
        /// </summary>
        public T Value
        {
            get { return _value; }
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Create a disposer to provide onEnter and onExit functionality.
        /// </summary>
        /// <param name="onEnter"></param>
        /// <param name="onExit"></param>
        /// <returns></returns>
        protected Disposer<T> Create(Action<T> onEnter, Action<T> onExit)
        {
            #region Input validation

            Insist.IsNotNull(onEnter, "onEnter");
            Insist.IsNotNull(onExit, "onExit");

            #endregion

            return Disposer<T>.Create(_value, onEnter, onExit);
        }

        #endregion
    }
}
