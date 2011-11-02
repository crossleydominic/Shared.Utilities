using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.RegularExpressions;

namespace Shared.Utilities.Tests.RegexHelperTests
{
	/// <summary>
	/// A set of tests for the RegexHelper class
	/// </summary>
	[TestFixture]
	public class RegexHelperTests
	{
		#region IsInteger Tests

		/// <summary>
		/// Tests when the match string is null
		/// </summary>
		[Test]
		public void IsInteger_NullOrEmptyString()
		{
			string str = null;
			bool isInteger = RegexHelper.IsInteger(str);
			Assert.IsFalse(isInteger);

			str = string.Empty;
			isInteger = RegexHelper.IsInteger(str);
			Assert.IsFalse(isInteger);
		}

		/// <summary>
		/// Test when we're matching on positive numbers but supply a negative number
		/// </summary>
		[Test]
		public void IsInteger_NegativeNumberWhenRestrictingToPositive()
		{
			string str = "-1";
			bool isInteger = RegexHelper.IsInteger(str);
			Assert.IsFalse(isInteger);
		}

		/// <summary>
		/// Test when we're matching on a negative number
		/// </summary>
		[Test]
		public void IsInteger_NegativeNumber()
		{
			string str = "-1";
			bool isInteger = RegexHelper.IsInteger(str, false);
			Assert.IsTrue(isInteger);
		}

		/// <summary>
		/// Test when we're matching on a positive number
		/// </summary>
		[Test]
		public void IsInteger_PositiveNumber()
		{
			string str = "1";
			bool isInteger = RegexHelper.IsInteger(str);
			Assert.IsTrue(isInteger);
		}

		/// <summary>
		/// Test when we're matching on very large numbers (larger than Long.MaxValue)
		/// </summary>
		[Test]
		public void IsInteger_LargeNumber()
		{
			//Positive
			string str = "690234790346902737234672346869072346723468234672342342376723461";
			bool isInteger = RegexHelper.IsInteger(str);
			Assert.IsTrue(isInteger);

			//Negative
			str = "-690234790346902737234672346869072346723468234672342342376723461";
			isInteger = RegexHelper.IsInteger(str, false);
			Assert.IsTrue(isInteger);
		}

		/// <summary>
		/// Test when matching on numbers with characters in them
		/// </summary>
		[Test]
		public void IsInteger_NumberWithCharacters()
		{
			//Positive
			string str = "123abc";
			bool isInteger = RegexHelper.IsInteger(str);
			Assert.IsFalse(isInteger);

			//Negative
			str = "-123abc";
			isInteger = RegexHelper.IsInteger(str, false);
			Assert.IsFalse(isInteger);
		}

		/// <summary>
		/// Test when we're matching on numbers that have whitespace in them somewhere
		/// </summary>
		[Test]
		public void IsInteger_NumberWithWhiteSpace()
		{
			//Positive, embedded
			string str = "123 456";
			bool isInteger = RegexHelper.IsInteger(str);
			Assert.IsFalse(isInteger);

			//Negative, embedded
			str = "-123 456";
			isInteger = RegexHelper.IsInteger(str, false);
			Assert.IsFalse(isInteger);
	
			//Positive, trailing
			str = "123456 ";
			isInteger = RegexHelper.IsInteger(str);
			Assert.IsFalse(isInteger);

			//Negative, trailing
			str = "-123456 ";
			isInteger = RegexHelper.IsInteger(str, false);
			Assert.IsFalse(isInteger);
	
			//Positive, leading
			str = " 123456";
			isInteger = RegexHelper.IsInteger(str);
			Assert.IsFalse(isInteger);

			//Negative, leading
			str = " -123456";
			isInteger = RegexHelper.IsInteger(str, false);
			Assert.IsFalse(isInteger);
		}


		/// <summary>
		/// Test when we're matching on negative numbers that have too many negative signs at the start
		/// </summary>
		[Test]
		public void IsInteger_TooManyNegativeSigns()
		{
			string str = "--123456";
			bool isInteger = RegexHelper.IsInteger(str, false);
			Assert.IsFalse(isInteger);
		}

		/// <summary>
		/// Test when we're matchign on numbers that have a negative sign embedded in the middle of them
		/// </summary>
		[Test]
		public void IsInteger_EmbeddedNegativeSigns()
		{
			string str = "123-456";
			bool isInteger = RegexHelper.IsInteger(str);
			Assert.IsFalse(isInteger);
		}

		#endregion

		#region IsNumeric Tests

		/// <summary>
		/// Tests when the match string is null
		/// </summary>
		[Test]
		public void IsNumeric_NullOrEmptyString()
		{
			string str = null;
			bool isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsFalse(isNumeric);

			str = string.Empty;
			isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsFalse(isNumeric);
		}

		/// <summary>
		/// Test when we're matching on positive numbers but supply a negative number
		/// </summary>
		[Test]
		public void IsNumeric_NegativeNumberWhenRestrictingToPositive()
		{
			string str = "-1";
			bool isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsFalse(isNumeric);

			str = "-1.1";
			isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsFalse(isNumeric);
		}

