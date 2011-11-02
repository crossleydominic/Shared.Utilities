using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Collections.ObjectModel;

namespace Shared.Utilities.Tests
{
	/// <summary>
	/// Tests for the OperationResult class
	/// </summary>
	[TestFixture]
	public class OperationResultTests
	{
		#region Private Test methods

		/// <summary>
		/// A method that returns a Succeeded result
		/// </summary>
		private OperationResult TestMethodForSuccess()
		{
			return OperationResult.Succeeded;
		}

		/// <summary>
		/// A method that returns a failure result
		/// </summary>
		/// <returns></returns>
		private OperationResult TestMethodForFailure()
		{
			return new OperationResult("Failed");
		}

		#endregion

		#region Constructor Tests

		/// <summary>
		/// Construct a Failure result with an null string for the message
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentException))]
		public void Constructor_SingleError_MessageNull()
		{
			string str = null;
			new OperationResult(str);
		}

		/// <summary>
		/// Construct a Failure result with a null list for the messages
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void Constructor_MultipleErrorMessages_CollectionNull()
		{
			List<string> messages = null;
			new OperationResult(messages);
		}

		/// <summary>
		/// Construct a Failure result with an empty collection
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void Constructor_MultipleErrorMessages_CollectionEmpty()
		{
			List<string> messages = new List<string>() { };
			new OperationResult(messages);
		}

		/// <summary>
		/// Construct a Failure result with a null item in the collection
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void Constructor_MultipleErrorMessages_CollectionContainsNull()
		{
			List<string> messages = new List<string>() {"message1", null, "message2" };
			new OperationResult(messages);
		}

		#endregion

		#region Success tests

		/// <summary>
		/// Ensures the success property actually indicates success
		/// </summary>
		[Test]
		public void Success_OperationSucceeded()
		{
			Assert.IsTrue(OperationResult.Succeeded.Success);
		}

		/// <summary>
		/// Tests the success property when the operation failed with a single message
		/// </summary>
		[Test]
		public void Success_OperationFailed_SingleMessage()
		{
			OperationResult result = new OperationResult("error");
			Assert.IsFalse(result.Success);
		}

		/// <summary>
		/// Tests the success property when the operation failed with multiple messages
		/// </summary>
		[Test]
		public void Success_OperationFailed_MultipleMessages()
		{
			OperationResult result = new OperationResult(new List<string>() { "error1", "error2" });
			Assert.IsFalse(result.Success);
		}

		#endregion

		#region FirstErrorMessage Tests

		/// <summary>
		/// Tests that an exception is thrown when trying to access the FirstMessage
		/// when the operation succeeded
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(InvalidOperationException))]
		public void FirstErrorMessage_ThrowsOnSuccess()
		{
			string s = OperationResult.Succeeded.FirstErrorMessage;
		}

		/// <summary>
		/// Tests the first message is the same as the supplied text for a failure
		/// </summary>
		[Test]
		public void FirstErrorMessage_SingleMessage()
		{
			OperationResult result = new OperationResult("a");
			Assert.AreEqual(result.FirstErrorMessage, "a");
		}

		/// <summary>
		/// Tests the first message is the same as the first 
		/// element supplied as part of a failure collection
		/// </summary>
		[Test]
		public void FirstErrorMessage_MultipleMessages()
		{
			OperationResult result = new OperationResult(new List<string>() { "a", "b" });
			Assert.AreEqual(result.FirstErrorMessage, "a");
		}

		#endregion

		#region ErrorMessages Tests

		/// <summary>
		/// Test to make sure that an exception is thrown when trying to access
		/// the error messages collection when the operation succeeded.
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(InvalidOperationException))]
		public void ErrorMessages_ThrowsOnSuccess()
		{
			ReadOnlyCollection<string> s = OperationResult.Succeeded.ErrorMessages;
		}

		/// <summary>
		/// Test to make sure that the error messages collection contains one error when
		/// the operation result is constructed with a single failure message
		/// </summary>
		[Test]
		public void ErrorMessages_SingleMessage()
		{
			OperationResult result = new OperationResult("a");
			Assert.AreEqual(result.ErrorMessages.Count, 1);
			Assert.AreEqual(result.ErrorMessages[0], "a");
		}

		/// <summary>
		/// Test to make sure that the error messages collection contains all of the 
		/// errors that were used to construct the failure OperationResult
		/// </summary>
		[Test]
		public void ErrorMessages_MultipleMessage()
		{
			OperationResult result = new OperationResult(new List<string>(){"a","b","c"});
			Assert.AreEqual(result.ErrorMessages.Count, 3);
			Assert.AreEqual(result.ErrorMessages[0], "a");
			Assert.AreEqual(result.ErrorMessages[1], "b");
			Assert.AreEqual(result.ErrorMessages[2], "c");
		}

		#endregion

		#region AllErrorMessages Tests

		/// <summary>
		/// Test to ensure that an exception is thrown when trying to access the AllErrorMessages
		/// property when the operation succeeded.
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(InvalidOperationException))]
		public void AllErrorMessages_ThrowsOnSuccess()
		{
			string s = OperationResult.Succeeded.AllErrorMessages;
		}

		/// <summary>
		/// Test to make sure that AllErrorMessages contains the error string when the OperationResult
		/// was constructed with a single failure message
		/// </summary>
		[Test]
		public void AllErrorMessages_SingleMessage()
		{
			OperationResult result = new OperationResult("a");
			Assert.IsTrue(result.AllErrorMessages.Contains("a"));
		}
		/// <summary>
		/// Test to make sure that AllErrorMessages contains all of the error strings when the 
		/// OperationResult was constructed with a failure message collection
		/// </summary>
		[Test]
		public void AllErrorMessages_MultipleMessage()
		{
			OperationResult result = new OperationResult(new List<string>() { "a", "b", "c" });
			Assert.IsTrue(result.AllErrorMessages.Contains("a"));
			Assert.IsTrue(result.AllErrorMessages.Contains("b"));
			Assert.IsTrue(result.AllErrorMessages.Contains("c"));
		}

		#endregion

		#region Typecasting tests

		/// <summary>
		/// Test to ensure that an OperationResult is implicitly castable to a boolean
		/// </summary>
		[Test]
		public void ImplicitlyCastableToBool()
		{
			bool successMethodSucceeded = false;
			bool failureMethodFailed = false;
			
			if (TestMethodForSuccess())
			{
				successMethodSucceeded = true;
			}

			if (!TestMethodForFailure())
			{
				failureMethodFailed = true;
			}

			Assert.IsTrue(successMethodSucceeded);
			Assert.IsTrue(failureMethodFailed);
		}

		#endregion

		#region Equality Tests

		/// <summary>
		/// Test to make sure that two success OperationResults are equivalent with each other
		/// </summary>
		[Test]
		public void Equality_SuccessResultsAreEqual()
		{
			OperationResult result1 = TestMethodForSuccess();

			Assert.IsTrue(result1 == OperationResult.Succeeded);
		}

		/// <summary>
		/// Test to make sure that two failure OperationResults are not equivalent with each other
		/// </summary>
		[Test]
		public void Equality_FailureResultsAreNotEqual()
		{
			OperationResult result1 = new OperationResult("Error");
			OperationResult result2 = TestMethodForFailure();

			Assert.AreNotEqual(result1, result2);
		}

		#endregion
	}
}
