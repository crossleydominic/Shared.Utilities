using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.ExtensionMethods.Primitives;
using Crypto = System.Security.Cryptography;

namespace Shared.Utilities.Tests.ExtensionMethods.Primitives
{
	[TestFixture]
	public class StringExtensionsTests
	{
		#region ContainsIgnoreCase Tests

		/// <summary>
		/// Test when the first string is not null but the second string is null
		/// </summary>
		[Test]
		public void ContainsIgnoreCase_Str1NotNullStr2Null()
		{
			string str1 = string.Empty;
			string str2 = null;

			Assert.IsFalse(str1.ContainsIgnoreCase(str2));
		}

		/// <summary>
		/// Test when the first string is null but the second string is not null
		/// </summary>
		[Test]
		public void ContainsIgnoreCase_Str1NullStr2NotNull()
		{
			string str1 = null;
			string str2 = string.Empty;

			Assert.IsFalse(str1.ContainsIgnoreCase(str2));
		}

		/// <summary>
		/// Test when the first string is empty but the second string is null
		/// </summary>
		[Test]
		public void ContainsIgnoreCase_Str1EmptyStr2Null()
		{
			string str1 = string.Empty;
			string str2 = null;

			Assert.IsFalse(str1.ContainsIgnoreCase(str2));
		}

		/// <summary>
		/// Test when both strings are null.
		/// </summary>
		[Test]
		public void ContainsIgnoreCase_BothStringsNull()
		{
			string str1 = null;
			string str2 = null;

			Assert.IsTrue(str1.ContainsIgnoreCase(str2));
		}

		/// <summary>
		/// Test when the strings are equal (including case).
		/// </summary>
		[Test]
		public void ContainsIgnoreCase_StringsEqual()
		{
			string str1 = "abcdef";

			//Use an intermediate StringBuilder to prevent string interning
			//(otherwise we'd only be testing reference-equality and not
			//value-equality
			StringBuilder builder = new StringBuilder();
			builder.Append("abc");
			builder.Append("def");

			string str2 = builder.ToString();

			Assert.IsTrue(str1.ContainsIgnoreCase(str2));
		}

		/// <summary>
		/// Tests when the both strings are different
		/// </summary>
		[Test]
		public void ContainsIgnoreCase_StringsDiffer()
		{
			string str1 = "abcdef";

			//Use an intermediate StringBuilder to prevent string interning
			//(otherwise we'd only be testing reference-equality and not
			//value-equality
			StringBuilder builder = new StringBuilder();
			builder.Append("abc");
			builder.Append("defg");

			string str2 = builder.ToString();

			Assert.IsFalse(str1.ContainsIgnoreCase(str2));
		}

		/// <summary>
		/// Test when the the strings contain the same characters and differ in 
		/// case only.
		/// </summary>
		[Test]
		public void ContainsIgnoreCase_StringsDifferInCaseOnly()
		{
			string str1 = "abcdef";

			//Use an intermediate StringBuilder to prevent string interning
			//(otherwise we'd only be testing reference-equality and not
			//value-equality
			StringBuilder builder = new StringBuilder();
			builder.Append("ABC");
			builder.Append("def");

			string str2 = builder.ToString();

			Assert.IsTrue(str1.ContainsIgnoreCase(str2));
		}

		/// <summary>
		/// Test when the string to find is at the start
		/// </summary>
		[Test]
		public void ContainsIgnoreCase_StringAtStart()
		{
			string str1 = "abcdef";

			//Use an intermediate StringBuilder to prevent string interning
			//(otherwise we'd only be testing reference-equality and not
			//value-equality
			StringBuilder builder = new StringBuilder();
			builder.Append("abc");

			string str2 = builder.ToString();

			Assert.IsTrue(str1.ContainsIgnoreCase(str2));
		}

