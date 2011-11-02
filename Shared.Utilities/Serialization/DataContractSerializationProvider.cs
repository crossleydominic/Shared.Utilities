using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using log4net;
using Shared.Utilities.ExtensionMethods.Logging;

namespace Shared.Utilities.Serialization
{
	public class DataContractSerializationProvider : TypedSerializationProvider<DataContractSerializer>
	{
		#region Logging

		/// <summary>
		/// Used to log information about the class
		/// </summary>
		private static ILog _logger = LogManager.GetLogger(typeof(DataContractSerializationProvider));

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
		/// Create a new instance of a DataContractSerializationProvider that uses an underlying
		/// DataContractSerializer that is created with the supplied arguments.
		/// </summary>
		/// <param name="type">
		/// The type of object that will be serialized/deserialized
		/// </param>
		public DataContractSerializationProvider(Type type)
			: base(type, new DataContractSerializer(type)) { }

		/// <summary>
		/// Create a new instance of a DataContractSerializationProvider that uses an underlying
		/// DataContractSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractSerializationProvider(Type type, IEnumerable<Type> knownTypes)
			: base(type, new DataContractSerializer(type, knownTypes)) { }

		/// <summary>
		/// Create a new instance of a DataContractSerializationProvider that uses an underlying
		/// DataContractSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractSerializationProvider(Type type, string rootName, string rootNamespace)
			: base(type, new DataContractSerializer(type, rootName, rootNamespace)) { }

		/// <summary>
		/// Create a new instance of a DataContractSerializationProvider that uses an underlying
		/// DataContractSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractSerializationProvider(Type type, XmlDictionaryString rootName, XmlDictionaryString rootNamespace)
			: base(type, new DataContractSerializer(type, rootNamespace, rootNamespace)) { }

		/// <summary>
		/// Create a new instance of a DataContractSerializationProvider that uses an underlying
		/// DataContractSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractSerializationProvider(Type type, string rootName, string rootNamespace, IEnumerable<Type> knownTypes)
			: base(type, new DataContractSerializer(type, rootName, rootNamespace, knownTypes)) { }

		/// <summary>
		/// Create a new instance of a DataContractSerializationProvider that uses an underlying
		/// DataContractSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractSerializationProvider(Type type, XmlDictionaryString rootName, XmlDictionaryString rootNamespace, IEnumerable<Type> knownTypes)
			: base(type, new DataContractSerializer(type, rootName, rootNamespace, knownTypes)) { }

		/// <summary>
		/// Create a new instance of a DataContractSerializationProvider that uses an underlying
		/// DataContractSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractSerializationProvider(Type type, IEnumerable<Type> knownTypes, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, bool preserveObjectReferences, IDataContractSurrogate dataContractSurrogate)
			: base(type, new DataContractSerializer(type, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate)) { }

#if !DOTNET35
		/// <summary>
		/// Create a new instance of a DataContractSerializationProvider that uses an underlying
		/// DataContractSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractSerializationProvider(Type type, IEnumerable<Type> knownTypes, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, bool preserveObjectReferences, IDataContractSurrogate dataContractSurrogate, DataContractResolver dataContractResolver)
			: base(type, new DataContractSerializer(type, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject,preserveObjectReferences, dataContractSurrogate, dataContractResolver)) { }

#endif

		/// <summary>
		/// Create a new instance of a DataContractSerializationProvider that uses an underlying
		/// DataContractSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractSerializationProvider(Type type, string rootName, string rootNamespace, IEnumerable<Type> knownTypes, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, bool preserveObjectReferences, IDataContractSurrogate dataContractSurrogate)
			: base(type, new DataContractSerializer(type, rootName, rootNamespace, knownTypes, maxItemsInObjectGraph,ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate)) { }

		/// <summary>
		/// Create a new instance of a DataContractSerializationProvider that uses an underlying
		/// DataContractSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractSerializationProvider(Type type, XmlDictionaryString rootName, XmlDictionaryString rootNamespace, IEnumerable<Type> knownTypes, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, bool preserveObjectReferences, IDataContractSurrogate dataContractSurrogate)
			: base(type, new DataContractSerializer(type, rootName, rootNamespace, knownTypes, maxItemsInObjectGraph,ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate)) { }

#if !DOTNET35

		/// <summary>
		/// Create a new instance of a DataContractSerializationProvider that uses an underlying
		/// DataContractSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractSerializationProvider(Type type, string rootName, string rootNamespace, IEnumerable<Type> knownTypes, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, bool preserveObjectReferences, IDataContractSurrogate dataContractSurrogate, DataContractResolver dataContractResolver)
			: base(type, new DataContractSerializer(type, rootName, rootNamespace, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate, dataContractResolver)) { }

		/// <summary>
		/// Create a new instance of a DataContractSerializationProvider that uses an underlying
		/// DataContractSerializer that is created with the supplied arguments.
		/// </summary>
		public DataContractSerializationProvider(Type type, XmlDictionaryString rootName, XmlDictionaryString rootNamespace, IEnumerable<Type> knownTypes, int maxItemsInObjectGraph, bool ignoreExtensionDataObject, bool preserveObjectReferences, IDataContractSurrogate dataContractSurrogate, DataContractResolver dataContractResolver)
			: base(type, new DataContractSerializer(type, rootName, rootNamespace, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate, dataContractResolver)) { }

#endif 
		#endregion

		#region Overridden methods

		/// <summary>
		/// Serializae the object using the concrete serializer
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
