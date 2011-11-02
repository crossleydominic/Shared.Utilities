using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.Tests.TestObjects;
using Shared.Utilities.Serialization;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;

namespace Shared.Utilities.Tests.Serialization
{
	[TestFixture]
	public class XmlSerializationProviderTests
	{
		#region Pre serialized object test data.

		/// <summary>
		/// A pre serialized object. This is a SerializableObject and will be used in the Deserialize tests
		/// </summary>
		byte[] _preSerializedBuffer;

		#endregion

        [TestFixtureSetUp]
        public void InitialiseData() {

            XmlSerializer formatter = new XmlSerializer(typeof(SerializableObject));

            using(MemoryStream stream = new MemoryStream()) {

                formatter.Serialize(stream, new SerializableObject());

                byte[] tempData = stream.GetBuffer();
                _preSerializedBuffer = new byte[stream.Position];

                Array.Copy(tempData, _preSerializedBuffer, stream.Position);
            }

        }

		#region Serialize Tests

		/// <summary>
		/// Test to serialize a null object
		/// </summary>
		[Test]
		public void Serialize_ObjectNull()
		{
			CommonSerializationProviderTests.Serialize_ObjectNull(new XmlSerializationProvider(typeof(SerializableObject)));
		}

		/// <summary>
		/// Tests to serialize to a null stream
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void Serialize_StreamNull()
		{
			CommonSerializationProviderTests.Serialize_StreamNull(new XmlSerializationProvider(typeof(SerializableObject)));
		}

		/// <summary>
		/// Tests to serialize a non serializable object
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(InvalidOperationException))]
		public void Serialize_NonSerializableObject()
		{
			CommonSerializationProviderTests.Serialize_NonSerializableObject(new XmlSerializationProvider(typeof(SerializableObject)));
		}

		/// <summary>
		/// Test to serialize a serializable object to a buffer
		/// </summary>
		[Test]
		public void Serialize_SerializableObjectToBuffer()
		{
			CommonSerializationProviderTests.Serialize_SerializableObjectToBuffer(new XmlSerializationProvider(typeof(SerializableObject)));
		}

		/// <summary>
		/// Test to serialize a serializable object to a stream
		/// </summary>
		[Test]
		public void Serialize_SerializableObjectToStream()
		{
			CommonSerializationProviderTests.Serialize_SerializableObjectToStream(new XmlSerializationProvider(typeof(SerializableObject)));
		}

		/// <summary>
		/// Test to serialize a serializable object to a non writeable stream.
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void Serialize_SerializableObjectToNonWritableStream()
		{
			CommonSerializationProviderTests.Serialize_SerializableObjectToNonWritableStream(new XmlSerializationProvider(typeof(SerializableObject)));
		}

		#endregion

		#region Deserialize

		/// <summary>
		/// Tests to deserialize a null or empty buffer
		/// </summary>
		[Test]
		public void Deserialize_BufferNullOrEmpty()
		{
			CommonSerializationProviderTests.Deserialize_BufferNullOrEmpty(new XmlSerializationProvider(typeof(SerializableObject)));
		}

		/// <summary>
		/// Test to deserialize from a null stream
		/// </summary>
		[Test]
		public void Deserialize_StreamNull()
		{
			CommonSerializationProviderTests.Deserialize_StreamNull(new XmlSerializationProvider(typeof(SerializableObject)));
		}

		/// <summary>
		/// Test to deserialize from a stream that is non readable.
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void Deserialize_StreamNonReadable()
		{
			CommonSerializationProviderTests.Deserialize_StreamNonReadable(new XmlSerializationProvider(typeof(SerializableObject)));
		}

		/// <summary>
		/// Test to deserialize an object into a different object
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(InvalidCastException))]
		public void Deserialize_TypeMismatch()
		{
			CommonSerializationProviderTests.Deserialize_TypeMismatch(new XmlSerializationProvider(typeof(SerializableObject)), _preSerializedBuffer);
		}

		/// <summary>
		/// Tests to deserialize a previously serialized object
		/// </summary>
		[Test]
		public void Deserialize_DeserializeSerializableObject()
		{
			CommonSerializationProviderTests.Deserialize_DeserializeSerializableObject(new XmlSerializationProvider(typeof(SerializableObject)), _preSerializedBuffer);
		}

		/// <summary>
		/// Test to deserialize from a stream
		/// </summary>
		[Test]
		public void Deserialize_DeserializeFromStream()
		{
			CommonSerializationProviderTests.Deserialize_DeserializeFromStream(new XmlSerializationProvider(typeof(SerializableObject)), _preSerializedBuffer);
		}

		#endregion

		#region UnderlyingSerializer Tests

		/// <summary>
		/// Tests to make sure that the UnderlyingSerializer if of the expected typr
		/// </summary>
		[Test]
		public void UnderlyingSerializer_IsOfCorrectType()
		{
			CommonSerializationProviderTests.UnderlyingSerializer_IsOfCorrectType<XmlSerializer>(new XmlSerializationProvider(typeof(SerializableObject)));
		}

		#endregion

		#region AssociatedType Tests

		/// <summary>
		/// Tests to make sure that the UnderlyingSerializer if of the expected typr
		/// </summary>
		[Test]
		public void AssociatedType_IsOfCorrectType()
		{
			CommonSerializationProviderTests.AssociatedType_IsOfCorrectType<XmlSerializer>(new XmlSerializationProvider(typeof(SerializableObject)), typeof(SerializableObject));
		}

		#endregion

		#region Roundtrip Tests

		/// <summary>
		/// Tests what happens when we serialize and object then deserialize it.
		/// </summary>
		[Test]
		public void RoundTrip_SerializeObject()
		{
			SerializableObject obj = new SerializableObject();
			obj._stringField1 = "Changed field 1";

			CommonSerializationProviderTests.RoundTrip_SerializeObject(new XmlSerializationProvider(typeof(SerializableObject)), obj);
		}

		#endregion

	}
}