		/// <summary>
		/// Test when the string to find is at the end
		/// </summary>
		[Test]
		public void ContainsIgnoreCase_StringAtEnd()
		{
			string str1 = "abcdef";

			//Use an intermediate StringBuilder to prevent string interning
			//(otherwise we'd only be testing reference-equality and not
			//value-equality
			StringBuilder builder = new StringBuilder();
			builder.Append("def");

			string str2 = builder.ToString();

			Assert.IsTrue(str1.ContainsIgnoreCase(str2));
		}

		/// <summary>
		/// Test when the string is in the middle
		/// </summary>
		[Test]
		public void ContainsIgnoreCase_StringInTheMiddle()
		{
			string str1 = "abcdef";

			//Use an intermediate StringBuilder to prevent string interning
			//(otherwise we'd only be testing reference-equality and not
			//value-equality
			StringBuilder builder = new StringBuilder();
			builder.Append("bc");
			builder.Append("de");

			string str2 = builder.ToString();

			Assert.IsTrue(str1.ContainsIgnoreCase(str2));
		}


		#endregion

		#region OnlyConsistsOf Test

		/// <summary>
		/// Tests when the string is null
		/// </summary>
		[Test]
		public void OnlyConsistsOf_StringNull()
		{
			string str = null;
			bool result = str.OnlyConsistsOf('a');

			Assert.IsFalse(result);
		}

		/// <summary>
		/// Tests when the string is empty
		/// </summary>
		[Test]
		public void OnlyConsistsOf_StringEmpty()
		{
			string str = string.Empty;
			bool result = str.OnlyConsistsOf('a');

			Assert.IsFalse(result);
		}

		/// <summary>
		/// Test when the string contains some whitespace
		/// </summary>
		[Test]
		public void OnlyConsistsOf_ContainsWhitespace()
		{
			string str = " a";
			bool result = str.OnlyConsistsOf('a');

			Assert.IsFalse(result);
		}

		/// <summary>
		/// Test when the string contains a single instance of a single character
		/// </summary>
		[Test]
		public void OnlyConsistsOf_ContainsSingleCharacter()
		{
			string str = "a";
			bool result = str.OnlyConsistsOf('a');

			Assert.IsTrue(result);
		}

		/// <summary>
		/// Test when the string contains a single instance of multiple characters
		/// </summary>
		[Test]
		public void OnlyConsistsOf_ContainsMultipleCharacters()
		{
			string str = "abc";
			bool result = str.OnlyConsistsOf('a');

			Assert.IsFalse(result);
		}

		/// <summary>
		/// Test when the string contains multiple instances of a single character
		/// </summary>
		[Test]
		public void OnlyConsistsOf_ContainsMultipleSingleCharacter()
		{
			string str = "aaa";
			bool result = str.OnlyConsistsOf('a');

			Assert.IsTrue(result);
		}

		#endregion

		#region RemoveLeadingCharacters Tests

		/// <summary>
		/// Tests when the string is null
		/// </summary>
		[Test]
		public void RemoveLeadingCharacters_StringNull()
		{
			string str = null;
			str = str.RemoveLeadingCharacters('a');

			Assert.IsNull(str);
		}

		/// <summary>
		/// Test when the string is empty
		/// </summary>
		[Test]
		public void RemoveLeadingCharacters_StringEmpty()
		{
			string str = string.Empty;
			str = str.RemoveLeadingCharacters('a');

			Assert.IsEmpty(str);
		}

		/// <summary>
		/// Test when all characters in the string will be removed
		/// </summary>
		[Test]
		public void RemoveLeadingCharacters_RemoveAll()
		{
			string str = "aaa";
			str = str.RemoveLeadingCharacters('a');

			Assert.IsEmpty(str);
		}

		/// <summary>
		/// Test when only the first character in the string should be removed
		/// </summary>
		[Test]
		public void RemoveLeadingCharacters_RemoveFirst()
		{
			string str = "abc";
			str = str.RemoveLeadingCharacters('a');

			Assert.IsTrue(str == "bc");
		}

		/// <summary>
		/// Test when the string consists of only 1 character that will get removed
		/// </summary>
		[Test]
		public void RemoveLeadingCharacters_RemoveSingleCharacter()
		{
			string str = "a";
			str = str.RemoveLeadingCharacters('a');

			Assert.IsEmpty(str);
		}

