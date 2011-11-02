using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.Threading
{
	/// <summary>
	/// This class is used to support the Padlock, ThreadShared infrastructure.
	/// Do not use directly. Do not call any method on this class directly.
	/// 
	/// A simple class that represents a Padlock locking operation.
	/// Implements IDisposable so that we can use it in a Using block
	/// and get Padlock.Unlock for free when the Using block closes.
	/// </summary>
	public class LockToken : IDisposable
	{
		#region Private members

		/// <summary>
		/// The owning Padlock
		/// </summary>
		private Padlock _parent;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new LockToken and associates it with the supplied parent
		/// </summary>
		internal LockToken(Padlock parent)
		{
			Insist.IsNotNull(parent, "parent");

			_parent = parent;
		}

		#endregion

		#region IDisposable members

		/// <summary>
		/// Unlocks the parent Padlock. Do not call this method yourself.
		/// This should be invoked by the end of the Using{} block only.
		/// </summary>
		public void Dispose()
		{
			_parent.Unlock();
		}

		#endregion
	}
}
