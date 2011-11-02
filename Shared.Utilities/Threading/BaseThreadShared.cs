using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.Threading
{
	/// <summary>
	/// The base class for ThreadShared resources.
	/// Handles registration between ThreadShared and Padlocks and also
	/// lock reference counting (for lock re-entrancy, think recursive functions)
	/// </summary>
	public abstract class BaseThreadShared
	{
		#region Private members

		/// <summary>
		/// The set of padlocks that this resource will be protected by.
		/// We'll also store the number of times each lock has been entered
		/// for re-entrancy detection.
		/// </summary>
		private Dictionary<Padlock, int> _associatedPadlocks;

		/// <summary>
		/// An object we can lock on to keep our internal state consistent.
		/// </summary>
		private object _internalStateLock = new object();

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new ThreadShared resource
		/// </summary>
		/// <param name="firstMandatoryLock">
		/// All ThreadShared resources MUST be associated with at least 1 lock
		/// </param>
		/// <param name="otherLocks">
		/// Some resources need to be protected by more than 1 lock, specify
		/// all the other padlocks that protect this resource here.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if FirstMandatorLock is null
		/// </exception>
		protected BaseThreadShared(Padlock firstMandatoryLock, params Padlock[] otherLocks)
		{
			Insist.IsNotNull(firstMandatoryLock, "firstMandatoryLock");
			Insist.AllItemsAreNotNull(otherLocks, "otherLocks");

			_associatedPadlocks = new Dictionary<Padlock, int>();
			_associatedPadlocks.Add(firstMandatoryLock, 0);

			foreach (Padlock currentPadlock in otherLocks)
			{
				_associatedPadlocks.Add(currentPadlock, 0);
			}

			//Register this ThreadShared resource with all of the associated Padlocks.
			foreach (Padlock currentPadlock in _associatedPadlocks.Keys)
			{
				currentPadlock.Register(this);
			}
		}

		#endregion

		#region Internal Methods

		/// <summary>
		/// Called from a Padlock when the padlock is locked.
		/// </summary>
		/// <param name="plock">
		/// The padlock that is attempting to start protection of this resource.
		/// </param>
		internal void StartProtection(Padlock plock)
		{
			lock (_internalStateLock)
			{
				EnsureLockIsAssociatedWithThisResource(plock);

				_associatedPadlocks[plock]++;
			}
		}

		/// <summary>
		/// Called from a Padlock when the padlock is unlocked.
		/// </summary>
		/// <param name="plock">
		/// The padlock that is attempting to end protection of this resource.
		/// </param>
		internal void EndProtection(Padlock plock)
		{
			lock (_internalStateLock)
			{
				EnsureLockIsAssociatedWithThisResource(plock);

				_associatedPadlocks[plock]--;
			}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Ensures that the supplied Padlock is associated with this protected resource.
		/// </summary>	
		/// <exception cref="System.ArgumentException">
		/// Thrown if the supplied padlock is not associated with this resource.
		/// </exception>
		private void EnsureLockIsAssociatedWithThisResource(Padlock plock)
		{
			//MAKE SURE THAT _internalStateLock IS TAKEN BEFORE CALLING THIS METHOD

			if (!_associatedPadlocks.Keys.Contains(plock))
			{
				throw new ArgumentException("This resource is not protected by this lock");
			}
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Called by child class to ensure that all the required locks 
		/// have been taken before this resource can be interacted with.
		/// </summary>
		protected void EnsureResourceIsProtected()
		{
			lock (_internalStateLock)
			{
				foreach (int i in _associatedPadlocks.Values)
				{
					if (i == 0)
					{
						throw new InvalidOperationException("This resource is not fully locked and cannot be modified.");
					}
				}
			}
		}

		#endregion
	}
}
