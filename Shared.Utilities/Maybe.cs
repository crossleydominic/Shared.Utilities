using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities
{
    /// <summary>
    /// Class that represents either a value or nothing
    /// </summary>
    public class Maybe<T>
        where T : class
    {
        #region Public constants

        public static readonly Maybe<T> Nothing = new Maybe<T>();

        #endregion

        #region Private members

        /// <summary>
        /// The value being wrapped
        /// </summary>
        private T _value;

        /// <summary>
        /// Whether or not this Maybe is wrapping a value
        /// </summary>
        private bool _hasValue;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a new Maybe without a value
        /// </summary>
        private Maybe()
        {
            _value = default(T);
            _hasValue = false;
        }

        /// <summary>
        /// Construct a new Maybe that wraps the specified value
        /// </summary>
        /// <param name="value">The value to wrap</param>
        public Maybe(T value)
        {
            _value = value;
            _hasValue = value != null;
        }

        #endregion

        #region Type conversion

        /// <summary>
        /// Automatically lift any type into a Maybe
        /// </summary>
        /// <param name="value">The value to wrap</param>
        public static implicit operator Maybe<T>(T value)
        {
            return new Maybe<T>(value);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Applies the supplied function to the value within this maybe and returns a new Maybe wrapping the new value.
        /// Protects against nulls and exceptions. Otherwise known as fmap.
        /// </summary>
        /// <typeparam name="TOut">The output type to wrap</typeparam>
        /// <param name="func">The function to apply the value inside this Maybe</param>
        /// <returns>A new maybe wrapping the output value</returns>
        public Maybe<TOut> Apply<TOut>(Func<T, TOut> func)
            where TOut : class
        {
            if (func == null || this.HasValue == false)
            {
                return Maybe<TOut>.Nothing;
            }

            try
            {
                TOut result = func(this.Value);
                if (result != null)
                {
                    return new Maybe<TOut>(result);
                }
                else
                {
                    return Maybe<TOut>.Nothing;
                }
            }
            catch
            {
                return Maybe<TOut>.Nothing;
            }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Get the wrapped value
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if this Maybe is not wrapping a value (HasValue is false)
        /// </exception>
        public T Value
        {
            get
            {
                if (!_hasValue)
                {
                    throw new InvalidOperationException("Value is not set");
                }

                return _value;
            }
        }

        /// <summary>
        /// Get's whether or not this Maybe is wrapping a value.
        /// </summary>
        public bool HasValue
        {
            get { return _hasValue; }
        }

        #endregion
    }
}
