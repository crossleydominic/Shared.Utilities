using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Shared.Utilities.Tests.TestObjects
{
	/// <summary>
	/// A simple class that simulates a stream that is not writable.
	/// </summary>
	public class NonWritableStream : MemoryStream
	{
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}
	}
}
