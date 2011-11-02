using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using log4net;
using Shared.Utilities.ExtensionMethods.Logging;

namespace Shared.Utilities.Serialization
{
	/// <summary>
	/// The base class for all Serialization Providers.  It's not envisaged that you
	/// will inherit directly from this class but rather from ExposableSerializationProvider.
	/// 
	/// Provides a standard set of abstract methods for Serializing/Deserializing objects to/from streams.
	/// </summary>
	public abstract class SerializationProvider
	{
		#region Logging

		/// <summary>
		/// Used to log information about the class
		/// </summary>
		private static ILog _logger = LogManager.GetLogger(typeof(SerializationProvider));

		#endregion

		#region Serialization

		/// <summary>
		/// Provides the ability to serialise the supplied object to an in memory buffer
		/// </summary>
		/// <param name="obj">
		/// The object you want to serialise
		/// </param>
		/// <returns>
		/// A byte[] that contains the serialised data if object contained serializable data.
		/// An empty buffer if obj was null.
		/// </returns>
		public byte[] Serialize(object obj)
		{
			_logger.DebugMethodCalled(obj);

			byte[] returnData = null;
			using (MemoryStream memStream = new MemoryStream())
			{
				Serialize(obj, memStream);
				byte[] tempBuffer = memStream.GetBuffer();
				returnData = new byte[memStream.Position];

				//Only return the actual data and not the entire buffer.
				//The entire buffer will contain nulls which some serializers
				//do not like when deserializing.
				Array.Copy(tempBuffer, returnData, memStream.Position);
			}

			return returnData;
		}

		/// <summary>
		/// Serilialize the supplied object to the supplied stream.
		/// </summary>
		/// <param name="obj">
		/// The object to serialize
		/// </param>
		/// <param name="stream">
		/// The stream to write the serialized object to. This stream will remain unaffected if the
		/// object is null.  The stream must be open and writable before calling this method.
		/// </param>
		public void Serialize(object obj, Stream stream)
		{
			_logger.DebugMethodCalled(obj, stream);

			#region Input Validation

			if (obj == null)
				//Just do nothing for null objects,  we don't want to throw
				//an exception because it will force more null checking in calling code.
			{
				return;
			}

			Insist.IsNotNull(stream, "stream");

			if (!stream.CanWrite)
			{
				throw new ArgumentException("Cannot write to stream", "stream");
			}

			#endregion

			//Delegate the actual serialization to a child class.
			OnSerialize(obj, stream);
		}

		/// <summary>
		/// Abstract method that all inheriting classes must implement to perform
		/// the actual serialization
		/// </summary>
		protected abstract void OnSerialize(object obj, Stream stream);

		#endregion

		#region Deserialization

		/// <summary>
		/// Deserializes the supplied buffer into the supplied object type.
		/// </summary>
		/// <typeparam name="T">
		/// The type that the buffer will be deserialized as.
		/// </typeparam>
		/// <param name="buffer">
		/// The buffer containing the serialized data.
		/// </param>
		/// <returns>
		/// The deserialized object.
		/// If the buffer is null or empty then a default(T) is returned.
		/// </returns>
		public T Deserialize<T>(byte[] buffer)
		{
			_logger.DebugMethodCalled(buffer);

			#region Input Validation

			if (buffer == null ||
				buffer.LongLength == 0)
				//Same as serializing. If we're passed a null buffer then don't throw
				//an exception, just return the default type.  This keeps null checks
				//in calling code optional.
			{
				return default(T);
			}

			#endregion

			T returnObj = default(T);

			//Turn the buffer into a stream and invoke the overload.
			using (MemoryStream memStream = new MemoryStream(buffer))
			{
				returnObj = Deserialize<T>(memStream);
			}

			return returnObj;
		}

		/// <summary>
		/// Deserializes an object from the supplied stream into the supplied type.
		/// </summary>
		/// <typeparam name="T">
		/// The type that the data from the stream will be deserialized as.
		/// </typeparam>
		/// <param name="stream">
		/// The stream to read the serialized data from. The stream must be
		/// opened and readable.
		/// </param>
		/// <returns>
		/// The deserialized object
		/// </returns>
		public T Deserialize<T>(Stream stream)
		{
			_logger.DebugMethodCalled(stream);

			#region Input validation

			if (stream == null)
			{
				return default(T);
			}

			if (!stream.CanRead)
			{
				throw new ArgumentException("Cannot read from supplied stream", "stream");
			}

			#endregion

			//Delegate actual deserializtion to a child class.
			return OnDeserialize<T>(stream);
		}

		/// <summary>
		/// An abstract method that all inheriting classes must implement to 
		/// provide the actual deserialization functionality.
		/// </summary>
		protected abstract T OnDeserialize<T>(Stream stream);

		#endregion
	}
}
