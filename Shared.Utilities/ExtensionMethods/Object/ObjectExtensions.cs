using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.ExtensionMethods.Object
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Allows properties on an object to be safely dereferenced even if the object is null. Not advised for production code, 
        /// more for use in prototyping code.  If the object is null then the null is propogated and a NullRefEx is not thrown.
        /// </summary>
        /// <param name="obj">The object to dereference</param>
        /// <param name="deRefFunc">The value obtaining function</param>
        /// <returns>
        /// The value obtained from the deRefRunction if the object was not null. Otherwise null.
        /// </returns>
        public static TOut SafeDeRef<TIn, TOut>(this TIn obj, Func<TIn, TOut> deRefFunc)
            where TIn : class
        {
            #region Input validation

            Insist.IsNotNull(deRefFunc, "deRefFunc");

            #endregion

            if (obj == null)
            {
                return default(TOut);
            }

            return deRefFunc(obj);
        }
    }
}
