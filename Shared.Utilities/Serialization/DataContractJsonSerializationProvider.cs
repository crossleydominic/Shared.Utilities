using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using log4net;
using Shared.Utilities.ExtensionMethods.Logging;

#if !DOTNET35
using System.Runtime.Serialization.Json;

namespace Shared.Utilities.Serialization
{
	/// <summary>
	/// A class that will serialize objects using the DataContractJsonSerializationProvider
	/// </summary>
	public class DataContractJsonSerializationProvider : TypedSerializationProvider<DataContractJsonSerializer>
	{
		#region Logging

		/// <summary>
		/// Used to log information about the class
		/// </summary>
		private static ILog _logger = LogManager.GetLogger(typeof(DataContractJsonSerializationProvider));

		#endregion

		#region Constructors

		/*
		 * NOTE
		 * 
		 * These constructors are basically pass through any arguments to the concrete
		 * serializer.  This is because most of the properties on the serializer are not
		 * changeable once it has been created.
		 */

		/// <summary>
		/// Create a new instance of a DataContractJsonSerializationProvider that uses an underlying
		/// DataContractJsonSerializer that is created with the supplied arguments.
		/// </summary>
		/// <param name="type">
		/// The type of object that will be serialized/deserialized
		/// </param>
		public DataContractJsonSerializationProvider(Type type)
			: base(type, new DataContractJsonSerializer(type)) { }

		/// <summary>
		/// Create a new instance of a DataContractJsonSerializationProvider that uses an underlying
		/// DataContractJsonSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractJsonSerializationProvider(Type type, IEnumerable<Type> knownTypes)
			: base(type, new DataContractJsonSerializer(type, knownTypes)) { }

		/// <summary>
		/// Create a new instance of a DataContractJsonSerializationProvider that uses an underlying
		/// DataContractJsonSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractJsonSerializationProvider(Type type, string rootName)
			: base(type, new DataContractJsonSerializer(type, rootName)) { }

		/// <summary>
		/// Create a new instance of a DataContractJsonSerializationProvider that uses an underlying
		/// DataContractJsonSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractJsonSerializationProvider(Type type, XmlDictionaryString rootName)
			: base(type, new DataContractJsonSerializer(type, rootName)) { }

		/// <summary>
		/// Create a new instance of a DataContractJsonSerializationProvider that uses an underlying
		/// DataContractJsonSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractJsonSerializationProvider(Type type, string rootName, IEnumerable<Type> knownTypes)
			: base(type, new DataContractJsonSerializer(type, rootName, knownTypes)) { }

		/// <summary>
		/// Create a new instance of a DataContractJsonSerializationProvider that uses an underlying
		/// DataContractJsonSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractJsonSerializationProvider(Type type, XmlDictionaryString rootName, IEnumerable<Type> knownTypes)
			: base(type, new DataContractJsonSerializer(type, rootName, knownTypes)) { }

		/// <summary>
		/// Create a new instance of a DataContractJsonSerializationProvider that uses an underlying
		/// DataContractJsonSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractJsonSerializationProvider(Type type, IEnumerable<Type> knownTypes, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, IDataContractSurrogate dataContractSurrogate, bool alwaysEmitTypeInformation)
			: base(type, new DataContractJsonSerializer(type, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, dataContractSurrogate, alwaysEmitTypeInformation)) { }

		/// <summary>
		/// Create a new instance of a DataContractJsonSerializationProvider that uses an underlying
		/// DataContractJsonSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractJsonSerializationProvider(Type type, string rootName, IEnumerable<Type> knownTypes, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, IDataContractSurrogate dataContractSurrogate, bool alwaysEmitTypeInformation)
			: base(type, new DataContractJsonSerializer(type, rootName, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, dataContractSurrogate, alwaysEmitTypeInformation)) { }

		/// <summary>
		/// Create a new instance of a DataContractJsonSerializationProvider that uses an underlying
		/// DataContractJsonSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractJsonSerializationProvider(Type type, XmlDictionaryString rootName, IEnumerable<Type> knownTypes, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, IDataContractSurrogate dataContractSurrogate, bool alwaysEmitTypeInformation)
			: base(type, new DataContractJsonSerializer(type, rootName, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, dataContractSurrogate, alwaysEmitTypeInformation)) { }

		#endregion

		#region Overridden methods

		/// <summary>
		/// Serialize the object using the concrete serializer
		/// </summary>
		protected override void OnSerialize(object obj, Stream stream)
		{
			_logger.DebugMethodCalled(obj, stream);

			UnderlyingSerializer.WriteObject(stream, obj);
		}

		/// <summary>
		/// Deserialize the object using the concrete serializer
		/// </summary>
		protected override T OnDeserialize<T>(Stream stream)
		{
			_logger.DebugMethodCalled(stream);

			return (T)UnderlyingSerializer.ReadObject(stream);
		}

		#endregion
	}
}
#endif