		/// <summary>
		/// Test when every character in the string but the last should be removed.
		/// </summary>
		[Test]
		public void RemoveLeadingCharacters_RemoveAllButLast()
		{
			string str = "aaab";
			str = str.RemoveLeadingCharacters('a');

			Assert.IsTrue(str == "b");
		}

		/// <summary>
		/// Test to make sure that only the leading character is removed when the removal
		/// character exists later in the string.
		/// </summary>
		[Test]
		public void RemoveLeadingCharacters_TrailingRemovalCharacter()
		{
			string str = "aba";
			str = str.RemoveLeadingCharacters('a');

			Assert.IsTrue(str == "ba");
		}

		/// <summary>
		/// Test when the string contains leading whitespace
		/// </summary>
		[Test]
		public void RemoveLeadingCharacters_WithLeadingWhitespace()
		{
			string str = " abc";
			str = str.RemoveLeadingCharacters('a');

			Assert.IsTrue(str == " abc");
		}

		#endregion

		#region EqualsIgnoreCase Tests

		/// <summary>
		/// Test when the first string is not null but the second string is null
		/// </summary>
		[Test]
		public void EqualsIgnoreCase_Str1NotNullStr2Null()
		{
			string str1 = string.Empty;
			string str2 = null;

			Assert.IsFalse(str1.EqualsIgnoreCase(str2));
		}

		/// <summary>
		/// Test when the first string is null but the second string is not null
		/// </summary>
		[Test]
		public void EqualsIgnoreCase_Str1NullStr2NotNull()
		{
			string str1 = null;
			string str2 = string.Empty;

			Assert.IsFalse(str1.EqualsIgnoreCase(str2));
		}

		/// <summary>
		/// Test when the first string is empty but the second string is null
		/// </summary>
		[Test]
		public void EqualsIgnoreCase_Str1EmptyStr2Null()
		{
			string str1 = string.Empty;
			string str2 = null;

			Assert.IsFalse(str1.EqualsIgnoreCase(str2));
		}

		/// <summary>
		/// Test when both strings are null.
		/// </summary>
		[Test]
		public void EqualsIgnoreCase_BothStringsNull()
		{
			string str1 = null;
			string str2 = null;

			Assert.IsTrue(str1.EqualsIgnoreCase(str2));
		}

		/// <summary>
		/// Test when the strings are equal (including case).
		/// </summary>
		[Test]
		public void EqualsIgnoreCase_StringsEqual()
		{
			string str1 = "abcdef";

			//Use an intermediate StringBuilder to prevent string interning
			//(otherwise we'd only be testing reference-equality and not
			//value-equality
			StringBuilder builder = new StringBuilder();
			builder.Append("abc");
			builder.Append("def");

			string str2 = builder.ToString();

			Assert.IsTrue(str1.EqualsIgnoreCase(str2));
		}

		/// <summary>
		/// Tests when the both strings are different
		/// </summary>
		[Test]
		public void EqualsIgnoreCase_StringsDiffer()
		{
			string str1 = "abcdef";

			//Use an intermediate StringBuilder to prevent string interning
			//(otherwise we'd only be testing reference-equality and not
			//value-equality
			StringBuilder builder = new StringBuilder();
			builder.Append("abc");
			builder.Append("defg");

			string str2 = builder.ToString();

			Assert.IsFalse(str1.EqualsIgnoreCase(str2));
		}

		/// <summary>
		/// Test when the the strings contain the same characters and differ in 
		/// case only.
		/// </summary>
		[Test]
		public void EqualsIgnoreCase_StringsDifferInCaseOnly()
		{
			string str1 = "abcdef";

			//Use an intermediate StringBuilder to prevent string interning
			//(otherwise we'd only be testing reference-equality and not
			//value-equality
			StringBuilder builder = new StringBuilder();
			builder.Append("ABC");
			builder.Append("def");

			string str2 = builder.ToString();

			Assert.IsTrue(str1.EqualsIgnoreCase(str2));
		}

