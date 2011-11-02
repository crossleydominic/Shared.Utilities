using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Shared.Utilities.ExtensionMethods.Logging;
using System.Security.Cryptography;

namespace Shared.Utilities.ExtensionMethods.Primitives
{
	public static class StringExtensions
	{
		#region Logging

		/// <summary>
		/// Used to log information about the class
		/// </summary>
		private static ILog _logger = LogManager.GetLogger(typeof(StringExtensions));

		#endregion

		/// <summary>
		/// Checks this string to see if str is contained within it.
		/// Uses the StringComparison.OrdinalIgnoreCase comparison type
		/// </summary>
		/// <returns>
		/// True if toFind exists with this string (case insensitive search), otherwise false
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if str is null
		/// </exception>
		public static bool ContainsIgnoreCase(this string str, string toFind)
		{
			if (str == null && toFind == null)
			{
				return true;
			}

			if (str == null && toFind != null)
			{
				return false;
			}

			if (str != null && toFind == null)
			{
				return false;
			}

			return (str.IndexOf(toFind, StringComparison.OrdinalIgnoreCase) >= 0); 
		}

		/// <summary>
		/// Checks to see if the supplied string is only made up of the supplied
		/// character
		/// </summary>
		/// <param name="ch">
		/// The character to check inside the string
		/// </param>
		/// <returns>
		/// True if every character in the string is an instance of ch
		/// </returns>
		public static bool OnlyConsistsOf(this string str, char ch)
		{
			if (string.IsNullOrEmpty(str))
			{
				return false;
			}
			
			foreach (char c in str)
			{
				if (c != ch)
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Removes all occurrences of the supplied charachter from the start of the string.
		/// </summary>
		/// <param name="ch">
		/// The character to remove from the start of the string
		/// </param>
		/// <returns>
		/// The original string with all occurrences of the ch character removed 
		/// from the start of it.
		/// </returns>
		public static string RemoveLeadingCharacters(this string str, char ch)
		{
			if (string.IsNullOrEmpty(str))
			{
				return str;
			}

			int counter = 0;
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] != ch)
				{
					break;
				}
				counter++;
			}
			
			return str.Substring(counter);
		}

