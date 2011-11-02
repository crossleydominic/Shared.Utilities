using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading;
using Shared.Utilities.Threading;

namespace Shared.Utilities.Tests.Threading
{
	/// <summary>
	/// Some tests for the ThreadRunner class
	/// </summary>
	[TestFixture]
	public class ThreadRunnerTests
	{
		#region RunInSTA Tests

		/// <summary>
		/// Tests to make sure that the delegate is run in an STA when created from and STA
		/// </summary>
		[Test]
		public void RunInSTA_EnsureRunInSTAFromSTA()
		{
			bool wasRunInSTA = RunInApartmentFromApartment(ApartmentState.STA, ApartmentState.STA);

			Assert.IsTrue(wasRunInSTA);
		}

		/// <summary>
		/// Tests to make sure that the delegate is run in a STA when created from a MTA
		/// </summary>
		[Test]
		public void RunInSTA_EnsureRunInSTAFromMTA()
		{
			bool wasRunInSTA = RunInApartmentFromApartment(ApartmentState.MTA, ApartmentState.STA);

			Assert.IsTrue(wasRunInSTA);
		}

		#endregion

		#region RunInMTA Tests

		/// <summary>
		/// Tests to make sure that the delegate is run in an MTA when created from and STA
		/// </summary>
		[Test]
		public void RunInMTA_EnsureRunInMTAFromSTA()
		{
			bool wasRunInMTA = RunInApartmentFromApartment(ApartmentState.STA, ApartmentState.MTA);

			Assert.IsTrue(wasRunInMTA);
		}

		/// <summary>
		/// Tests to make sure that the delegate is run in a MTA when created from a MTA
		/// </summary>
		[Test]
		public void RunInMTA_EnsureRunInMTAFromMTA()
		{
			bool wasRunInMTA = RunInApartmentFromApartment(ApartmentState.MTA, ApartmentState.MTA);

			Assert.IsTrue(wasRunInMTA);
		}

		#endregion

		#region Private common methods

		/// <summary>
		/// A common helper method that tests running a test in an apartment from an apartment.
		/// </summary>
		private bool RunInApartmentFromApartment(ApartmentState fromApartMent, ApartmentState inApartment)
		{
			bool wasRunInCorrectApartment = false;

			Thread runnerThread = new Thread((ThreadStart)delegate
			{
				if (inApartment == ApartmentState.MTA)
				{
					ThreadRunner.RunInMTA(delegate
					{
						wasRunInCorrectApartment = Thread.CurrentThread.GetApartmentState() == inApartment;
					});
				}
				else if (inApartment == ApartmentState.STA)
				{
					ThreadRunner.RunInSTA(delegate
					{
						wasRunInCorrectApartment = Thread.CurrentThread.GetApartmentState() == inApartment;
					});
				}
			}
			);
			runnerThread.SetApartmentState(fromApartMent);
			runnerThread.Start();
			runnerThread.Join();

			return wasRunInCorrectApartment;
		}

		#endregion

	}
}
