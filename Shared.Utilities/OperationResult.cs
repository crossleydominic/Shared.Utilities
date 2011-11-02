using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Shared.Utilities
{
	/// <summary>
	/// A simple class that allows a success/failed flag to be returned
	/// along with some error messages if the operation failed.
	/// </summary>
	public sealed class OperationResult
	{
		#region Private members

		/// <summary>
		/// Whether or not the operation succeeded
		/// </summary>
		private bool _success;

		/// <summary>
		/// If the operation failed, a list of error messages that describe what the problem was
		/// </summary>
		private List<string> _errorMessages;

		#endregion

		#region Static members

		/// <summary>
		/// A single instance of this class that represents success.
		/// It is not possible for external code to create a new OperationResult that
		/// represents success.
		/// </summary>
		public static readonly OperationResult Succeeded = new OperationResult();

		#endregion

		#region Type Initializer

		/// <summary>
		/// Type initializer.  Will construct the only OperationResult object that
		/// represents success.
		/// </summary>
		static OperationResult()
		{
			Succeeded = new OperationResult();
			Succeeded._success = true;
			Succeeded._errorMessages = null;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Disallow external code to create default instances of this class.
		/// </summary>
		private OperationResult() { }

		/// <summary>
		/// Construct a new Failure OperationResult with the supplied error message
		/// </summary>
		/// <param name="errorMessage">
		/// The error message indicating the failure
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if
		///		errorMessage is null
		/// </exception>
		public OperationResult(string errorMessage) : this(new string[] { errorMessage }) { }

		/// <summary>
		/// Construct a new Failure OperationResult with the supplied list of error messages
		/// </summary>
		/// <param name="errorMessages">
		/// The collection of error messages that indicate why the operation failed
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// Throw if
		///		errorMessages is null
		///		errorMessages contains a null element
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// Throw if
		///		errorMessages contains no elements
		/// </exception>
		public OperationResult(ICollection<string> errorMessages)
		{
			#region Input Validation

			Insist.IsNotNull(errorMessages, "errorMessages");
			Insist.ContainsAtLeast(errorMessages, 1, "errorMessages");
			Insist.AllItemsAreNotNull(errorMessages, "errorMessages");

			#endregion

			_success = false;
			_errorMessages = new List<string>();
			_errorMessages.AddRange(errorMessages);
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Gets whether or not this operation succeeded
		/// </summary>
		public bool Success
		{
			get { return _success; }
		}

		/// <summary>
		/// Gets the first error message in the list of ErrorMessages.
		/// </summary>
		/// <exception cref="System.InvalidOperationException">
		/// Thrown if Success is true
		/// </exception>
		public string FirstErrorMessage
		{
			get
			{
				EnsureFailureState();

				//if the operation failed then _errorMessages WILL ALWAYS have at 
				//least one item in it.
				return _errorMessages[0];
			}
		}

		/// <summary>
		/// Gets all of the errormessages concatenated together. Each error
		/// message is seperated by a newline character
		/// </summary>
		/// <exception cref="System.InvalidOperationException">
		/// Thrown if Success is true
		/// </exception>
		public string AllErrorMessages
		{
			get
			{
				EnsureFailureState();

				return ConcatenateErrorMessages(Environment.NewLine);
			}
		}

		/// <summary>
		/// Gets the list of error messages.
		/// </summary>
		/// <exception cref="System.InvalidOperationException">
		/// Thrown if Success is true
		/// </exception>
		public ReadOnlyCollection<string> ErrorMessages
		{
			get
			{
				EnsureFailureState();

				return _errorMessages.AsReadOnly();
			}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Ensures that this operation failed before allowing access
		/// to any of the error messages.
		/// </summary>
		private void EnsureFailureState()
		{
			if (_success)
			{
				throw new InvalidOperationException("The operation succeeded, there are no error messages.");
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Concatenates all the error messages together into a single string
		/// using the supplied string to seperate them
		/// </summary>
		/// <exception cref="System.InvalidOperationException">
		/// Thrown if Success is true
		/// </exception>
		public string ConcatenateErrorMessages(string seperator)
		{
			EnsureFailureState();
#if !DOTNET35
			return String.Join(seperator, _errorMessages);
#else
			return String.Join(seperator, _errorMessages.ToArray());
#endif
		}

		#endregion

		#region Implicit type casting

		/// <summary>
		/// Implicitly cast this object to a boolean
		/// </summary>
		public static implicit operator bool(OperationResult result)
		{
			return result._success;
		}

		#endregion

		public override string ToString()
		{
			if (Success)
			{
				return "Success";
			}
			else
			{
				return string.Format("Failure{0}{1}", Environment.NewLine, AllErrorMessages);
			}
		}
	}
}
