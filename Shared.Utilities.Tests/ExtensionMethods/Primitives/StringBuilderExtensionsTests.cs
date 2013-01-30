using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.ExtensionMethods.Primitives;

namespace Shared.Utilities.Tests.ExtensionMethods.Primitives
{
	/// <summary>
	/// A set of tests for the String Builder Extensions.
	/// </summary>
	[TestFixture]
	public class StringBuilderExtensionsTests
	{
		#region RemoveTrailingCharacter Tests

		/// <summary>
		/// Tests when the string builder is null or empty
		/// </summary>
		[Test]
		public void RemoveTrailingCharacter_StringBuilderNullOrEmpty()
		{
			StringBuilder builder = null;
			builder.RemoveTrailingCharacter('A');
			Assert.IsNull(builder);

			builder = new StringBuilder();
			builder.RemoveTrailingCharacter('A');
			Assert.IsTrue(builder.ToString() == string.Empty);
		}

		/// <summary>
		/// Tests when the trailing character only exists once at the end of the string
		/// </summary>
		[Test]
		public void RemoveTrailingCharacter_TrailingCharacterExistsOnce()
		{
			string noTrailingWhiteSpaceStr = "a b c d e g h";
			string trailingWhiteSpaceStr = "a b c d e g h \t ";

			StringBuilder noTrailingWhiteSpaceBuilder;
			StringBuilder trailingWhiteSpaceBuilder;

			//No whitespace, ignore
			noTrailingWhiteSpaceBuilder = new StringBuilder();
			noTrailingWhiteSpaceBuilder.Append(noTrailingWhiteSpaceStr);
			noTrailingWhiteSpaceBuilder.RemoveTrailingCharacter('h');
			Assert.IsTrue(noTrailingWhiteSpaceBuilder.ToString() == "a b c d e g ");

			//no whitespace, no ignore
			noTrailingWhiteSpaceBuilder = new StringBuilder();
			noTrailingWhiteSpaceBuilder.Append(noTrailingWhiteSpaceStr);
			noTrailingWhiteSpaceBuilder.RemoveTrailingCharacter('h', false);
			Assert.IsTrue(noTrailingWhiteSpaceBuilder.ToString() == "a b c d e g ");

			//whitespace, ignore
			trailingWhiteSpaceBuilder = new StringBuilder();
			trailingWhiteSpaceBuilder.Append(trailingWhiteSpaceStr);
			trailingWhiteSpaceBuilder.RemoveTrailingCharacter('h');
			Assert.IsTrue(trailingWhiteSpaceBuilder.ToString() == "a b c d e g ");

			//no whitespace, no ignore
			trailingWhiteSpaceBuilder = new StringBuilder();
			trailingWhiteSpaceBuilder.Append(trailingWhiteSpaceStr);
			trailingWhiteSpaceBuilder.RemoveTrailingCharacter('h', false);
			Assert.IsTrue(trailingWhiteSpaceBuilder.ToString() == "a b c d e g h \t ");

		}

		/// <summary>
		/// Tests what happens when the trailing character appears more than once at the end.
		/// </summary>
		[Test]
		public void RemoveTrailingCharacter_TrailingCharacterExistsMoreThanOnce()
		{
			string noTrailingWhiteSpaceStr = "a b c d e g hhhh";
			string trailingWhiteSpaceStr = "a b c d e g hhhh \t ";

			StringBuilder noTrailingWhiteSpaceBuilder;
			StringBuilder trailingWhiteSpaceBuilder;

			//No whitespace, ignore
			noTrailingWhiteSpaceBuilder = new StringBuilder();
			noTrailingWhiteSpaceBuilder.Append(noTrailingWhiteSpaceStr);
			noTrailingWhiteSpaceBuilder.RemoveTrailingCharacter('h');
			Assert.IsTrue(noTrailingWhiteSpaceBuilder.ToString() == "a b c d e g hhh");

			//no whitespace, no ignore
			noTrailingWhiteSpaceBuilder = new StringBuilder();
			noTrailingWhiteSpaceBuilder.Append(noTrailingWhiteSpaceStr);
			noTrailingWhiteSpaceBuilder.RemoveTrailingCharacter('h', false);
			Assert.IsTrue(noTrailingWhiteSpaceBuilder.ToString() == "a b c d e g hhh");

			//whitespace, ignore
			trailingWhiteSpaceBuilder = new StringBuilder();
			trailingWhiteSpaceBuilder.Append(trailingWhiteSpaceStr);
			trailingWhiteSpaceBuilder.RemoveTrailingCharacter('h');
			Assert.IsTrue(trailingWhiteSpaceBuilder.ToString() == "a b c d e g hhh");

			//no whitespace, no ignore
			trailingWhiteSpaceBuilder = new StringBuilder();
			trailingWhiteSpaceBuilder.Append(trailingWhiteSpaceStr);
			trailingWhiteSpaceBuilder.RemoveTrailingCharacter('h', false);
			Assert.IsTrue(trailingWhiteSpaceBuilder.ToString() == "a b c d e g hhhh \t ");
		}

