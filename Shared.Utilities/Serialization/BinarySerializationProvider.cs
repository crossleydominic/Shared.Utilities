using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using log4net;
using Shared.Utilities.ExtensionMethods.Logging;

namespace Shared.Utilities.Serialization
{
	/// <summary>
	/// A class that will provide binary serialization of objects
	/// </summary>
	public class BinarySerializationProvider : ExposableSerializationProvider<BinaryFormatter>
	{
		#region Logging

		/// <summary>
		/// Used to log information about the class
		/// </summary>
		private static ILog _logger = LogManager.GetLogger(typeof(BinarySerializationProvider));

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance of the BinarySerialization class with a default Underylying BinaryFormatter
		/// </summary>
		public BinarySerializationProvider() 
		{
			_logger.DebugMethodCalled();

			UnderlyingSerializer = new BinaryFormatter();
		}

		/// <summary>
		/// Creates a new instance of the BinarySerialization class with an underlying BinaryFormatter created
		/// with the supplied arguments
		/// </summary>
		public BinarySerializationProvider(ISurrogateSelector selector, StreamingContext context)
		{
			_logger.DebugMethodCalled(selector, context);

			UnderlyingSerializer = new BinaryFormatter(selector, context);
		}

		#endregion

		#region Overridden methods

		/// <summary>
		/// Serialize the object using the binary formatter
		/// </summary>
		protected override void OnSerialize(object obj, Stream stream)
		{
			_logger.DebugMethodCalled(obj, stream);

			UnderlyingSerializer.Serialize(stream, obj);
		}

		
		/// <summary>
		/// Deserialize the object using the binary formatter
		/// </summary>
		protected override T OnDeserialize<T>(Stream stream)
		{
			_logger.DebugMethodCalled(stream);

			return (T)UnderlyingSerializer.Deserialize(stream);
		}

		#endregion
	}
}
