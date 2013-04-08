using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities
{
    /// <summary>
    /// Helper class to provide type-inferred utility functions for working with Eithers
    /// </summary>
    public class Either
    {
        /// <summary>
        /// Partition a set of Eithers into a Tuple that collates all the Left and Right values togehter
        /// </summary>
        /// <returns>
        /// A tuple that contains collates set of Left and Right eithers
        /// </returns>
        public static Tuple<IEnumerable<TLeft>, IEnumerable<TRight>> Partition<TLeft, TRight>(IEnumerable<Either<TLeft, TRight>> eithers)
        {
            #region Input validation

            Insist.IsNotNull(eithers, "eithers");

            #endregion

            return new Tuple<IEnumerable<TLeft>, IEnumerable<TRight>>(
                Lefts(eithers),
                Rights(eithers));
        }

        /// <summary>
        /// Collates all Left values from a set of eithers
        /// </summary>
        /// <param name="eithers">
        /// The set of eithers from which to obtain the Left values
        /// </param>
        /// <returns>
        /// All the left values from the supplied set of eithers
        /// </returns>
        public static IEnumerable<TLeft> Lefts<TLeft, TRight>(IEnumerable<Either<TLeft, TRight>> eithers)
        {
            #region Input validation

            Insist.IsNotNull(eithers, "eithers");

            #endregion

            return eithers.Where(e => !e.HasRight).Select(e => e.LeftValue);
        }

        /// <summary>
        /// Collates all Right values from a set of eithers
        /// </summary>
        /// <param name="eithers">
        /// The set of eithers from which to obtain the left values
        /// </param>
        /// <returns>
        /// All the left values from the supplied set of eithers
        /// </returns>
        public static IEnumerable<TRight> Rights<TLeft, TRight>(IEnumerable<Either<TLeft, TRight>> eithers)
        {
            #region Input validation

            Insist.IsNotNull(eithers, "eithers");

            #endregion

            return eithers.Where(e => e.HasRight).Select(e => e.RightValue);
        }
    }

    /// <summary>
    /// Represents either a value of one type or another. Null values are dissallowed
    /// </summary>
    /// <typeparam name="TLeft">The type for the Left value</typeparam>
    /// <typeparam name="TRight">The type for the Right value</typeparam>
    public class Either<TLeft, TRight> : Either
    {
        #region Private members

        /// <summary>
        /// The Left value
        /// </summary>
        private TLeft _leftVal;

        /// <summary>
        /// The Right value
        /// </summary>
        private TRight _rightVal;

        /// <summary>
        /// Whether or not this Either was constructed with a Left or Right value
        /// </summary>
        private bool _hasRight;

        #endregion

        #region Constructors

        /// <summary>
        /// Disallow direct construction. Use static helper methods instead.
        /// </summary>
        protected Either() { }

        #endregion

        #region Static methods

        /// <summary>
        /// Construct an Either that wraps a Left value
        /// </summary>
        public static Either<TLeft, TRight> CreateLeft(TLeft leftValue)
        {
            #region Input validation

            Insist.IsNotNull(leftValue, "leftValue");

            #endregion

            Either<TLeft, TRight> either = new Either<TLeft, TRight>();
            either._leftVal = leftValue;
            either._hasRight = false;

            return either;
        }

        /// <summary>
        /// Construct an Either that wraps a Right value
        /// </summary>
        public static Either<TLeft, TRight> CreateRight(TRight rightValue)
        {
            #region Input validation

            Insist.IsNotNull(rightValue, "rightValue");

            #endregion

            Either<TLeft, TRight> either = new Either<TLeft, TRight>();
            either._rightVal = rightValue;
            either._hasRight = true;

            return either;
        }

        #endregion

        #region Public methods

        public void Apply(Action<TLeft> leftAction, Action<TRight> rightAction)
        {
            #region Input Validation

            Insist.IsNotNull(leftAction, "leftAction");
            Insist.IsNotNull(rightAction, "rightAction");

            #endregion

            if (_hasRight)
            {
                rightAction(_rightVal);
            }
            else
            {
                leftAction(_leftVal);
            }
        }

        public TOut Apply<TOut>(Func<TLeft, TOut> leftFunc, Func<TRight, TOut> rightFunc)
        {
            #region Input Validation

            Insist.IsNotNull(leftFunc, "leftFunc");
            Insist.IsNotNull(rightFunc, "rightFunc");

            #endregion

            if (_hasRight)
            {
                return rightFunc(_rightVal);
            }
            else
            {
                return leftFunc(_leftVal);
            }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Has this Either got a Right value, if not then it will have a Left value 
        /// </summary>
        public bool HasRight
        {
            get { return _hasRight; }
        }

        /// <summary>
        /// Gets the Left value, if it exists. If a Left value does not exist then an
        /// exception is thrown
        /// </summary>
        public TLeft LeftValue
        {
            get
            {
                if (_hasRight)
                {
                    throw new InvalidOperationException("Cannot obtain left value for an Either constructed via Right");
                }

                return _leftVal;
            }
        }

        /// <summary>
        /// Gets the Right value, if it exists. If a Right value does not exist then an
        /// exception is thrown
        /// </summary>
        public TRight RightValue
        {
            get
            {
                if (!_hasRight)
                {
                    throw new InvalidOperationException("Cannot obtain right value for an Either constructed via Left");
                }

                return _rightVal;
            }
        }

        #endregion
    }
}
