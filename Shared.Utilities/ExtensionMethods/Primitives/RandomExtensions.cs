using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.ExtensionMethods.Primitives
{
	public static class RandomExtensions
	{
		#region Private constants

		/// <summary>
		/// The starting printable ascii code
		/// </summary>
		private const int STARTING_ASCII_CODE = 33;

		/// <summary>
		/// The ending printable ascii code
		/// </summary>
		private const int ENDING_ASCII_CODE = 125;

		/// <summary>
		/// The starting code for uppercase alphabet ascii characters
		/// </summary>
		private const int UPPERCASE_ALPHABET_STARTING_CODE = 65;

		/// <summary>
		/// The ending code for uppercase alphabet ascii characters
		/// </summary>
		private const int UPPERCASE_ALPHABET_ENDING_CODE = 90;

		/// <summary>
		/// The starting code for lowercase alphabet ascii characters
		/// </summary>
		private const int LOWERCASE_ALPHABET_STARTING_CODE = 97;

		/// <summary>
		/// The ending code for lowercase alphabet ascii characters
		/// </summary>
		private const int LOWERCASE_ALPHABET_ENDING_CODE = 122;

		/// <summary>
		/// The starting code for numeric ascii characters
		/// </summary>
		private const int NUMBER_STARTING_CODE = 48;

		/// <summary>
		/// The ending code for numeric ascii characters
		/// </summary>
		private const int NUMBER_ENDING_CODE = 57;

		#endregion

		#region Public static methods

		/// <summary>
		/// Generates a random boolean value
		/// </summary>
		/// <param name="random">
		/// The random number generator to use
		/// </param>
		/// <returns>
		/// True or false
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown when random is null
		/// </exception>
		public static bool NextBool(this Random random)
		{
			Insist.IsNotNull(random, "random");

			return random.Next().IsEven();
		}

		/// <summary>
		/// Generates a random string that is as long as length. Only contains
		/// upper and lower case a-z characters.
		/// </summary>
		/// <param name="random">
		/// The random number generator to use
		/// </param>
		/// <param name="length">
		/// The length of the string to be generated. Returns an empty string if length
		/// is zero.
		/// </param>
		/// <returns>
		/// A random string of the specified length
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown when random is null
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// Throw when length is less than 0.
		/// </exception>
		public static string NextString(this Random random, int length)
		{
			return NextString(random, length, false, false);
		}

		/// <summary>
		/// Generates a random string that is as long as length
		/// </summary>
		/// <param name="random">
		/// The random number generator to use
		/// </param>
		/// <param name="length">
		/// The length of the string to be generated. Returns an empty string if length
		/// is zero.
		/// </param>
		/// <param name="allowNumbers">
		/// Whether or not to allow numbers in the generated string
		/// </param>
		/// <param name="allowSymbols">
		/// Whether or not to allow symbols in the string
		/// </param>
		/// <returns>
		/// A random string of the specified length
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown when random is null
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// Throw when length is less than 0.
		/// </exception>
		public static string NextString(this Random random, int length, bool allowNumbers, bool allowSymbols)
		{
			#region Input Validation

			Insist.IsNotNull(random, "random");
			Insist.IsAtLeast(length, 0, "length");

			#endregion

			if(length == 0)
			{
				return string.Empty;
			}

			StringBuilder builder = new StringBuilder();
			while (builder.Length < length)
			{
				//+1 because MaxValue is exclusive but minValue is inclusive
				int i = random.Next(STARTING_ASCII_CODE, ENDING_ASCII_CODE + 1);
				bool isNumber = false;
				bool isSymbol = false;
				bool canAdd = true;

				if ((i >= UPPERCASE_ALPHABET_STARTING_CODE && i <= UPPERCASE_ALPHABET_ENDING_CODE) ||
					(i >= LOWERCASE_ALPHABET_STARTING_CODE && i <= LOWERCASE_ALPHABET_ENDING_CODE))
				{
					canAdd = true;
				}
				else if (i >= NUMBER_STARTING_CODE && i <= NUMBER_ENDING_CODE)
				{
					isNumber = true;
				}
				else
				{
					isSymbol = true;
				}

				if (isNumber == true && 
					allowNumbers == false)
				{
					canAdd = false;
				}

				if (isSymbol == true &&
					allowSymbols == false)
				{
					canAdd = false;
				}

				if (canAdd)
				{
					builder.Append((char)i);
				}
			}

			return builder.ToString();
		}

		#endregion
	}
}
