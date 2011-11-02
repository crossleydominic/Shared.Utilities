using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.Threading
{
	/// <summary>
	/// A wrapper class that wraps up a resource/variable/memory location 
	/// so that access to it is ONLY allowed when all of the locks that
	/// protect this resource have been taken.
	/// </summary>
	public class ThreadShared<T> : BaseThreadShared
	{
		#region Private members

		/// <summary>
		/// The variable to protect.
		/// </summary>
		private T _value;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new ThreadShared resource.
		/// </summary>
		/// <param name="value">
		/// The variable that is shared
		/// </param>
		/// <param name="firstMandatoryLock">
		/// All ThreadShared must be protected by at least 1 Padlock
		/// </param>
		/// <param name="otherLocks">
		/// All ThreadShared MAY be protected by a set of Padlocks, pass the
		/// other Padlocks that this resource is controlled by here.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if firstMandatoryLock is null
		/// </exception>
		public ThreadShared(
			T value,
			Padlock firstMandatoryLock,
			params Padlock[] otherLocks)
			: base(firstMandatoryLock, otherLocks)
		{
			_value = value;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Get/sets the ThreadShared value (does the same as the _ property).
		/// </summary>
		public T Value
		{
			get
			{
				return _;
			}
			set
			{
				_ = value;
			}
		}

		/// <summary>
		/// Shortcut naming convention so that getting access
		/// to the ThreadShared resource is almost the same as if
		/// the resource was not wrapped.
		/// </summary>
		public T _
		{
			get
			{
				base.EnsureResourceIsProtected();
				return _value;
			}
			set
			{
				base.EnsureResourceIsProtected();
				_value = value;
			}
		}

		#endregion

		#region Public overrides

		/// <summary>
		/// Gets the ToString() representation of the ThreadShared resource.
		/// </summary>
		public override string ToString()
		{
			if (_value == null)
			{
				return null;
			}
			else
			{
				return _value.ToString();
			}
		}

		#endregion
	}
}
