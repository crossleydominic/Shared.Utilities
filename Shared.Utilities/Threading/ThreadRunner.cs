using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Security.Permissions;
using System.Reflection;

namespace Shared.Utilities.Threading
{
	/// <summary>
	/// A helper class that allows a delegate to be run in either an MTA or
	/// STA from any other Apartment type. Useful when trying to create/interact
	/// with UI elements from an MTA.
	/// </summary>
	public static class ThreadRunner
	{
		/// <summary>
		/// Runs the specified delegate in a Single Threaded Apartment
		/// </summary>
		/// <param name="userDelegate"></param>
		public static void RunInSTA(ThreadStart userDelegate)
		{
			RunInApartment(userDelegate, ApartmentState.STA);
		}

		/// <summary>
		/// Runs the specified delegate in a Multi Threaded Apartment
		/// </summary>
		/// <param name="userDelegate"></param>
		public static void RunInMTA(ThreadStart userDelegate)
		{
			RunInApartment(userDelegate, ApartmentState.MTA);
		}

		/// <summary>
		/// Runs the specified delegate in the specified apartment
		/// </summary>
		private static void RunInApartment(ThreadStart userDelegate, ApartmentState apartment)
		{
			if (apartment == ApartmentState.Unknown)
			{
				throw new InvalidOperationException("Can only run in STAs or MTAs");
			}

			ApartmentOperationRunner runner = new ApartmentOperationRunner();
			runner.Run(userDelegate, apartment);
		}

		/// <summary>
		/// Runs a specific job in a specific thread apartment
		/// </summary>
		private class ApartmentOperationRunner
		{
			/// <summary>
			/// Runs a specific method in Single Threaded apartment
			/// </summary>
			/// <param name="userDelegate">A delegate to run</param>
			public void Run(ThreadStart userDelegate, ApartmentState apartment)
			{
				if (Thread.CurrentThread.GetApartmentState() != apartment)
				{
					RunInApartment(userDelegate, apartment);
				}
				else
				{
					userDelegate.Invoke();
				}
			}

			/// <summary>
			/// Runs in the specified apartment
			/// </summary>
			private void RunInApartment(ThreadStart userDelegate, ApartmentState apartmentState)
			{
				Exception thrownException = null;

				Thread thread = new Thread(
				  delegate()
				  {
					  try
					  {
						  userDelegate.Invoke();
					  }
					  catch (Exception e)
					  {
						  thrownException = e;
					  }
				  });
				thread.SetApartmentState(apartmentState);

				thread.Start();
				thread.Join();

				if (thrownException != null)
				{
					ThrowExceptionPreservingStack(thrownException);
				}
			}

			/// <summary>
			/// Rethrows the threads exception but preserves the original 
			/// stack trace.
			/// </summary>
			[ReflectionPermission(SecurityAction.Demand)]
			private static void ThrowExceptionPreservingStack(Exception exception)
			{
				FieldInfo remoteStackTraceString = typeof(Exception).GetField(
				  "_remoteStackTraceString",
				  BindingFlags.Instance | BindingFlags.NonPublic);
				remoteStackTraceString.SetValue(exception, exception.StackTrace + Environment.NewLine);
				throw exception;
			}
		}
	}
}