		/// <summary>
		/// Tests what happens on a string builder only contains one character
		/// </summary>
		[Test]
		public void RemoveTrailingCharacter_SingleCharacterString()
		{
			string noTrailingWhiteSpaceStr = "a";
			string trailingWhiteSpaceStr = "a \t ";

			StringBuilder noTrailingWhiteSpaceBuilder;
			StringBuilder trailingWhiteSpaceBuilder;

			//No whitespace, ignore
			noTrailingWhiteSpaceBuilder = new StringBuilder();
			noTrailingWhiteSpaceBuilder.Append(noTrailingWhiteSpaceStr);
			noTrailingWhiteSpaceBuilder.RemoveTrailingCharacter('a');
			Assert.IsTrue(noTrailingWhiteSpaceBuilder.ToString() == string.Empty);

			//no whitespace, no ignore
			noTrailingWhiteSpaceBuilder = new StringBuilder();
			noTrailingWhiteSpaceBuilder.Append(noTrailingWhiteSpaceStr);
			noTrailingWhiteSpaceBuilder.RemoveTrailingCharacter('a', false);
			Assert.IsTrue(noTrailingWhiteSpaceBuilder.ToString() == string.Empty);

			//whitespace, ignore
			trailingWhiteSpaceBuilder = new StringBuilder();
			trailingWhiteSpaceBuilder.Append(trailingWhiteSpaceStr);
			trailingWhiteSpaceBuilder.RemoveTrailingCharacter('a');
			Assert.IsTrue(trailingWhiteSpaceBuilder.ToString() == string.Empty);

			//no whitespace, no ignore
			trailingWhiteSpaceBuilder = new StringBuilder();
			trailingWhiteSpaceBuilder.Append(trailingWhiteSpaceStr);
			trailingWhiteSpaceBuilder.RemoveTrailingCharacter('a', false);
			Assert.IsTrue(trailingWhiteSpaceBuilder.ToString() == "a \t ");
		}

		/// <summary>
		/// Tests what happens to the string builder when the supplied character does not exist at the end.
		/// </summary>
		[Test]
		public void RemoveTrailingCharacter_CharacterDoesNotAppear()
		{
			string noTrailingWhiteSpaceStr = "abc";
			string trailingWhiteSpaceStr = "abc \t ";

			StringBuilder noTrailingWhiteSpaceBuilder;
			StringBuilder trailingWhiteSpaceBuilder;

			//No whitespace, ignore
			noTrailingWhiteSpaceBuilder = new StringBuilder();
			noTrailingWhiteSpaceBuilder.Append(noTrailingWhiteSpaceStr);
			noTrailingWhiteSpaceBuilder.RemoveTrailingCharacter('z');
			Assert.IsTrue(noTrailingWhiteSpaceBuilder.ToString() == "abc");

			//no whitespace, no ignore
			noTrailingWhiteSpaceBuilder = new StringBuilder();
			noTrailingWhiteSpaceBuilder.Append(noTrailingWhiteSpaceStr);
			noTrailingWhiteSpaceBuilder.RemoveTrailingCharacter('z', false);
			Assert.IsTrue(noTrailingWhiteSpaceBuilder.ToString() == "abc");

			//whitespace, ignore
			trailingWhiteSpaceBuilder = new StringBuilder();
			trailingWhiteSpaceBuilder.Append(trailingWhiteSpaceStr);
			trailingWhiteSpaceBuilder.RemoveTrailingCharacter('z');
			Assert.IsTrue(trailingWhiteSpaceBuilder.ToString() == "abc \t ");

			//no whitespace, no ignore
			trailingWhiteSpaceBuilder = new StringBuilder();
			trailingWhiteSpaceBuilder.Append(trailingWhiteSpaceStr);
			trailingWhiteSpaceBuilder.RemoveTrailingCharacter('z', false);
			Assert.IsTrue(trailingWhiteSpaceBuilder.ToString() == "abc \t ");
		}

		#endregion

		#region Contains Tests

		/// <summary>
		/// Tests what happens when the string builder is null
		/// </summary>
		[Test]
		public void Contains_StringBuilderNull()
		{
			StringBuilder builder = null;
			bool doesContain = builder.Contains("abc");
			Assert.IsFalse(doesContain);
		}

		/// <summary>
		/// Tests what happens when the substring is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentException))]
		public void Contains_SubStringNull()
		{
			StringBuilder builder = new StringBuilder();
			string subString = null;
			bool doesContain = builder.Contains(subString);
			Assert.IsFalse(doesContain);
		}

