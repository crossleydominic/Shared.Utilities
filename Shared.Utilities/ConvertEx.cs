using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Utilities.ExtensionMethods.Primitives;

namespace Shared.Utilities
{
	/// <summary>
	/// A class that wraps a set of TryParse methods and returns a nullable
	/// version of the value type.
	/// </summary>
	public static class ConvertEx
	{
		#region Public delegates

		/// <summary>
		/// A delegate that can point at a TryParse method
		/// </summary>
		public delegate bool TryParseMethod<T>(string toParse, out T parsedVal) where T : struct;

		/// <summary>
		/// A delegate that can point at an Enums TryParse method that also captures the 
		/// ingoreCase arguments
		/// </summary>
		public delegate bool EnumTryParseIgnoreCaseMethod<T>(string toParse, bool ignoreCase, out T parsedVal) where T : struct;

		#endregion

		#region Private constants

		private const string FALSE_ZERO = "0";
		private const string FALSE_STRING = "false";
		private const string FALSE_N = "n";
		private const string FALSE_NO = "no";

		private const string TRUE_MINUS_ONE = "-1";
		private const string TRUE_ONE = "1";
		private const string TRUE_STRING = "true";
		private const string TRUE_Y = "y";
		private const string TRUE_YES = "yes";

		#endregion

		#region Public static methods

		/// <summary>
		/// Converts the supplied string to the Enum Type specified using the supplied parseMethod,
		/// optionally ignoring case
		/// </summary>
		/// <param name="toParse">
		/// The string to convert into a byte
		/// </param>
		/// <param name="tryParseMethod">
		/// The method that will be used to parse the string value
		/// </param>
		/// <param name="ignoreCase">
		/// Whether or not to ignore the case during parsing.
		/// </param>
		/// <typeparam name="T">
		/// The type that the string will be parsed into
		/// </typeparam>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the T value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static T? ToAny<T>(string toParse, EnumTryParseIgnoreCaseMethod<T> tryParseMethod, bool ignoreCase) where T : struct
		{
			#region Input validation

			Insist.IsNotNull(tryParseMethod, "tryParseMethod");

			#endregion

			T val = default(T);
			if (tryParseMethod(toParse, ignoreCase, out val))
			{
				return new Nullable<T>(val);
			}
			else
			{
				return new Nullable<T>();
			}
		}

		/// <summary>
		/// Converts the supplied string to the Type specified using the supplied parseMethod
		/// </summary>
		/// <param name="toParse">
		/// The string to convert into a byte
		/// </param>
		/// <param name="tryParseMethod">
		/// The method that will be used to parse the string value
		/// </param>
		/// <typeparam name="T">
		/// The type that the string will be parsed into
		/// </typeparam>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the T value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static T? ToAny<T>(string toParse, TryParseMethod<T> tryParseMethod) where T : struct
		{
			#region Input validation

			Insist.IsNotNull(tryParseMethod, "tryParseMethod");

			#endregion 

			T val = default(T);
			if (tryParseMethod(toParse, out val))
			{
				return new Nullable<T>(val);
			}
			else
			{
				return new Nullable<T>();
			}
		}

		/// <summary>
		/// Converts the supplied string to a byte
		/// </summary>
		/// <param name="s">
		/// The string to convert into a byte
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the byte value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static byte? ToByte(string s)
		{
			return ToAny<byte>(s, byte.TryParse);
		}

		/// <summary>
		/// Converts the supplied string to a char
		/// </summary>
		/// <param name="s">
		/// The string to convert into a char
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the char value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static char? ToChar(string s)
		{
			return ToAny<char>(s, char.TryParse);
		}

		/// <summary>
		/// Converts the supplied string to a SByte
		/// </summary>
		/// <param name="s">
		/// The string to convert into a SByte
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the SByte value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static sbyte? ToSByte(string s)
		{
			return ToAny<sbyte>(s, sbyte.TryParse);
		}

		/// <summary>
		/// Converts the supplied string to a Int32
		/// </summary>
		/// <param name="s">
		/// The string to convert into a Int32
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the Int32 value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static int? ToInt32(string s)
		{
			return ToAny<int>(s, int.TryParse);
		}

		/// <summary>
		/// Converts the supplied string to a UInt32
		/// </summary>
		/// <param name="s">
		/// The string to convert into a UInt32
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the UInt32 value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static uint? ToUInt32(string s)
		{
			return ToAny<uint>(s, uint.TryParse);
		}

		/// <summary>
		/// Converts the supplied string to a Int16
		/// </summary>
		/// <param name="s">
		/// The string to convert into a Int16
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the Int16 value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static short? ToInt16(string s)
		{
			return ToAny<short>(s, short.TryParse);
		}

		/// <summary>
		/// Converts the supplied string to a UInt16
		/// </summary>
		/// <param name="s">
		/// The string to convert into a UInt16
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the UInt16 value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static ushort? ToUInt16(string s)
		{
			return ToAny<ushort>(s, ushort.TryParse);
		}

		/// <summary>
		/// Converts the supplied string to a Int64
		/// </summary>
		/// <param name="s">
		/// The string to convert into a Int64
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the Int64 value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static long? ToInt64(string s)
		{
			return ToAny<long>(s, long.TryParse);
		}

		/// <summary>
		/// Converts the supplied string to a UInt64
		/// </summary>
		/// <param name="s">
		/// The string to convert into a UInt64
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the UInt64 value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static ulong? ToUInt64(string s)
		{
			return ToAny<ulong>(s, ulong.TryParse);
		}

