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
	/// A set of tests to stress test the Padlock and ThreadShared
	/// classes to show up and problems in their locking strategies
	/// which would result in race conditions.
	/// </summary>
	[TestFixture]
	public class ThreadSharedAndPadlockThreadSafetyStressTests
	{
		#region Test class, Incrementor

		/// <summary>
		/// Simple interface so we can create safe and non-safe incrementor
		/// </summary>
		public interface IIncrementor
		{
			void IncrementI();
			void DecrementI();
			bool IsIEqualToZero { get; }
		}

		/// <summary>
		/// Non thread-safe object to increment a value
		/// </summary>
		public class NonSafeIncrementor : IIncrementor
		{
			private int _i = 0;

			public void IncrementI()
			{
				_i++;
			}

			public void DecrementI()
			{
				_i--;
			}

			public bool IsIEqualToZero
			{
				get { return _i == 0; }
			}
		}

		/// <summary>
		/// Thread-safe object to increment a value using Protected&lt;T&gt;
		/// </summary>
		public class SafeIncrementor : IIncrementor
		{
			Padlock p = new Padlock();
			private ThreadShared<int> _i;

			public SafeIncrementor()
			{
				_i = new ThreadShared<int>(0, p);
			}

			public void IncrementI()
			{
				using (p.Lock()) { _i.Value++; }
			}

			public void DecrementI()
			{
				using (p.Lock()) { _i.Value--; }
			}

			public bool IsIEqualToZero
			{
				get { using (p.Lock()) { return _i.Value == 0; } }
			}
		}

		#endregion

		#region Test class, List

		/// <summary>
		/// An interface so that we can create thread-safe and non-thread-safe
		/// lists to use to test against.
		/// </summary>
		public interface ISimpleList
		{
			void Add(object o);
			void Remove();
			int Count { get; }
		}

		/// <summary>
		/// An implementation of a list that is non thread-safe.
		/// </summary>
		public class NonSafeList : ISimpleList
		{
			private List<object> _list = new List<object>();

			public void Add(object o)
			{
				_list.Add(o);
			}

			public void Remove()
			{
				_list.RemoveAt(0);
			}

			public int Count
			{
				get { return _list.Count; }
			}
		}

		/// <summary>
		/// An implementation of a simple list that is thread safe.
		/// </summary>
		public class SafeList : ISimpleList
		{
			private Padlock p = new Padlock();
			private ThreadShared<List<object>> _list;

			public SafeList()
			{
				_list = new ThreadShared<List<object>>(new List<object>(), p);
			}

			public void Add(object o)
			{
				using (p.Lock()) { _list.Value.Add(o); }
			}

			public void Remove()
			{
				using (p.Lock()) { _list.Value.RemoveAt(0); }
			}

			public int Count
			{
				get { using (p.Lock()) { return _list.Value.Count; } }
			}
		
		}


		#endregion

		#region Threaded Stress tests

		/// <summary>
		/// Inserts and removes a set of items into a List protected by a Padlock and ThreadShared
		/// member using multiple threads.
		/// </summary>
		[Test]
		public void RunSimpleListThreaded()
		{
			int NUMBER_OF_ITERATIONS = 1000;
			int NUMBER_OF_THREADS_PER_OP = 10;
			int NUMBER_OF_STARTING_ITEMS = NUMBER_OF_ITERATIONS * NUMBER_OF_THREADS_PER_OP;

			//Change the below object from SafeList to
			//NonSafeList to see the test fail.
			ISimpleList list = new SafeList();

			//Prefill the list so that we KNOW that after all additions and removals
			//there will be the original number of items still in the list.
			//This is because doing a Remove on an empty list will not reduce the count.
			for (int i = 0; i < NUMBER_OF_STARTING_ITEMS; i++)
			{
				list.Add(new Object());
			}

			List<Thread> threads = new List<Thread>();

			for (int k = 0; k < NUMBER_OF_THREADS_PER_OP; k++)
			{
				threads.Add(new Thread(delegate()
					{
						Thread.Sleep(10); //Allow time for other threads to start
						for (int i = 0; i < NUMBER_OF_ITERATIONS; i++)
						{
							list.Add(new object());
						}
					})
				);
			}

			for (int k = 0; k < 10; k++)
			{
				threads.Add(new Thread(delegate()
					{
						Thread.Sleep(10); //Allow time for other threads to start
						for (int i = 0; i < NUMBER_OF_ITERATIONS; i++)
						{
							list.Remove();
						}
					})
				);
			}

			//Start all the threads
			foreach (Thread t in threads)
			{
				t.Start();
			}

			//Wait for them all to end
			foreach (Thread t in threads)
			{
				t.Join();
			}

			Assert.AreEqual(list.Count, NUMBER_OF_STARTING_ITEMS);
		}

		/// <summary>
		/// Incremenets and decrements a counter which is protected by a Padlock and ThreadShared
		/// member using multiple threads.
		/// </summary>
		[Test]
		public void RunIncrementorThreaded()
		{
			int NUMBER_OF_ITERATIONS = 100000;
			int NUMBER_OF_THREADS_PER_OP = 10;

			//Change the below objet from SafeIncrementor to
			//NonSafeIncrementor to see the test fail.
			IIncrementor nsi = new SafeIncrementor();

			List<Thread> threads = new List<Thread>();

			for (int k = 0; k < NUMBER_OF_THREADS_PER_OP; k++)
			{
				threads.Add(new Thread(delegate()
					{
						Thread.Sleep(10);//Allow time for other threads to start
						for (int i = 0; i < NUMBER_OF_ITERATIONS; i++)
						{
							nsi.IncrementI();
						}
					})
				);
			}

			for (int k = 0; k < NUMBER_OF_THREADS_PER_OP; k++)
			{
				threads.Add(new Thread(delegate()
					{
						Thread.Sleep(10);//Allow time for other threads to start
						for (int i = 0; i < NUMBER_OF_ITERATIONS; i++)
						{
							nsi.DecrementI();
						}
					})
				);
			}

			//Start all the threads
			foreach (Thread t in threads)
			{
				t.Start();
			}

			//Wait for them all to end
			foreach (Thread t in threads)
			{
				t.Join();
			}

			Assert.IsTrue(nsi.IsIEqualToZero);
		}

		#endregion
	}
}
