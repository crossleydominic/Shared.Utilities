using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.Serialization;
using System.IO;
using System.Runtime.Serialization;
using Shared.Utilities.Tests.TestObjects;
using System.Runtime.Serialization.Formatters.Binary;

namespace Shared.Utilities.Tests.Serialization
{
	/// <summary>
	/// A set of tests that apply to multiple SerializationProviders.
	/// </summary>
	public class CommonSerializationProviderTests
	{
        public static byte[] GetDummySerialisedObject() {

            BinaryFormatter formatter = new BinaryFormatter();
            byte[] returnData;
            
            using(MemoryStream stream = new MemoryStream()) {

                formatter.Serialize(stream, new SerializableObject());

                byte[] tempData = stream.GetBuffer();
                returnData = new byte[stream.Position];

                Array.Copy(tempData, returnData, stream.Position);
            }

            return returnData;
        }

		#region Common Serialization test code

		/// <summary>
		/// Common Test code to try and serialize a null object
		/// </summary>
		/// <param name="provider">
		/// The provider to use to perform the serialization
		/// </param>
		public static void Serialize_ObjectNull(SerializationProvider provider)
		{
			SerializableObject obj1 = null;

			byte[] buffer = provider.Serialize(obj1);

			Assert.IsTrue(buffer != null && buffer.Length == 0);
		}

		/// <summary>
		/// Common Test code to try and serialize to a null stream
		/// </summary>
		/// <param name="provider">
		/// The provider to use to perform the serialization
		/// </param>
		public static void Serialize_StreamNull(SerializationProvider provider)
		{
			Stream stream = null;
			SerializableObject obj1 = new SerializableObject();
			provider.Serialize(obj1, stream);
		}

		/// <summary>
		/// Common Test code to try and serialize a non serializable object
		/// </summary>
		/// <param name="provider">
		/// The provider to use to perform the serialization
		/// </param>
		public static void Serialize_NonSerializableObject(SerializationProvider provider)
		{
			NonSerializableObject obj1 = new NonSerializableObject();
			provider.Serialize(obj1);
		}

		/// <summary>
		/// Common Test code to try and serialize an object to a buffer
		/// </summary>
		/// <param name="provider">
		/// The provider to use to perform the serialization
		/// </param>
		public static void Serialize_SerializableObjectToBuffer(SerializationProvider provider)
		{
			SerializableObject obj1 = new SerializableObject();
			byte[] buffer = provider.Serialize(obj1);
			Assert.IsTrue(buffer != null && buffer.LongLength > 0);
		}

		/// <summary>
		/// Common Test code to try and serialize an object to a stream
		/// </summary>
		/// <param name="provider">
		/// The provider to use to perform the serialization
		/// </param>
		public static void Serialize_SerializableObjectToStream(SerializationProvider provider)
		{
			SerializableObject obj1 = new SerializableObject();
			using (MemoryStream memStream = new MemoryStream())
			{
				provider.Serialize(obj1, memStream);
				Assert.IsTrue(memStream.Position > 0);
			}
		}

		/// <summary>
		/// Common Test code to try and serialize an object to a non writeable stream
		/// </summary>
		/// <param name="provider">
		/// The provider to use to perform the serialization
		/// </param>
		public static void Serialize_SerializableObjectToNonWritableStream(SerializationProvider provider)
		{
			SerializableObject obj1 = new SerializableObject();
			using (NonWritableStream memStream = new NonWritableStream())
			{
				provider.Serialize(obj1, memStream);
			}
		}

		#endregion

		#region Common Deserialization test code

		/// <summary>
		/// Common Test code to try and deserialize a null or empty buffer
		/// </summary>
		/// <param name="provider">
		/// The provider to use to perform the serialization
		/// </param>
		public static void Deserialize_BufferNullOrEmpty(SerializationProvider provider)
		{
			byte[] buffer = null;

			SerializableObject obj1 = provider.Deserialize<SerializableObject>(buffer);
			Assert.IsNull(obj1);

			buffer = new byte[0];
			obj1 = provider.Deserialize<SerializableObject>(buffer);
			Assert.IsNull(obj1);
		}

