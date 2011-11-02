using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities {

	/// <summary>
	///  Static class providing often used exception creation patterns
	///  via methods.
	/// </summary>
	public static class Insist
	{
		#region IsNotNull

		/// <summary>
		///  Validates a required argument, throwing an ArgumentNullException
		///  if it is null.
		/// </summary>
		/// <param name="value">The argument value to be validated.</param>
		/// <exception cref="System.ArgumentNullException">If the value is null.</exception>
		public static void IsNotNull(Object value) 
		{
			Insist.IsNotNull(value, null, null);
		}

		/// <summary>
		///  Validates a required argument, throwing an ArgumentNullException
		///  if it is null.
		/// </summary>
		/// <param name="value">The argument value to be validated.</param>
		/// <param name="argumentName">The argument name.</param>
		/// <exception cref="System.ArgumentNullException">If the value is null.</exception>
		public static void IsNotNull(Object value, string argumentName)
		{
			Insist.IsNotNull(value, argumentName, null);
		}

		/// <summary>
		///  Validates a required argument, throwing an ArgumentNullException
		///  if it is null.
		/// </summary>
		/// <param name="value">The argument value to be validated.</param>
		/// <param name="argumentName">The argument name.</param>
		/// <param name="message">
		/// An optional message to include in the exception
		/// </param>
		/// <exception cref="System.ArgumentNullException">If the value is null.</exception>
		public static void IsNotNull(Object value, string argumentName, string message)
		{
			if (value == null)
			{
				throw new ArgumentNullException(
					argumentName ?? "value",
					message ?? "The supplied argument cannot be null");
			}
		}

		#endregion

		#region IsNotNullOrEmpty

		/// <summary>
		///  Validates a required argument, throwing an ArgumentNullException
		///  if it is null.
		/// </summary>
		/// <param name="value">The argument value to be validated.</param>
		/// <exception cref="System.ArgumentNullException">If the value is null or empty.</exception>
		public static void IsNotNullOrEmpty(string value) {
			Insist.IsNotNullOrEmpty(value, null, null);
		}

		/// <summary>
		///  Validates a required argument, throwing an ArgumentNullException
		///  if it is null.
		/// </summary>
		/// <param name="value">The argument value to be validated.</param>
		/// <param name="argumentName">The argument name to appear in the exception.</param>
		/// <exception cref="System.ArgumentNullException">If the value is null.</exception>
		public static void IsNotNullOrEmpty(string value, string argumentName) 
		{
			Insist.IsNotNullOrEmpty(value, argumentName, null);
		}

		/// <summary>
		///  Validates a required argument, throwing an ArgumentNullException
		///  if it is null.
		/// </summary>
		/// <param name="value">The argument value to be validated.</param>
		/// <param name="argumentName">The argument name to appear in the exception.</param>
		/// <param name="message">A message to include in the exception</param>
		/// <exception cref="System.ArgumentNullException">If the value is null.</exception>
		public static void IsNotNullOrEmpty(string value, string argumentName, string message)
		{
			if (String.IsNullOrEmpty(value))
			{
				throw new ArgumentException(
					message ?? "The string cannot be null or empty",
					argumentName ?? "value");
			}
		}

		#endregion

		#region IsWithinBounds

		/// <summary>
		///  Validates a integral argument, throwing ArgumentException if it falls outside
		///  the acceptable bounds.
		/// </summary>
		/// <param name="value">The value to be validated.</param>
		/// <param name="minValue">The minimum acceptable value.</param>
		/// <param name="maxValue">The maximum acceptable value.</param>
		public static void IsWithinBounds<T>(T value, T minValue, T maxValue) where T : IComparable<T>
		{
			Insist.IsWithinBounds(value, minValue, maxValue, null, null);
		}

		/// <summary>
		///  Validates a integral argument, throwing ArgumentException if it falls outside
		///  the acceptable bounds.
		/// </summary>
		/// <param name="value">The value to be validated.</param>
		/// <param name="argumentName">The argument name.</param>
		/// <param name="minValue">The minimum acceptable value.</param>
		/// <param name="maxValue">The maximum acceptable value.</param>
		public static void IsWithinBounds<T>(T value, T minValue, T maxValue, string argumentName) where T : IComparable<T>
		{
			Insist.IsWithinBounds(value, minValue, maxValue, argumentName, null);
		}

		/// <summary>
		///  Validates a integral argument, throwing ArgumentException if it falls outside
		///  the acceptable bounds.
		/// </summary>
		/// <param name="value">The value to be validated.</param>
		/// <param name="argumentName">The argument name.</param>
		/// <param name="message">A message to include in the exception</param>
		/// <param name="minValue">The minimum acceptable value.</param>
		/// <param name="maxValue">The maximum acceptable value.</param>
		public static void IsWithinBounds<T>(T value, T minValue, T maxValue,string argumentName, string message) where T : IComparable<T>
		{
			if ((value.CompareTo(minValue) < 0) ||
				(value.CompareTo(maxValue) > 0))
			{
				throw new ArgumentException(
					message ?? (String.Format("The value must be between {0} and {1}.", minValue, maxValue)), 
					argumentName ?? "value");
			}
		}

		#endregion

		#region IsAtLeast

		/// <summary>
		///  Validates an integral argument, throwing ArgumentException if it is less than the acceptable
		///  minimum value.
		/// </summary>
		/// <param name="value">The value to be validated.</param>
		/// <param name="argumentName">The argument name.</param>
		/// <param name="minValue">The minimum accepted value.</param>
		public static void IsAtLeast<T>(T value, T minValue) where T : IComparable<T>
		{
			Insist.IsAtLeast(value, minValue, null, null);
		}

		/// <summary>
		///  Validates an integral argument, throwing ArgumentException if it is less than the acceptable
		///  minimum value.
		/// </summary>
		/// <param name="value">The value to be validated.</param>
		/// <param name="argumentName">The argument name.</param>
		/// <param name="minValue">The minimum accepted value.</param>
		public static void IsAtLeast<T>(T value, T minValue, string argumentName) where T : IComparable<T>
		{
			Insist.IsAtLeast(value, minValue, argumentName, null);
		}

		/// <summary>
		///  Validates an integral argument, throwing ArgumentException if it is less than the acceptable
		///  minimum value.
		/// </summary>
		/// <param name="value">The value to be validated.</param>
		/// <param name="argumentName">The argument name.</param>
		/// <param name="message">A message to include in the exception</param>
		/// <param name="minValue">The minimum accepted value.</param>
		public static void IsAtLeast<T>(T value, T minValue, string argumentName, string message) where T : IComparable<T>
		{
			if (value.CompareTo(minValue) < 0)
			{
				throw new ArgumentException(
					message ?? String.Format("The value must be greater than or equal to {0}.", minValue),
					argumentName ?? "value");
			}
		}

		#endregion

		#region IsAtMost

		/// <summary>
		///  Validates an integral argument, throwing ArgumentException if it is more than the acceptable
		///  maximum value.
		/// </summary>
		/// <param name="value">The value to be validated.</param>
		/// <param name="argumentName">The argument name.</param>
		/// <param name="maxValue">The maximum accepted value.</param>
		public static void IsAtMost<T>(T value, T maxValue) where T:IComparable<T>
		{
			Insist.IsAtMost(value, maxValue, null, null);
		}

		/// <summary>
		///  Validates an integral argument, throwing ArgumentException if it is more than the acceptable
		///  maximum value.
		/// </summary>
		/// <param name="value">The value to be validated.</param>
		/// <param name="argumentName">The argument name.</param>
		/// <param name="maxValue">The maximum accepted value.</param>
		public static void IsAtMost<T>(T value, T maxValue, string argumentName) where T : IComparable<T>
		{
			Insist.IsAtMost(value, maxValue, argumentName, null);
		}

		/// <summary>
		///  Validates an integral argument, throwing ArgumentException if it is more than the acceptable
		///  maximum value.
		/// </summary>
		/// <param name="value">The value to be validated.</param>
		/// <param name="argumentName">The argument name.</param>
		/// <param name="message">A message to include in the exception</param>
		/// <param name="maxValue">The maximum accepted value.</param>
		public static void IsAtMost<T>(T value, T maxValue, string argumentName, string message) where T : IComparable<T>
		{
			if (value.CompareTo(maxValue) > 0)
			{
				throw new ArgumentException(
					message ?? String.Format("The value must be less than or equal to {0}.", maxValue),
					argumentName ?? "value");
			}
		}

		#endregion

		#region IsAssignableFrom

		/// <summary>
		///  Validates a type argument, throwing ArgumentException if it is not assignable from the specified type.
		/// </summary>
		/// <typeparam name="T">The type the argument must implement or inherit from.</typeparam>
		/// <param name="value">The type to be validated..</param>
		public static void IsAssignableFrom<T>(Type value)
		{
			Insist.IsAssignableFrom<T>(value, null, null);
		}

		/// <summary>
		///  Validates a type argument, throwing ArgumentException if it is not assignable from the specified type.
		/// </summary>
		/// <typeparam name="T">The type the argument must implement or inherit from.</typeparam>
		/// <param name="value">The type to be validated..</param>
		/// <param name="argumentName">The argument name.</param>
		public static void IsAssignableFrom<T>(Type value, string argumentName)
		{
			Insist.IsAssignableFrom<T>(value, argumentName, null);
		}

		/// <summary>
		///  Validates a type argument, throwing ArgumentException if it is not assignable from the specified type.
		/// </summary>
		/// <typeparam name="T">The type the argument must implement or inherit from.</typeparam>
		/// <param name="value">The type to be validated..</param>
		/// <param name="message">A message to include in the exception</param>
		/// <param name="argumentName">The argument name.</param>
		public static void IsAssignableFrom<T>(Type value, string argumentName, string message)
		{
			if (!typeof(T).IsAssignableFrom(value))
			{
				throw new ArgumentException(
					message ?? String.Format("The type {0} is not assignable from {1}.", value, typeof(T)),
					argumentName ?? "value");
			}
		}

		#endregion

		#region Equality

		/// <summary>
		///  Verifies that an input value matches an expected value, throwing an ArgumentException if not.
		/// </summary>
		/// <param name="expectedValue">The expected value.</param>
		/// <param name="actualValue">The actual value.</param>
		/// <exception cref="System.ArgumentException">If the actual value does not match the expected value.</exception>
		public static void Equality(Object actualValue, Object expectedValue)
		{
			Insist.Equality(actualValue, expectedValue, null, null);
		}

		/// <summary>
		///  Verifies that an input value matches an expected value, throwing an ArgumentException if not.
		/// </summary>
		/// <param name="expectedValue">The expected value.</param>
		/// <param name="actualValue">The actual value.</param>
		/// <param name="argumentName">The argument name.</param>
		/// <exception cref="System.ArgumentException">If the actual value does not match the expected value.</exception>
		public static void Equality(Object actualValue, Object expectedValue, string argumentName)
		{
			Insist.Equality(actualValue, expectedValue, argumentName, null);
		}

		/// <summary>
		///  Verifies that an input value matches an expected value, throwing an ArgumentException if not.
		/// </summary>
		/// <param name="expectedValue">The expected value.</param>
		/// <param name="actualValue">The actual value.</param>
		/// <param name="argumentName">The argument name.</param>
		/// <exception cref="System.ArgumentException">If the actual value does not match the expected value.</exception>
		public static void Equality(Object actualValue, Object expectedValue, string argumentName, string message)
		{
			if (!Object.Equals(actualValue, expectedValue))
			{
				throw new ArgumentException(
					message ?? string.Format("The value provided ({0}) did not match the expected value of {1}.", actualValue, expectedValue),
					argumentName ?? "actualValue");
			}
		}

		#endregion

		#region AllItemsAreNotNull

		/// <summary>
		/// Ensures that all items in the supplied collection are not null
		/// </summary>
		/// <param name="collection">
		/// The collection to be validated.
		/// </param>
		public static void AllItemsAreNotNull<T>(IEnumerable<T> collection)
		{
			AllItemsAreNotNull(collection, null, null);
		}

		/// <summary>
		/// Ensures that all items in the supplied collection are not null
		/// </summary>
		/// <param name="collection">
		/// The collection to be validated.
		/// </param>
		/// <param name="argumentName">
		/// The argument name.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// If the collection itself is null
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// If the collection contains a null value
		/// </exception>
		public static void AllItemsAreNotNull<T>(IEnumerable<T> collection, string argumentName)
		{
			Insist.AllItemsAreNotNull(collection, argumentName, null);
		}


		/// <summary>
		/// Ensures that all items in the supplied collection are not null
		/// </summary>
		/// <param name="collection">
		/// The collection to be validated.
		/// </param>
		/// <param name="argumentName">
		/// The argument name.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// If the collection itself is null
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// If the collection contains a null value
		/// </exception>
		public static void AllItemsAreNotNull<T>(IEnumerable<T> collection, string argumentName, string message)
		{
			Insist.IsNotNull(collection, "collection");

			Insist.AllItemsSatisfyCondition(
				collection,
				(i) => { return i != null; },
				argumentName ?? "collection",
				message ?? "A null item was found in the collection");
		}

		#endregion

		#region AllItemsSatisfyCondition

		/// <summary>
		/// Ensures that all items in the supplied collection satisfy the supplied predicate
		/// </summary>
		/// <param name="collection">
		/// The collection to be validated.
		/// </param>
		/// <param name="predicate">
		/// The predicate that is applied to every element in the collection
		/// </param>
		public static void AllItemsSatisfyCondition<T>(IEnumerable<T> collection, Predicate<T> predicate)
		{
			AllItemsSatisfyCondition(collection, predicate, null, null);
		}

		/// <summary>
		/// Ensures that all items in the supplied collection satisfy the supplied predicate
		/// </summary>
		/// <param name="collection">
		/// The collection to be validated.
		/// </param>
		/// <param name="predicate">
		/// The predicate that is applied to every element in the collection
		/// </param>
		/// <param name="argumentName">
		/// The argument name.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// If the collection itself is null
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// If the collection contains a value that violates a predicate
		/// </exception>
		public static void AllItemsSatisfyCondition<T>(IEnumerable<T> collection, Predicate<T> predicate, string argumentName)
		{
			Insist.AllItemsSatisfyCondition(collection, predicate, argumentName, null);
		}

		/// <summary>
		/// Ensures that all items in the supplied collection satisfy the supplied predicate
		/// </summary>
		/// <param name="collection">
		/// The collection to be validated.
		/// </param>
		/// <param name="predicate">
		/// The predicate that is applied to every element in the collection
		/// </param>
		/// <param name="argumentName">
		/// The argument name.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// If the collection itself is null
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// If the collection contains a value that violates a predicate
		/// </exception>
		public static void AllItemsSatisfyCondition<T>(IEnumerable<T> collection, Predicate<T> predicate, string argumentName, string message)
		{
			Insist.IsNotNull(collection, "collection");
			Insist.IsNotNull(predicate, "predicate");

			foreach (T obj in collection)
			{
				if (!predicate(obj))
				{
					throw new ArgumentException(
						message ?? "An item was found in the collection that violates the predicate",
						argumentName ?? "collection");
				}
			}
		}

		#endregion

		#region ContainsAtLeast

		/// <summary>
		/// Ensures that the collection contains at least the number of specified items
		/// </summary>
		/// <param name="collection">
		/// The collection to be validated.
		/// </param>
		/// <param name="minNumberOfItems">
		/// The minimum number of items that the collection must have
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// If the collection itself is null
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// If the collection contains less than the minumum number of items
		/// </exception>
		public static void ContainsAtLeast<T>(IEnumerable<T> collection, int minNumberOfItems)
		{
			ContainsAtLeast(collection, minNumberOfItems, null, null);
		}

		/// <summary>
		/// Ensures that the collection contains at least the number of specified items
		/// </summary>
		/// <param name="collection">
		/// The collection to be validated.
		/// </param>
		/// <param name="minNumberOfItems">
		/// The minimum number of items that the collection must have
		/// </param>
		/// <param name="argumentName">
		/// The argument name.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// If the collection itself is null
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// If the collection contains less than the minumum number of items. OR. the minNumberOfItems
		/// argument is less than zero.
		/// </exception>
		public static void ContainsAtLeast<T>(IEnumerable<T> collection, int minNumberOfItems, string argumentName)
		{
			Insist.ContainsAtLeast(collection, minNumberOfItems, argumentName, null);
		}


		/// <summary>
		/// Ensures that the collection contains at least the number of specified items
		/// </summary>
		/// <param name="collection">
		/// The collection to be validated.
		/// </param>
		/// <param name="minNumberOfItems">
		/// The minimum number of items that the collection must have
		/// </param>
		/// <param name="argumentName">
		/// The argument name.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// If the collection itself is null
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// If the collection contains less than the minumum number of items. OR. the minNumberOfItems
		/// argument is less than zero.
		/// </exception>
		public static void ContainsAtLeast<T>(IEnumerable<T> collection, int minNumberOfItems, string argumentName, string message)
		{
			Insist.IsNotNull(collection, "collection");
			Insist.IsAtLeast(minNumberOfItems, 0, "minNumberOfItems");

			if (CountItems(collection) < minNumberOfItems)
			{
				throw new ArgumentException(
					message ?? "collection contains less than the minimum number of items",
					argumentName ?? "collection");
			}
		}

		#endregion

		#region ContainsAtMost

		/// <summary>
		/// Ensures that the collection contains at most the number of specified items
		/// </summary>
		/// <param name="collection">
		/// The collection to be validated.
		/// </param>
		/// <param name="minNumberOfItems">
		/// The maximum number of items that the collection must have
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// If the collection itself is null
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// If the collection contains more than the minumum number of items. OR. the maxNumberOfItems
		/// argument is less than zero.
		/// </exception>
		public static void ContainsAtMost<T>(IEnumerable<T> collection, int maxNumberOfItems)
		{
			Insist.ContainsAtMost(collection, maxNumberOfItems, null, null);
		}

		/// <summary>
		/// Ensures that the collection contains at most the number of specified items
		/// </summary>
		/// <param name="collection">
		/// The collection to be validated.
		/// </param>
		/// <param name="minNumberOfItems">
		/// The maximum number of items that the collection must have
		/// </param>
		/// <param name="argumentName">
		/// The argument name.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// If the collection itself is null
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// If the collection contains more than the minumum number of items. OR. the maxNumberOfItems
		/// argument is less than zero.
		/// </exception>
		public static void ContainsAtMost<T>(IEnumerable<T> collection, int maxNumberOfItems, string argumentName)
		{
			Insist.ContainsAtMost(collection, maxNumberOfItems, argumentName, null);
		}

		/// <summary>
		/// Ensures that the collection contains at most the number of specified items
		/// </summary>
		/// <param name="collection">
		/// The collection to be validated.
		/// </param>
		/// <param name="minNumberOfItems">
		/// The maximum number of items that the collection must have
		/// </param>
		/// <param name="argumentName">
		/// The argument name.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// If the collection itself is null
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// If the collection contains more than the minumum number of items. OR. the maxNumberOfItems
		/// argument is less than zero.
		/// </exception>
		public static void ContainsAtMost<T>(IEnumerable<T> collection, int maxNumberOfItems, string argumentName, string message)
		{
			Insist.IsNotNull(collection, "collection");
			Insist.IsAtLeast(maxNumberOfItems, 0, "maxNumberOfItems");

			if (CountItems(collection) > maxNumberOfItems)
			{
				throw new ArgumentException(
					message??"collection contains more than the maximum number of items", 
					argumentName ?? "collection");
			}
		}

		#endregion

		#region IsDefined

		/// <summary>
		/// Ensures that the specified value is defined in the specified Enum
		/// </summary>
		/// <param name="value">
		/// The value to check for existance in the enum
		/// </param>
		/// <typeparam name="T">
		/// The Enum to check the value against.
		/// </typeparam>
		public static void IsDefined<T>(T value) where T : struct
		{
			Insist.IsDefined<T>(value, null, null);
		}

		/// <summary>
		/// Ensures that the specified value is defined in the specified Enum
		/// </summary>
		/// <param name="value">
		/// The value to check for existance in the enum
		/// </param>
		/// <param name="argumentName">
		/// The argument name.
		/// </param>
		/// <typeparam name="T">
		/// The Enum to check the value against.
		/// </typeparam>
		public static void IsDefined<T>(T value, string argumentName) where T : struct
		{
			Insist.IsDefined<T>(value, argumentName, null);
		}

		/// <summary>
		/// Ensures that the specified value is defined in the specified Enum
		/// </summary>
		/// <param name="value">
		/// The value to check for existance in the enum
		/// </param>
		/// <param name="argumentName">
		/// The argument name.
		/// </param>
		/// <typeparam name="T">
		/// The Enum to check the value against.
		/// </typeparam>
		public static void IsDefined<T>(T value, string argumentName, string message) where T : struct
		{
			if (!Enum.IsDefined(typeof(T), value))
			{
				throw new ArgumentException(
					message ?? string.Format("Value '{0}' is not defined for the enum '{1}'", value, typeof(T).FullName),
					argumentName ?? "value");
			}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Helper method to count the number of items in an IEnumerable
		/// </summary>
		private static int CountItems<T>(IEnumerable<T> items)
		{
			int count;

			ICollection<T> collection = (items as ICollection<T>);

			if(collection != null) {
				count = collection.Count;
			} else {
				count = items.Count();
			}
			
			return count;
		}

		#endregion
	}
}
