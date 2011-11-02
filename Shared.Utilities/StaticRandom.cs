using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Utilities.Threading;
using Shared.Utilities.ExtensionMethods.Primitives;

namespace Shared.Utilities
{
	/// <summary>
	/// A static class that wraps the Random class so that consumers
	/// no longer need to create an instance of it.
	/// </summary>
	public static class StaticRandom
	{
		#region Private static members

		/// <summary>
		/// The padlock used to protect _random
		/// </summary>
		private static Padlock _padlock;

		/// <summary>
		/// The random number generator to use.
		/// </summary>
		private static ThreadShared<Random> _random;

		#endregion

		#region Type initializer

		/// <summary>
		/// Initialize the random number generator
		/// </summary>
		static StaticRandom()
		{
			_padlock = new Padlock();
			_random = new ThreadShared<Random>(new Random(), _padlock);
		}

		#endregion

		#region Public static methods

		/// <summary>
		/// Gets the next non-negative random number
		/// </summary>
		public static int Next()
		{
			using (_padlock.Lock())
			{
				return _random._.Next();
			}
		}

		/// <summary>
		/// Gets the next random number less than the specified number
		/// </summary>
		public static int Next(int maxValue)
		{
			using (_padlock.Lock())
			{
				return _random._.Next(maxValue);
			}
		}

		/// <summary>
		/// Gets the next number in the specified range.
		/// </summary>
		/// <param name="minValue">The inclusive minimum number</param>
		/// <param name="maxValue">The exclusive maximum number</param>
		public static int Next(int minValue, int maxValue)
		{
			using (_padlock.Lock())
			{
				return _random._.Next(minValue, maxValue);
			}
		}

		/// <summary>
		/// Fills the supplied buffer with random bytes
		/// </summary>
		public static void NextBytes(byte[] buffer)
		{
			using(_padlock.Lock())
			{
				_random._.NextBytes(buffer);
			}
		}

		/// <summary>
		/// Gets the next random double value
		/// </summary>
		public static double NextDouble()
		{
			using (_padlock.Lock())
			{
				return _random._.NextDouble();
			}
		}

		/// <summary>
		/// Generates a random string of the specified length
		/// </summary>
		public static string NextString(int length)
		{
			using (_padlock.Lock())
			{
				return _random._.NextString(length);
			}
		}

		/// <summary>
		/// Generates a random string of the specified length. Optionally allow
		/// numbers and symbols to be included in the string
		/// </summary>
		public static string NextString(int length, bool allowNumbers, bool allowSymbols)
		{
			using (_padlock.Lock())
			{
				return _random._.NextString(length, allowNumbers, allowSymbols);
			}
		}

		/// <summary>
		/// Generates a random boolean.
		/// </summary>
		/// <returns>
		/// True or false.
		/// </returns>
		public static bool NextBool()
		{
			using (_padlock.Lock())
			{
				return _random._.NextBool();
			}
		}

		#endregion
	}
}
