using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Shared.Utilities.Tests.TestObjects
{
	/// <summary>
	/// A simple class that simulates a stream that is not readable.
	/// </summary>
	public class NonReadableStream : MemoryStream
	{
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}
	}
}
