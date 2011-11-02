using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading;

namespace Shared.Utilities.Tests
{
	/// <summary>
	/// A set of tests for the StaticRandom class
	/// </summary>
	[TestFixture]
	public class StaticRandomTests
	{
		#region Thread-safety tests

		/// <summary>
		/// A test that will call StaticRandom from multiple threads
		/// to ensure that it is thread safe. Any non-safe access should
		/// throw an exception.
		/// </summary>
		[Test]
		public static void TestThreadSafety()
		{
			int NUMBER_OF_THREADS = 5;
			int NUMBER_OF_OPS = 100000;

			List<Thread> threads = new List<Thread>();

			ThreadStart threadDelegate = (ThreadStart)delegate()
			{
				Thread.Sleep(0);
				byte[] buffer = new byte[1];
				for (int i = 0; i < NUMBER_OF_OPS; i++)
				{
					StaticRandom.Next();
					StaticRandom.Next(100);
					StaticRandom.Next(10, 100);
					StaticRandom.NextDouble();
					StaticRandom.NextBytes(buffer);
					StaticRandom.NextString(1, true, true);
				}
			};

			for (int i = 0; i < NUMBER_OF_THREADS; i++)
			{
				threads.Add(new Thread(threadDelegate));
			}

			foreach (Thread t in threads)
			{
				t.Start();
			}

			foreach (Thread t in threads)
			{
				t.Join();
			}
		}

		#endregion
	}
}
