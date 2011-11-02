using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;

namespace Shared.Utilities
{
	/// <summary>
	/// A class that replicates a switch{..} statement but can be used
	/// with types that a switch statement does not support.
	/// 
	/// There is little point in this class really, an if-elseif-else 
	/// statement would do the same job. I only implemented it as an
	/// intellectual exercise.
	/// 
	/// EXPERIMENTAL - DO NOT USE.
	/// </summary>
	internal sealed class Switcher
	{
		#region Internal classes

		/// <summary>
		/// A simple class that represents a Case in a switch statment.
		/// </summary>
		public class SwitchCase
		{
			#region Private members

			/// <summary>
			/// The value that will be tested against for this case
			/// </summary>
			private object _valueToTest;

			/// <summary>
			/// The action to perform if this case is satisfied
			/// </summary>
			private Action _actionToPerform;

			/// <summary>
			/// Whether or not this case will be treated as a fall-through case. e.g
			/// 
			/// switch(someString)
			/// {
			///		case "a":	//This is a fall-through case.
			///		case "b":
			///			DoSomething();
			///			break;
			/// }
			/// </summary>
			private bool _isFallThroughCase;

			/// <summary>
			/// Whether or not this case should be treated as the default case. e.g
			/// 
			/// switch(someString)
			/// {
			///		case "a":
			///			DoSomething();
			///			break;
			///		default:	//This is the default case
			///			DoSomethingElse();
			///			break;
			/// }
			/// </summary>
			private bool _isDefaultCase;

			#endregion

			#region Constructors

			/// <summary>
			/// Create a new SwitchCase that will be treated as a default case
			/// </summary>
			/// <param name="actionToPerform">
			/// The action that will get invoked if this case is satisfied
			/// </param>
			internal SwitchCase(Action actionToPerform) : this(null, actionToPerform, true) { }

			/// <summary>
			/// Create a new SwitchCase that will be treated as a fall-through case
			/// </summary>
			/// <param name="valueToTest">
			/// The value to test against
			/// </param>
			internal SwitchCase(object valueToTest) : this(valueToTest, null, false) { }

			/// <summary>
			/// Create a new SwitchCase that will be treated as a normal case
			/// </summary>
			/// <param name="valueToTest">
			/// The value to test against
			/// </param>
			/// <param name="actionToPerform">
			/// The action that will get invoked if this case is satisfied
			/// </param>
			internal SwitchCase(object valueToTest, Action actionToPerform) : this(valueToTest, actionToPerform, false) { }

			/// <summary>
			/// Creates a new SwitchCase in any of Normal, Default or Fall-through modes
			/// </summary>
			/// <param name="valueToTest">
			/// The value to test against
			/// </param>
			/// <param name="actionToPerform">
			/// The action that will get invoked if this case is satisfied
			/// </param>
			/// <param name="createAsDefault">
			/// Whether or not this case should be treated as the default case.
			/// </param>
			private SwitchCase(object valueToTest, Action actionToPerform, bool createAsDefault)
			{
				if (createAsDefault)
				{
					_valueToTest = null;
					_actionToPerform = actionToPerform;
					_isFallThroughCase = false;
					_isDefaultCase = true;
				}
				else
				{
					_valueToTest = valueToTest;
					_actionToPerform = actionToPerform;
					_isFallThroughCase = (_actionToPerform == null);
					_isDefaultCase = false;
				}
			}

			#endregion

			#region Internal members

			/// <summary>
			/// Gets the value that we want to test against
			/// </summary>
			internal object ValueToTest
			{
				get
				{
					return _valueToTest;
				}
			}

			/// <summary>
			/// Gets/sets the action that will be performed if this
			/// case is satisfied
			/// </summary>
			internal Action ActionToPerform
			{
				get
				{
					return _actionToPerform;
				}
				set
				{
					_actionToPerform = value;
				}
			}

			/// <summary>
			/// Gets whether or not this case is a fall-through case.
			/// </summary>
			internal bool IsFallThroughCase
			{
				get
				{
					return _isFallThroughCase;
				}
			}

			/// <summary>
			/// Gets whether or not this case is a default case.
			/// </summary>
			internal bool IsDefaultCase
			{
				get
				{
					return _isDefaultCase;
				}
			}

			#endregion
		}

		#endregion

		#region Public static methods

		/// <summary>
		/// Executes all of the supplied cases against the supplied value and
		/// invokes any actions for the first satisfied case.
		/// An exception is generated if non of the cases match.
		/// </summary>
		/// <param name="value">
		/// The value to test for
		/// </param>
		/// <param name="cases">
		/// The set of cases to test against
		/// </param>
		/// <exception cref="System.ArgumentException">
		/// Thrown if 
		///		cases contains a null element
		///		cases contains less than 1 case
		///		default is the only supplied case
		///		default is not the last case
		///		a default case is specified more than once.
		///		a fall-through case is the last case
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if
		///		cases is null
		/// </exception>
		/// <exception cref="System.InvalidOperationException">
		/// Thrown if non of the cases match the supplied value.
		/// </exception>
		public static void MustDo(object value, params SwitchCase[] cases)
		{
			Do(value, true, cases);
		}