		/// <summary>
		/// Compares two strings in a case-insensitive manner using the 
		/// StringComparison.OrdinalIgnoreCase comparison type.
		/// </summary>
		/// <returns>
		/// True if the strings are equal, otherwise false.
		/// Will return true if both strings are null.
		/// </returns>
		public static bool EqualsIgnoreCase(this string str1, string str2)
		{
			if (str1 == null)
			{
				if (str2 == null)
				{
					return true;
				}
				else
				{
					return false;
				}
			}

			if (object.ReferenceEquals(str1, str2))
			{
				return true;
			}

			return str1.Equals(str2, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Computes the Levenshtein distance of this string compared with the supplied string.
		/// This algorithm is CASE SENSITIVE
		/// </summary>
		/// <returns>The Levenshtein distance between the two strings.</returns>
		public static int Distance(this string str1, string str2)
		{
			_logger.DebugMethodCalled(str1, str2);

			if (str1 == str2)
				//Strings are the same, therefore the distance between them is 0;
			{
				return 0;
			}

			if (string.IsNullOrEmpty(str1))
				//s1 is empty but s2 may not be, therefore the distance between
				//the strings is the number of insertions required to transform
				//s1 into s2 i.e, we need to do as many insertions to s1 as there are 
				//characters in s2. 
			{
				return str2.Length;
			}

			if (string.IsNullOrEmpty(str2))
			{
				return str1.Length;
			}

			// cache string length for speed
			int str1Length = str1.Length;
			int str2Length = str2.Length;

			int[] previous = new int[str1Length + 1];
			int[] current = new int[str1Length + 1];

			// Add indexing for insertion to first row
			for (int i = 0; i <= str1Length; previous[i] = i++)
			{
			}

			for (int outer = 0; outer < str2Length; outer++)
			{

				// Add indexing for deletion
				current[0] = outer + 1;

				// Levenshtein algorithm 
				for (int inner = 0; inner < str1Length; inner++)
				{
					if (str1[inner] == str2[outer])
						//IF YOU WANT TO MAKE THIS ALGORITHM CASE *INSENSITIVE* THEN 
						//THE IF STATEMENT JUST ABOVE IS THE LINE YOU NEED TO MODIFY.
						//CHARACTERS WOULD NEED TO BE COMPARED IN A CASE INSENSITIVE MANNER.
					{
						current[inner + 1] = previous[inner];
					}
					else
					{
						current[inner + 1] =
							Math.Min(
								Math.Min(
									previous[inner + 1] + 1,    // deletion
									current[inner] + 1          // insertion
								)
							, previous[inner] + 1);             // substitution
					}
				}


				{   // swap current and previous
					int[] tmp = current;
					current = previous;
					previous = tmp;
				}
			}

			return previous[str1Length];
		}

		/// <summary>
        ///  Pads the given string with the character specified to the given length. 
        /// </summary>
        /// <param name="str">The string to be padded.</param>
        /// <param name="padCharacter">The character to be used as padding.</param>
        /// <param name="padToLength">The length of the new, padded string.</param>
        /// <param name="mode">The side the padding should be applied to.</param>
        /// <returns>The padded string.</returns>
        public static string Pad(this string str, char padCharacter, int padToLength, PaddingMode mode) 
		{
			_logger.DebugMethodCalled(str, padCharacter, padToLength, mode);
    
            #region Input Validation

			Insist.IsAtLeast(padToLength, 1, "padToLength");

            #endregion

			bool createPadding = false;

			if (str != null)
			{
				if (str.Length > padToLength )
					//No need to do any padding if the string is already long enough
				{
					createPadding = false;
				}
				else
					//String is too short so padding required.
				{
					createPadding = true;
				}
			}
			else
				//The string is null so we'll set the output string to just the
				//padded string
			{
				createPadding = true;
			}

            string paddedStr;

			if (createPadding)
			{
				int requiredPadding = padToLength - (str == null ? 0 : str.Length);
				string padding = new string(padCharacter, requiredPadding);

				switch (mode)
				{
					case PaddingMode.Left:
						paddedStr = padding + (str == null ? string.Empty : str);
						break;

					case PaddingMode.Right:
						paddedStr = (str == null ? string.Empty : str) + padding;
						break;

					default:
						throw new NotSupportedException(String.Format("The padding mode {0} is not supported.", mode));
				}
			}
			else
			{
				paddedStr = str;
			}

            return paddedStr;
        }

		/// <summary>
		/// Hashes the string using UTF8 encoding and the SHA256 hashing algorithm.
		/// </summary>
		/// <param name="str">
		/// The string to hash
		/// </param>
		/// <returns>
		/// A string representing the hash of the original input string.
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Thrown if str is null or empty
		/// </exception>
		public static string Hash(this string str)
		{
			using (SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider())
			{
				return Hash(str, sha256);
			}
		}

		/// <summary>
		/// Hashes a string using the UTF8 encoding and the supplied hashing algorithm
		/// </summary>
		/// <param name="str">
		/// The string to compute the hash for
		/// </param>
		/// <param name="hashAlgorithm">
		/// The hashing algorithm to use to perform the hash.
		/// </param>
		/// <returns>
		/// A string representing the hash of the original input string.
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Thrown if str is null or empty.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// Thrown if hashAlgorithm is null.
		/// </exception>
		public static string Hash(this string str, HashAlgorithm hashAlgorithm)
		{
			return Hash(str, hashAlgorithm, Encoding.UTF8);
		}

		/// <summary>
		/// Hashes a string using the supplied encoding and the supplied hashing algorithm
		/// </summary>
		/// <param name="str">
		/// The string to compute the hash for
		/// </param>
		/// <param name="hashAlgorithm">
		/// The hashing algorithm to use to perform the hash.
		/// </param>
		/// <param name="encoding">
		/// The encoding used to encode the string into a byte[]
		/// </param>
		/// <returns>
		/// A string representing the hash of the original input string.
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Thrown if str is null or empty.
		/// </exception>
		/// <exception cref="ArgumentNullException">
		/// Thrown if hashAlgorithm is null or encoding is null.
		/// </exception>
		public static string Hash(this string str, HashAlgorithm hashAlgorithm, Encoding encoding)
		{
			_logger.DebugMethodCalled(str, hashAlgorithm, encoding);

			Insist.IsNotNullOrEmpty(str, "str");
			Insist.IsNotNull(hashAlgorithm, "hashAlgorithm");
			Insist.IsNotNull(encoding, "encoding");

			byte[] strBytes = encoding.GetBytes(str);
			byte[] hashed = hashAlgorithm.ComputeHash(strBytes, 0, strBytes.Length);
			return BitConverter.ToString(hashed).Replace("-", "");
		}

		/// <summary>
		/// Wraps a string onto multiple lines using the supplied argument as the line length.
		/// Respects any words so that a line is wrapped on any preceding whitespace for a word
		/// that would get split over multiple lines.
		/// </summary>
		/// <param name="str">
		/// The string to split
		/// </param>
		/// <param name="lineLength">
		/// The maximum length each line should be after wrapping
		/// </param>
		/// <returns>
		/// A string that has been wrapped to the supplied line length
		/// </returns>
		/// <exception cref="System.ArgumentException">
		/// Thrown if lineLength is less than or equal to 0
		/// </exception>
		public static string Wrap(this string str, int lineLength)
		{
			return Wrap(str, lineLength, true);
		}

		/// <summary>
		/// Wraps a string onto multiple lines using the supplied argument as the line length.
		/// Optionally respects any words so that a line is wrapped on any preceding whitespace for a word
		/// that would get split over multiple lines.
		/// </summary>
		/// <param name="str">
		/// The string to split
		/// </param>
		/// <param name="respectWords">
		/// Whether or not to respect words during wrapping. If true then a word will only be split
		/// if there is no preceding whitespace. If false then words will be split in half if they
		/// span a line boundary.
		/// </param>
		/// <param name="lineLength">
		/// The maximum length each line should be after wrapping
		/// </param>
		/// <returns>
		/// A string that has been wrapped to the supplied line length
		/// </returns>
		/// <exception cref="System.ArgumentException">
		/// Thrown if lineLength is less than or equal to 0
		/// </exception>
		public static string Wrap(this string str, int lineLength, bool respectWords)
		{
			_logger.DebugMethodCalled(str, lineLength, respectWords);

			#region Input validation

			Insist.IsAtLeast(lineLength, 1, "lineLength");

			#endregion

			#region Shortcuts

			if (string.IsNullOrEmpty(str))
			{
				return str;
			}

			if (str.Length <= lineLength)
			{
				return str;
			}

			#endregion

			StringBuilder builder = new StringBuilder();
			int startingIndex = 0;
			int charsLeft = str.Length;

			while (charsLeft > 0)
			{
				//Watch for the end of the string
				if (charsLeft <= lineLength)
				{
					builder.Append(str.Substring(startingIndex));
					charsLeft = 0;
				}
				else
				{
					int adjustedWrapLength = lineLength;
					if (respectWords)
					{
						while (adjustedWrapLength > 0)
						{
							if (str[startingIndex + adjustedWrapLength] == '\t' ||
								str[startingIndex + adjustedWrapLength] == ' ')
							{
								//Readjust index so that when we do a substring
								//further down we'll also grab the whitespace 
								//character.
								adjustedWrapLength++;
								break;
							}
							adjustedWrapLength--;
						}

						if (adjustedWrapLength == 0)
						//Didnt find any whitespace, just split the
						//line at the wrapLength position
						{
							adjustedWrapLength = lineLength;
						}
					}

					builder.AppendLine(str.Substring(startingIndex, adjustedWrapLength));
					startingIndex += adjustedWrapLength;
					charsLeft = str.Length - startingIndex;
				}
			}

			return builder.ToString();
		}

		/// <summary>
		/// Formats the supplied string using the supplied arguments
		/// </summary>
		/// <param name="str">
		/// The string pattern to format
		/// </param>
		/// <param name="args">
		/// A list of arguments used in the pattern
		/// </param>
		/// <returns>
		/// The formatted string
		/// </returns>
		/// <exception cref="System.ArgumentException">
		/// Thrown when str is null or empty.
		/// </exception>
		public static string Format(this string str, params object[] args)
		{
			Insist.IsNotNullOrEmpty(str, "str");

			return string.Format(str, args);
		}
    }

    /// <summary>
    ///  Enumerates possible sides for padding to be applied.
    /// </summary>
    public enum PaddingMode 
	{

        /// <summary>
        ///  Padding is applied to the left side of the target string.
        /// </summary>
        Left,

        /// <summary>
        ///  Padding is applied to the right side of the target string.
        /// </summary>
        Right

    }
}
