using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.ExtensionMethods.Primitives
{
	/// <summary>
	/// Some extension methods for EventHandlers
	/// </summary>
	public static class EventHandlerExtensions
	{
		/// <summary>
		/// An extension method to raise an event that includes the null check.
		/// </summary>
		/// <typeparam name="T">
		/// The eventArgs type
		/// </typeparam>
		/// <param name="theEvent">
		/// The event handler to invoke
		/// </param>
		/// <param name="sender">
		/// The sender object
		/// </param>
		/// <param name="args">
		/// The event arguments
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if sender is null
		/// </exception>
		public static void Raise<T>(this EventHandler<T> theEvent, object sender, T args) where T : EventArgs
		{
			Insist.IsNotNull(sender, "sender");

			if (theEvent != null)
			{
				theEvent(sender, args);
			}
		}
	}
}
