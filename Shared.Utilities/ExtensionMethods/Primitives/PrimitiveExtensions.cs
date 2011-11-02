using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Shared.Utilities.ExtensionMethods.Logging;

namespace Shared.Utilities.ExtensionMethods.Primitives
{
	public static class PrimitiveExtensions
	{
		#region Logging

		/// <summary>
		/// Used to log information about the class
		/// </summary>
		private static ILog _logger = LogManager.GetLogger(typeof(PrimitiveExtensions));

		#endregion

		/// <summary>
		///  Gets a flag indicating whether the number is between the given
		///  minimum and maximum values (inclusive). 
		/// </summary>
		/// <param name="number">The number to be tested.</param>
		/// <param name="minValue">The minimum value.</param>
		/// <param name="maxValue">The maximum value.</param>
		/// <returns>True if the number is between the minimum and maximum (inclusive); false otherwise.</returns>
		public static bool Between<T>(this T number, T minValue, T maxValue) where T : IComparable<T>
		{
			_logger.DebugMethodCalled(number, minValue, maxValue);

			#region Input Validation

			if (minValue.CompareTo(maxValue) > 0)
			{
				throw new ArgumentException("The maximum value must be greater than or equal to the minimum value.", "maxValue");
			}

			#endregion

			return (number.CompareTo(minValue) >=0 && number.CompareTo(maxValue) <= 0);
		}

		/// <summary>Determines whether or not the number is even.</summary>
		public static bool IsEven(this int i) { return (i % 2 == 0); }

		/// <summary>Determines whether or not the number is even.</summary>
		public static bool IsEven(this uint i) { return (i % 2 == 0); }

		/// <summary>Determines whether or not the number is even.</summary>
		public static bool IsEven(this short i) { return (i % 2 == 0); }

		/// <summary>Determines whether or not the number is even.</summary>
		public static bool IsEven(this ushort i) { return (i % 2 == 0); }

		/// <summary>Determines whether or not the number is even.</summary>
		public static bool IsEven(this long i) { return (i % 2 == 0); }

		/// <summary>Determines whether or not the number is even.</summary>
		public static bool IsEven(this ulong i) { return (i % 2 == 0); }

		/// <summary>Determines whether or not the number is odd.</summary>
		public static bool IsOdd(this int i) { return !i.IsEven(); }

		/// <summary>Determines whether or not the number is odd.</summary>
		public static bool IsOdd(this uint i) { return !i.IsEven(); }

		/// <summary>Determines whether or not the number is odd.</summary>
		public static bool IsOdd(this short i) { return !i.IsEven(); }

		/// <summary>Determines whether or not the number is odd.</summary>
		public static bool IsOdd(this ushort i) { return !i.IsEven(); }

		/// <summary>Determines whether or not the number is odd.</summary>
		public static bool IsOdd(this long i) { return !i.IsEven(); }

		/// <summary>Determines whether or not the number is odd.</summary>
		public static bool IsOdd(this ulong i) { return !i.IsEven(); }

	}
}
