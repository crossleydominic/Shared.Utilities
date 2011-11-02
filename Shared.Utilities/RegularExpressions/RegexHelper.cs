using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using log4net;
using Shared.Utilities.ExtensionMethods.Logging;

namespace Shared.Utilities.RegularExpressions
{
	/// <summary>
	///  Class containing static helper methods for commonly used
	///  regular expressions.
	/// </summary>
	public static class RegexHelper
	{
		#region Logging

		private static ILog _logger = LogManager.GetLogger(typeof(RegexHelper));

		#endregion

		#region IsInteger
		/// <summary>
		///  The regular expression used to match integers (positive or negative).
		/// </summary>
		public const string PATTERN_IS_INTEGER = "^-??([0-9]+)$";

		/// <summary>
		///  The regular expression used to match integers (positive only).
		/// </summary>
		public const string PATTERN_IS_POSITIVE_INTEGER = "^([0-9]+)$";

		/// <summary>
		///  Returns a flag specifying whether the given string is an integer (positive integers only).
		/// </summary>
		/// <param name="input">The string to test.</param>
		/// <returns>True if the string is a positive integer, false otherwise.</returns>
		public static bool IsInteger(string input)
		{
			_logger.DebugMethodCalled(input);

			return IsInteger(input, true);
		}

		/// <summary>
		///  Returns a flag specifying whether the given string is an integer.
		/// </summary>
		/// <param name="input">The string to test.</param>
		/// <param name="positiveIntegersOnly">Match positive integers only.</param>
		/// <returns>True if the string is a positive or negative integer, false otherwise.</returns>
		public static bool IsInteger(string input, bool positiveIntegersOnly)
		{
			_logger.DebugMethodCalled(input, positiveIntegersOnly);

			string patternToUse;
			if (positiveIntegersOnly)
			{
				patternToUse = PATTERN_IS_POSITIVE_INTEGER;
			}
			else
			{
				patternToUse = PATTERN_IS_INTEGER;
			}

			return Regex.IsMatch(String.IsNullOrEmpty(input) ? String.Empty : input, patternToUse);
		}

		#endregion

		#region IsNumeric

		/// <summary>
		///  The regular expression used to match numerics (postive and negative).
		/// </summary>
		public const string PATTERN_IS_NUMERIC = "^-??([0-9]+)(\\.([0-9]+))?$";

		/// <summary>
		/// The regulare expression used to match numerics (positive only) 
		/// </summary>
		public const string PATTERN_IS_POSITIVE_NUMERIC = "^([0-9]+)(\\.([0-9]+))?$";

		/// <summary>
		///  Returns a flag specifying whether the given string is a positive numeric value.
		/// </summary>
		/// <param name="input">The string to test.</param>
		/// <returns>True if the string is a positive numeric value, false otherwise.</returns>
		public static bool IsNumeric(string input)
		{
			_logger.DebugMethodCalled(input);

			return IsNumeric(input, true);
		}

		/// <summary>
		///  Returns a flag specifying whether the given string is a positive or negative numeric value.
		/// </summary>
		/// <param name="input">The string to test.</param>
		/// <returns>True if the string is a positive or negative numeric value, false otherwise.</returns>
		public static bool IsNumeric(string input, bool positiveNumericsOnly)
		{
			_logger.DebugMethodCalled(input, positiveNumericsOnly);

			string patternToUse;

			if (positiveNumericsOnly)
			{
				patternToUse = PATTERN_IS_POSITIVE_NUMERIC;
			}
			else
			{
				patternToUse = PATTERN_IS_NUMERIC;
			}

			return Regex.IsMatch(String.IsNullOrEmpty(input) ? String.Empty : input, patternToUse);
		}

		#endregion

		#region IsEmailAddress

		/// <summary>
		/// The regular expression used to match email addresses (THIS COMES STRAIGHT OUT OF THE SMC
		/// FOR ALL THE EXISTING EMAIL_ADDRESS COLUMNS).
		/// </summary>
		public const string PATTERN_IS_EMAIL = @"^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$";

		/// <summary>
		/// Returns whether or not the given string is an email address
		/// </summary>
		public static bool IsEmailAddress(string input)
		{
			_logger.DebugMethodCalled(input);

			return Regex.IsMatch(string.IsNullOrEmpty(input) ? string.Empty : input, PATTERN_IS_EMAIL);
		}

		#endregion

	}
}
