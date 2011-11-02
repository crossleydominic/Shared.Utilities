using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if !DOTNET35
namespace Shared.Utilities.ExtensionMethods.Delegates
{
	/// <summary>
	/// Some predefined rules for use with the Func.Attempt extensions
	/// 
	/// EXPERIMENTAL - DO NOT USE.
	/// </summary>
	internal class AttemptSuccessConditions
	{
		/// <summary>
		/// Returns true (success) if the supplied object is not null
		/// </summary>
		public static readonly Func<object, bool> NotNullIsSuccess = (o) => { return o != null; };

		/// <summary>
		/// Returns true (success) if the supplied string is not null or empty.
		/// </summary>
		public static readonly Func<string, bool> StringNotNullOrEmptyIsSuccess = (o) => { return !string.IsNullOrEmpty(o); };

		/// <summary>
		/// Returns true (success) if the supplied integer is greater than 0
		/// </summary>
		public static readonly Func<int, bool> IntGreaterThanZeroIsZuccess = (i) => { return i > 0; };
	}
}
#endif
