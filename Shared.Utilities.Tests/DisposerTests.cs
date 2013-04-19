using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using System.Runtime.InteropServices;
using System.Data;

namespace Shared.Utilities.Tests
{
	[TestFixture]
	public class DisposerTests
	{
		#region Test Classes

		private class TestClass
		{
			private bool _isClosed;
			int _closeCounter;

			public TestClass()
			{
				_isClosed = false;
				_closeCounter = 0;
			}

			public void Close()
			{
				_isClosed = true;
				_closeCounter++;
			}

			public void CloseWithException()
			{
				Close();
				throw new ApplicationException();
			}

			public void CloseWithCountException()
			{
				Close();
				if (_closeCounter > 1)
				{
					throw new ApplicationException();
				}
			}

			public bool IsClosed
			{
				get
				{
					return
						_isClosed;
				}
			}
		}

		#endregion
        
		#region Create Tests

		/// <summary>
		/// Tests when the object instance is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void Create_ValueNull()
		{
			using (Disposer<TestClass> tc = Disposer<TestClass>.Create(null, (t) => { }))
			{

			}
		}

		/// <summary>
		/// Tests when the dispose action is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void Create_ActionNull()
		{
			using (Disposer<TestClass> tc = Disposer<TestClass>.Create(new TestClass(), null))
			{

			}
		}

		/// <summary>
		/// tests when both the object instance and dispose action are both null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void Create_ValueAndActionNull()
		{
			using (Disposer<TestClass> tc = Disposer<TestClass>.Create(null, null))
			{

			}
		}

        /// <summary>
        /// tests when both the object instance and dispose action are both null
        /// </summary>
        [Test]
        public void Create_EntryActionExecuted()
        {
            TestClass tc = new TestClass();
            bool startupActionCalled = false;
            bool disposeActionCalled = false;
            using (Disposer<TestClass> subject = Disposer<TestClass>.Create(
                tc,
                t => startupActionCalled = true,
                t => disposeActionCalled = true))
            {}

            Assert.IsTrue(startupActionCalled);
            Assert.IsTrue(disposeActionCalled);
        }

		#endregion

		#region Dispose Tests

		/// <summary>
		/// Tests to ensure that the dispose action is called when Dispose is 
		/// invoked explicitly
		/// </summary>
		[Test]
		public void Dispose_CalledExplicitly()
		{
            Disposer<TestClass> tc = Disposer<TestClass>.Create(
				new TestClass(),
				(t) => { t.Close(); });
			TestClass rawTc = tc.Value;

			tc.Dispose();

			Assert.IsTrue(tc.IsDisposed);
			Assert.IsTrue(rawTc.IsClosed);
		}

		/// <summary>
		/// Tests to ensure that Dispose can be called more than once without
		/// throwing an exception.
		/// </summary>
		[Test]
		public void Dispose_CalledExplicitlyMoreThanOnce()
		{
            Disposer<TestClass> tc = Disposer<TestClass>.Create(
				new TestClass(),
				(t) => { t.Close(); });

			TestClass rawTc = tc.Value;

			//Dispose should be callable multiple times without throwing an error.
			tc.Dispose();
			tc.Dispose();

			Assert.IsTrue(tc.IsDisposed);
			Assert.IsTrue(rawTc.IsClosed);
		}

		/// <summary>
		/// Test to make sure that the dispose action is invoked
		/// when in a Using block
		/// </summary>
		[Test]
		public void Dispose_CalledFromUsingBlock()
		{
			Disposer<TestClass> tc;
			TestClass rawTc;
            using (tc = Disposer<TestClass>.Create(
				new TestClass(),
				(t) => { t.Close(); }))
			{
				rawTc = tc.Value;
			}

			Assert.IsTrue(tc.IsDisposed);
			Assert.IsTrue(rawTc.IsClosed);
		}

		/// <summary>
		/// Test of functionality when the the dispose action
		/// causes an exception to be thrown
		/// </summary>
		[Test]
		public void Dispose_DisposeCausesException()
		{
			Disposer<TestClass> tc = null;
			try
			{
                using (tc = Disposer<TestClass>.Create(
					new TestClass(),
					(t) => { t.CloseWithException(); }))
				{

				}
			}
			catch (ApplicationException){ }

			Assert.IsTrue(tc.IsDisposed);
		}

		#endregion

		#region Value Tests

		/// <summary>
		/// Test to make sure that the value isn't retrievable once the 
		/// Disposer has been disposed.
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException=typeof(ObjectDisposedException))]
		public void Value_ValueIsDisposed()
		{
			Disposer<TestClass> tc = null;
            using (tc = Disposer<TestClass>.Create(new TestClass(), (t) => { }))
			{

			}

			TestClass rawTc = tc.Value;
		}

		#endregion
	}
}
