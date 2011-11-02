using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.ExtensionMethods.Collections;

namespace Shared.Utilities.Tests.ExtensionMethods.Collections
{
	[TestFixture]
	public class IDictionaryExtensionsTests
	{
		#region Sort Tests

		/*
		 * NOTE
		 * 
		 * This extension method shouldn't really be used. It relies on an implementation detail
		 * of Microsofts Dictionary which could be subject to change.  If MS decide to modify 
		 * their Dictionary class (they think of a better packing algorithm or something) then
		 * the Sort method will no longer work.
		 * 
		 * A SortedList<Key, Value> should be used instead (it's really a dictionary despite it's stupid name).
		 * 
		 * 
		 * Also, the correctness of the sorting is based entirely on the Comparison delegate that 
		 * you supply to the Sort method.  Because of this, it makes no sense to try and test
		 * the Sort method other to test it's invariants. Doing so would not really be testing the
		 * Sort method itself but would be testing the Comparison function and the tests could get
		 * polluted by bugs in the comparison functions.
		 * 
		 */

		/// <summary>
		/// Tests what happens when the collection is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void Sort_CollectionNull()
		{
			Dictionary<int, int> dictionary = null;
			dictionary.Sort(new Comparison<KeyValuePair<int, int>>(
				(kvp1, kvp2) => 
				{
					if (kvp1.Value < kvp2.Value) { return -1; }
					if (kvp1.Value > kvp2.Value) { return 1; }
					return 0;
				}));
		}

		/// <summary>
		/// Tests what happens when the comparison delegate is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void Sort_ComparisonNull()
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			dictionary.Sort(null);
		}

		#endregion

		#region FindByValue Tests

		/// <summary>
		/// Test when the collection is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void FindByValue_CollectionNull()
		{
			Dictionary<int, int> collection = null;
			collection.FindByValue(0);
		}

		/// <summary>
		/// Tests to ensure that the equality comparison within the function will 
		/// cascade the equality calculations to derived types if they are overridden
		/// </summary>
		[Test]
		public void FindByValue_EqualityOnDerivation()
		{
			Dictionary<int, object> collection = new Dictionary<int,object>();
			collection.Add(0, new object());
			collection.Add(1, 1);
			collection.Add(2, "a");

			Dictionary<int, object> foundItems = collection.FindByValue("a");

			Assert.IsTrue(foundItems.Count == 1);
		}

		/// <summary>
		/// Test when the collection is empty.
		/// </summary>
		[Test]
		public void FindByValue_CollectionEmpty()
		{
			Dictionary<int, string> collection = new Dictionary<int, string>();

			Dictionary<int, string> foundItems = collection.FindByValue("d");

			Assert.IsTrue(foundItems != null && foundItems.Count == 0);
		}

		/// <summary>
		/// Test when the item does not exist in the collection
		/// </summary>
		[Test]
		public void FindByValue_ItemDoesNotExist()
		{
			Dictionary<int, string> collection = new Dictionary<int, string>();
			collection.Add(0, "a");
			collection.Add(1, "b");
			collection.Add(2, "c");

			Dictionary<int, string> foundItems = collection.FindByValue("d");

			Assert.IsTrue(foundItems.Count == 0);
		}

		/// <summary>
		/// Test to find an item that exists more than once in the collection
		/// </summary>
		[Test]
		public void FindByValue_ItemExistsMoreThanOnce()
		{
			Dictionary<int, string> collection = new Dictionary<int, string>();
			collection.Add(0, "a");
			collection.Add(1, "b");
			collection.Add(2, "c");
			collection.Add(3, "c");
			collection.Add(4, "c");

			Dictionary<int, string> foundItems = collection.FindByValue("c");

			Assert.IsTrue(foundItems.Count == 3);
			Assert.IsTrue(foundItems.ContainsKey(2) && foundItems[2] == "c");
			Assert.IsTrue(foundItems.ContainsKey(3) && foundItems[3] == "c");
			Assert.IsTrue(foundItems.ContainsKey(4) && foundItems[4] == "c");

		}

		#endregion

		#region FindAll Tests

		/*
		 * NOTE
		 * 
		 * As with the Sort tests, the FindAll method is wholly dependant on the Predicate
		 * you supply to do the finding. Writing tests for this method would really be testing the
		 * predicate which is not what we want to do.
		 * 
		 * The only tests for this method are the method invariants.
		 * 
		 */

		/// <summary>
		/// Tests what happens when the collection is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void FindAll_CollectionNull()
		{
			Dictionary<int, int> dictionary = null;
			dictionary.FindAll(new Predicate<KeyValuePair<int, int>>((kvp) => { return false; }));
		}

		/// <summary>
		/// Tests what happens when the predicate is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void FindAll_PredicateNull()
		{
			Dictionary<int, int> dictionary = new Dictionary<int,int>();
			dictionary.FindAll(null);
		}

		/// <summary>
		/// Tests when the collection is empty
		/// </summary>
		[Test]
		public void FindAll_CollectionEmpty()
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			Dictionary<int, int> foundItems = dictionary.FindAll(new Predicate<KeyValuePair<int, int>>((kvp) => { return false; }));

