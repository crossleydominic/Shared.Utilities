using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Shared.Utilities.ExtensionMethods.Logging;

namespace Shared.Utilities
{
	/// <summary>
	/// Provides a way to associate some cleanup code with an object.
	/// Useful for objects that have been badly designed (i.e. objects that SHOULD have
	/// implemented IDisposable but don't) or objects which require cleanup more than
	/// just calling Dispose (transactions in ebs require this).
	/// This allows the objects to be used in USING blocks which will gaurantee their
	/// cleanup code is run.
	/// 
	/// EXPERIMENTAL, DO NOT USE.
	/// </summary>
	internal sealed class Disposer<T> : IDisposable 
	{
		#region Logging

		private static ILog _log = LogManager.GetLogger(typeof(Disposer<T>));

		#endregion

		#region Private members

		/// <summary>
		/// The resource that we're wrapping
		/// </summary>
		private T _value;

		/// <summary>
		/// Whether or not this object has been disposed
		/// </summary>
		private bool _isDisposed;

		/// <summary>
		/// The action to perform when dispose is called.
		/// </summary>
		private Action<T> _disposeAction;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance of a Disposer for the 
		/// supplied instance and dispose action
		/// </summary>
		/// <param name="value">
		/// The instance to wrap
		/// </param>
		/// <param name="disposeAction">
		/// The code that will get run when this Disposer is disposed.
		/// Used to cleanup the wrapped instance value.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if value or disposeAction are null
		/// </exception>
		public Disposer(T value, Action<T> disposeAction)
		{
			_log.DebugMethodCalled(value, disposeAction);

			#region Input validation

			Insist.IsNotNull(value, "value");
			Insist.IsNotNull(disposeAction, "disposeAction");

			#endregion

			_value = value;
			_isDisposed = false;
			_disposeAction = disposeAction;
		}

		#endregion

		#region Public static methods

		/// <summary>
		/// Provides a slightly nicer way to create a new DisposeWrapper 
		/// </summary>
		/// <param name="value">
		/// The instance to wrap
		/// </param>
		/// <param name="disposeAction">
		/// The code that will get run when this Disposer is disposed.
		/// Used to cleanup the wrapped instance value.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if value or disposeAction are null
		/// </exception>
		public static Disposer<T> Create(T value, Action<T> disposeAction)
		{
			return new Disposer<T>(value, disposeAction);
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Gets access to the resource we're wrapping.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException">
		/// Called if trying to get access to Value after this
		/// object has been disposed.
		/// </exception>
		public T Value
		{
			get
			{
				if (_isDisposed)
				{
					throw new ObjectDisposedException(this.GetType().FullName);
				}

				return _value;
			}
		}

		/// <summary>
		/// Whether or not this instance has been disposed.
		/// </summary>
		public bool IsDisposed
		{
			get
			{
				return _isDisposed;
			}
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Executes the cleanup code on the resource.
		/// </summary>
		public void Dispose()
		{
			_log.DebugMethodCalled();

			if (_isDisposed)
			{
				return;
			}

			try
			{
				_isDisposed = true;
				_disposeAction(_value);
			}
			catch (Exception oe)
			{
				_log.Debug("An exception occurred whilst running the disposeAction code", oe);
				throw;
			}
		}

		#endregion
	}
}