		/// <summary>
		/// Tests what happens when the substring is empty (but not null).
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void Contains_SubStringEmpty()
		{
			StringBuilder builder = new StringBuilder();
			string subString = null;
			bool doesContain = builder.Contains(subString);
			Assert.IsFalse(doesContain);
		}
		
		/// <summary>
		/// Tests what happens when the substring is within the string builder but
		/// differes in case
		/// </summary>
		[Test]
		public void Contains_SubStringDiffersInCaseOnly()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("abcdefg");
			string subString = "CDE";
			bool doesContain = builder.Contains(subString);
			Assert.IsFalse(doesContain);
		}

		/// <summary>
		/// Tests the substring routine that do not appear in the ascii table
		/// </summary>
		[Test]
		public void Contains_StringsAreNonAscii()
		{
			StringBuilder builder = new StringBuilder();

			//Some random japanese characters, not a perfect test I know but it shows
			//that the function works with non ascii characters.
			builder.Append("\u3041\u3042\u3043");
			string subString = "\u3042";
			bool doesContain = builder.Contains(subString);
			Assert.IsTrue(doesContain);
		}

		/// <summary>
		/// Test what happens when the substring overlaps the start of the string builder
		/// </summary>
		[Test]
		public void Contains_SubStringOverlapsStart()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("bcdef");
			string subString = "abc";
			bool doesContain = builder.Contains(subString);
			Assert.IsFalse(doesContain);
		}


		/// <summary>
		/// Test what happens when the substring overlaps the end of the string builder
		/// </summary>
		[Test]
		public void Contains_SubStringOverlapsEnd()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("bcdef");
			string subString = "efg";
			bool doesContain = builder.Contains(subString);
			Assert.IsFalse(doesContain);
		}


		/// <summary>
		/// Test what happens when the substring is at the start of the string builder
		/// </summary>
		[Test]
		public void Contains_SubStringAtStart()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("bcdef");
			string subString = "bcd";
			bool doesContain = builder.Contains(subString);
			Assert.IsTrue(doesContain);
		}


		/// <summary>
		/// Test what happens when the substring is at the end of the string builder
		/// </summary>
		[Test]
		public void Contains_SubStringAtEnd()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("bcdef");
			string subString = "def";
			bool doesContain = builder.Contains(subString);
			Assert.IsTrue(doesContain);
		}

		/// <summary>
		/// Test what happens when the substring is in the middle of the string builder.
		/// </summary>
		[Test]
		public void Contains_SubStringInMiddle()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("bcdef");
			string subString = "cde";
			bool doesContain = builder.Contains(subString);
			Assert.IsTrue(doesContain);
		}

		#endregion

		#region StartsWith Tests

		/// <summary>
		/// Tests when the string builder is null
		/// </summary>
		[Test]
		public void StartsWith_StringBuilderNull()
		{
			StringBuilder builder = null;
			bool startsWith = builder.StartsWith("a");
			Assert.IsFalse(startsWith);
		}

		/// <summary>
		/// Tests when the test string is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentException))]
		public void StartsWith_StringNull()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("abcdefg");
			string startString = null;
			bool startsWith = builder.StartsWith(startString);
		}

		/// <summary>
		/// Tests when the test string is empty (not null)
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void StartsWith_StringEmpty()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("abcdefg");
			string startString = string.Empty;
			bool startsWith = builder.StartsWith(startString);
		}

		/// <summary>
		/// Tests with non ascii characters
		/// </summary>
		[Test]
		public void StartsWith_ContainsNonAsciiCharacters()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("\u3041\u3042\u3043\u3044");
			string startString = "\u3041\u3042";
			bool startsWith = builder.StartsWith(startString);
			Assert.IsTrue(startsWith);
		}

		/// <summary>
		/// Tests with the test string at the start of the string builder
		/// </summary>
		[Test]
		public void StartsWith_StringAtStart()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("abcdefg");
			string startString = "abc";

			bool startsWith = builder.StartsWith(startString);
			Assert.IsTrue(startsWith);

			startString = "ABC";
			startsWith = builder.StartsWith(startString, StringComparison.OrdinalIgnoreCase);
			Assert.IsTrue(startsWith);
		}

		/// <summary>
		/// Tests when the test string is not at the start of the string builder.
		/// </summary>
		[Test]
		public void StartsWith_StringNotAtStart()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("abcdefg");
			string startString = "bcd";

			bool startsWith = builder.StartsWith(startString);
			Assert.IsFalse(startsWith);

			startString = "BCD";
			startsWith = builder.StartsWith(startString, StringComparison.OrdinalIgnoreCase);
			Assert.IsFalse(startsWith);
		}

		#endregion

		#region EndsWith Tests

		/// <summary>
		/// Tests when the string builder is null
		/// </summary>
		[Test]
		public void EndsWith_StringBuilderNull()
		{
			StringBuilder builder = null;
			bool endsWith = builder.EndsWith("a");
			Assert.IsFalse(endsWith);
		}

		/// <summary>
		/// Tests when the test string is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void EndsWith_StringNull()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("abcdefg");
			string endString = null;
			bool endsWith = builder.EndsWith(endString);
		}

		/// <summary>
		/// Tests when the test string is empty (not null)
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void EndsWith_StringEmpty()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("abcdefg");
			string endString = string.Empty;
			bool endsWith = builder.EndsWith(endString);
		}

		/// <summary>
		/// Tests with non ascii characters
		/// </summary>
		[Test]
		public void EndsWith_ContainsNonAsciiCharacters()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("\u3041\u3042\u3043\u3044");
			string endString = "\u3043\u3044";
			bool endsWith = builder.EndsWith(endString);
			Assert.IsTrue(endsWith);
		}

		/// <summary>
		/// Tests with the test string at the start of the string builder
		/// </summary>
		[Test]
		public void EndsWith_StringAtEnd()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("abcdefg");
			string endString = "efg";

			bool endsWith = builder.EndsWith(endString);
			Assert.IsTrue(endsWith);

			endString = "EFG";
			endsWith = builder.EndsWith(endString, StringComparison.OrdinalIgnoreCase);
			Assert.IsTrue(endsWith);
		}

		/// <summary>
		/// Tests when the test string is not at the start of the string builder.
		/// </summary>
		[Test]
		public void EndsWith_StringNotAtEnd()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("abcdefg");
			string endString = "bcd";

			bool endsWith = builder.EndsWith(endString);
			Assert.IsFalse(endsWith);

			endString = "BCD";
			endsWith = builder.EndsWith(endString, StringComparison.OrdinalIgnoreCase);
			Assert.IsFalse(endsWith);
		}

		#endregion

		#region SubString Tests

		/// <summary>
		/// Tests what happens when the string builder is null.
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentNullException))]
		public void SubString_StringBuilderNull()
		{
			StringBuilder builder = null;
			builder.SubString(0);
		}

		/// <summary>
		/// Test for when the string builder is empty
		/// </summary>
		[Test]
        [ExpectedException(ExpectedException = typeof(ArgumentOutOfRangeException))]
		public void SubString_StringBuilderEmpty()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(string.Empty);
			builder.SubString(0);
		}

		/// <summary>
		/// Test for when the starting index is zero
		/// </summary>
		[Test]
		public void SubString_StartIndexIsZero()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("abcdefg");
			string subStr = builder.SubString(0);
			Assert.IsTrue(subStr == "abcdefg");
		}

		/// <summary>
		/// Test for when the starting index is less than zero
		/// </summary>
		[Test]
        [ExpectedException(ExpectedException = typeof(ArgumentOutOfRangeException))]
		public void SubString_StartIndexIsLessThanZero()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("abcdefg");
			builder.SubString(Int32.MinValue);
		}

		/// <summary>
		/// Test for when the specified starting index and length would cause the subtring
		/// to run past the end of the string builder.
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(IndexOutOfRangeException))]
		public void SubString_LengthTooLong()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("abcdefg");
			builder.SubString(0, 10);
		}

		/// <summary>
		/// Test for when a length is specified that is less than zero
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void SubString_LengthIsLessThanZero()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("abcdefg");
			builder.SubString(3, -1);
		}

		/// <summary>
		/// Test for then when the specified length is zero
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void SubString_LengthIsZero()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("abcdefg");
			builder.SubString(3, 0);
		}

		/// <summary>
		/// Test to obtain a subtring on a stringbuilder that contains non ascii characters
		/// </summary>
		[Test]
		public void SubString_ContainsNonAsciiCharacters()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("\u3041\u3042\u3043\u3044\u3045");
			string subStr = builder.SubString(2,2);
			Assert.IsTrue(subStr == "\u3043\u3044");
		}

		/// <summary>
		/// Test to attempt to get a substring from a specified starting length to the
		/// end of the string builder
		/// </summary>
		[Test]
		public void SubString_GetSubStringToEnd()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("abcdefg");
			string subStr = builder.SubString(2);
			Assert.IsTrue(subStr == "cdefg");
		}

		/// <summary>
		/// Test to attempt to get a substring using a specified starting index and length
		/// </summary>
		[Test]
		public void SubString_GetSubStringByLength()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("abcdefg");
			string subStr = builder.SubString(2, 2);
			Assert.IsTrue(subStr == "cd");
		}

		#endregion
	}
}
