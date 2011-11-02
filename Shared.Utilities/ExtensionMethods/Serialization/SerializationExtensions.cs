using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Shared.Utilities.Serialization;
using log4net;
using Shared.Utilities.ExtensionMethods.Logging;

namespace Shared.Utilities.ExtensionMethods.Serialization
{
	public static class SerializationExtensions
	{
		#region Logging

		/// <summary>
		/// Used to log information about the class
		/// </summary>
		private static ILog _logger = LogManager.GetLogger(typeof(SerializationExtensions));

		#endregion

		#region Serialization

		/// <summary>
		/// Serialize the object to an in memory buffer using binary serialization
		/// </summary>
		/// <param name="obj">
		/// The object to serialize
		/// </param>
		/// <returns>
		/// An empty buffer if obj is null, otherwise a byte array containing the serialized
		/// object.
		/// </returns>
		public static byte[] Serialize(this object obj)
		{
			_logger.DebugMethodCalled(obj);

			return Serialize(obj, new BinarySerializationProvider());
		}

		/// <summary>
		/// Serialize the object to an in memory buffer using the supplied SerializationProvider
		/// </summary>
		/// <param name="obj">
		/// The object to serialize
		/// </param>
		/// <param name="provider">
		/// The SerializationProvider to use to perform the serialization
		/// </param>
		/// <returns>
		/// An empty buffer if obj is null, otherwise a byte array containing the serialized
		/// object.
		/// </returns>
		public static byte[] Serialize(this object obj, SerializationProvider provider)
		{
			_logger.DebugMethodCalled(obj, provider);

			#region Input Validation

			Insist.IsNotNull(provider, "provider");

			#endregion

			return provider.Serialize(obj);
		}

		/// <summary>
		/// Binary serializes the supplied object to the supplied stream.
		/// </summary>
		/// <param name="obj">
		/// The object to serialize. If object is null then the supplied stream is left unchanged.
		/// </param>
		/// <param name="stream">
		/// The stream to serialize to.  Must be open and writable.
		/// </param>
		public static void Serialize(this object obj, Stream stream)
		{
			_logger.DebugMethodCalled(obj, stream);

			Serialize(obj, stream, new BinarySerializationProvider());
		}

		/// <summary>
		/// Serializes the supplied object to the supplied stream using the supplied SerializationProvider
		/// </summary>
		/// <param name="obj">
		/// The object to serialize. If object is null then the supplied stream is left unchanged.
		/// </param>
		/// <param name="stream">
		/// The stream to serialize to.  Must be open and writable.
		/// </param>
		/// <param name="provider">
		/// The SerializationProvider to use to perform the serialization
		/// </param>
		public static void Serialize(this object obj, Stream stream, SerializationProvider provider)
		{
			_logger.DebugMethodCalled(obj, stream, provider);

			#region Input Validation

			Insist.IsNotNull(provider, "provider");

			#endregion

			provider.Serialize(obj, stream);
		}

		#endregion

		#region Deserialization

		/// <summary>
		/// Binary deserializes an object into the supplied type.
		/// </summary>
		/// <typeparam name="T">
		/// The type that you want to deserialize the buffer as
		/// </typeparam>
		/// <param name="buffer">
		/// The buffer containing the binary data to deserialize.  If this is null or empty then
		/// you'll get a default(T) back.
		/// </param>
		/// <returns>
		/// The deserialized object of the requested type or a default(T).
		/// </returns>
		public static T Deserialize<T>(this byte[] buffer)
		{
			_logger.DebugMethodCalled(buffer);

			return Deserialize<T>(buffer, new BinarySerializationProvider());
		}

		/// <summary>
		/// Deserializes an object into the supplied type using the supplied SerializationProvider
		/// </summary>
		/// <typeparam name="T">
		/// The type that you want to deserialize the buffer as
		/// </typeparam>
		/// <param name="buffer">
		/// The buffer containing the binary data to deserialize.  If this is null or empty then
		/// you'll get a default(T) back.
		/// </param>
		/// <param name="provider">
		/// The SerializationProvider to use to perform the deserialization
		/// </param>
		/// <returns>
		/// The deserialized object of the requested type or a default(T).
		/// </returns>
		public static T Deserialize<T>(this byte[] buffer, SerializationProvider provider)
		{
			_logger.DebugMethodCalled(buffer, provider);

			#region Input Validation

			Insist.IsNotNull(provider, "provider");

			#endregion

			return provider.Deserialize<T>(buffer);
		}

		/// <summary>
		/// Binary deserializes the data in the supplied stream into the supplied type.
		/// </summary>
		/// <typeparam name="T">
		/// The type that you want to deserialize the buffer as
		/// </typeparam>
		/// <param name="stream">
		/// The stream to get the binary data from.  If this is null then a default(T) is returned.
		/// </param>
		/// <returns>
		/// The deserialized object of the requested type or a default(T).
		/// </returns>
		public static T Deserialize<T>(this Stream stream)
		{
			_logger.DebugMethodCalled(stream);

			return Deserialize<T>(stream, new BinarySerializationProvider());
		}

		/// <summary>
		/// Deserialized the data in the supplied stream into the supplied type using the supplied SerializationProvider
		/// </summary>
		/// <typeparam name="T">
		/// The type that you want to deserialize the buffer as
		/// </typeparam>
		/// <param name="stream">
		/// The stream to get the binary data from.  If this is null then a default(T) is returned.
		/// </param>
		/// <param name="provider">
		/// The SerializationProvider to use to perform the deserialization
		/// </param>
		/// <returns>
		/// The deserialized object of the requested type or a default(T).
		/// </returns>
		public static T Deserialize<T>(this Stream stream, SerializationProvider provider)
		{
			_logger.DebugMethodCalled(stream, provider);

			#region Input Validation

			Insist.IsNotNull(provider, "provider");

			#endregion

			return provider.Deserialize<T>(stream);
		}

		#endregion
	}
}
