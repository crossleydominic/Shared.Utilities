using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.ExtensionMethods.Primitives;

namespace Shared.Utilities.Tests.ExtensionMethods.Primitives
{
	/// <summary>
	/// Tests for the RandomExtensions
	/// </summary>
	[TestFixture]
	public class RandomExtensionsTests
	{
		#region NextString tests

		/// <summary>
		/// Tests when Random is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentNullException))]
		public void NextString_RandomNull()
		{
			Random r = null;
			r.NextString(10);
		}

		/// <summary>
		/// Tests when the length of the random string should be less than zero
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void NextString_LengthLessThanZero()
		{
			Random r = new Random();
			r.NextString(-1);
		}

		/// <summary>
		/// Tests when the length of the random string should be zero
		/// </summary>
		[Test]
		public void NextString_LengthZero()
		{
			Random r = new Random();
			string str = r.NextString(0);

			Assert.AreEqual(str, string.Empty);
		}

		/// <summary>
		/// Tests when the length is of the random string should be greater than zero
		/// </summary>
		[Test]
		public void NextString_LengthGreaterThanZero()
		{
			Random r = new Random();
			string str = r.NextString(10);

			Assert.AreEqual(str.Length, 10);
		}

		#endregion

		#region NextBool Tests

		/// <summary>
		/// Tests when Random is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void NextBool_RandomNull()
		{
			Random r = null;
			r.NextBool();
		}

		#endregion
	}
}
