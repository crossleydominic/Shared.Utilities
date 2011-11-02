using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.ExtensionMethods.Primitives;

namespace Shared.Utilities.Tests.ExtensionMethods.Primitives
{
	/// <summary>
	/// A set of tests for the Primitive Extensions
	/// </summary>
	[TestFixture]
	public class PrimitiveExtensionsTests
	{
		#region Between Tests

		/// <summary>
		/// Tests what happens when the minimum number is greater than the maximum number
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentException))] 
		public void Between_MinGreaterThanMax()
		{
			int i = 10;
			i.Between(15, 5);
		}

		/// <summary>
		/// Tests a successful between operation
		/// </summary>
		[Test]
		public void Between_NumBetweenMinAndMax()
		{
			int i = 10;
			bool success = i.Between(5, 15);
			Assert.IsTrue(success);
		}

		/// <summary>
		/// Tests what happens when the minimum number is equal to the compare number
		/// </summary>
		[Test]
		public void Between_MinEqualToNum()
		{
			int i = 10;
			bool success = i.Between(10, 15);
			Assert.IsTrue(success);
		}

		/// <summary>
		/// Tests what happens when the minimum and maximum number are equal to the compare number
		/// </summary>
		[Test]
		public void Between_MinAndMaxEqualToNum()
		{
			int i = 10;
			bool success = i.Between(10, 10);
			Assert.IsTrue(success);
		}

		/// <summary>
		/// Tests what happens when the minimum number is greater than the compare number
		/// </summary>
		[Test]
		public void Between_MinGreaterThanNum()
		{
			int i = 10;
			bool success = i.Between(15, 15);
			Assert.IsFalse(success);
		}

		/// <summary>
		/// Tests what happens when the maximum number is less than the compare number
		/// </summary>
		[Test]
		public void Between_MaxLessThanNum()
		{
			int i = 10;
			bool success = i.Between(5, 5);
			Assert.IsFalse(success);
		}

		/// <summary>
		/// Tests what happens when we try and do a between with very large numbers
		/// </summary>
		[Test]
		public void Between_WithLargeNumbers()
		{
			int i = 10;
			bool success = i.Between(Int32.MinValue, Int32.MaxValue);
			Assert.IsTrue(success);
		}

		#endregion

		#region IsEven Tests

		/// <summary>
		/// Tests the IsEven functionality
		/// </summary>
		[Test]
		public void IsEven_NumberIsZero()
		{
			int i = 0;
			uint ui = 0;
			short s = 0;
			ushort us = 0;
			long l = 0;
			ulong ul = 0;

			Assert.IsTrue(i.IsEven());
			Assert.IsTrue(ui.IsEven());
			Assert.IsTrue(s.IsEven());
			Assert.IsTrue(us.IsEven());
			Assert.IsTrue(l.IsEven());
			Assert.IsTrue(ul.IsEven());
		}

		/// <summary>
		/// Tests the IsEven functionality
		/// </summary>
		[Test]
		public void IsEven_NumberIsEven()
		{
			int i = 4;
			uint ui = 4;
			short s = 4;
			ushort us = 4;
			long l = 4;
			ulong ul = 4;

			Assert.IsTrue(i.IsEven());
			Assert.IsTrue(ui.IsEven());
			Assert.IsTrue(s.IsEven());
			Assert.IsTrue(us.IsEven());
			Assert.IsTrue(l.IsEven());
			Assert.IsTrue(ul.IsEven());

			i = -4;
			s = -4;
			l = -4;

			Assert.IsTrue(i.IsEven());
			Assert.IsTrue(s.IsEven());
			Assert.IsTrue(l.IsEven());
		}

		#endregion

		#region IsOdd Tests

		/// <summary>
		/// Tests the IsOdd functionality
		/// </summary>
		[Test]
		public void IsOdd_NumberIsZero()
		{
			int i = 0;
			uint ui = 0;
			short s = 0;
			ushort us = 0;
			long l = 0;
			ulong ul = 0;

			Assert.IsFalse(i.IsOdd());
			Assert.IsFalse(ui.IsOdd());
			Assert.IsFalse(s.IsOdd());
			Assert.IsFalse(us.IsOdd());
			Assert.IsFalse(l.IsOdd());
			Assert.IsFalse(ul.IsOdd());
		}

		/// <summary>
		/// Tests the IsOdd functionality
		/// </summary>
		[Test]
		public void IsOdd_NumberIsOdd()
		{
			int i = 3;
			uint ui = 3;
			short s = 3;
			ushort us = 3;
			long l = 3;
			ulong ul = 3;

			Assert.IsTrue(i.IsOdd());
			Assert.IsTrue(ui.IsOdd());
			Assert.IsTrue(s.IsOdd());
			Assert.IsTrue(us.IsOdd());
			Assert.IsTrue(l.IsOdd());
			Assert.IsTrue(ul.IsOdd());

			i = -3;
			s = -3;
			l = -3;

			Assert.IsTrue(i.IsOdd());
			Assert.IsTrue(s.IsOdd());
			Assert.IsTrue(l.IsOdd());
		}

		#endregion
	}
}
