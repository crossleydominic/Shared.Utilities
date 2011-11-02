using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository;

//DO NOT PUT THIS CLASS IN A NAMESPACE
//IF THIS CLASS EXISTS OUTSIDE OF A NAMESPACE THEN NUNIT WILL CALL
//THE BELOW SETUP MESSAGE ONCE FOR THE ENTIRE ASSEMBLY, WHICH IS 
//WHAT WE WANT SO WE CAN TURN DEBUGGING ON.

/// <summary>
/// A class to provide one-time setup for the entire assembly.
/// Used to enable debugging.
/// </summary>
[SetUpFixture]
public class GlobalAssemblyTestSetup
{
	[SetUp]
	public void BeforeAnyTestsRun()
	{
		//We'll create a logger so that we can log out all of the
		//tests.

		//TODO: MAYBE MOVE THIS CONFIGURATION INTO AN XML FILE AT SOME POINT
		ConsoleAppender console = new ConsoleAppender();
		console.Layout = new PatternLayout();
		console.Threshold = log4net.Core.Level.Debug;

		//Ensure any repositories are shutdown before creating a new one.
		ShutDownAllRepositories();

		log4net.Config.BasicConfigurator.Configure(console);
	}

	[TearDown]
	public void AfterAllTestsRun()
	{
		ShutDownAllRepositories();
	}

	private void ShutDownAllRepositories()
	{
		foreach (ILoggerRepository rep in log4net.LogManager.GetAllRepositories())
		{
			rep.Shutdown();
		}
	}
}

