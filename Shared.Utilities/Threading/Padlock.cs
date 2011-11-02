using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.Threading
{
	/// <summary>
	/// An object that can be used to lock on for multithreaded programming.
	/// A Padlock knows which ThreadShared it is meant to protect.
	/// </summary>
	public class Padlock
	{
		#region Private members

		/// <summary>
		/// The name of this padlock
		/// </summary>
		private string _name;

		/// <summary>
		/// The unique Guid for this padlock
		/// </summary>
		private Guid _guid;

		/// <summary>
		/// The raw object that we'll be locking on.
		/// </summary>
		private object _monitor;

		/// <summary>
		/// The list of ThreadShared objects that are protected by this padlock
		/// </summary>
		private List<BaseThreadShared> _resourcesProtectedByMe;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new padlock with the default name
		/// </summary>
		public Padlock() : this(string.Empty) { }


		/// <summary>
		/// Creates a new padlock with the specified name
		/// </summary>
		/// <param name="name">
		/// The name of the padlock
		/// </param>
		public Padlock(string name)
		{
			_guid = Guid.NewGuid();

			if (string.IsNullOrEmpty(name))
			{
				_name = string.Format("Padlock-{0}", _guid.ToString());
			}
			else
			{
				_name = name;
			}

			_monitor = new object();
			_resourcesProtectedByMe = new List<BaseThreadShared>();
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Locks this padlock and notifies all associated ThreadShared resources
		/// that this lock has been taken.
		/// </summary>
		public LockToken Lock()
		{
			System.Threading.Monitor.Enter(_monitor);

			foreach (BaseThreadShared resource in _resourcesProtectedByMe)
			{
				resource.StartProtection(this);
			}

			return new LockToken(this);
		}

		#endregion

		#region Internal methods

		/// <summary>
		/// Called by a ThreadShared resource when it is created. Notifies this padlock
		/// that it is responsible for controlling access to that Protected resource.
		/// </summary>
		/// <param name="protectedResource">
		/// The Protected object to associate with this padlock
		/// </param>
		internal void Register(BaseThreadShared protectedResource)
		{
			_resourcesProtectedByMe.Add(protectedResource);
		}

		/// <summary>
		/// Unlocks this padlock and notifies any associated ThreadShared resources
		/// that the lock has been lost.
		/// </summary>
		internal void Unlock()
		{
			foreach (BaseThreadShared resource in _resourcesProtectedByMe)
			{
				resource.EndProtection(this);
			}

			System.Threading.Monitor.Exit(_monitor);
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Gets the Padlock's name
		/// </summary>
		public string Name
		{
			get { return _name; }
		}

		/// <summary>
		/// Gets this Padlocks Guid
		/// </summary>
		public Guid Guid
		{
			get { return _guid; }
		}

		#endregion

		#region Public overrides

		/// <summary>
		/// Gets the name of this padlock
		/// </summary>
		public override string ToString()
		{
			return _name;
		}

		#endregion
	}
}
