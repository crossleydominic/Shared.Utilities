using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Shared.Utilities.Tests.TestObjects
{
	/// <summary>
	/// A simple class that contains some data that can be serialized.
	/// </summary>
	[Serializable]
	public class SerializableObject
	{
		public string _stringField1 = "Blahblahblah";

		[NonSerialized]
		[XmlIgnore]
		public string _stringField2 = "Cannot Change";
	}
}