		/// <summary>
		/// Executes all of the supplied cases against the supplied value and
		/// invokes any actions for the first satisfied case.
		/// </summary>
		/// <param name="value">
		/// The value to test for
		/// </param>
		/// <param name="cases">
		/// The set of cases to test against
		/// </param>
		/// <exception cref="System.ArgumentException">
		/// Thrown if 
		///		cases contains a null element
		///		cases contains less than 1 case
		///		default is the only supplied case
		///		default is not the last case
		///		a default case is specified more than once.
		///		a fall-through case is the last case
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if
		///		cases is null
		/// </exception>
		public static void Do(object value, params SwitchCase[] cases)
		{
			Do(value, false, cases);
		}

		/// <summary>
		/// Creates a new case.
		/// </summary>
		/// <param name="valueToTest">
		/// The value to test for
		/// </param>
		/// <param name="actionToPerform">
		/// The action to invoke if this case is satisfied
		/// </param>
		/// <returns>
		/// A new SwitchCase object
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if actionToPerform is null.
		/// </exception>
		public static SwitchCase Case(object valueToTest,
			Action actionToPerform)
		{
			Insist.IsNotNull(actionToPerform, "actionToPerform");

			return new SwitchCase(valueToTest, actionToPerform);
		}

		/// <summary>
		/// Creates a new fall-through case.
		/// </summary>
		/// <param name="valueToTest">
		/// The value to test for
		/// </param>
		/// <returns>
		/// A new SwitchCase object
		/// </returns>
		public static SwitchCase Case(object valueToTest)
		{
			return new SwitchCase(valueToTest);
		}

		/// <summary>
		/// Creates a new default case.
		/// </summary>
		/// <param name="actionToPerform">
		/// The action to invoke if none of the other cases are satisfied
		/// </param>
		/// <returns>
		/// A new SwitchCase object
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if actionToPerform is null.
		/// </exception>
		public static SwitchCase Default(Action actionToPerform)
		{
			return new SwitchCase(actionToPerform);
		}

		#endregion

		#region Private static methods

		/// <summary>
		/// Execute the switch statement. Optionally ensure that one case statement was satisfied.
		/// </summary>
		private static void Do(object value, bool guaranteeHandling, SwitchCase[] cases)
		{
			#region Input Validation

			Insist.IsNotNull(cases, "cases");
			Insist.AllItemsAreNotNull(cases, "cases");
			Insist.ContainsAtLeast(cases, 1, "cases");
			EnsureDefaultIsNotTheOnlyCase(cases);
			EnsureSingleDefaultCase(cases);
			EnsureDefaultIsLastCase(cases);
			EnsureFallThroughCaseIsNotTheLastCase(cases);

			#endregion

			CopyActionsToFallThroughCases(cases);

			bool handled = false;

			foreach (SwitchCase c in cases)
			{
				if (c.IsDefaultCase ||
					object.Equals(value, c.ValueToTest))
				{
					c.ActionToPerform();
					handled = true;
					break;
				}
			}

			if (guaranteeHandling == true &&
				handled == false)
			{
				throw new InvalidOperationException("No cases in this switch statement have been satisfied.");
			}
		}

		/// <summary>
		/// This method will walk backwards through a list of cases and copy any actions
		/// from a non fall-through case to the above fall-through cases.  This ensures
		/// that the case checking code can be kept clean becase each fall-through case
		/// will have a reference to an action to perform.
		/// </summary>
		private static void CopyActionsToFallThroughCases(SwitchCase[] cases)
		{
			Action currentAction = null;
			
			//Navigate backwards because we are walking "up" the switch statement
			//copying actions to any fall-through cases that sit above the current one.
			for (int i = cases.Length - 1; i >= 0; i--)
			{
				if (cases[i].IsFallThroughCase == false)
				{
					currentAction = cases[i].ActionToPerform;
				}
				else
				{
					cases[i].ActionToPerform = currentAction;
				}
			}
		}

		/// <summary>
		/// Ensures that only one default case is specified per switch
		/// </summary>
		private static void EnsureSingleDefaultCase(SwitchCase[] cases)
		{
			if (cases.Count((c) => { return c.IsDefaultCase; }) > 1)
			{
				throw new ArgumentException("A switch statement can only contain a single default case.");
			}
		}
		
		/// <summary>
		/// Ensures that the switch statement consists of more than just a single
		/// default case.  Although this is allowable, strictly speaking, why would
		/// someone want to do this.
		/// </summary>
		private static void EnsureDefaultIsNotTheOnlyCase(SwitchCase[] cases)
		{
			if (cases.Length == 1 &&
				cases[0].IsDefaultCase)
			{
				throw new ArgumentException("The switch block cannot contain only a single default case.");
			}
		}

		/// <summary>
		/// Ensures that a fall-through case is not the last case in a switch.
		/// This ensures that all cases will have an action associated with them.
		/// </summary>
		private static void EnsureFallThroughCaseIsNotTheLastCase(SwitchCase[] cases)
		{
			SwitchCase lastCase = cases[cases.Length - 1];
			if (lastCase.IsFallThroughCase)
			{
				throw new ArgumentException("The last case must provide an action to invoke.");
			}
		}

		/// <summary>
		/// Ensures that the default case is the last case specified.
		/// </summary>
		private static void EnsureDefaultIsLastCase(SwitchCase[] cases)
		{
			if (cases.Any((c) => { return c.IsDefaultCase; }))
			{
				if (cases[cases.Length - 1].IsDefaultCase == false)
				{
					throw new ArgumentException("The default case must be the last case in the switch.");
				}
			}
		}

		#endregion
	}
}
