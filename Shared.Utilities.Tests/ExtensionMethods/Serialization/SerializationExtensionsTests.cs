using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Utilities.ExtensionMethods.Serialization;
using System.IO;
using NUnit.Framework;
using System.Runtime.Serialization;
using Shared.Utilities.Serialization;
using Shared.Utilities.Tests.TestObjects;

namespace Shared.Utilities.Tests.ExtensionMethods.Serialization
{
	[TestFixture]
	public class SerializationExtensionsTests
	{
		/*
		 * Note
		 * 
		 * Testing of the functionality of the SerializationProviders themselves
		 * are handled in the Serialization namespace.  These tests just test
		 * the public interface to the extension methods.
		 */

		#region Serialization Tests

		/// <summary>
		/// Test when the supplied serializationProvider is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentNullException))]
		public void Serialize_ProviderNull()
		{
			SerializableObject obj1 = new SerializableObject();
			BinarySerializationProvider provider = null;
			byte[] buffer = obj1.Serialize(provider);
		}

		#endregion

		#region Deserialization Tets

		/// <summary>
		/// Test when the supplied serializationProvider is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void Deserialize_ProviderNull()
		{
			SerializableObject obj1 = new SerializableObject();
			BinarySerializationProvider provider = null;
			byte[] buffer = obj1.Serialize(provider);
		}

		#endregion
	}
}
