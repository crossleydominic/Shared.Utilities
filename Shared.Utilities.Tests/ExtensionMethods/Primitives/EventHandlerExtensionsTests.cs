using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.ExtensionMethods.Primitives;

namespace Shared.Utilities.Tests.ExtensionMethods.Primitives
{
	[TestFixture]
	public class EventHandlerExtensionsTests
	{
		#region Test classes

		public class TestEventArgs : EventArgs{}

		public class TestEventClass
		{
			public event EventHandler<TestEventArgs> TestEvent;

			public void InvokeEvent()
			{
				TestEvent.Raise(this, new TestEventArgs());
			}

			public void InvokeWithoutSender()
			{
				TestEvent.Raise(null, new TestEventArgs());
			}
		}

		#endregion

		#region Raise Tests

		/// <summary>
		/// Test to make sure that calling Raise will not throw an exception if the 
		/// event has no subscribers
		/// </summary>
		[Test]
		public void Raise_EventHasNoSubscribers()
		{
			TestEventClass tec = new TestEventClass();
			tec.InvokeEvent();
		}

		/// <summary>
		/// Test to make sure that the Raise method invokes the subscriber
		/// </summary>
		[Test]
		public void Raise_EventHasOneSubscriber()
		{
			TestEventClass tec = new TestEventClass();

			int counter = 0;

			tec.TestEvent += (o, e) => { counter += 1; };
			tec.InvokeEvent();

			Assert.AreEqual(counter, 1);
		}

		/// <summary>
		/// Test to make sure that the Raise method invokes all of the subscribers
		/// </summary>
		[Test]
		public void Raise_EventHasMultipleSubscriber()
		{
			TestEventClass tec = new TestEventClass();

			int counter = 0;

			tec.TestEvent += (o, e) => { counter += 1; };
			tec.TestEvent += (o, e) => { counter += 1; };
			tec.InvokeEvent();

			Assert.AreEqual(counter, 2);
		}

		/// <summary>
		/// Test to ensure that Raise cannot be called when the sender argument is
		/// set to null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentNullException))]
		public void Raise_EventRaisedWithNoSender()
		{
			TestEventClass tec = new TestEventClass();
			tec.InvokeWithoutSender();
		}

		#endregion
	}
}
