using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.ExtensionMethods.Primitives
{
    /// <summary>
    /// Extensions methods for Nullable types
    /// </summary>
    public static class NullableExtensions
    {
        /// <summary>
        /// Gets a nullables value as a string or returns the emptry string if there is no value
        /// </summary>
        /// <param name="value">
        /// The nullable to obtain the value from
        /// </param>
        /// <returns>
        /// The string representation of the value or an emptry string if there is no value
        /// </returns>
        public static string GetValueAsStringOrEmpty<T>(this Nullable<T> value) where T : struct
        {
            return GetValueAsStringOrEmpty(value, v => v.ToString());
        }

        /// <summary>
        /// Gets a nullables value as a string using the conversion function or returns the emptry string if there is no value
        /// </summary>
        /// <param name="value">
        /// The nullable to obtain the value from
        /// </param>
        /// <param name="conversionFunc">
        /// The function that should be used to convert the value into a string
        /// </param>
        /// <returns>
        /// The string representation of the value or an emptry string if there is no value
        /// </returns>
        public static string GetValueAsStringOrEmpty<T>(this Nullable<T> value, Func<T, string> conversionFunc) where T : struct
        {
            return SelectValueOrDefault(value, conversionFunc, string.Empty);
        }

        /// <summary>
        /// Gets a nullables value, projects it and returns it. If there is no value then a defualt is returned
        /// </summary>
        /// <typeparam name="T">
        /// The input type of the nullable
        /// </typeparam>
        /// <typeparam name="TOut">
        /// The return type
        /// </typeparam>
        /// <param name="value">
        /// The nullable value
        /// </param>
        /// <param name="projection">
        /// A function to convert the nullable value into another type
        /// </param>
        /// <param name="default">
        /// The default to return if there is no value
        /// </param>
        /// <returns>
        /// The project value if the value exist otherwise the default.
        /// </returns>
        public static TOut SelectValueOrDefault<T, TOut>(this Nullable<T> value, Func<T, TOut> projection, TOut @default) where T : struct
        {
            #region Input validation

            Insist.IsNotNull(projection, "projection");

            #endregion

            if (value != null && value.HasValue)
            {
                return projection(value.Value);
            }

            return @default;
        }
    }
}
