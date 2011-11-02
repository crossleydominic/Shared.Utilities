using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using log4net;
using Shared.Utilities.ExtensionMethods.Logging;

namespace Shared.Utilities.Serialization
{
	public class XmlSerializationProvider : TypedSerializationProvider<XmlSerializer>
	{
		#region Logging

		/// <summary>
		/// Used to log information about the class
		/// </summary>
		private static ILog _logger = LogManager.GetLogger(typeof(XmlSerializationProvider));

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
		/// Create a new instance of a XmlSerializationProvider that uses an underlying
		/// XmlSerializer that is created with the supplied arguments.
		/// </summary>
		/// <param name="type">
		/// The type of object that will be serialized/deserialized
		/// </param>
		public XmlSerializationProvider(Type type) 
			: base(type, new XmlSerializer(type)) { }

		/// <summary>
		/// Create a new instance of a XmlSerializationProvider that uses an underlying
		/// XmlSerializer that is created with the supplied arguments.
		/// </summary>
		public XmlSerializationProvider(Type type, string defaultNamespace) 
			: base(type, new XmlSerializer(type, defaultNamespace)) { }

		/// <summary>
		/// Create a new instance of a XmlSerializationProvider that uses an underlying
		/// XmlSerializer that is created with the supplied arguments.
		/// </summary>
		public XmlSerializationProvider(Type type, Type[] extraTypes) 
			: base(type, new XmlSerializer(type, extraTypes)) { }

		/// <summary>
		/// Create a new instance of a XmlSerializationProvider that uses an underlying
		/// XmlSerializer that is created with the supplied arguments.
		/// </summary>
		public XmlSerializationProvider(Type type, XmlAttributeOverrides overrides) 
			: base(type, new XmlSerializer(type, overrides)) { }

		/// <summary>
		/// Create a new instance of a XmlSerializationProvider that uses an underlying
		/// XmlSerializer that is created with the supplied arguments.
		/// </summary>
		public XmlSerializationProvider(Type type, XmlRootAttribute root) 
			: base(type, new XmlSerializer(type, root)) { }

		/// <summary>
		/// Create a new instance of a XmlSerializationProvider that uses an underlying
		/// XmlSerializer that is created with the supplied arguments.
		/// </summary>
		public XmlSerializationProvider(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace) 
			: base(type, new XmlSerializer(type, overrides, extraTypes, root, defaultNamespace)) { }

#if !DOTNET35
		/// <summary>
		/// Create a new instance of a XmlSerializationProvider that uses an underlying
		/// XmlSerializer that is created with the supplied arguments.
		/// </summary>
		public XmlSerializationProvider(Type type, XmlAttributeOverrides overrides, Type[] extraTypes, XmlRootAttribute root, string defaultNamespace, string location) 
			: base(type, new XmlSerializer(type, overrides, extraTypes, root, defaultNamespace, location)) { }
#endif

		#endregion

		#region Overridden methods

		/// <summary>
		/// Serializae the object using the concrete serializer
		/// </summary>
		protected override void OnSerialize(object obj, Stream stream)
		{
			_logger.DebugMethodCalled(obj, stream);

			UnderlyingSerializer.Serialize(stream, obj);
		}

		/// <summary>
		/// Deserializae the object using the concrete serializer
		/// </summary>
		protected override T OnDeserialize<T>(Stream stream)
		{
			_logger.DebugMethodCalled(stream);

			return (T)UnderlyingSerializer.Deserialize(stream);
		}

		#endregion
	}
}