		#endregion

		#region Distance Tests

		/// <summary>
		/// Tests when both strings are null
		/// </summary>
		[Test]
		public void Distance_Str1NullStr2Null()
		{
			string str1 = null;
			string str2 = null;

			int distance = str1.Distance(str2);

			Assert.IsTrue(distance == 0);
		}

		/// <summary>
		/// Tests when the first string is null but the second string isnt
		/// </summary>
		[Test]
		public void Distance_Str1NullStr2NotNull()
		{
			string str1 = null;
			string str2 = "abcdef";

			int distance = str1.Distance(str2);

			Assert.IsTrue(distance == 6);
		}

		/// <summary>
		/// Tests when the first string it not null but the second string is null.
		/// </summary>
		[Test]
		public void Distance_Str1NotNullStr2Null()
		{
			string str1 = "abcdef";
			string str2 = null;

			int distance = str1.Distance(str2);

			Assert.IsTrue(distance == 6);
		}

		/// <summary>
		/// Tests when both string contain the same letters but in different cases
		/// </summary>
		[Test]
		public void Distance_DifferentCases()
		{
			string str1 = "abcdef";
			string str2 = "ABCDEF";

			int distance = str1.Distance(str2);

			Assert.IsTrue(distance == 6);
		}

		/// <summary>
		/// Tests the score when we insert some characters
		/// </summary>
		[Test]
		public void Distance_Insertion()
		{
			string str1 = "abcdef";
			string str2 = "abczdefz";

			int distance = str1.Distance(str2);

			Assert.IsTrue(distance == 2);
		}

		/// <summary>
		/// Tests the score when we remove some characters
		/// </summary>
		[Test]
		public void Distance_Deletion()
		{
			string str1 = "abcdef";
			string str2 = "abdf";

			int distance = str1.Distance(str2);

			Assert.IsTrue(distance == 2);
		}

		/// <summary>
		/// Tests the scroe when we substitute some characters
		/// </summary>
		[Test]
		public void Distance_Substitution()
		{
			string str1 = "abcdef";
			string str2 = "abzdzf";

			int distance = str1.Distance(str2);

			Assert.IsTrue(distance == 2);
		}

		/// <summary>
		/// Tests the score when the second string has some whitespace in it.
		/// </summary>
		[Test]
		public void Distance_ContainsWhiteSpace()
		{
			string str1 = "abcdef";
			string str2 = "ab\td f";

			int distance = str1.Distance(str2);

			Assert.IsTrue(distance == 2);
		}

		/// <summary>
		/// Test the score when both strings are equal (including case)
		/// </summary>
		[Test]
		public void Distance_StringsEqual()
		{
			string str1 = "abcdef";
			string str2 = "abcdef";

			int distance = str1.Distance(str2);

			Assert.IsTrue(distance == 0);
		}

		/// <summary>
		/// Tests the score when the first string contains unicode escaped characters.
		/// </summary>
		[Test]
		public void Distance_ContainsNonAsciiCharacters()
		{
			string str1 = "ab\u0063def";
			string str2 = "abcdef";

			int distance = str1.Distance(str2);

			Assert.IsTrue(distance == 0);

			str1 = "\u3041\u3042\u3043\u3044";
			str2 = "\u3041\u3043\u3043\u3044";

			distance = str1.Distance(str2);

			Assert.IsTrue(distance == 1);
		}

		#endregion

		#region Pad Tests

		/// <summary>
		/// Tests what happens when you specify 0 as a padding length
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void Pad_ZeroPadding()
		{
			string str = "ABCDEFG";
			string strPadded = str.Pad('A', 0, PaddingMode.Left);
		}

		/// <summary>
		/// Test what happens when you specify a negative number as a padding length
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void Pad_LessThanZeroPadding()
		{
			string str = "ABCDEFG";
			string strPadded = str.Pad('A', Int32.MinValue, PaddingMode.Left);
		}

