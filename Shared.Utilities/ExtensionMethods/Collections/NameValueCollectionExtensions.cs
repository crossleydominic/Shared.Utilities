using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Shared.Utilities.RegularExpressions;
using log4net;
using Shared.Utilities.ExtensionMethods.Logging;

namespace Shared.Utilities.ExtensionMethods.Collections
{
	public static class NameValueCollectionExtensions
	{
		#region Logging

		/// <summary>
		/// Used to log information about the class
		/// </summary>
		private static ILog _logger = LogManager.GetLogger(typeof(NameValueCollectionExtensions));

		#endregion

		/// <summary>
		///  Returns a flag indicating whether the given key in the collection is either null or empty.
		/// </summary>
		/// <param name="collection">The collection to check.</param>
		/// <param name="key">The key to check.</param>
		/// <returns>True if the value is null or empty; false otherwise.</returns>
		public static bool IsNullOrEmpty(this NameValueCollection collection, string key)
		{
			_logger.DebugMethodCalled(collection, key);

			#region Input validation

			Insist.IsNotNull(collection, "collection");
			Insist.IsNotNullOrEmpty(key, "key");

			#endregion

			return (collection[key] == null || String.IsNullOrEmpty(collection[key]));
		}
	}
}
