using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.Threading;

namespace Shared.Utilities.Tests.Threading
{
	/// <summary>
	/// A set of tests for the Padlock class
	/// </summary>
	[TestFixture]
	public class PadlockTests
	{
		#region Constructor tests

		/// <summary>
		/// Test to construct a padlock with no name supplied.
		/// </summary>
		[Test]
		public void Constructor_ConstructWithNoName()
		{
			Padlock p1 = new Padlock();
			Assert.IsTrue(p1.Name.StartsWith("Padlock-"));
		}

		/// <summary>
		/// Test to construct a padlock by supplying a name.
		/// </summary>
		[Test]
		public void Constructor_ConstructWithName()
		{
			Padlock p1 = new Padlock("PadlockX");
			Assert.IsTrue(p1.Name.Equals("PadlockX"));
		}

		#endregion
	}
}
