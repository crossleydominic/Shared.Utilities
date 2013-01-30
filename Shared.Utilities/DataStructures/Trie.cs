using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.DataStructures
{
    /// <summary>
    /// Trie data structure
    /// </summary>
    public class Trie<TKey, TSubKey, TValue> : IDictionary<TKey, TValue>
        where TKey : IEnumerable<TSubKey>
    {
        #region Intneral classes

        /// <summary>
        /// Internal helper class that maintains the connection between current
        /// branch in the tree, it's value and all the children.
        /// </summary>
        private class Node
        {
            #region Private members

            /// <summary>
            /// The complete key (the path) which is used to traverse the tree to get to this node
            /// </summary>
            private TKey _compositeKey;

            /// <summary>
            /// The node's key for it's current position in the tree.
            /// </summary>
            private TSubKey _nodeKey;

            /// <summary>
            /// The value that this node holds (if one has been set)
            /// </summary>
            private TValue _value;

            /// <summary>
            /// Whether or not this node is holding a value or is merely a branch leading to sub nodes
            /// </summary>
            private bool _hasValue;

            /// <summary>
            /// The list of child nodes that are reachable from this node.
            /// </summary>
            private List<Node> _children;

            #endregion

            #region Static helper methods

            /// <summary>
            /// Creates a new empty Node that does not have a key and can act as a root node
            /// </summary>
            public static Node Empty
            {
                get { return new Node(); }
            }

            /// <summary>
            /// Creates a new node that is associated with a key. A value can be assigned to this node
            /// using the AssignValue method
            /// </summary>
            public static Node Create(TSubKey nodeKey)
            {
                Node node = new Node();
                node._compositeKey = default(TKey);
                node._hasValue = false;
                node._nodeKey = nodeKey;
                node._value = default(TValue);

                return node;
            }

            #endregion

            #region Constructors

            /// <summary>
            /// Private constructor. Classes should use the static helper methods
            /// to create a new Node in different scenarios
            /// </summary>
            private Node()
            {
                _hasValue = false;
                _compositeKey = default(TKey);
                _nodeKey = default(TSubKey);
                _value = default(TValue);
                _children = new List<Node>();
            }

            #endregion

            #region Public Methods

            /// <summary>
            /// Assigns a value to this node for the given composite key.
            /// If a value already exists for this Node then it is overwritten
            /// </summary>
            public void AssignValue(TKey compositeKey, TValue value)
            {
                _value = value;
                _hasValue = true;
                _compositeKey = compositeKey;
            }

            /// <summary>
            /// Clears any value that this node is holding
            /// </summary>
            public void Clear()
            {
                _value = default(TValue);
                _hasValue = false;
            }

            /// <summary>
            /// Tries to get a child node that matches the supplied key using the supplied 
            /// comparer.  Returns null if a matching node cannot be found
            /// </summary>
            public Node GetChildForKey(TSubKey key, IEqualityComparer<TSubKey> subKeyComparer)
            {
                return _children.FirstOrDefault(c => subKeyComparer.Equals(key, c.NodeKey));
            }

            /// <summary>
            /// Adds a node to the list of this nodes children
            /// </summary>
            public void AddNode(Node newNode)
            {
                _children.Add(newNode);
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets this nodes key
            /// </summary>
            public TSubKey NodeKey { get { return _nodeKey; } }

            /// <summary>
            /// Gets the nodes composite key
            /// </summary>
            public TKey CompositeKey { get { return _compositeKey; } }

            /// <summary>
            /// Gets the nodes value, if it has one
            /// </summary>
            public TValue Value
            {
                get
                {
                    if (!_hasValue)
                    {
                        throw new InvalidOperationException("Cannot obtain the value for this node because it does not have one.");
                    }

                    return _value;
                }
            }

            /// <summary>
            /// Gets whether or not this node has a value associated with it.
            /// </summary>
            public bool HasValue { get { return _hasValue; } }

            /// <summary>
            /// Returns the set of children of this node
            /// </summary>
            public IEnumerable<Node> Children { get { return _children; } }

            #endregion
        }

        #endregion

        #region Private members

        /// <summary>
        /// The root node of the data structure
        /// </summary>
        private Node _root;

        /// <summary>
        /// An comparer that can be used to compare subkeys for equality
        /// </summary>
        private IEqualityComparer<TSubKey> _subKeyComparer;

        /// <summary>
        /// The number of items stored in this Trie
        /// </summary>
        private int _count;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a new Trie using the default equality comparer
        /// </summary>
        public Trie() : this(EqualityComparer<TSubKey>.Default) { }

        /// <summary>
        /// Construct a new Trie using the specific equality comparer
        /// </summary>
        public Trie(IEqualityComparer<TSubKey> subKeyComparer)
        {
            #region Input Validation

            if (subKeyComparer == null)
            {
                throw new ArgumentNullException("subKeyComparer");
            }

            #endregion

            _root = Node.Empty;
            _count = 0;
            _subKeyComparer = subKeyComparer;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a new item to this Trie indexed at the supplied key
        /// </summary>
        public void Add(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key cannot be null", "key");
            }

            Node node = FindNode(key, NotFoundAction.CreateNew);

            if (node.HasValue)
            {
                throw new InvalidOperationException("An element with the same key already exists");
            }
            else
            {
                node.AssignValue(key, value);
                _count++;
            }
        }

        /// <summary>
        /// Adds a new item to this Trie indexed at the supplied key
        /// </summary>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// Determines whether or not this Trie contains an item stored under the given key
        /// </summary>
        public bool ContainsKey(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key cannot be null", "key");
            }

            Node node = FindNode(key, NotFoundAction.ReturnNull);

            return node != null && node.HasValue;
        }

        /// <summary>
        /// Determines whether or not this Trie contains an item stored under the given key
        /// </summary>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ContainsKey(item.Key);
        }

        /// <summary>
        /// Removes the item index at the supplied key. Returns true if the item
        /// was successfully removed, otherwise false
        /// </summary>
        public bool Remove(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key cannot be null", "key");
            }

            Node node = FindNode(key, NotFoundAction.ReturnNull);

            if (node == null || node.HasValue == false)
            {
                return false;
            }
            else
            {
                _count--;
                node.Clear();
                return true;
            }
        }

        /// <summary>
        /// Attempts to retrieve the item using the specified key
        /// </summary>
        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);

            if (key == null)
            {
                return false;
            }

            Node node = FindNode(key, NotFoundAction.ReturnNull);

            if (node == null || node.HasValue == false)
            {
                return false;
            }
            else
            {
                value = node.Value;
                return true;
            }
        }

        /// <summary>
        /// Gets and sets an item at the specified key
        /// </summary>
        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                {
                    throw new ArgumentNullException("key cannot be null", "key");
                }

                TValue val;
                if (TryGetValue(key, out val))
                {
                    return val;
                }
                else
                {
                    throw new KeyNotFoundException("The given key was not present in the dictionary");
                }
            }
            set
            {
                if (key == null)
                {
                    throw new ArgumentNullException("key cannot be null", "key");
                }

                Node node = FindNode(key, NotFoundAction.CreateNew);

                if (node.HasValue == false)
                {
                    _count++;
                }

                node.AssignValue(key, value);
            }
        }

        /// <summary>
        /// Removes all items from this Trie
        /// </summary>
        public void Clear()
        {
            _count = 0;
            _root = Node.Empty;
        }

        /// <summary>
        /// Copies the contents of this Trie to the supplied array at the specified index
        /// </summary>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("arrayIndex");
            }

            if (this.Count > (array.Length - arrayIndex))
            {
                throw new ArgumentException("The number of elements in the collection is greater than the size of the destination array");
            }

            int currentIndex = 0;
            foreach (KeyValuePair<TKey, TValue> kvp in this)
            {
                array[arrayIndex + currentIndex] = kvp;
                currentIndex++;
            }
        }

        /// <summary>
        /// Removes the item using the specific key on the supplied item
        /// </summary>
        /// <returns>True if the item was removed, otherwise false</returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        /// <summary>
        /// Gets an enumerator that allows iteration through the items in this collection
        /// </summary>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            List<Node> toProcess = new List<Node>();
            toProcess.AddRange(_root.Children);

            while (toProcess.Count != 0)
            {
                Node currentNode = toProcess.First();
                toProcess.RemoveAt(0);
                toProcess.AddRange(currentNode.Children);

                if (currentNode.HasValue)
                {
                    yield return new KeyValuePair<TKey, TValue>(currentNode.CompositeKey, currentNode.Value);
                }
            }
        }

        /// <summary>
        /// Gets an enumerator that allows iteration through the items in this collection
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the collection of values
        /// </summary>
        public ICollection<TValue> Values
        {
            get { return this.Select(x => x.Value).ToList(); }
        }

        /// <summary>
        /// Gets the collection of keys
        /// </summary>
        public ICollection<TKey> Keys
        {
            get { return this.Select(x => x.Key).ToList(); }
        }

        /// <summary>
        /// Gets the number of items in the Trie
        /// </summary>
        public int Count
        {
            get { return _count; }
        }

        /// <summary>
        /// Gets whether or not this Trie is readonly
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the equality comparer that is used to compare the subkeys
        /// </summary>
        public IEqualityComparer<TSubKey> SubKeyComparer
        {
            get { return _subKeyComparer; }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Find a node from the supplied key.  The action is used to determine what
        /// to do if the node does not exist.
        /// </summary>
        private Node FindNode(TKey key, NotFoundAction action)
        {
            Node currentNode = _root;

            foreach (TSubKey subKey in key)
            {
                Node found = currentNode.GetChildForKey(subKey, _subKeyComparer);

                if (found == null)
                {
                    if (action == NotFoundAction.CreateNew)
                    {
                        found = Node.Create(subKey);
                        currentNode.AddNode(found);
                    }
                    else if (action == NotFoundAction.ReturnNull)
                    {
                        return null;
                    }
                    else
                    {
                        throw new InvalidOperationException("Inexhuastive branches");
                    }
                }

                currentNode = found;
            }

            return currentNode;
        }

        /// <summary>
        /// What action to perform when looking for a new and it doesnt exist
        /// </summary>
        private enum NotFoundAction
        {
            ReturnNull,
            CreateNew
        }

        #endregion
    }
}
