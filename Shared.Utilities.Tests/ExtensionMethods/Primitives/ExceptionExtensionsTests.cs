using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.ExtensionMethods.Primitives;
using System.Diagnostics;

namespace Shared.Utilities.Tests.ExtensionMethods.Primitives
{
	[TestFixture]
	public class ExceptionExtensionsTests
	{
		#region AllMessages Tests

		/// <summary>
		/// Test when the exception object is null.
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentNullException))]
		public void AllMessages_ExceptionNull()
		{
			Exception e = null;
			e.AllMessages();
		}

		/// <summary>
		/// Test when creating the message for a single, non-nested exception
		/// </summary>
		[Test]
		public void AllMessages_SingleException()
		{
			string messageWithoutStack = null;
			string messageWithStack = null;
			try
			{
				throw new ArgumentException("Test message");
			}
			catch (ArgumentException ae)
			{
				messageWithoutStack = ae.AllMessages(false);
				messageWithStack = ae.AllMessages();
			}

			Assert.IsTrue(messageWithStack.IndexOf("Test message") != -1);
			Assert.IsTrue(messageWithoutStack.IndexOf("Test message") != -1);

			Assert.IsTrue(messageWithStack.IndexOf((new StackFrame()).GetMethod().Name) != -1);
		}

		/// <summary>
		/// Test when creating a message for a set of nested exception objects.
		/// </summary>
		[Test]
		public void AllMessages_NestedException()
		{
			string messageWithoutStack = null;
			string messageWithStack = null;
			try
			{
				try
				{
					try
					{
						throw new ArgumentException("Inner message");
					}
					catch (ArgumentException aeInner)
					{
						throw new ArgumentException("Middle message", aeInner);
					}
				}
				catch (ArgumentException aeMiddle)
				{
					throw new ArgumentException("Outer message", aeMiddle);
				}
			}
			catch (ArgumentException ae)
			{
				messageWithoutStack = ae.AllMessages(false);
				messageWithStack = ae.AllMessages();
			}

			Assert.IsTrue(messageWithStack.IndexOf("Outer message") != -1);
			Assert.IsTrue(messageWithStack.IndexOf("Middle message") != -1);
			Assert.IsTrue(messageWithStack.IndexOf("Inner message") != -1);

			Assert.IsTrue(messageWithoutStack.IndexOf("Outer message") != -1);
			Assert.IsTrue(messageWithoutStack.IndexOf("Middle message") != -1);
			Assert.IsTrue(messageWithoutStack.IndexOf("Inner message") != -1);

			Assert.IsTrue(messageWithStack.IndexOf((new StackFrame()).GetMethod().Name) != -1);
		}

		#endregion
	}
}
