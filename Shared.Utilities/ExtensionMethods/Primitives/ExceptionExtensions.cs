using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.ExtensionMethods.Primitives
{
	public static class ExceptionExtensions
	{
		/// <summary>
		/// Flattens the supplied exception graph's error message down into a flat 
		/// string. Useful for logging nested exception objects.
		/// Includes stack trace information in the returned message.
		/// </summary>
		/// <param name="ex">
		/// The exception object to get the error messages and stack traces for.
		/// </param>
		/// <returns>
		/// A string that contains all of the error messages and stack traces
		/// for the supplied exception object graph.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// Throws ArgumentNullExceptiono if ex is null.
		/// </exception>
		public static string AllMessages(this Exception ex)
		{
			return AllMessages(ex, true);
		}

		/// <summary>
		/// Flattens the supplied exception graph's error message down into a flat 
		/// string. Useful for logging nested exception objects.
		/// Optionally includes the stack trace in the error message.
		/// </summary>
		/// <param name="ex">
		/// The exception object to get the error messages and stack traces for.
		/// </param>
		/// <param name="includeStackTrace">
		/// Whether or not to include the stack trace in the returned string.
		/// </param>
		/// <returns>
		/// A string that contains all of the error messages and stack traces
		/// for the supplied exception object graph.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// Throws ArgumentNullExceptiono if ex is null.
		/// </exception>
		public static string AllMessages(this Exception ex, bool includeStackTrace)
		{
			#region Input validation

			Insist.IsNotNull(ex, "ex");

			#endregion

			StringBuilder builder = new StringBuilder();

			string indent = string.Empty;

			do
			{
				if (builder.Length > 0)
				{
					builder.Append(Environment.NewLine);
				}

				builder.Append(indent);
				builder.Append(ex.Message);
				if (includeStackTrace)
				{
					builder.Append(Environment.NewLine);

					builder.Append(indent);
					builder.Append(ex.GetType());
					builder.Append(Environment.NewLine);

					builder.Append(indent);
					builder.Append(ex.StackTrace);
					builder.Append(Environment.NewLine);
				}
				ex = ex.InnerException;
				indent = indent + "\t";
			} while (ex != null);

			return builder.ToString();
		}
	}
}