		/// <summary>
		/// Test when we're matching on a negative number
		/// </summary>
		[Test]
		public void IsNumeric_NegativeNumber()
		{
			string str = "-1";
			bool isNumeric = RegexHelper.IsNumeric(str, false);
			Assert.IsTrue(isNumeric);

			str = "-1.1";
			isNumeric = RegexHelper.IsNumeric(str, false);
			Assert.IsTrue(isNumeric);
		}

		/// <summary>
		/// Test when we're matching on a positive number
		/// </summary>
		[Test]
		public void IsNumeric_PositiveNumber()
		{
			string str = "1";
			bool isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsTrue(isNumeric);

			str = "1.1";
			isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsTrue(isNumeric);
		}

		/// <summary>
		/// Test when we're matching on very large numbers (larger than Long.MaxValue)
		/// </summary>
		[Test]
		public void IsNumeric_LargeNumber()
		{
			//Positive
			string str = "690234790346902737234672346869072346723468234672342342376723461";
			bool isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsTrue(isNumeric);

			//Negative
			str = "-690234790346902737234672346869072346723468234672342342376723461";
			isNumeric = RegexHelper.IsNumeric(str, false);
			Assert.IsTrue(isNumeric);

			//Positive
			str = "690234790346902737234672346869072346723468234672342342376723461.905902347723467234607267290367902372390679023";
			isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsTrue(isNumeric);

			//Negative
			str = "-690234790346902737234672346869072346723468234672342342376723461.905902347723467234607267290367902372390679023";
			isNumeric = RegexHelper.IsNumeric(str, false);
			Assert.IsTrue(isNumeric);
		}

		/// <summary>
		/// Test when matching on numbers with characters in them
		/// </summary>
		[Test]
		public void IsNumeric_NumberWithCharacters()
		{
			//Positive
			string str = "123abc";
			bool isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsFalse(isNumeric);

			//Negative
			str = "-123abc";
			isNumeric = RegexHelper.IsNumeric(str, false);
			Assert.IsFalse(isNumeric);

			//Positive
			str = "123.45abc";
			isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsFalse(isNumeric);

			//Negative
			str = "-123.45abc";
			isNumeric = RegexHelper.IsNumeric(str, false);
			Assert.IsFalse(isNumeric);
		}

		/// <summary>
		/// Test when we're matching on numbers that have whitespace in the middle of them
		/// </summary>
		[Test]
		public void IsNumeric_NumberWithWhiteSpace()
		{
			//Positive
			string str = "123 456";
			bool isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsFalse(isNumeric);

			//Negative
			str = "-123 456";
			isNumeric = RegexHelper.IsNumeric(str, false);
			Assert.IsFalse(isNumeric);

			//Positive
			str = "123 456.78";
			isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsFalse(isNumeric);

			//Negative
			str = "-123 456.78";
			isNumeric = RegexHelper.IsNumeric(str, false);
			Assert.IsFalse(isNumeric);

			//Positive
			str = "123456.78 ";
			isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsFalse(isNumeric);

			//Negative
			str = "-123456.78 ";
			isNumeric = RegexHelper.IsNumeric(str, false);
			Assert.IsFalse(isNumeric);

			//Positive
			str = " 123456.78";
			isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsFalse(isNumeric);

			//Negative
			str = " -123456.78";
			isNumeric = RegexHelper.IsNumeric(str, false);
			Assert.IsFalse(isNumeric);
		}

		/// <summary>
		/// Test when we're matching on negative numbers that have too many negative signs at the start
		/// </summary>
		[Test]
		public void IsNumeric_TooManyNegativeSigns()
		{
			string str = "--123456";
			bool isNumeric = RegexHelper.IsNumeric(str, false);
			Assert.IsFalse(isNumeric);

			str = "--12345.786";
			isNumeric = RegexHelper.IsNumeric(str, false);
			Assert.IsFalse(isNumeric);
		}

		/// <summary>
		/// Test when we're matchign on numbers that have a negative sign embedded in the middle of them
		/// </summary>
		[Test]
		public void IsNumeric_EmbeddedNegativeSigns()
		{
			string str = "123-456.78";
			bool isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsFalse(isNumeric);

			str = "123-456.78";
			isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsFalse(isNumeric);
		}

		/// <summary>
		/// Test when we're matchign on numbers that have too many decimal points in them
		/// </summary>
		[Test]
		public void IsNumeric_ContainsTooManyDecimalPoints()
		{
			string str = "123456..78";
			bool isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsFalse(isNumeric);
		}

		/// <summary>
		/// Test when we're matchign on numbers that have decimal points at the beginning or end.
		/// </summary>
		[Test]
		public void IsNumeric_LeadingAndTrailingDecimalPoints()
		{
			string str = "12345678.";
			bool isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsFalse(isNumeric);

			str = ".12345678";
			isNumeric = RegexHelper.IsNumeric(str);
			Assert.IsFalse(isNumeric);
		}

