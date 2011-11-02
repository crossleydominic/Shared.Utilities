using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.Threading;
using System.Threading;

namespace Shared.Utilities.Tests.Threading
{
	/// <summary>
	/// A set of tests for the ThreadShared class
	/// </summary>
	[TestFixture]
	public class ThreadSharedTests
	{
		#region Constructor tests

		/// <summary>
		/// Construct a new ThreadShared resource using a value type
		/// </summary>
		[Test]
		public void Constructor_ConstructWithValueType()
		{
			Padlock p = new Padlock();
			ThreadShared<int> i = new ThreadShared<int>(10, p);

			using (p.Lock())
			{
				Assert.AreEqual(i.Value, 10);
			}
		}

		/// <summary>
		/// Construct a new ThreadShared resource with a reference type.
		/// </summary>
		[Test]
		public void Constructor_ConstructWithReferenceType()
		{
			Padlock p = new Padlock();

			//Uri is a random reference type;
			ThreadShared<Uri> i = new ThreadShared<Uri>(new Uri("http://example.com"), p);

			using (p.Lock())
			{
				Assert.AreEqual(i.Value, new Uri("http://example.com"));
			}
		}

		/// <summary>
		/// Attempt to construct a ThreadShared with a null Padlock
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentNullException))]
		public void Constructor_FirstPadlockNull()
		{
			Padlock p = null;
			ThreadShared<int> i = new ThreadShared<int>(0, p);
		}

		/// <summary>
		/// Attempt to construct a ThreadShared with any null Padlock
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void Constructor_OtherPadlockNull()
		{
			Padlock p1 = new Padlock();
			Padlock p2 = null;
			Padlock p3 = new Padlock();
			ThreadShared<int> i = new ThreadShared<int>(0, p1, p2, p3);
		}

		/// <summary>
		/// Attempts to construct a ThreadShared with valid Padlocks
		/// </summary>
		[Test]
		public void Constructor_AllPadlocksValid()
		{
			Padlock p1 = new Padlock();
			Padlock p2 = new Padlock();
			ThreadShared<int> i = new ThreadShared<int>(0, p1, p2);
		}

		#endregion

		#region Comparing value getters

		/// <summary>
		/// Test to ensure that the .Value and ._ properties always return
		/// the same results.
		/// </summary>
		[Test]
		public void ValueGettersAreEqual()
		{
			Padlock p1 = new Padlock();
			ThreadShared<int> i = new ThreadShared<int>(0, p1);

			using (p1.Lock())
			{
				Assert.AreEqual(i.Value, i._);
				i.Value++;

				Assert.AreEqual(i.Value, i._);
				Assert.AreEqual(i.Value, 1);
				Assert.AreEqual(i._, 1);

				i._++;
				Assert.AreEqual(i.Value, i._);
				Assert.AreEqual(i.Value, 2);
				Assert.AreEqual(i._, 2);
			}
		}

		#endregion

		#region Taking/Leaving lock tests

		/// <summary>
		/// Attempt to access a ThreadShared without it's lock being taken
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(InvalidOperationException))]
		public void OneLockNotTaken()
		{
			Padlock p1 = new Padlock();
			ThreadShared<int> i = new ThreadShared<int>(0, p1);

			//Access value outside of lock.
			i.Value++;
		}

		/// <summary>
		/// Attemot to access a ThreadShared with it's lock being taken
		/// </summary>
		[Test]
		public void OneLockTaken()
		{
			Padlock p1 = new Padlock();
			ThreadShared<int> i = new ThreadShared<int>(0, p1);

			using (p1.Lock())
			{
				i.Value++;
				Assert.AreEqual(i.Value, 1);
			}
		}

		/// <summary>
		/// Attempts to access a ThreadShared that is protected by two locks but the first lock
		/// is not taken
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(InvalidOperationException))]
		public void TwoLocksFirstNotTaken()
		{
			Padlock p1 = new Padlock();
			Padlock p2 = new Padlock();
			ThreadShared<int> i = new ThreadShared<int>(0, p1, p2);

			//Access value outside of both locks.
			using (p2.Lock())
			{
				i.Value++;
			}
		}

		/// <summary>
		/// Attempts to access a ThreadShared that is protected by two locks but the second lock
		/// is not taken
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(InvalidOperationException))]
		public void TwoLocksSecondNotTaken()
		{
			Padlock p1 = new Padlock();
			Padlock p2 = new Padlock();
			ThreadShared<int> i = new ThreadShared<int>(0, p1, p2);

			//Access value outside of both locks.
			using (p1.Lock())
			{
				i.Value++;
			}
		}

		/// <summary>
		/// Attempts to access a ThreadShared that is protected by two locks and both
		/// locks are taken
		/// </summary>
		[Test]
		public void TwoLocksBothTaken()
		{
			Padlock p1 = new Padlock();
			Padlock p2 = new Padlock();
			ThreadShared<int> i = new ThreadShared<int>(0, p1, p2);

			using (p1.Lock())
			{
				using (p2.Lock())
				{
					i.Value++;
					Assert.AreEqual(i.Value, 1);
				}
			}
		}

		/// <summary>
		/// Attempts to access a ThreadShared using a recusrive function that attemts
		/// to acquire the lock recursively.
		/// </summary>
		[Test]
		public void LockTakenRecursively()
		{
			Padlock p1 = new Padlock();
			ThreadShared<int> i = new ThreadShared<int>(0, p1);
			RecursiveFunction(5, 0, p1, i);

			using (p1.Lock())
			{
				Assert.AreEqual(i.Value, 5);
			}
		}
		private void RecursiveFunction(int maxRecursionDepth, int currentDepth, Padlock p, ThreadShared<int> i)
		{
			if (currentDepth < maxRecursionDepth)
			{
				using (p.Lock())
				{
					i.Value++;
					RecursiveFunction(maxRecursionDepth, ++currentDepth, p, i);
				}
			}
		}

		/// <summary>
		/// Attempts to associate one Padlock with multiple ThreadShared resources
		/// </summary>
		[Test]
		public void OnePadlockToMultipleResources()
		{
			Padlock p1 = new Padlock();
			ThreadShared<int> i1 = new ThreadShared<int>(0, p1);
			ThreadShared<int> i2 = new ThreadShared<int>(0, p1);

			//Can access both ThreadShared in same block...
			using (p1.Lock())
			{
				i1.Value++;
				i2.Value++;
			}

			//... or individually
			using (p1.Lock())
			{
				i1.Value++;
			}

			using (p1.Lock())
			{
				i2.Value++;
			}

			using(p1.Lock())
			{
				Assert.AreEqual(i1.Value, 2);
				Assert.AreEqual(i2.Value, 2);
				Assert.AreEqual(i1.Value, i2.Value);
			}
		}

		#endregion

		#region Throwing Exception tests

		/// <summary>
		/// Throws an exception whilst a lock is held to ensure that the lock
		/// is properly released.
		/// </summary>
		[Test]
		public void ExceptionThrownWhileLockHeld()
		{
			Padlock p1 = new Padlock();
			ThreadShared<int> i = new ThreadShared<int>(0, p1);

			try
			{
				using (p1.Lock())
				{
					i.Value++;
					throw new InvalidOperationException();
				}
			}
			catch (InvalidOperationException) {	}

			//Locks should have been released.  Create a new thread
			//to obtain the lock to ensure no deadlocks ensue.

			Thread secondThread = new Thread(delegate()
				{
					using (p1.Lock())
					{
						i.Value++;
					}
				});

			secondThread.Start();
			secondThread.Join();

			using (p1.Lock())
			{
				Assert.AreEqual(i.Value, 2);
			}
		}

		#endregion
	}
}
