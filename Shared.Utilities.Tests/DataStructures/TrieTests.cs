using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.DataStructures;

namespace Shared.Utilities.Tests.DataStructures
{
    [TestFixture]
    public class TrieTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_SubKeyComparerIsNull_Throws()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>(null);
        }

        [Test]
        public void Constructor_CreatesDefaultEqualityComparer()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            Assert.AreEqual(EqualityComparer<char>.Default, trie.SubKeyComparer);
        }

        [Test]
        public void Add_ItemIsAdded()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key", 5);

            Assert.AreEqual(1, trie.Count);
            Assert.AreEqual(5, trie["key"]);
        }

        [Test]
        public void Add_NullItemIsAdded()
        {
            Trie<string, char, object> trie = new Trie<string, char, object>();

            trie.Add("key", null);

            Assert.AreEqual(1, trie.Count);
            Assert.AreEqual(null, trie["key"]);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_KeyIsNull_Throws()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add(null, 5);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Add_ItemExists_Throws()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key", 5);
            trie.Add("key", 6);
        }

        [Test]
        public void Add_KVP_ItemIsAdded()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add(new KeyValuePair<string, int>("key", 5));

            Assert.AreEqual(1, trie.Count);
            Assert.AreEqual(5, trie["key"]);
        }

        [Test]
        public void Add_KVP_NullItemIsAdded()
        {
            Trie<string, char, object> trie = new Trie<string, char, object>();

            trie.Add(new KeyValuePair<string, object>("key", null));

            Assert.AreEqual(1, trie.Count);
            Assert.AreEqual(null, trie["key"]);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_KVP_KeyIsNull_Throws()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add(new KeyValuePair<string, int>(null, 5));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Add_KVP_ItemExists_Throws()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add(new KeyValuePair<string, int>("key", 5));
            trie.Add(new KeyValuePair<string, int>("key", 6));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ContainsKey_KeyIsNull_Throws()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.ContainsKey(null);
        }

        [Test]
        public void ContainsKey_KeyDoesNotExist()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key", 5);

            Assert.IsFalse(trie.ContainsKey("key2"));
        }

        [Test]
        public void ContainsKey_KeyExists()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key", 5);

            Assert.IsTrue(trie.ContainsKey("key"));
        }

        [Test]
        public void Contains_KeyDoesNotExist()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key", 5);

            Assert.IsFalse(trie.Contains(new KeyValuePair<string, int>("key2", 1)));
        }

        [Test]
        public void Contains_KeyExists()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key", 5);

            Assert.IsTrue(trie.Contains(trie.First()));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Remove_KeyIsNull_Throws()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key", 5);

            trie.Remove(null);
        }

        [Test]
        public void Remove_ItemIsRemoved()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);
            trie.Add("key2", 6);

            Assert.AreEqual(2, trie.Count);

            bool result = trie.Remove("key1");

            Assert.IsTrue(result);
            Assert.AreEqual(1, trie.Count);
            Assert.IsFalse(trie.ContainsKey("key1"));
            Assert.IsTrue(trie.ContainsKey("key2"));
        }

        [Test]
        public void Remove_KeyDoesNotExist()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);

            Assert.AreEqual(1, trie.Count);

            bool result = trie.Remove("key2");

            Assert.IsFalse(result);
            Assert.AreEqual(1, trie.Count);
            Assert.IsTrue(trie.ContainsKey("key1"));
            Assert.IsFalse(trie.ContainsKey("key2"));
        }

        [Test]
        public void Remove_KVP_ItemIsRemoved()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);
            trie.Add("key2", 6);

            Assert.AreEqual(2, trie.Count);

            bool result = trie.Remove(trie.First(k => k.Key == "key1"));

            Assert.IsTrue(result);
            Assert.AreEqual(1, trie.Count);
            Assert.IsFalse(trie.ContainsKey("key1"));
            Assert.IsTrue(trie.ContainsKey("key2"));
        }

        [Test]
        public void Remove__KVPKeyDoesNotExist()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);

            Assert.AreEqual(1, trie.Count);

            bool result = trie.Remove(new KeyValuePair<string, int>("key2", 1));

            Assert.IsFalse(result);
            Assert.AreEqual(1, trie.Count);
            Assert.IsTrue(trie.ContainsKey("key1"));
            Assert.IsFalse(trie.ContainsKey("key2"));
        }

        [Test]
        public void TryGetValue_KeyIs_Null_DoesNotThrow()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);

            int outVal = -1;

            bool result = trie.TryGetValue(null, out outVal);

            Assert.IsFalse(result);
        }

        [Test]
        public void TryGetValue_KeyExists()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);

            int outVal = -1;

            bool result = trie.TryGetValue("key1", out outVal);

            Assert.IsTrue(result);
            Assert.AreEqual(5, outVal);
        }

        [Test]
        public void TryGetValue_KeyDoesNotExist()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);

            int outVal = -1;

            bool result = trie.TryGetValue("key2", out outVal);

            Assert.IsFalse(result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Indexer_Get_KeyIsNull_Throws()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);

            int value = trie[null];
        }

        [Test]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Indexer_Get_KeyDoesNotExist_Throws()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);

            int value = trie["key2"];
        }

        [Test]
        public void Indexer_Get_KeyExists()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);

            int value = trie["key1"];

            Assert.AreEqual(5, value);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Indexer_Set_KeyIsNull_Throws()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);

            trie[null] = 6;
        }

        [Test]
        public void Indexer_Set_KeyDoesNotExist()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);

            Assert.AreEqual(1, trie.Count);

            trie["key2"] = 6;

            Assert.AreEqual(2, trie.Count);
            Assert.AreEqual(6, trie["key2"]);
        }

        [Test]
        public void Indexer_Set_KeyExists()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);
            trie.Add("key2", 6);

            Assert.AreEqual(2, trie.Count);
            Assert.AreEqual(5, trie["key1"]);
            Assert.AreEqual(6, trie["key2"]);

            trie["key2"] = 7;

            Assert.AreEqual(2, trie.Count);
            Assert.AreEqual(7, trie["key2"]);
        }

        [Test]
        public void Clear_EmptyCollection()
        {
            Trie<string, char, object> trie = new Trie<string, char, object>();

            Assert.AreEqual(0, trie.Count);

            trie.Clear();

            Assert.AreEqual(0, trie.Count);
        }

        [Test]
        public void Clear_NonEmptyCollection()
        {
            Trie<string, char, object> trie = new Trie<string, char, object>();

            trie.Add("key1", 5);
            trie.Add("key2", 5);

            Assert.AreEqual(2, trie.Count);
            Assert.IsTrue(trie.ContainsKey("key1"));
            Assert.IsTrue(trie.ContainsKey("key2"));

            trie.Clear();

            Assert.AreEqual(0, trie.Count);
            Assert.IsFalse(trie.ContainsKey("key1"));
            Assert.IsFalse(trie.ContainsKey("key2"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyTo_ArrayIsNull_Throws()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);
            trie.Add("key2", 6);

            trie.CopyTo(null, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyTo_ArrayIndexLessThanZero_Thows()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);
            trie.Add("key2", 6);

            KeyValuePair<string, int>[] vals = new KeyValuePair<string, int>[2];
            trie.CopyTo(vals, -1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyTo_NotEnoughSpace_IndexZero_Throws()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);
            trie.Add("key2", 6);

            KeyValuePair<string, int>[] vals = new KeyValuePair<string, int>[1];
            trie.CopyTo(vals, 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyTo_NotEnoughSpace_IndexNonZero_Throws()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);

            KeyValuePair<string, int>[] vals = new KeyValuePair<string, int>[1];
            trie.CopyTo(vals, 1);
        }

        [Test]
        public void CopyTo_IndexZero_ValuesCopied()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);
            trie.Add("key2", 6);

            KeyValuePair<string, int>[] vals = new KeyValuePair<string, int>[4];
            trie.CopyTo(vals, 0);

            Assert.IsTrue(vals.Any(k => k.Key == "key1" && k.Value == 5));
            Assert.IsTrue(vals.Any(k => k.Key == "key2" && k.Value == 6));

            Assert.AreEqual(0, vals[2].Value);
            Assert.AreEqual(0, vals[3].Value);
        }

        [Test]
        public void CopyTo_IndexNonZero_ValuesCopied()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);
            trie.Add("key2", 6);

            KeyValuePair<string, int>[] vals = new KeyValuePair<string, int>[4];
            trie.CopyTo(vals, 2);

            Assert.AreEqual(0, vals[0].Value);
            Assert.AreEqual(0, vals[1].Value);
            Assert.IsTrue(vals.Skip(2).Any(k => k.Key == "key1" && k.Value == 5));
            Assert.IsTrue(vals.Skip(2).Any(k => k.Key == "key2" && k.Value == 6));
        }

        [Test]
        public void IsReadOnly_ShouldBeFalse()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            Assert.IsFalse(trie.IsReadOnly);
        }

        [Test]
        public void GetEnumerator_CollectionEmpty()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            List<KeyValuePair<string, int>> results = trie.ToList();

            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void GetEnumerator_CollectionNotEmpty()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);
            trie.Add("key2", 6);

            List<KeyValuePair<string, int>> results = trie.ToList();

            Assert.AreEqual(2, results.Count);
            Assert.IsTrue(results.Any(k => k.Key == "key1" && k.Value == 5));
            Assert.IsTrue(results.Any(k => k.Key == "key2" && k.Value == 6));
        }

        [Test]
        public void Values_CollectionEmpty()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            ICollection<int> results = trie.Values;

            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void Values_CollectionNotEmpty()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);
            trie.Add("key2", 6);

            ICollection<int> results = trie.Values;

            Assert.AreEqual(2, results.Count);
            Assert.IsTrue(results.Any(k => k == 5));
            Assert.IsTrue(results.Any(k => k == 6));
        }

        [Test]
        public void Keys_CollectionEmpty()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            ICollection<string> results = trie.Keys;

            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void Keys_CollectionNotEmpty()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);
            trie.Add("key2", 6);

            ICollection<string> results = trie.Keys;

            Assert.AreEqual(2, results.Count);
            Assert.IsTrue(results.Any(k => k == "key1"));
            Assert.IsTrue(results.Any(k => k == "key2"));
        }

        [Test]
        public void Count_EmptyList()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            Assert.AreEqual(0, trie.Count);
        }

        [Test]
        public void Count_AfterInserting()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            Assert.AreEqual(0, trie.Count);

            trie.Add("key1", 5);
            Assert.AreEqual(1, trie.Count);

            trie.Add("key2", 6);
            Assert.AreEqual(2, trie.Count);
        }

        [Test]
        public void Count_AfterRemoving()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);
            trie.Add("key2", 6);

            Assert.AreEqual(2, trie.Count);

            trie.Remove("key1");
            Assert.AreEqual(1, trie.Count);

            trie.Remove("key2");
            Assert.AreEqual(0, trie.Count);
        }

        [Test]
        public void Count_AfterCleaning()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);
            trie.Add("key2", 6);

            trie.Clear();

            Assert.AreEqual(0, trie.Count);
        }

        [Test]
        public void Count_AfterSetting()
        {
            Trie<string, char, int> trie = new Trie<string, char, int>();

            trie.Add("key1", 5);
            trie.Add("key2", 6);

            Assert.AreEqual(2, trie.Count);

            trie["key1"] = 10;
            Assert.AreEqual(2, trie.Count);

            trie["key2"] = 10;
            Assert.AreEqual(2, trie.Count);
        }
    }
}
