using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Diagnostics;

namespace Shared.Utilities.Tests
{
	/// <summary>
	/// Tests for the Retry class
	/// </summary>
	[TestFixture]
	public class RetryTests
	{
		#region Attempt Tests

		/// <summary>
		/// Tests when the function to invoke is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void Attempt_FunctionNull()
		{
			Retry.Attempt(null);
		}

		/// <summary>
		/// Tests when the number of attempts is less than zero
		/// </summary>
		[Test]
        [ExpectedException(ExpectedException = typeof(ArgumentOutOfRangeException))]
		public void Attempt_AttemptsLessThanZero()
		{
			Retry.Attempt(() => { return true; }, -1, TimeSpan.FromSeconds(1));
		}

		/// <summary>
		/// Tests when the number of attempts is equal to zero
		/// </summary>
		[Test]
        [ExpectedException(ExpectedException = typeof(ArgumentOutOfRangeException))]
		public void Attempt_AttemptsEqualsZero()
		{
			Retry.Attempt(() => { return true; }, 0, TimeSpan.FromSeconds(1));
		}

		/// <summary>
		/// Tests when the number of attempts is greather than zero
		/// </summary>
		[Test]
		public void Attempt_AttemptsGreaterThanZero()
		{
			bool set = false;
			Retry.Attempt(() => { set = true; return true; }, 1, TimeSpan.FromSeconds(1));

			Assert.IsTrue(set);
		}

		/// <summary>
		/// Tests when the interval equals zero
		/// </summary>
		[Test]
        [ExpectedException(ExpectedException = typeof(ArgumentOutOfRangeException))]
		public void Attempt_IntervalEqualsZero()
		{
			Retry.Attempt(() => { return true; }, 1, TimeSpan.FromSeconds(0));
		}

		/// <summary>
		/// Tests when the interval is less than zero
		/// </summary>
		[Test]
        [ExpectedException(ExpectedException = typeof(ArgumentOutOfRangeException))]
		public void Attempt_IntervalLessThanZero()
		{
			Retry.Attempt(() => { return true; }, 1, (TimeSpan.FromSeconds(0) - TimeSpan.FromSeconds(1)));
		}

		/// <summary>
		/// Tests when the interval is greater than zero
		/// </summary>
		[Test]
		public void Attempt_IntervalGreaterThanZero()
		{
			bool set = false;
			Retry.Attempt(() => { set = true; return true; }, 1, TimeSpan.FromSeconds(1));

			Assert.IsTrue(set);
		}

		/// <summary>
		/// Test to make sure that the number of attempts is respected
		/// </summary>
		[Test]
		public void Attempt_NumberOfAttemptsReached()
		{
			int counter = 0;
			Retry.Attempt(() => { counter++; return false; }, 5, TimeSpan.FromMilliseconds(1));

			Assert.AreEqual(counter, 5);
		}

		/// <summary>
		/// Test for a function that will succeed on its last attempt
		/// </summary>
		[Test]
		public void Attempt_SucceedsOnLastAttempt()
		{
			int counter = 0;
			bool success = Retry.Attempt(() => { counter++; return counter == 5; }, 5, TimeSpan.FromMilliseconds(1));

			Assert.IsTrue(success);
		}

		/// <summary>
		/// Test to make sure that the supplied interval between attempts is repsected
		/// </summary>
		[Test]
		public void Attempt_IntervalRespected()
		{
			Stopwatch watch = new Stopwatch();
			watch.Start();
			Retry.Attempt(() => { return false; }, 10, TimeSpan.FromMilliseconds(100));
			watch.Stop();

			//The elapsed time is a bit lower than the real time we're expecting.
			//Thread yielding is a request only and not exact.
			Assert.GreaterOrEqual(watch.ElapsedMilliseconds, 900);
		}

		/// <summary>
		/// Test to make sure that Retry.Attempt will report a true boolean if the function succeeds
		/// </summary>
		[Test]
		public void Attempt_Success()
		{
			bool success = Retry.Attempt(() => { return true; }, 5, TimeSpan.FromMilliseconds(1));

			Assert.IsTrue(success);
		}

		/// <summary>
		/// Test to makre sure that Retry.Attempt will report a false boolean if the function fails.
		/// </summary>
		[Test]
		public void Attempt_Failed()
		{
			bool success = Retry.Attempt(() => { return false; }, 5, TimeSpan.FromMilliseconds(1));

			Assert.IsFalse(success);
		}

		/// <summary>
		/// Test to makre sure that Retry.Attempt will report a false boolean if the function fails.
		/// </summary>
		[Test]
		public void Attempt_ThrowException_HandleExceptions()
		{
			OperationResult result = Retry.Attempt(() => { throw new InvalidOperationException(); }, 5, TimeSpan.FromMilliseconds(1));

			Assert.IsFalse(result);
			Assert.IsFalse(string.IsNullOrEmpty(result.AllErrorMessages));
			Assert.IsFalse(result.ErrorMessages.Count == 0);
		}

		/// <summary>
		/// Test to makre sure that Retry.Attempt will report a false boolean if the function fails.
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(InvalidOperationException))]
		public void Attempt_ThrowException_DontHandleExceptions()
		{
			OperationResult result = Retry.Attempt(() => { throw new InvalidOperationException(); }, 5, TimeSpan.FromMilliseconds(1), RetryExceptionBehaviour.DoNotHandle);
		}

		#endregion
	}
}
