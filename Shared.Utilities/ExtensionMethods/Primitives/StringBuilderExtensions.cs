using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Shared.Utilities.ExtensionMethods.Logging;

namespace Shared.Utilities.ExtensionMethods.Primitives
{
	/// <summary>
	/// A set of methods to provide useful functionality for the stringbuilder class
	/// </summary>
	public static class StringBuilderExtensions
	{
		#region Logging

		/// <summary>
		/// Used to log information about the class
		/// </summary>
		private static ILog _logger = LogManager.GetLogger(typeof(StringBuilderExtensions));

		#endregion

		#region Private constants

		private const char LINE_FEED = '\n';
		private const char CARRIAGE_RETURN = '\r';
		private const char TAB = '\t';
		private const char SPACE = ' ';

		#endregion

		#region Public extension methods

		/// <summary>
		/// Removes the last occurrence of the supplied character from the string builder. This will
		/// ignore any whitespace at the end of the string builder
		/// </summary>
		/// <param name="trailingCharacter">The character to remove.</param>
		public static StringBuilder RemoveTrailingCharacter(this StringBuilder builder, char trailingCharacter)
		{
			_logger.DebugMethodCalled(builder, trailingCharacter);

			return RemoveTrailingCharacter(builder, trailingCharacter, true);
		}

		/// <summary>
		/// Removes the last occurrence of the supplied character from the string builder. 
		/// </summary>
		/// <param name="trailingCharacter">
		/// The character to remove.
		/// </param>
		/// <param name="ignoreWhitespace">
		/// Whether or not to ignore any trailing whitespace after the final character.
		/// If set to false then the last character is used to test against regardless of whether 
		/// it is a whitespace character or not.
		/// </param>
		public static StringBuilder RemoveTrailingCharacter(this StringBuilder builder, char trailingCharacter, bool ignoreWhitespace)
		{
			_logger.DebugMethodCalled(builder, trailingCharacter, ignoreWhitespace);

			if (builder == null ||
				builder.Length == 0)
			{
				return builder;
			}

			int whiteSpaceToRemove = 0;

			if (ignoreWhitespace)
			{
				for (int i = builder.Length - 1; i >= 0; i--)
				{
					char currentChar = builder[i];
					if (currentChar == LINE_FEED ||
						currentChar == CARRIAGE_RETURN ||
						currentChar == TAB ||
						currentChar == SPACE)
					{
						whiteSpaceToRemove++;
					}
					else
					{
						break;
					}
				}
			}

			if (builder[builder.Length - 1 - whiteSpaceToRemove] == trailingCharacter)
			{
				if (ignoreWhitespace)
				{
					builder.Remove(builder.Length - whiteSpaceToRemove, whiteSpaceToRemove);
				}

				builder.Remove(builder.Length - 1, 1);
			}

			return builder;
		}

		/// <summary>
		///  Returns a flag indicating whether a given substring exists in the StringBuilder.
		/// </summary>
		/// <param name="str">The StringBuilder to inspect.</param>
		/// <param name="substring">The substring to look for.</param>
		/// <returns>True if the StringBuilder contains the string; false otherwise.</returns>
		public static bool Contains(this StringBuilder str, string substring)
		{
			_logger.DebugMethodCalled(str, substring);

			#region Input Validation

			Insist.IsNotNullOrEmpty(substring, "substring");

			#endregion

			bool result = false;

			if (str != null &&
				str.Length >= substring.Length)
			{

				char[] substringChars = substring.ToCharArray();
				int endIndex = (str.Length - substring.Length);

				for (int i = 0; i <= endIndex; i++)
				{

					char[] tempChars = GetChars(str, i, i + (substring.Length - 1));

					if (Match(tempChars, substringChars))
					{
						result = true;
						break;
					}
				}
			}

			return result;
		}

		/// <summary>
		///  Returns a flag indicating whether the StringBuilder starts with
		///  the string given.
		/// </summary>
		/// <param name="str">The StringBuilder to inspect.</param>
		/// <param name="start">The string to look for.</param>
		/// <param name="comparison">The string comparison to be used.</param>
		/// <returns>True if the StringBuilder starts with the given string; false otherwise.</returns>
		public static bool StartsWith(this StringBuilder str, string start, StringComparison comparison)
		{
			_logger.DebugMethodCalled(str, start, comparison);

			Insist.IsNotNullOrEmpty(start, "start");

			bool result = false;

			if (str != null &&
				str.Length >= start.Length)
			{
				result = str.SubString(0, start.Length).Equals(start, comparison);
			}

			return result;
		}

