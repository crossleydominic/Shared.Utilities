using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Shared.Utilities.ExtensionMethods.Primitives
{
    /// <summary>
    /// A set of extensions for the Type class
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Returns the first custom attribute of the specified type, T.
        /// Parent types are also searched.
        /// </summary>
        /// <typeparam name="T">
        /// The custom attribute type to find
        /// </typeparam>
        /// <param name="member">
        /// The MemberInfo object to search
        /// </param>
        /// <returns>
        /// The first matching custom attribute or null if a match couldn't be found.
        /// </returns>
        public static T GetCustomAttribute<T>(this MemberInfo member)
        {
            return GetCustomAttribute<T>(member, true);
        }

        /// <summary>
        /// Returns the first custom attribute of the specified type, T
        /// </summary>
        /// <typeparam name="T">
        /// The custom attribute type to find
        /// </typeparam>
        /// <param name="member">
        /// The MemberInfo object to search
        /// </param>
        /// <param name="inherit">
        /// Whether or not to include parent types in the search.
        /// </param>
        /// <returns>
        /// The first matching custom attribute or null if a match couldn't be found.
        /// </returns>
        public static T GetCustomAttribute<T>(this MemberInfo member, bool inherit)
        {
            return GetCustomAttributes<T>(member, inherit).FirstOrDefault();
        }

        /// <summary>
        /// Returns the all custom attributes of the specified type, T.
        /// Parent types are also searched.
        /// </summary>
        /// <typeparam name="T">
        /// The custom attribute type to find
        /// </typeparam>
        /// <param name="member">
        /// The MemberInfo object to search
        /// </param>
        /// <param name="inherit">
        /// Whether or not to include parent types in the search.
        /// </param>
        /// <returns>
        /// A list containing all matching custom attributes.
        /// </returns>
        public static IList<T> GetCustomAttributes<T>(this MemberInfo member)
        {
            return GetCustomAttributes<T>(member, true);
        }

        /// <summary>
        /// Returns the all custom attributes of the specified type, T
        /// </summary>
        /// <typeparam name="T">
        /// The custom attribute type to find
        /// </typeparam>
        /// <param name="member">
        /// The MemberInfo object to search
        /// </param>
        /// <param name="inherit">
        /// Whether or not to include parent types in the search.
        /// </param>
        /// <returns>
        /// A list containing all matching custom attributes.
        /// </returns>
        public static IList<T> GetCustomAttributes<T>(this MemberInfo member, bool inherit)
        {
            #region Input validation

            Insist.IsNotNull(member, "member");

            #endregion

            object[] attributes = member.GetCustomAttributes(inherit);

            List<T> matching = new List<T>();

            foreach (object attr in attributes)
            {
                if (attr is T)
                {
                    matching.Add((T)attr);
                }
            }

            return matching;
        }
    }
}
