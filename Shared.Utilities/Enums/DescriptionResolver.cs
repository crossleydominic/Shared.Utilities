using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Utilities.Threading;
using System.Reflection;

namespace Shared.Utilities.Enums
{
    /// <summary>
    /// A class that will return a description for a DescriptionAttribute for a 
    /// specified enum value.
    /// </summary>
    public static class DescriptionResolver
    {
        #region Private static members

        /// <summary>
        /// A cache of Enum type along with their values and descriptions 
        /// </summary>
        private static ThreadShared<Dictionary<string, Dictionary<string,string>>> _cache;

        /// <summary>
        /// The padlock used to protect the cache.
        /// </summary>
        private static Padlock _cachePadlock;

        #endregion

        #region Type initializer

        /// <summary>
        /// Set up the cache.
        /// </summary>
        static DescriptionResolver()
        {
            _cachePadlock = new Padlock();
            _cache = new ThreadShared<Dictionary<string, Dictionary<string, string>>>(
                new Dictionary<string, Dictionary<string, string>>(),
                _cachePadlock);
        }

        #endregion

        #region Public static methods

        /// <summary>
        /// Tries to get the description for the specified enum value.
        /// Will not throw an exception but will return value.ToString() if
        /// the value does not exist as part of the enum.
        /// </summary>
        /// <typeparam name="T">
        /// The enum type.
        /// </typeparam>
        /// <param name="value">
        /// The enum value
        /// </param>
        /// <returns>
        /// The description string if a description was specified for this enum member.
        /// If no description was specified then value.ToString() is returned.
        /// If T is not an enum then an emptry string is returned.
        /// </returns>
        public static string TryGetDescription(Enum value)
        {
            return GetDescription(value, false); 
        }

        /// <summary>
        /// Gets the description for the specified enum value.
        /// </summary>
        /// <typeparam name="T">
        /// The enum type.
        /// </typeparam>
        /// <param name="value">
        /// The enum value
        /// </param>
        /// <returns>
        /// The description string if a description was specified for this enum member.
        /// If no description was specified then value.ToString() is returned.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// Thrown if value is not defined for T
        /// </exception>
        public static string GetDescription(Enum value)
        {
            return GetDescription(value, true); 
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Gets the description from the enum. Optionally throw an exception if the
        /// value isn't defined for T
        /// </summary>
        private static string GetDescription(Enum value, bool throwOnError)
        {
            #region Input validaton

            if (value == null)
            {
                if (throwOnError)
                {
                    throw new ArgumentNullException("value");
                }
                else
                {
                    return string.Empty;
                }
            }

            if (!Enum.IsDefined(value.GetType(), value))
            {
                if (throwOnError)
                {
                    throw new ArgumentException(string.Format("value '{0}' is not defined in enum '{1}'", value.GetType(), value));
                }
                else
                {
                    return value.ToString();
                }
            }

            if (!value.GetType().IsEnum)
            {
                if (throwOnError)
                {
                    throw new ArgumentException(string.Format("Type '{0}' is not an enum", value.GetType()));
                }
                else
                {
                    return value.ToString();
                }
            }

            #endregion

            Dictionary<string,string> enumCache = GetEnumCache(value.GetType());

            string valueAsString = value.ToString();

            if (enumCache.ContainsKey(valueAsString))
            {
                return enumCache[valueAsString];
            }
            else
            {
                return valueAsString;
            }

        }

        /// <summary>
        /// Gets the Cache for the supplied enum type.
        /// </summary>
        private static Dictionary<string, string> GetEnumCache(Type type)
        {
            using (_cachePadlock.Lock())
            {
                Dictionary<string, string> enumCache;

                if (_cache._.ContainsKey(type.FullName))
                {
                    enumCache = _cache.Value[type.FullName];
                }
                else
                {
                    enumCache = InspectEnum(type);
                    _cache.Value.Add(type.FullName, enumCache);
                }

                return enumCache;
            }
        }

        /// <summary>
        /// Inspects the enum type and returns a cache of value to descriptions.
        /// </summary>
        private static Dictionary<string, string> InspectEnum(Type type)
        {
            Dictionary<string, string> valueAndDescriptions = new Dictionary<string, string>();

            FieldInfo[] memInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            foreach (FieldInfo info in memInfos)
            {
                object[] attrs = info.GetCustomAttributes(typeof(DescriptionAttribute), false);

                string enumVal = info.GetValue(null).ToString();

                if (attrs == null ||
                    attrs.Length != 1)
                    //Enum value has no Description attribute on it.
                {
                    valueAndDescriptions.Add(enumVal, enumVal);
                }
                else
                    //Found description attribute, add it to cache.
                {
                    valueAndDescriptions.Add(enumVal, ((DescriptionAttribute)attrs[0]).Description);
                }
            }

            return valueAndDescriptions;
        }

        #endregion
    }
}
