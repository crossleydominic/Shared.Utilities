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
	public class FuncExtensionsTests
	{
		#region Test classes

		private class TestClass
		{
			public int counter = 0;
			public string AMethodThatTakesNoArgumentsAndReturnsAnNonEmptyStringForSuccess()
			{
				if (counter == 0)
					//Simulate failure the first time
				{
					counter++;
					return string.Empty;
				}
				else
				{
					return "Method succeeded";
				}
			}

			public int AMethodThatTakesNoArgumentsAndReturnsAPositiveIntegerForSuccess()
			{
				if (counter == 0)
				//Simulate failure the first time
				{
					counter++;
					return -1;
				}
				else
				{
					return 1;
				}
			}

			public int AMethodThatTakesNoArgumentsAndThrowsAnExceptionForFailure()
			{
				if (counter == 0)
				//Simulate failure the first time
				{
					counter++;
					throw new InvalidOperationException();
				}
				else
				{
					return 1;
				}
			}

			public string AMethodThatTakesOneArgument(int i1)
			{
				//Simulate failure the first time
				if (counter == 0) { counter++; return null; }

				return i1.ToString();
			}

			public string AMethodThatTakesTwoArguments(int i1, string i2)
			{
				//Simulate failure the first time
				if (counter == 0) { counter++; return null; }

				return i1.ToString() + i2;
			}

			public string AMethodThatTakesThreeArguments(int i1, string i2, int i3)
			{
				//Simulate failure the first time
				if (counter == 0) { counter++; return null; }

				return i1.ToString() + i2 + i3.ToString();
			}

			public string AMethodThatTakesFourArguments(int i1, string i2, int i3, string i4)
			{
				//Simulate failure the first time
				if (counter == 0) { counter++; return null; }

				return i1.ToString() + i2 + i3.ToString() + i4;
			}

			public string AMethodThatTakesFiveArguments(int i1, string i2, int i3, string i4, int i5)
			{
				//Simulate failure the first time
				if (counter == 0) { counter++; return null; }

				return i1.ToString() + i2 + i3.ToString() + i4 + i5.ToString();
			}

			public string AMethodThatTakesSixArguments(int i1, string i2, int i3, string i4, int i5, string i6)
			{
				//Simulate failure the first time
				if (counter == 0) { counter++; return null; }

				return i1.ToString() + i2 + i3.ToString() + i4 + i5.ToString() + i6;
			}

			public string AMethodThatTakesSevenArguments(int i1, string i2, int i3, string i4, int i5, string i6, int i7)
			{
				//Simulate failure the first time
				if (counter == 0) { counter++; return null; }

				return i1.ToString() + i2 + i3.ToString() + i4 + i5.ToString() + i6 + i7.ToString();
			}

			public string AMethodThatAlwaysFailsByReturningNull()
			{
				counter++;
				return null;
			}

			public string AMethodThatAlwaysFailsByThrowingAnException()
			{
				counter++;
				throw new InvalidOperationException();
			}
		}

		#endregion

		#region Attempt Tests

		[Test]
		public void Attempt_CallMethodWithNoArgsReturningANonEmptyStringForSuccess()
		{
			//Call a method that:
			//	accepts no arguments
			//	returns a string
			//	uses a non-null/non-empty string to denote success
			TestClass tc = new TestClass();
			Func<string> functionToAttempt = tc.AMethodThatTakesNoArgumentsAndReturnsAnNonEmptyStringForSuccess;
			string functionReturnValue;
			OperationResult result = functionToAttempt.Attempt(AttemptSuccessConditions.StringNotNullOrEmptyIsSuccess, out functionReturnValue);

			Assert.IsTrue(result);
			Assert.IsFalse(string.IsNullOrEmpty(functionReturnValue));
		}

		[Test]
		public void Attempt_CallMethodWithNoArgsReturningAPositiveIntForSuccess()
		{
			//Call a method that:
			//	accepts no arguments
			//	returns an int
			//	uses value greater than 0 to denote success
			TestClass tc = new TestClass();
			Func<int> functionToAttempt = tc.AMethodThatTakesNoArgumentsAndReturnsAPositiveIntegerForSuccess;
			int functionReturnValue;
			OperationResult result = functionToAttempt.Attempt(AttemptSuccessConditions.IntGreaterThanZeroIsZuccess, out functionReturnValue);

			Assert.IsTrue(result);
			Assert.Greater(functionReturnValue, 0);
		}

		[Test]
		public void Attempt_CallMethodWithNoArgsThatThrowsAnExceptionForFailure()
		{
			//Call a method that:
			//	accepts no arguments
			//	returns an int
			//	throws an exception to denote failure
			TestClass tc = new TestClass();
			Func<int> functionToAttempt = tc.AMethodThatTakesNoArgumentsAndThrowsAnExceptionForFailure;
			int functionReturnValue;
			OperationResult result = functionToAttempt.Attempt(out functionReturnValue);

			Assert.IsTrue(result);
		}

		[Test]
		public void Attempt_CallMethodThatAlwaysFailsByThrowingWithAutomaticExceptionHandling()
		{
			//Call a method that:
			//	accepts no arguments
			//	returns a string
			//	throws an exception to denote failure
			TestClass tc = new TestClass();
			Func<string> functionToAttempt = tc.AMethodThatAlwaysFailsByThrowingAnException;
			string functionReturnValue;
			OperationResult result = functionToAttempt.Attempt(out functionReturnValue);

			Assert.IsFalse(result);
		}

		[Test]
		[ExpectedException(ExpectedException=typeof(InvalidOperationException))]
		public void Attempt_CallMethodThatAlwaysFailsByThrowingWithoutAutomaticExceptionHandling()
		{
			//Call a method that:
			//	accepts no arguments
			//	returns a string
			//	throws an exception to denote failure
			TestClass tc = new TestClass();
			Func<string> functionToAttempt = tc.AMethodThatAlwaysFailsByThrowingAnException;
			string functionReturnValue;

			//The method will throw an exception but we've turned automatic exception handling off
			//so we expect the Retry class to rethrow the exception and bail out of the retry attempts
			OperationResult result = functionToAttempt.Attempt(
				AttemptSuccessConditions.NotNullIsSuccess,
				Retry.DEFAULT_ATTEMPTS,
				Retry.DEFAULT_INTERVAL,
				RetryExceptionBehaviour.DoNotHandle,
				out functionReturnValue);
		}

		[Test]
		public void Attempt_CallMethodThatAlwaysFailsByReturningNull()
		{
			//Call a method that:
			//	accepts no arguments
			//	returns a string
			//	uses a null reference for failure
			TestClass tc = new TestClass();
			Func<string> functionToAttempt = tc.AMethodThatAlwaysFailsByReturningNull;
			string functionReturnValue;
			OperationResult result = functionToAttempt.Attempt(AttemptSuccessConditions.NotNullIsSuccess, out functionReturnValue);

			Assert.IsFalse(result);
		}

		[Test]
		public void Attempt_MethodAttemptedCorrectNumberOfTimes()
		{
			//Call a method that:
			//	accepts no arguments
			//	returns a string
			//	uses a null reference for failure
			TestClass tc = new TestClass();
			Func<string> functionToAttempt = tc.AMethodThatAlwaysFailsByReturningNull;
			string functionReturnValue;
			OperationResult result = functionToAttempt.Attempt(
				AttemptSuccessConditions.NotNullIsSuccess,
				5,
				TimeSpan.FromMilliseconds(1),
				RetryExceptionBehaviour.HandleAndCollate,
				out functionReturnValue);

			Assert.AreEqual(5, tc.counter);
			Assert.IsFalse(result);
		}

		[Test]
		public void Attempt_CallAMethodWith1Arg()
		{
			//Call a method that:
			//	accepts arguments:
			//		int
			//	returns a string
			//	uses a null reference/emptry string for failure
			TestClass tc = new TestClass();
			Func<int, string> functionToAttempt = tc.AMethodThatTakesOneArgument;

			int arg1 = 1;

			string functionReturnValue;
			OperationResult result = functionToAttempt.Attempt(
				arg1, 
				AttemptSuccessConditions.StringNotNullOrEmptyIsSuccess, 
				out functionReturnValue);

			Assert.IsTrue(result);
			Assert.AreEqual(functionReturnValue, "1");
		}

		[Test]
		public void Attempt_CallAMethodWith2Args()
		{
			//Call a method that:
			//	accepts arguments:
			//		int, string
			//	returns a string
			//	uses a null reference/emptry string for failure
			TestClass tc = new TestClass();
			Func<int, string, string> functionToAttempt = tc.AMethodThatTakesTwoArguments;

			int arg1 = 1;
			string arg2 = "2";

			string functionReturnValue;
			OperationResult result = functionToAttempt.Attempt(
				arg1,
				arg2,
				AttemptSuccessConditions.StringNotNullOrEmptyIsSuccess,
				out functionReturnValue);

			Assert.IsTrue(result);
			Assert.AreEqual(functionReturnValue, "12");
		}

		[Test]
		public void Attempt_CallAMethodWith3Args()
		{
			//Call a method that:
			//	accepts arguments:
			//		int, string, int
			//	returns a string
			//	uses a null reference/emptry string for failure
			TestClass tc = new TestClass();
			Func<int, string, int, string> functionToAttempt = tc.AMethodThatTakesThreeArguments;

			int arg1 = 1;
			string arg2 = "2";
			int arg3 = 3;

			string functionReturnValue;
			OperationResult result = functionToAttempt.Attempt(
				arg1,
				arg2,
				arg3,
				AttemptSuccessConditions.StringNotNullOrEmptyIsSuccess,
				out functionReturnValue);

			Assert.IsTrue(result);
			Assert.AreEqual(functionReturnValue, "123");
		}

		[Test]
		public void Attempt_CallAMethodWith4Args()
		{
			//Call a method that:
			//	accepts arguments:
			//		int, string, int, string
			//	returns a string
			//	uses a null reference/emptry string for failure
			TestClass tc = new TestClass();
			Func<int, string, int, string, string> functionToAttempt = tc.AMethodThatTakesFourArguments;

			int arg1 = 1;
			string arg2 = "2";
			int arg3 = 3;
			string arg4 = "4";

			string functionReturnValue;
			OperationResult result = functionToAttempt.Attempt(
				arg1,
				arg2,
				arg3,
				arg4,
				AttemptSuccessConditions.StringNotNullOrEmptyIsSuccess,
				out functionReturnValue);

			Assert.IsTrue(result);
			Assert.AreEqual(functionReturnValue, "1234");
		}

		[Test]
		public void Attempt_CallAMethodWith5Args()
		{
			//Call a method that:
			//	accepts arguments:
			//		int, string, int, string, int
			//	returns a string
			//	uses a null reference/emptry string for failure
			TestClass tc = new TestClass();
			Func<int, string, int, string, int, string> functionToAttempt = tc.AMethodThatTakesFiveArguments;

			int arg1 = 1;
			string arg2 = "2";
			int arg3 = 3;
			string arg4 = "4";
			int arg5 = 5;

			string functionReturnValue;
			OperationResult result = functionToAttempt.Attempt(
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				AttemptSuccessConditions.StringNotNullOrEmptyIsSuccess,
				out functionReturnValue);

			Assert.IsTrue(result);
			Assert.AreEqual(functionReturnValue, "12345");
		}

		[Test]
		public void Attempt_CallAMethodWith6Args()
		{
			//Call a method that:
			//	accepts arguments:
			//		int, string, int, string, int, string
			//	returns a string
			//	uses a null reference/emptry string for failure
			TestClass tc = new TestClass();
			Func<int, string, int, string, int, string, string> functionToAttempt = tc.AMethodThatTakesSixArguments;

			int arg1 = 1;
			string arg2 = "2";
			int arg3 = 3;
			string arg4 = "4";
			int arg5 = 5;
			string arg6 = "6";

			string functionReturnValue;
			OperationResult result = functionToAttempt.Attempt(
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				AttemptSuccessConditions.StringNotNullOrEmptyIsSuccess,
				out functionReturnValue);

			Assert.IsTrue(result);
			Assert.AreEqual(functionReturnValue, "123456");
		}


		[Test]
		public void Attempt_CallAMethodWith7Args()
		{
			//Call a method that:
			//	accepts arguments:
			//		int, string, int, string, int, string, int
			//	returns a string
			//	uses a null reference/emptry string for failure
			TestClass tc = new TestClass();
			Func<int, string, int, string, int, string, int, string> functionToAttempt = tc.AMethodThatTakesSevenArguments;

			int arg1 = 1;
			string arg2 = "2";
			int arg3 = 3;
			string arg4 = "4";
			int arg5 = 5;
			string arg6 = "6";
			int arg7 = 7;

			string functionReturnValue;
			OperationResult result = functionToAttempt.Attempt(
				arg1,
				arg2,
				arg3,
				arg4,
				arg5,
				arg6,
				arg7,
				AttemptSuccessConditions.StringNotNullOrEmptyIsSuccess,
				out functionReturnValue);

			Assert.IsTrue(result);
			Assert.AreEqual(functionReturnValue, "1234567");
		}

		#endregion
	}
}
#endif