		/// <summary>
		/// Tests what happens when you try and pad a null string
		/// </summary>
		[Test]
		public void Pad_NullStringPad()
		{
			string strPadded = null;

			//Pad left
			string str = null;
			strPadded = str.Pad('A', 3, PaddingMode.Left);
			Assert.AreEqual(
				strPadded,
				"AAA");

			//Pad right
			strPadded = str.Pad('A', 3, PaddingMode.Right);
			Assert.AreEqual(
				strPadded,
				"AAA");
		}

		/// <summary>
		/// Tests what happens when you try and pad a string that is already long
		/// than the padToLength
		/// </summary>
		[Test]
		public void Pad_StringAlreadyTooLong()
		{
			string str = "ABCDEFG";
			string strPadded = null;

			//Pad left
			strPadded = str.Pad('A', 3, PaddingMode.Left);
			Assert.AreEqual(strPadded, "ABCDEFG");

			//Pad right
			strPadded = str.Pad('A', 3, PaddingMode.Right);
			Assert.AreEqual(strPadded, "ABCDEFG");
		}

		/// <summary>
		/// Test what happens when you try and pad a string which is less than the pad to length
		/// </summary>
		[Test]
		public void Pad_StringLessThanPadToLength()
		{
			string str = "ABCDEFG";
			string strPadded = null;

			//Pad left
			strPadded = str.Pad('A', 10, PaddingMode.Left);
			Assert.AreEqual(strPadded, "AAAABCDEFG");

			//Pad right
			strPadded = str.Pad('A', 10, PaddingMode.Right);
			Assert.AreEqual(strPadded, "ABCDEFGAAA");
		}

		#endregion

		#region Hash Tests

		/// <summary>
		/// Tests when the string to hash is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentException))]
		public void Hash_NullString()
		{
			string str = null;
			string hash = str.Hash();
		}

		/// <summary>
		/// Tests when the string to hash is empty
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void Hash_EmptyString()
		{
			string str = string.Empty;
			string hash = str.Hash();
		}

		/// <summary>
		/// Tests when the hashing algorithm to use is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void Hash_AlgorithmNull()
		{
			string str = "abc123";
			string hash = str.Hash(null);
		}

		/// <summary>
		/// Tests when the encoding to use is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void Hash_EncodingNull()
		{
			string str = "abc123";
			string hash = str.Hash(new Crypto.SHA256CryptoServiceProvider(), null);
		}

		/// <summary>
		/// Tests to make sure that the default hashing algorithm is SHA256
		/// </summary>
		[Test]
		public void Hash_DefaultAlgorithm()
		{
			string str = "abc123";
			string hashSha256 = str.Hash();

			//Ensure the default hash algorithm is sha256.
			string hashSha256Specified = str.Hash(new Crypto.SHA256CryptoServiceProvider());
			Assert.AreEqual(hashSha256, hashSha256Specified);
		}

		/// <summary>
		/// Tests to make sure that the default encoding is UTF8
		/// </summary>
		[Test]
		public void Hash_DefaultEncoding()
		{
			string str = "abc123";
			string hashUtf8 = str.Hash(new Crypto.SHA256CryptoServiceProvider());

			//Ensure the default hash algorithm is sha256.
			string hashUtf8Specified = str.Hash(new Crypto.SHA256CryptoServiceProvider(), Encoding.UTF8);
			Assert.AreEqual(hashUtf8, hashUtf8Specified);
		}