		/// <summary>
		/// Converts the supplied string to a float
		/// </summary>
		/// <param name="s">
		/// The string to convert into a float
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the float value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static float? ToFloat(string s)
		{
			return ToAny<float>(s, Single.TryParse);
		}

		/// <summary>
		/// Converts the supplied string to a double
		/// </summary>
		/// <param name="s">
		/// The string to convert into a double
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the double value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static double? ToDouble(string s)
		{
			return ToAny<double>(s, double.TryParse);
		}

		/// <summary>
		/// Converts the supplied string to a decimal
		/// </summary>
		/// <param name="s">
		/// The string to convert into a decimal
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the decimal value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static decimal? ToDecimal(string s)
		{
			return ToAny<decimal>(s, decimal.TryParse);
		}

		/// <summary>
		/// Converts the supplied string to a DateTime
		/// </summary>
		/// <param name="s">
		/// The string to convert into a DateTime
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the DateTime value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static DateTime? ToDateTime(string s)
		{
			return ToAny<DateTime>(s, DateTime.TryParse);
		}

		/// <summary>
		/// Converts the supplied string to a bool. 
		/// This function will evaulate the following strings
		/// to False (case insensitive)
		///		"0"
		///		"False"
		///		"N"
		///		"No"
		///		null
		///		string.empty
		///	The following strings will evaluate to True
		///		"-1"
		///		"1"
		///		"y"
		///		"yes"
		///		"true"
		///	Any other value will return a null value.
		/// </summary>
		/// <param name="s">
		/// The string to convert into a bool
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the bool value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static bool? ToBool(string s)
		{
			Nullable<bool> val = ToAny<bool>(s, bool.TryParse);

			if (val.HasValue)
			{
				return val;
			}

			if (string.IsNullOrEmpty(s))
			{
				return new Nullable<bool>(false);
			}

			string boolString = s.Trim();
			if (boolString.EqualsIgnoreCase(FALSE_N) ||
				boolString.EqualsIgnoreCase(FALSE_NO) ||
				boolString.EqualsIgnoreCase(FALSE_STRING) ||
				boolString.EqualsIgnoreCase(FALSE_ZERO) )
			{
				return  new Nullable<bool>(false);
			}

			if (boolString.EqualsIgnoreCase(TRUE_MINUS_ONE) ||
				boolString.EqualsIgnoreCase(TRUE_ONE) ||
				boolString.EqualsIgnoreCase(TRUE_STRING) ||
				boolString.EqualsIgnoreCase(TRUE_Y) ||
				boolString.EqualsIgnoreCase(TRUE_YES))
			{
				return  new Nullable<bool>(true);
			}

			return new Nullable<bool>();
		}

		/// <summary>
		/// Converts the supplied string to a bool. This will only convert strings that
		/// are Boolean.TrueString and Boolean.FalseString to a bool. If you want to convert
		/// "1", "-1", or any other truth-y strings then use ToBool
		/// </summary>
		/// <param name="s">
		/// The string to convert into a bool
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the bool value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static bool? ToBoolStrict(string s)
		{
			return ToAny<bool>(s, bool.TryParse);
		}

		/// <summary>
		/// Converts the supplied string to a TimeSpan
		/// </summary>
		/// <param name="s">
		/// The string to convert into a TimeSpan
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the TimeSpan value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static TimeSpan? ToTimeSpan(string s)
		{
			return ToAny<TimeSpan>(s, TimeSpan.TryParse);
		}

#if !DOTNET35

		/// <summary>
		/// Converts the supplied string to a Guid
		/// </summary>
		/// <param name="s">
		/// The string to convert into a Guid
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the Guid value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static Guid? ToGuid(string s)
		{
			return ToAny<Guid>(s, Guid.TryParse);
		}

		/// <summary>
		/// Converts the supplied string to an Enum ignoring case
		/// </summary>
		/// <param name="s">
		/// The string to convert into an Enum value
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the Enum value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static T? ToEnum<T>(string s) where T : struct
		{
			return ToAny<T>(s, Enum.TryParse, true);
		}

		/// <summary>
		/// Converts the supplied string to an Enum
		/// </summary>
		/// <param name="s">
		/// The string to convert into an Enum value
		/// </param>
		/// <param name="ignoreCase">
		/// Whether or not to ignore the case of the Enum value
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the Enum value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static T? ToEnum<T>(string s, bool ignoreCase) where T : struct
		{
			return ToAny<T>(s, Enum.TryParse, ignoreCase);
		}
#else

		//THESE METHODS ARE FOR .NET 3.5 CONSUMERS AND CAN BE REMOVED
		//IN THE FUTURE.

		/// <summary>
		/// Converts the supplied string to an Enum
		/// </summary>
		/// <param name="s">
		/// The string to convert into an Enum value
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the Enum value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static T? ToEnum<T>(string s) where T : struct
		{
			return ToEnum<T>(s, true);
		}

		/// <summary>
		/// Converts the supplied string to an Enum
		/// </summary>
		/// <param name="s">
		/// The string to convert into an Enum value
		/// </param>
		/// <param name="ignoreCase">
		/// Whether or not to ignore the case of the Enum value
		/// </param>
		/// <returns>
		/// If conversion succeeded then the return value is a nullable object
		/// wrapping the Enum value.  If conversion failed then the return value
		/// is null
		/// </returns>
		public static T? ToEnum<T>(string s, bool ignoreCase) where T : struct
		{
			T val = default(T);
			bool success = false;
			try
			{
				val = (T)Enum.Parse(typeof(T), s, ignoreCase);
				success = true;
			}
			catch { success = false; }

			if (success)
			{
				return new Nullable<T>(val);
			}
			else
			{
				return new Nullable<T>();
			}
		}

#endif

		#endregion
	}
}