		/// <summary>
		///  Returns a flag indicating whether the StringBuilder starts with
		///  the string given.
		/// </summary>
		/// <param name="str">The StringBuilder to inspect.</param>
		/// <param name="start">The string to look for.</param>
		/// <returns>True if the StringBuilder starts with the given string; false otherwise.</returns>
		public static bool StartsWith(this StringBuilder str, string start)
		{
			_logger.DebugMethodCalled(str, start);

			return StartsWith(str, start, StringComparison.Ordinal);
		}

		/// <summary>
		///  Returns a flag indicating whehter the StringBuilder ends with the given string.
		/// </summary>
		/// <param name="str">The StringBuilder to inspect.</param>
		/// <param name="end">The string to look for.</param>
		/// <param name="comparison">The string comparison to be used.</param>
		/// <returns>True if the StringBuilder ends with the given string; false otherwise.</returns>
		public static bool EndsWith(this StringBuilder str, string end, StringComparison comparison)
		{
			_logger.DebugMethodCalled(str, end, comparison);

			Insist.IsNotNullOrEmpty(end, "end");

			bool result = false;

			if (str != null &&
				str.Length >= end.Length)
			{
				result = str.SubString(str.Length - end.Length).Equals(end, comparison);
			}

			return result;
		}

		/// <summary>
		///  Returns a flag indicating whehter the StringBuilder ends with the given string.
		/// </summary>
		/// <param name="str">The StringBuilder to inspect.</param>
		/// <param name="end">The string to look for.</param>
		/// <returns>True if the StringBuilder ends with the given string; false otherwise.</returns>
		public static bool EndsWith(this StringBuilder str, string end)
		{
			_logger.DebugMethodCalled(str, end);

			return EndsWith(str, end, StringComparison.Ordinal);
		}

		/// <summary>
		///  Returns the substring of the StringBuilder.
		/// </summary>
		/// <param name="str">The StringBuilder to get the substring of.</param>
		/// <param name="startIndex">The starting index of the substring.</param>
		/// <param name="length">The (optional) length of the sub string.</param>
		/// <returns>The characters making up the given sub string.</returns>
		public static string SubString(this StringBuilder str, int startIndex, int? length)
		{
			_logger.DebugMethodCalled(str, startIndex, length);

			#region Input Validation

			Insist.IsNotNull(str, "str");
			Insist.IsAtLeast(str.Length, 1, "str", "The substring operation is not valid on a zero length string.");
			Insist.IsAtLeast(startIndex, 0, "startIndex");

			//Calculate the end index
			int endIndex;

			if (length.HasValue)
			{
				endIndex = startIndex + length.Value - 1;
			}
			else
			{
				endIndex = (str.Length - 1);
			}

			if (length.HasValue && endIndex > (str.Length - 1))
			{
				throw new IndexOutOfRangeException("The end index must be a value less than or equal to the final index of the string builder.");
			}

			if (length.HasValue && endIndex < startIndex)
			{
				throw new ArgumentException("The end index must be greater than or equal to the start index.", "length");
			}

			#endregion

			//Get the sub string
			char[] chars = new char[(endIndex - startIndex) + 1];

			for (int index = startIndex; index <= endIndex; index++)
			{
				chars[index - startIndex] = str[index];
			}

			return new String(chars);
		}

		/// <summary>
		///  Returns the substring of the StringBuilder.
		/// </summary>
		/// <param name="str">The StringBuilder to get the substring of.</param>
		/// <param name="startIndex">The starting index of the substring.</param>
		/// <returns>The characters making up the given sub string.</returns>
		public static string SubString(this StringBuilder str, int startIndex)
		{
			_logger.DebugMethodCalled(str, startIndex);

			return SubString(str, startIndex, null);
		}

		#endregion

		#region Private methods

		private static char[] GetChars(StringBuilder str, int startIndex, int endIndex)
		{
			_logger.DebugMethodCalled(str, startIndex, endIndex);

			List<char> chars = new List<char>();

			for (int i = startIndex; i <= endIndex; i++)
			{
				chars.Add(str[i]);
			}

			return chars.ToArray();
		}

		private static bool Match(char[] a, char[] b)
		{
			_logger.DebugMethodCalled(a, b);

			bool result = true;

			if (a == null && b != null)
			{
				result = false;
			}
			else if (a != null && b == null)
			{
				result = false;
			}
			else if (a.Length != b.Length)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < a.Length; i++)
				{
					if (a[i] != b[i])
					{
						result = false;
						break;
					}
				}
			}

			return result;
		}

		#endregion
	}
}