		/// <summary>
		/// Tests to make sure that the supplied hash algorithm and encoding
		/// produce the expected hashes.
		/// </summary>
		[Test]
		public void Hash_AlgorithmAndEncodingNotNull()
		{
			string str = "abc123";

			string precomputedUtf8Sha256Hash = "6CA13D52CA70C883E0F0BB101E425A89E8624DE51DB2D2392593AF6A84118090";
			string precomputedUtf16Sha256Hash = "C18F3E0599590D1F028AC69563D25C03F83F3A4981AFAB4A040A0137C4F9FB78";
			string precomputedUtf8Md5Hash = "E99A18C428CB38D5F260853678922E03";
			string precomputedUtf16Md5Hash = "6E9B3A7620AAF77F362775150977EEB8";

			Assert.AreEqual(precomputedUtf8Sha256Hash, str.Hash(new Crypto.SHA256CryptoServiceProvider(), Encoding.UTF8).ToUpper());
			Assert.AreEqual(precomputedUtf16Sha256Hash, str.Hash(new Crypto.SHA256CryptoServiceProvider(), Encoding.Unicode).ToUpper());
			Assert.AreEqual(precomputedUtf8Md5Hash, str.Hash(new Crypto.MD5CryptoServiceProvider(), Encoding.UTF8).ToUpper());
			Assert.AreEqual(precomputedUtf16Md5Hash, str.Hash(new Crypto.MD5CryptoServiceProvider(), Encoding.Unicode).ToUpper());
		}

		#endregion

		#region Wrap Tests

		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentException))]
		public void Wrap_LineLengthLessThanZero()
		{
			string str = null;
			str.Wrap(-1);
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void Wrap_LineLengthIsZero()
		{
			string str = null;
			str.Wrap(0);
		}

		[Test]
		public void Wrap_StringNull()
		{
			string str = null;
			str.Wrap(10);

			Assert.IsNull(str);
		}

		[Test]
		public void Wrap_LineLengthLongerThanString()
		{
			string STRING_TO_TRIM = "abcdefg";

			string str = STRING_TO_TRIM;
			str = str.Wrap(STRING_TO_TRIM.Length + 1);

			Assert.AreEqual(str, STRING_TO_TRIM);
		}

		[Test]
		public void Wrap_LineLengthAsLongThanString()
		{
			string STRING_TO_TRIM = "abcdefg";

			string str = STRING_TO_TRIM;
			str = str.Wrap(STRING_TO_TRIM.Length);

			Assert.AreEqual(str, STRING_TO_TRIM);
		}

		[Test]
		public void Wrap_LineLengthShorterThanString()
		{
			string str= "abcdefg";
			str = str.Wrap(4);

			string[] parts = str.Split(new char[] { '\r' });
			Assert.AreEqual(parts[0].Trim(), "abcd");
			Assert.AreEqual(parts[1].Trim(), "efg");
		}

		[Test]
		public void Wrap_WordSpansLineBreakWhilstRespectingWhitespace()
		{
			string str = "ab cdefg";

			//We want to split in the middle of the word "cdefg"
			str = str.Wrap(4);

			string[] parts = str.Split(new char[] { '\r' });
			Assert.AreEqual(parts[0].Trim(), "ab");
			Assert.AreEqual(parts[1].Trim(), "cdef");
			Assert.AreEqual(parts[2].Trim(), "g");
		}

		[Test]
		public void Wrap_WordSpansLineBreakWhilstNotRespectingWhitespace()
		{
			string str = "ab cdefg";

			//We want to split in the middle of the word "cdefg"
			str = str.Wrap(4, false);

			string[] parts = str.Split(new char[] { '\r' });
			Assert.AreEqual(parts[0].Trim(), "ab c");
			Assert.AreEqual(parts[1].Trim(), "defg");
		}

		#endregion

		#region Format Tests

		/// <summary>
		/// Tests when the pattern string is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentException))]
		public void Format_StringIsNull()
		{
			string str = null;
			str.Format(1);
		}

		/// <summary>
		/// Tests when the pattern string is empty
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void Format_StringIsEmpty()
		{
			string str = string.Empty;
			str.Format(1);
		}

		/// <summary>
		/// Tests when the pattern has no replacement placeholders
		/// </summary>
		[Test]
		public void Format_PatternWithNoArguments()
		{
			string str = "Some String";
			str = str.Format();

			Assert.AreEqual(str, "Some String");
		}

		/// <summary>
		/// Tests when the pattern has multiple replacement placeholders
		/// </summary>
		[Test]
		public void Format_PatternWithMultipleArguments()
		{
			string str = "{0}{1}{0}";
			str = str.Format(0,1);

			Assert.AreEqual(str, "010");
		} 

		#endregion
	}
}