		#endregion

		#region IsEmailAddress Tests

		/// <summary>
		/// Tests when the email address string is empty
		/// </summary>
		[Test]
		public void IsEmail_NullOrEmptyString()
		{
			string str = null;
			bool isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsFalse(isEmail);

			str = string.Empty;
			isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsFalse(isEmail);
		}

		/// <summary>
		/// Tests when there is nothing before the @ symbol
		/// </summary>
		[Test]
		public void IsEmail_MissingLocalPart()
		{
			string str = "@example.com";
			bool isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsFalse(isEmail);
		}

		/// <summary>
		/// Tests when there is no domain name after the @
		/// </summary>
		[Test]
		public void IsEmail_MissingDomainName()
		{
			string str = "example@";
			bool isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsFalse(isEmail);
		}

		/// <summary>
		/// Tests when the domain name has no TLD specified
		/// </summary>
		[Test]
		public void IsEmail_MalformedDomainName()
		{
			string str = "example@example";
			bool isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsFalse(isEmail);
		}

		/// <summary>
		/// Tests when the TLD is multi part e.g .co.uk
		/// </summary>
		[Test]
		public void IsEmail_MultiPartDomainName()
		{
			string str = "example@example.co.uk";
			bool isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsTrue(isEmail);
		}

		/// <summary>
		/// Tests when the local part is not split
		/// </summary>
		[Test]
		public void IsEmail_SinglePartLocalPart()
		{
			string str = "example@example.com";
			bool isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsTrue(isEmail);
		}

		/// <summary>
		/// Test when the local part is split with .s
		/// </summary>
		[Test]
		public void IsEmail_MultiPartLocalPart()
		{
			string str = "ex.ample@example.com";
			bool isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsTrue(isEmail);
		}

		/// <summary>
		/// Test when the email address contains underscores
		/// </summary>
		[Test]
		public void IsEmail_ContainsUnderscores()
		{
			string str = "ex_ample@example.com";
			bool isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsTrue(isEmail);

			str = "example@exa_mple.com";
			isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsFalse(isEmail);
		}

		/// <summary>
		/// Test when the email address contains hyphens
		/// </summary>
		[Test]
		public void IsEmail_ContainsHyphens()
		{
			string str = "ex-ample@example.com";
			bool isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsTrue(isEmail);

			str = "example@exa-mple.com";
			isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsTrue(isEmail);
		}

		/// <summary>
		/// Test when the email address contains whitespace
		/// </summary>
		[Test]
		public void IsEmail_ContainsWhiteSpace()
		{
			//Leading
			string str = " ex.ample@example.com";
			bool isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsFalse(isEmail);

			//Trailing
			str = "ex.ample@example.com ";
			isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsFalse(isEmail);

			//Embedded
			str = " ex.ample@ex ample.com";
			isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsFalse(isEmail);
		}

		/// <summary>
		/// Test when the email address does not have an @ symbol
		/// </summary>
		[Test]
		public void IsEmail_MissingAt()
		{
			string str = "ex.ampleexample.com";
			bool isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsFalse(isEmail);
		}

		/// <summary>
		/// Test when the email address contains consecutive dots
		/// </summary>
		[Test]
		public void IsEmail_ContainsConsecutiveDots()
		{
			string str = "ex..ample@example.com";
			bool isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsFalse(isEmail);

			str = "ex.ample@example..com";
			isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsFalse(isEmail);
		}

		/// <summary>
		/// Tests when the email address has leading or trailing dots.
		/// </summary>
		[Test]
		public void IsEmail_ContainsLeadingAndTrailingDots()
		{
			string str = ".ex.ample@example.com";
			bool isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsFalse(isEmail);

			str = "ex.ample@example.com.";
			isEmail = RegexHelper.IsEmailAddress(str);
			Assert.IsFalse(isEmail);
		}
		
		/// <summary>
		/// Test a set of email addresses when they contain ascii characters that are not
		/// a-z, A-Z, 0-9, _, -
		/// </summary>
		[Test]
		public void IsEmail_ContainsSymbols()
		{
			//Generate a set of email addresses with symbols in them
			string baseAddress = "ex{0}ample@example.com";
			List<string> emailAddresses = new List<string>();

			int printableAsciiCharacterUpperLimit = 127;

			for (int i = 0; i <= printableAsciiCharacterUpperLimit; i++)
			{
				//We're relying on the fact that runs of ascii characters appear together
				//in the ascii table.
				if ((i < ((int)'0') || i > ((int)'9')) &&
					(i < ((int)'A') || i > ((int)'Z')) &&
					(i < ((int)'a') || i > ((int)'z')) &&
					(i != ((int)'@')) &&
					(i != ((int)'.')) &&
					(i != ((int)'_')) &&
					(i != ((int)'-')))
				{
					emailAddresses.Add(string.Format(baseAddress, (char)i));
				}
			}

			foreach (string address in emailAddresses)
			{
				Assert.IsFalse(RegexHelper.IsEmailAddress(address));
			}
		}

		#endregion
	}
}
