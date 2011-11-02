using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.ExtensionMethods.Logging
{
	/// <summary>
	/// Options for use with ILogExtensions.  Used to optionally expand enumerables
	/// when logging.
	/// </summary>
	public enum EnumerableExpansion
	{
		DoNotExpand,
		Expand
	}
}
