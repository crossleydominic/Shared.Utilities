using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Utilities.RegularExpressions;
using log4net;
using Shared.Utilities.ExtensionMethods.Logging;

namespace Shared.Utilities.ExtensionMethods.Collections
{
	public static class IDictionaryExtensions
	{
		#region Logging

		/// <summary>
		/// Used to log information about the class
		/// </summary>
		private static ILog _logger = LogManager.GetLogger(typeof(IDictionaryExtensions));

		#endregion

		/// <summary>
		///  Sorts the collection using the comparison given.
		/// </summary>
		/// <typeparam name="TKey">The dictionary key type.</typeparam>
		/// <typeparam name="TValue">The dictionary value type.</typeparam>
		/// <param name="collection">The collection be sorted.</param>
		/// <param name="comparison">The comparison to be used when sorting.</param>
		[Obsolete("Consider using a SortedList<Key, Value> instead. A Dictionary makes no gaurantees about it's internal state regarding the order of it's items (it's not a list). The fact that this method works is due to an implementation detail of Microsofts Dictionary and cannot be relied upon", false)]
		public static void Sort<TKey, TValue>(this IDictionary<TKey, TValue> collection, Comparison<KeyValuePair<TKey, TValue>> comparison)
		{
			_logger.DebugMethodCalled(collection, comparison);

			#region Input Validation

			Insist.IsNotNull(collection, "collection");

			#endregion

			List<KeyValuePair<TKey, TValue>> sortedItems = new List<KeyValuePair<TKey, TValue>>();

			foreach (KeyValuePair<TKey, TValue> pair in collection)
			{
				sortedItems.Add(pair);
			}

			sortedItems.Sort(comparison);

			collection.Clear();

			foreach (KeyValuePair<TKey, TValue> pair in sortedItems)
			{
				collection.Add(pair.Key, pair.Value);
			}

		}

		/// <summary>
		/// Finds all entries in the dictionary with the given value.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="collection">The collection to be filtered.</param>
		/// <param name="value">The value to find.</param>
		/// <returns>A dictionary of the matching entries/</returns>
		/// <remarks>
		///  As this method does not restirct the types of the key or value care must be taken 
		///  when using collections which may contain null values.
		/// </remarks>
		public static Dictionary<TKey, TValue> FindByValue<TKey, TValue>(this IDictionary<TKey, TValue> collection, TValue value)
		{
			_logger.DebugMethodCalled(collection, value);

			#region Input Validation

			Insist.IsNotNull(collection, "collection");

			#endregion

			Dictionary<TKey, TValue> filtered = new Dictionary<TKey, TValue>();

			foreach (KeyValuePair<TKey, TValue> pair in collection)
			{
				if (pair.Value.Equals(value))
				{
					filtered.Add(pair.Key, pair.Value);
				}
			}

			return filtered;
		}

		/// <summary>
		///  Returns a dictionary which is a subset of the origianl dictionary containing
		///  items which match the given predicate.
		/// </summary>
		/// <typeparam name="TKey">The type of the dictionary key.</typeparam>
		/// <typeparam name="TValue">The type of the dictionary value.</typeparam>
		/// <param name="collection">The collection to search.</param>
		/// <param name="predicate">The predicate to use when selecting values.</param>
		/// <returns>A dictionary of all items from the original dictionary which match the given predicate.</returns>
		public static Dictionary<TKey, TValue> FindAll<TKey, TValue>(this IDictionary<TKey, TValue> collection, Predicate<KeyValuePair<TKey, TValue>> predicate)
		{
			_logger.DebugMethodCalled(collection, predicate);

			#region Input Validation

			Insist.IsNotNull(collection, "collection");
			Insist.IsNotNull(predicate, "predicate");

			#endregion

			Dictionary<TKey, TValue> matches = new Dictionary<TKey, TValue>();

			foreach (KeyValuePair<TKey, TValue> pair in collection)
			{
				if (predicate(pair))
				{
					matches.Add(pair.Key, pair.Value);
				}
			}

			return matches;
		}