			Assert.IsTrue(foundItems != null && foundItems.Count == 0);
		}

		#endregion

		#region Find Tests

		/*
		 * NOTE
		 * 
		 * As with the Sort/FindAll tests, the Find method is wholly dependant on the Predicate
		 * you supply to do the finding. Writing tests for this method would really be testing the
		 * predicate which is not what we want to do.
		 * 
		 * The only tests for this method are the method invariants.
		 * 
		 */

		/// <summary>
		/// Tests what happens when the collection is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void Find_CollectionNull()
		{
			Dictionary<int, int> dictionary = null;
			dictionary.Find(new Predicate<KeyValuePair<int, int>>((kvp) => { return false; }));
		}

		/// <summary>
		/// Tests what happens when the predicate is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void Find_PredicateNull()
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			dictionary.Find(null);
		}

		/// <summary>
		/// Tests when the collection is empty
		/// </summary>
		[Test]
		public void Find_CollectionEmpty()
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			KeyValuePair<int, int>? foundItem = dictionary.Find(new Predicate<KeyValuePair<int, int>>((kvp) => { return false; }));

			Assert.IsTrue(foundItem.HasValue == false);
		}

		#endregion

		#region IsNumeric Tests

		/*
		 * NOTE
		 * 
		 * Internally, the IsNumeric methods use the RegexHelper which has been
		 * extensively tested elsewhere.  The tests for IsNumeric will be 
		 * constrained to tests on its public interface.
		 * 
		 */

		/// <summary>
		/// Tests when the Dictionary is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void IsNumeric_CollectionNull()
		{
			IDictionary<string,string> collection = null;
			collection.IsNumeric(string.Empty);
		}

		#endregion

		#region IsInteger tests

		/*
		 * NOTE
		 * 
		 * Internally, the IsInteger methods use the RegexHelper which has been
		 * extensively tested elsewhere.  The tests for IsInteger will be 
		 * constrained to tests on its public interface.
		 * 
		 */

		/// <summary>
		/// Tests when the Dictionary is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void IsInteger_CollectionNull()
		{
			IDictionary<string, string> collection = null;
			collection.IsInteger(string.Empty);
		}

		#endregion

		#region GetOrAdd Tests

		/// <summary>
		/// Test to GetOrAdd when the collection is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentNullException))]
		public void GetOrAdd_CollectionNull()
		{
			IDictionary<string, string> dictionary = null;
			dictionary.GetOrAdd("key1", "value1");
		}

		/// <summary>
		/// Test to GetOrAdd when the key is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void GetOrAdd_KeyNull()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.GetOrAdd(null, "value1");
		}

		/// <summary>
		/// Test to GetOrAdd when the value is null
		/// </summary>
		[Test]
		public void GetOrAdd_ValueNull()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.GetOrAdd("key1", (string)null);

			Assert.IsTrue(dictionary["key1"] == null);
		}

		/// <summary>
		/// Test to GetOrAdd when the key already exists in the collection
		/// </summary>
		[Test]
		public void GetOrAdd_KeyAlreadyExists()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("key1", "value1");

			string value = dictionary.GetOrAdd("key1", "value2");

			Assert.IsTrue(value == "value1");
		}

		/// <summary>
		/// Test to GetOrAdd when the key doesnt already exist in the collection
		/// </summary>
		[Test]
		public void GetOrAdd_KeyDoesntAlreadyExists()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();

			string value = dictionary.GetOrAdd("key1", "value1");

			Assert.IsTrue(value == "value1");
		}

		/// <summary>
		/// Test to GetOrAdd, using a value factory, when the collection is null.
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void GetOrAdd_UsingValueFactory_CollectionNull()
		{
			IDictionary<string, string> dictionary = null;
			dictionary.GetOrAdd("key1", (k) => { return "value1"; });
		}

		/// <summary>
		/// Test to GetOrAdd, using a value factory, when the key is null.
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void GetOrAdd_UsingValueFactor_KeyNull()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.GetOrAdd(null, (k) => { return "value1"; });
		}

		/// <summary>
		/// Test to GetOrAdd, using a value factory, when the valueFactory is null.
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void GetOrAdd_UsingValueFactor_ValueFactoryNull()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.GetOrAdd("key1", (Func<string,string>)null);
		}

		/// <summary>
		/// Test to GetOrAdd, using a value factory, when the value generated by
		/// the valueFactory is null.
		/// </summary>
		[Test]
		public void GetOrAdd_UsingValueFactor_GeneratedValueNull()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.GetOrAdd("key1", (k) => { return null; });

			Assert.IsTrue(dictionary["key1"] == null);
		}

		/// <summary>
		/// Test to GetOrAdd, using a value factory, when the key already exists in the collection.
		/// </summary>
		[Test]
		public void GetOrAdd_UsingValueFactor_KeyAlreadyExists()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("key1", "value1");

			string value = dictionary.GetOrAdd("key1", (k) => { return "value2"; });

			Assert.IsTrue(value == "value1");
		}

		/// <summary>
		/// Test to GetOrAdd, using a value factory, when the key doesn't already exist in the collection.
		/// </summary>
		[Test]
		public void GetOrAdd_UsingValueFactor_KeyDoesntAlreadyExists()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();

			string value = dictionary.GetOrAdd("key1", (k) => { return "value1"; });

			Assert.IsTrue(value == "value1");
		}

		#endregion
	}
}
