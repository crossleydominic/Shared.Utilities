using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities;

namespace Shared.Utilities.Tests
{
	[TestFixture]
	public class SwitcherTests
	{
		#region Do Tests

		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentNullException))]
		public void Do_NullTestCases()
		{
			DateTime dt = DateTime.Parse("01/01/1970");

			Switcher.SwitchCase[] cases = null;
			Switcher.Do(dt, cases);
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void Do_NullTestCaseInCollection()
		{
			DateTime dt = DateTime.Parse("01/01/1970");

			Switcher.Do(dt,
				Switcher.Case(DateTime.Parse("01/01/1970"),
					() => { }),
				null,
				Switcher.Case(DateTime.Parse("02/01/1970"),
					() => { })
				);
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void Do_NullActionInCase()
		{
			DateTime dt = DateTime.Parse("01/01/1970");

			Switcher.Do(dt,
				Switcher.Case(DateTime.Parse("01/01/1970"),
					() => { }),
				Switcher.Case(DateTime.Parse("02/01/1970"),
					null)
				);
		}

		[Test]
		public void Do_NullValueInCase()
		{
			string obj = null;

			int caseSelected = -1;
			Switcher.Do(obj,
				Switcher.Case("str1",
					() => { caseSelected = 0; }),
				Switcher.Case(null,
					() => { caseSelected = 1; })
				);

			Assert.AreEqual(caseSelected, 1);
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void Do_MultipleDefaultCases()
		{
			DateTime dt = DateTime.Parse("01/01/1970");

			Switcher.Do(dt,
				Switcher.Case(DateTime.Parse("01/01/1970"),
					() => { }),
				Switcher.Case(DateTime.Parse("02/01/1970"),
					() => { }),
				Switcher.Default(
					()=>{}),
				Switcher.Default(
					()=>{})
				);
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void Do_FallThroughCaseIsLastCase()
		{
			DateTime dt = DateTime.Parse("01/01/1970");

			Switcher.Do(dt,
				Switcher.Case(DateTime.Parse("01/01/1970")),
				Switcher.Case(DateTime.Parse("02/01/1970"),
					()=>{}),
				Switcher.Case(DateTime.Parse("03/01/1970")),
				Switcher.Case(DateTime.Parse("04/01/1970"),
					()=>{}),
				Switcher.Case(DateTime.Parse("01/01/1970"))
				);
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void Do_DefaultCaseIsNotLast()
		{
			DateTime dt = DateTime.Parse("01/01/1970");

			Switcher.Do(dt,
				Switcher.Case(DateTime.Parse("01/01/1970"),
					() => { }),
				Switcher.Default(
					() => { }),
				Switcher.Case(DateTime.Parse("02/01/1970"),
					() => { })
				);
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void Do_SingleDefaultTestCases()
		{
			DateTime dt = DateTime.Parse("01/01/1970");

			Switcher.Do(dt,
				Switcher.Default(() => { })
			);
		}

		[Test]
		public void Do_SingleCase()
		{
			DateTime dt = DateTime.Parse("01/01/1970");

			int caseSelected = -1;
			Switcher.Do(dt,
				Switcher.Case(DateTime.Parse("01/01/1970"),
					() => { caseSelected = 0; })
			);

			Assert.AreEqual(caseSelected, 0);
		}

		[Test]
		public void Do_MultipleCases_FirstCaseSelected()
		{
			DateTime dt = DateTime.Parse("01/01/1970");

			int caseSelected = -1;
			Switcher.Do(dt,
				Switcher.Case(DateTime.Parse("01/01/1970"),
					() => { caseSelected = 0; }),
				Switcher.Case(DateTime.Parse("02/01/1970"),
					() => { caseSelected = 1; }),
				Switcher.Case(DateTime.Parse("03/01/1970"),
					() => { caseSelected = 2; })
			);

			Assert.AreEqual(caseSelected, 0);
		}

		[Test]
		public void Do_MultipleCases_MiddleCaseSelected()
		{
			DateTime dt = DateTime.Parse("02/01/1970");

			int caseSelected = -1;
			Switcher.Do(dt,
				Switcher.Case(DateTime.Parse("01/01/1970"),
					() => { caseSelected = 0; }),
				Switcher.Case(DateTime.Parse("02/01/1970"),
					() => { caseSelected = 1; }),
				Switcher.Case(DateTime.Parse("03/01/1970"),
					() => { caseSelected = 2; })
			);

			Assert.AreEqual(caseSelected, 1);
		}

		[Test]
		public void Do_MultipleCases_LastCaseSelected()
		{
			DateTime dt = DateTime.Parse("03/01/1970");

			int caseSelected = -1;
			Switcher.Do(dt,
				Switcher.Case(DateTime.Parse("01/01/1970"),
					() => { caseSelected = 0; }),
				Switcher.Case(DateTime.Parse("02/01/1970"),
					() => { caseSelected = 1; }),
				Switcher.Case(DateTime.Parse("03/01/1970"),
					() => { caseSelected = 2; })
			);

			Assert.AreEqual(caseSelected, 2);
		}

		[Test]
		public void Do_MultipleCases_DefaultCaseSelected()
		{
			DateTime dt = DateTime.Parse("04/01/1970");

			int caseSelected = -1;
			Switcher.Do(dt,
				Switcher.Case(DateTime.Parse("01/01/1970"),
					() => { caseSelected = 0; }),
				Switcher.Case(DateTime.Parse("02/01/1970"),
					() => { caseSelected = 1; }),
				Switcher.Case(DateTime.Parse("03/01/1970"),
					() => { caseSelected = 2; }),
				Switcher.Default(
					() => { caseSelected = 3; })
			);

			Assert.AreEqual(caseSelected, 3);
		}

		[Test]
		public void Do_MultipleCases_CaseDuplicated()
		{
			DateTime dt = DateTime.Parse("02/01/1970");

			int caseSelected = -1;

			//Switch statement contains multiple cases that do the same
			//comparison.  Switcher will select the first one.
			Switcher.Do(dt,
				Switcher.Case(DateTime.Parse("01/01/1970"),
					() => { caseSelected = 0; }),
				Switcher.Case(DateTime.Parse("02/01/1970"),
					() => { caseSelected = 1; }),
				Switcher.Case(DateTime.Parse("02/01/1970"),
					() => { caseSelected = 2; }),
				Switcher.Case(DateTime.Parse("02/01/1970"),
					() => { caseSelected = 3; })
			);

			Assert.AreEqual(caseSelected, 1);
		}

		[Test]
		public void Do_MultipleCases_NoCasesSelected()
		{
			DateTime dt = DateTime.Parse("05/01/1970");

			int caseSelected = -1;

			Switcher.Do(dt,
				Switcher.Case(DateTime.Parse("01/01/1970"),
					() => { caseSelected = 0; }),
				Switcher.Case(DateTime.Parse("02/01/1970"),
					() => { caseSelected = 1; }),
				Switcher.Case(DateTime.Parse("03/01/1970"),
					() => { caseSelected = 2; }),
				Switcher.Case(DateTime.Parse("04/01/1970"),
					() => { caseSelected = 3; })
			);

			Assert.AreEqual(caseSelected, -1);
		}

		[Test]
		public void Do_FallThrough_FallIntoCase()
		{
			DateTime dt = DateTime.Parse("01/01/1970");

			int caseSelected = -1;
			Switcher.Do(dt,
				Switcher.Case(DateTime.Parse("01/01/1970")),
				Switcher.Case(DateTime.Parse("02/01/1970"),
					() => { caseSelected = 1; }),
				Switcher.Case(DateTime.Parse("03/01/1970"),
					() => { caseSelected = 2; }),
				Switcher.Default(
					() => { caseSelected = 3; })
			);

			Assert.AreEqual(caseSelected, 1);
		}

		[Test]
		public void Do_FallThrough_FallIntoDefault()
		{
			DateTime dt = DateTime.Parse("04/01/1970");

			int caseSelected = -1;
			Switcher.Do(dt,
				Switcher.Case(DateTime.Parse("01/01/1970")),
				Switcher.Case(DateTime.Parse("02/01/1970"),
					() => { caseSelected = 1; }),
				Switcher.Case(DateTime.Parse("03/01/1970")),
				Switcher.Default(
					() => { caseSelected = 2; })
			);

			Assert.AreEqual(caseSelected, 2);
		}

		#endregion

		#region MustDo Tests

		[Test]
		[ExpectedException(ExpectedException=typeof(InvalidOperationException))]
		public void MustDo_MultipleCases_NoCasesSelected()
		{
			DateTime dt = DateTime.Parse("05/01/1970");

			int caseSelected = -1;

			Switcher.MustDo(dt,
				Switcher.Case(DateTime.Parse("01/01/1970"),
					() => { caseSelected = 0; }),
				Switcher.Case(DateTime.Parse("02/01/1970"),
					() => { caseSelected = 1; }),
				Switcher.Case(DateTime.Parse("03/01/1970"),
					() => { caseSelected = 2; }),
				Switcher.Case(DateTime.Parse("04/01/1970"),
					() => { caseSelected = 3; })
			);

			Assert.AreEqual(caseSelected, -1);
		}

		[Test]
		public void MustDo_MultipleCases_DefaultCaseSelected()
		{
			DateTime dt = DateTime.Parse("05/01/1970");

			int caseSelected = -1;

			Switcher.MustDo(dt,
				Switcher.Case(DateTime.Parse("01/01/1970"),
					() => { caseSelected = 0; }),
				Switcher.Case(DateTime.Parse("02/01/1970"),
					() => { caseSelected = 1; }),
				Switcher.Case(DateTime.Parse("03/01/1970"),
					() => { caseSelected = 2; }),
				Switcher.Case(DateTime.Parse("04/01/1970"),
					() => { caseSelected = 3; }),
				Switcher.Default(
					() => { caseSelected = 4; })
			);

			Assert.AreEqual(caseSelected, 4);
		}

		#endregion
	}
}