		/// <summary>
		///  Returns the first item in the given collection which matches the specified predicate.
		/// </summary>
		/// <typeparam name="TKey">The type of the dictionary key.</typeparam>
		/// <typeparam name="TValue">The type of the dictionary value.</typeparam>
		/// <param name="collection">The collection to search.</param>
		/// <param name="predicate">The predicate to use when selecting values.</param>
		/// <returns>The first item in the collection which matches the given predicate.</returns>
		public static KeyValuePair<TKey, TValue>? Find<TKey, TValue>(this IDictionary<TKey, TValue> collection, Predicate<KeyValuePair<TKey, TValue>> predicate)
		{
			_logger.DebugMethodCalled(collection, predicate);

			#region Input Validation

			Insist.IsNotNull(collection, "collection");
			Insist.IsNotNull(predicate, "predicate");

			#endregion

			KeyValuePair<TKey, TValue>? result = null;

			foreach (KeyValuePair<TKey, TValue> pair in collection)
			{
				if (predicate(pair))
				{
					result = new KeyValuePair<TKey, TValue>(pair.Key, pair.Value);
					break;
				}
			}

			return result;
		}

		/// <summary>
		/// Executes the given action against every key/value pair in the collection.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="collection">The collection to be iterated across.</param>
		/// <param name="action">The action to be performed.</param>
		public static void ForEach<TKey, TValue>(this IDictionary<TKey, TValue> collection, Action<KeyValuePair<TKey, TValue>> action)
		{
			_logger.DebugMethodCalled(collection, action);

			foreach (KeyValuePair<TKey, TValue> pair in collection)
			{
				action(pair);
			}
		}

		/// <summary>
		///  Returns a flag indicating whether the given item in a collection is of numeric type.
		/// </summary>
		/// <param name="collection">The collection to look in.</param>
		/// <param name="key">The key to look at.</param>
		/// <returns>True if the value is numeric; false otherwise.</returns>
		public static bool IsNumeric<TKey>(this IDictionary<TKey, String> collection, TKey key)
		{
			_logger.DebugMethodCalled(collection, key);

			#region Input Validation

			Insist.IsNotNull(collection, "collection");

			#endregion

			return RegexHelper.IsNumeric(collection[key]);
		}

		/// <summary>
		///  Returns a flag indicating whether the given item in a collection is of integral type.
		/// </summary>
		/// <param name="collection">The collection to look in.</param>
		/// <param name="key">The key to look at.</param>
		/// <returns>True if the value is integral; false otherwise.</returns>
		public static bool IsInteger<TKey>(this IDictionary<TKey, String> collection, TKey key)
		{
			_logger.DebugMethodCalled(collection, key);

			#region Input Validation

			Insist.IsNotNull(collection, "collection");

			#endregion

			return RegexHelper.IsInteger(collection[key]);
		}

		/// <summary>
		/// Attempts to retrieve an item using the specified key. If the item doesn't
		/// exist then a new entry will be created in the dictionary for the specified
		/// value
		/// </summary>
		/// <param name="key">
		/// The key to use to locate the value
		/// </param>
		/// <param name="value">
		/// The value to insert into the collection if the specified key doesn't exist
		/// </param>
		/// <returns>
		/// Either the value associated with the key. If the key didnt exist then the value parameter
		/// will be returned.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if the collection or key is null.
		/// </exception>
		public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> collection, TKey key, TValue value)
		{
			#region Input validaton

			Insist.IsNotNull(collection, "collection");
			Insist.IsNotNull(key, "key");

			#endregion


			if (collection.ContainsKey(key))
			{
				return collection[key];
			}
			else
			{
				collection.Add(key, value);
				return value;
			}
		}

		/// <summary>
		/// Attempts to retrieve an item using the specified key. If the item doesn't
		/// exist then a new entry will be created in the dictionary using the specifed
		/// delegate
		/// </summary>
		/// <param name="key">
		/// The key to use to locate the value
		/// </param>
		/// <param name="valueFactory">
		/// A delegate that will be invoked to create the value if the key doesn't exist in the
		/// dictionary
		/// </param>
		/// <returns>
		/// Either the value associated with the key. If the key didnt exist then the value
		/// created by the valueFactory will be returned.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if the collection, key or valueFactory is null.
		/// </exception>
		public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> collection, TKey key, Func<TKey, TValue> valueFactory)
		{
			#region Input validaton

			Insist.IsNotNull(collection, "collection");
			Insist.IsNotNull(key, "key");
			Insist.IsNotNull(valueFactory, "valueFactory");

			#endregion

			if (collection.ContainsKey(key))
			{
				return collection[key];
			}
			else
			{
				TValue value = valueFactory(key);
				collection.Add(key, value);
				return value;
			}
		}
	}
}
