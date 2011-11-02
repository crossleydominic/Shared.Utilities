using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using log4net;
using Shared.Utilities.ExtensionMethods.Logging;
using Shared.Utilities.ExtensionMethods.Primitives;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using System.Reflection;

namespace Shared.Utilities.Tests.ExtensionMethods.Logging
{
	[TestFixture]
	public class ILogExtensionsTests
	{
		public static TestAppender _testAppender = new TestAppender();

		public static ILog _log = LogManager.GetLogger(typeof(ILogExtensions));

		#region Test appender class

		public class TestAppender : IAppender
		{
			private string _lastMessageLogged = string.Empty;

			public void Close() { }

			public void DoAppend(log4net.Core.LoggingEvent loggingEvent)
			{
				_lastMessageLogged = loggingEvent.RenderedMessage;
			}

			public string Name
			{
				get { return typeof(TestAppender).FullName; }
				set { }
			}

			public string LastLoggedMessage { get { return _lastMessageLogged; } }
		}

		#endregion

		#region Setup

		[SetUp]
		public void BeforeAnyTestsRun()
		{
			//Change the default logger to our TestAppender so that 
			//we can run unit tests on the ILogExtensions ans examine
			//their output.

			GlobalAssemblyTestSetup.ShutDownAllRepositories();

			_testAppender = new TestAppender();

			log4net.Config.BasicConfigurator.Configure(_testAppender);
		}

		#endregion

		#region TearDown

		[TearDown]
		public void AfterAllTestsRun()
		{
			//Reconfigure the original ConsoleAppender so that when 
			//the other tests run their logging is spat out as normal

			GlobalAssemblyTestSetup.ConfigureConsoleAppendar();
		}

		#endregion

		#region DebugMethodCalled tests

		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentNullException))]
		public void DebugMethodCalled_ILogNull()
		{
			ILog logger = null;
			logger.DebugMethodCalled();
		}

		[Test]
		public void DebugMethodCalled_MethodNameExists()
		{
			_log.DebugMethodCalled();
			Assert.IsTrue(_testAppender.LastLoggedMessage.ContainsIgnoreCase(MethodBase.GetCurrentMethod().Name));
		}

		[Test]
		public void DebugMethodCalled_MethodCalledPhraseExists()
		{
			_log.DebugMethodCalled();
			Assert.IsTrue(_testAppender.LastLoggedMessage.ContainsIgnoreCase("method"));
			Assert.IsTrue(_testAppender.LastLoggedMessage.ContainsIgnoreCase("called"));
		}

		[Test]
		[TestCase("stringValue1", 5)]
		[TestCase("stringValue2", 6)]
		public void DebugMethodCalled_ArgumentValuesExist(string arg1, int arg2)
		{
			_log.DebugMethodCalled(arg1, arg2);
			Assert.IsTrue(_testAppender.LastLoggedMessage.Contains(arg1));
			Assert.IsTrue(_testAppender.LastLoggedMessage.Contains(arg2.ToString()));
		}

		[Test]
		[TestCase(null)]
		public void DebugMethodCalled_ArgumentValuesExist(string arg1)
		{
			_log.DebugMethodCalled(arg1);
			Assert.IsTrue(_testAppender.LastLoggedMessage.Contains("null"));
		}

		[Test]
		[TestCase(new int[] { 1, 2 })]
		public void DebugMethodCalled_EnumerablesAreExpanded(int[] arg1)
		{
			_log.DebugMethodCalled(EnumerableExpansion.Expand, arg1);
			Assert.IsTrue(_testAppender.LastLoggedMessage.Contains("1"));
			Assert.IsTrue(_testAppender.LastLoggedMessage.Contains("2"));
		}

		[Test]
		[TestCase(new int[]{1,2})]
		public void DebugMethodCalled_EnumerablesAreNotExpanded(int[] arg1)
		{
			_log.DebugMethodCalled(EnumerableExpansion.DoNotExpand, arg1);
			Assert.IsTrue(_testAppender.LastLoggedMessage.Contains("System.Int32[]"));
		}

		#endregion

		#region DebugMethodReturning tests

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void DebugMethodReturning_ILogNull()
		{
			ILog logger = null;
			logger.DebugMethodCalled();
		}

		[Test]
		public void DebugMethodReturning_MethodCalledPhraseExists()
		{
			_log.DebugMethodReturning("str");
			Assert.IsTrue(_testAppender.LastLoggedMessage.ContainsIgnoreCase("method"));
			Assert.IsTrue(_testAppender.LastLoggedMessage.ContainsIgnoreCase("returning"));
		}

		[Test]
		public void DebugMethodReturning_MethodNameExists()
		{
			_log.DebugMethodReturning(1);
			Assert.IsTrue(_testAppender.LastLoggedMessage.Contains(MethodBase.GetCurrentMethod().Name));
		}

		[Test]
		public void DebugMethodReturning_ArgumentValuesExist()
		{
			_log.DebugMethodReturning("val");
			Assert.IsTrue(_testAppender.LastLoggedMessage.Contains("val"));
		}

		[Test]
		[TestCase(null)]
		public void DebugMethodReturning_ArgumentValuesExist(string arg1)
		{
			_log.DebugMethodReturning(arg1);
			Assert.IsTrue(_testAppender.LastLoggedMessage.Contains("null"));
		}

		[Test]
		[TestCase(new int[] { 1, 2 })]
		public void DebugMethodReturning_EnumerablesAreExpanded(int[] arg1)
		{
			_log.DebugMethodReturning(EnumerableExpansion.Expand, arg1);
			Assert.IsTrue(_testAppender.LastLoggedMessage.Contains("1"));
			Assert.IsTrue(_testAppender.LastLoggedMessage.Contains("2"));
		}

		[Test]
		[TestCase(new int[] { 1, 2 })]
		public void DebugMethodReturning_EnumerablesAreNotExpanded(int[] arg1)
		{
			_log.DebugMethodReturning(EnumerableExpansion.DoNotExpand, arg1);
			Assert.IsTrue(_testAppender.LastLoggedMessage.Contains("System.Int32[]"));
		}

		#endregion

		#region DebugDumpArguments tests

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void DebugDumpArguments_ILogNull()
		{
			ILog logger = null;
			logger.DebugDumpArguments();
		}

		[Test]
		public void DebugDumpArguments_ArgumentValuesExist()
		{
			_log.DebugDumpArguments("val");
			Assert.IsTrue(_testAppender.LastLoggedMessage.Contains("val"));
		}

		[Test]
		[TestCase(null)]
		public void DebugDumpArguments_ArgumentValuesExist(string arg1)
		{
			_log.DebugDumpArguments(arg1);
			Assert.IsTrue(_testAppender.LastLoggedMessage.Contains("null"));
		}

		[Test]
		[TestCase(new int[] { 1, 2 })]
		public void DebugDumpArguments_EnumerablesAreExpanded(int[] arg1)
		{
			_log.DebugDumpArguments(EnumerableExpansion.Expand, arg1);
			Assert.IsTrue(_testAppender.LastLoggedMessage.Contains("1"));
			Assert.IsTrue(_testAppender.LastLoggedMessage.Contains("2"));
		}

		[Test]
		[TestCase(new int[] { 1, 2 })]
		public void DebugDumpArguments_EnumerablesAreNotExpanded(int[] arg1)
		{
			_log.DebugDumpArguments(EnumerableExpansion.DoNotExpand, arg1);
			Assert.IsTrue(_testAppender.LastLoggedMessage.Contains("System.Int32[]"));
		}

		#endregion

	}
}