		/// <summary>
		/// Common Test code to try and deserialize a null stream
		/// </summary>
		/// <param name="provider">
		/// The provider to use to perform the serialization
		/// </param>
		public static void Deserialize_StreamNull(SerializationProvider provider)
		{
			MemoryStream memStream = null;
			SerializableObject obj1 = provider.Deserialize<SerializableObject>(memStream);
			Assert.IsNull(obj1);
		}

		/// <summary>
		/// Common Test code to try and deserialize from a non-readable stream
		/// </summary>
		/// <param name="provider">
		/// The provider to use to perform the serialization
		/// </param>
		public static void Deserialize_StreamNonReadable(SerializationProvider provider)
		{
			NonReadableStream stream = new NonReadableStream();
			SerializableObject obj1 = provider.Deserialize<SerializableObject>(stream);
		}

		/// <summary>
		/// Common Test code to try and deserialize an object as an incompatible type
		/// </summary>
		/// <param name="provider">
		/// The provider to use to perform the serialization
		/// </param>
		public static void Deserialize_TypeMismatch(SerializationProvider provider, byte[] preSerializedBuffer)
		{
			//Attempt to deserialize a SerializableObject into a string.
			string incompatibleType = provider.Deserialize<string>(preSerializedBuffer);
		}

		/// <summary>
		/// Common Test code to try and deserialize a pre-serialized buffer
		/// </summary>
		/// <param name="provider">
		/// The provider to use to perform the serialization
		/// </param>
		public static void Deserialize_DeserializeSerializableObject(SerializationProvider provider, byte[] preSerializedBuffer)
		{
			SerializableObject obj1 = provider.Deserialize<SerializableObject>(preSerializedBuffer);
			Assert.IsTrue(obj1 != null && obj1._stringField1 == "Blahblahblah");
		}

		/// <summary>
		/// Common Test code to try and deserialize from a stream
		/// </summary>
		/// <param name="provider">
		/// The provider to use to perform the serialization
		/// </param>
		public static void Deserialize_DeserializeFromStream(SerializationProvider provider, byte[] preSerializedBuffer)
		{
			using (MemoryStream memStream = new MemoryStream(preSerializedBuffer))
			{
				SerializableObject obj1 = provider.Deserialize<SerializableObject>(memStream);
				Assert.IsTrue(obj1 != null && obj1._stringField1 == "Blahblahblah");
			}
		}

		#endregion

		#region Common UnderlyingSerializer test code

		/// <summary>
		/// Common Test code to check that the underlying serialize is of the correct type for the provider
		/// </summary>
		/// <param name="provider">
		/// The provider to use to perform the serialization
		/// </param>
		public static void UnderlyingSerializer_IsOfCorrectType<T>(ExposableSerializationProvider<T> provider) where T : class
		{
			Assert.IsTrue(provider.UnderlyingSerializer != null &&
				typeof(T) == provider.UnderlyingSerializer.GetType());
		}

		#endregion

		#region Common AssociatedType test code

		/// <summary>
		/// Common Test code to check that the AssociatedType property returns the correct information
		/// </summary>
		/// <param name="provider">
		/// The provider to use to perform the serialization
		/// </param>
		public static void AssociatedType_IsOfCorrectType<T>(TypedSerializationProvider<T> provider, Type expectedType) where T : class
		{
			Assert.IsTrue(provider.AssociatedType != null &&
				provider.AssociatedType == expectedType);
		}

		#endregion

		#region Common Serialization Round Trip test code

		/// <summary>
		/// Common Test code to try and seralize then deserialize an object.
		/// </summary>
		/// <param name="provider">
		/// The provider to use to perform the serialization
		/// </param>
		public static void RoundTrip_SerializeObject(SerializationProvider provider, SerializableObject unserializedObject)
		{
			byte[] buffer = provider.Serialize(unserializedObject);
			SerializableObject deserializedObj = provider.Deserialize<SerializableObject>(buffer);
			
			Assert.IsTrue(deserializedObj != null &&
				deserializedObj._stringField1.Equals(unserializedObject._stringField1));
		}

		#endregion
	}
}
