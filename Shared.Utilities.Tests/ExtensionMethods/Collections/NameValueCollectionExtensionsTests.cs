using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Collections.Specialized;
using Shared.Utilities.ExtensionMethods.Collections;

namespace Shared.Utilities.Tests.ExtensionMethods.Collections
{
	[TestFixture]
	public class NameValueCollectionExtensionsTests
	{
		#region IsNullOrEmpty Tests

		/// <summary>
		/// Tests when the collection is null.
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void IsNullOrEmpty_CollectionNull()
		{
			NameValueCollection collection = null;
			collection.IsNullOrEmpty(string.Empty);
		}

		/// <summary>
		/// Tests when the key is an empty string
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void IsNullOrEmpty_KeyEmpty()
		{
			NameValueCollection collection = new NameValueCollection();
			collection.Add("a", "b");
			collection.Add("c", "d");
			collection.IsNullOrEmpty(string.Empty);
		}

		/// <summary>
		/// Tests when the key is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void IsNullOrEmpty_KeyNull()
		{
			NameValueCollection collection = new NameValueCollection();
			collection.Add("a", "b");
			collection.Add("c", "d");
			collection.IsNullOrEmpty(null);
		}

		/// <summary>
		/// Test when the item exists in the collection
		/// </summary>
		[Test]
		public void IsNullOrEmpty_ItemExists()
		{
			NameValueCollection collection = new NameValueCollection();
			collection.Add("a", "b");
			collection.Add("c", "d");
			bool isNullOrEmpty = collection.IsNullOrEmpty("c");
			Assert.IsFalse(isNullOrEmpty);
		}

		/// <summary>
		/// Tests when the item does not exist in the collection
		/// </summary>
		[Test]
		public void IsNullOrEmpty_ItemDoesNotExist()
		{
			NameValueCollection collection = new NameValueCollection();
			collection.Add("a", "b");
			collection.Add("c", "d");
			bool isNullOrEmpty = collection.IsNullOrEmpty("e");
			Assert.IsTrue(isNullOrEmpty);
		}

		/// <summary>
		/// Tests when the item exists but the value is a null or empty string.
		/// </summary>
		[Test]
		public void IsNullOrEmpty_ItemExistsButIsNullOrEmpty()
		{
			NameValueCollection collection = new NameValueCollection();
			collection.Add("a", "b");
			collection.Add("c", "d");
			collection.Add("e", string.Empty);
			collection.Add("f", null);

			bool isNullOrEmpty = collection.IsNullOrEmpty("e");
			Assert.IsTrue(isNullOrEmpty);
			
			isNullOrEmpty = collection.IsNullOrEmpty("f");
			Assert.IsTrue(isNullOrEmpty);
		}

		#endregion
	}
}
