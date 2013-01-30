using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.DataStructures
{
    /// <summary>
    /// A Trie data structure that allows items to be indexed by a string 
    /// (and sub indexed by the characters within the string)
    /// </summary>
    public class StringTrie<TValue> : Trie<string, char, TValue> { }
}
