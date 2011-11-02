using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

#if !DOTNET35
using Shared.Utilities.ExtensionMethods.Delegates;

namespace Shared.Utilities.Tests.ExtensionMethods.Delegates
{
	[TestFixture]
	public class AttemptSuccessConditionsTests
	{
		#region NotNullIsSuccess Tests

		/// <summary>
		/// Tests to make sure that the NotNullIsSuccess delegate will return 
		/// a failure if passed a null reference
		/// </summary>
		[Test]
		public void NotNullIsSuccess_ValueNull()
		{
			Assert.IsFalse(AttemptSuccessConditions.NotNullIsSuccess(null));
		}

		/// <summary>
		/// Tests to make sure that the NotNullIsSuccess delegate will return 
		/// a success if passed a non-null reference.
		/// </summary>
		[Test]
		public void NotNullIsSuccess_ValueNotNUll()
		{
			Assert.IsTrue(AttemptSuccessConditions.NotNullIsSuccess(new object()));
		}

		#endregion

		#region StringNotNullOrEmptyIsSucces Tests

		/// <summary>
		/// Tests to make sure that the StringNotNullOrEmptyIsSucces delegate will return 
		/// a failure if passed a null reference
		/// </summary>
		[Test]
		public void StringNotNullOrEmptyIsSuccess_StringNull()
		{
			Assert.IsFalse(AttemptSuccessConditions.StringNotNullOrEmptyIsSuccess(null));
		}

		/// <summary>
		/// Tests to make sure that the StringNotNullOrEmptyIsSucces delegate will return 
		/// a failure if passed an empty string.
		/// </summary>
		[Test]
		public void StringNotNullOrEmptyIsSuccess_StringEmpty()
		{
			Assert.IsFalse(AttemptSuccessConditions.StringNotNullOrEmptyIsSuccess(string.Empty));
		}

		/// <summary>
		/// Tests to make sure that the StringNotNullOrEmptyIsSucces delegate will return 
		/// a success if passed a non-null/non-empty string.
		/// </summary>
		[Test]
		public void StringNotNullOrEmptyIsSuccess_StringNonNullOrEmpty()
		{
			Assert.IsTrue(AttemptSuccessConditions.StringNotNullOrEmptyIsSuccess("a"));
		}

		#endregion

		#region IntGreaterThanZeroIsZuccess Tests

		/// <summary>
		/// Tests to make sure that the IntGreaterThanZeroIsZuccess delegate will return 
		/// a failure if passed a value less than 0
		/// </summary>
		[Test]
		public void IntGreaterThanZeroIsZuccess_ValueLessThanZero()
		{
			Assert.IsFalse(AttemptSuccessConditions.IntGreaterThanZeroIsZuccess(-1));
		}

		/// <summary>
		/// Tests to make sure that the IntGreaterThanZeroIsZuccess delegate will return 
		/// a failure if passed a value equal to 0
		/// </summary>
		[Test]
		public void IntGreaterThanZeroIsZuccess_ValueZero()
		{
			Assert.IsFalse(AttemptSuccessConditions.IntGreaterThanZeroIsZuccess(0));
		}

		/// <summary>
		/// Tests to make sure that the IntGreaterThanZeroIsZuccess delegate will return 
		/// a success if passed a value greater than 0.
		/// </summary>
		[Test]
		public void IntGreaterThanZeroIsZuccess_ValueGreaterThanZero()
		{
			Assert.IsTrue(AttemptSuccessConditions.IntGreaterThanZeroIsZuccess(1));
		}

		#endregion
	}
}
#endif
