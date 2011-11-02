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
	/// This class is used as a base class for any serialization providers that have a strong
	/// tie between their underlying serializers and the types that are being serialized.
	/// E.g 
	/// On construction of an XmlSerializer you MUST supply the Type that is being serialized.
	/// </summery>
	/// <typeparam name="TUnderlyingSerializerType">
	/// The concrete serializer to use.
	/// </typeparam>
	public abstract class TypedSerializationProvider<TUnderlyingSerializerType> : ExposableSerializationProvider<TUnderlyingSerializerType> where TUnderlyingSerializerType : class
	{
		#region Logging

		/// <summary>
		/// Used to log information about the class
		/// </summary>
		private static ILog _logger = LogManager.GetLogger(typeof(TypedSerializationProvider<TUnderlyingSerializerType>));

		#endregion

		#region Private members
		/// <summary>
		/// The type that this TypedSerializationProvider is associated with.  
		/// </summary>
		private Type _associatedType;

		#endregion

		#region Constructors

		/// <summary>
		/// Not allowed to create an instance of this type without a Type argument
		/// </summary>
		private TypedSerializationProvider() { }

		/// <summary>
		/// Create an instance of this provider that is tied to a specific type
		/// </summary>
		/// <param name="type">
		/// The type this provider is associated with
		/// </param>
		/// <param name="serializer">
		/// The actual underlying serialiser object that will be used to perform the
		/// serialization/deserialization
		/// </param>
		public TypedSerializationProvider(Type type, TUnderlyingSerializerType serializer)
		{
			_logger.DebugMethodCalled(type, serializer);

			#region Input Validation

			Insist.IsNotNull(type, "type");
			Insist.IsNotNull(serializer, "serializer");

			#endregion

			_associatedType = type;

			//We are associated with a type so a concrete serializer could be created.
			//Store it for use later
			UnderlyingSerializer = serializer;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Gets the type that this serializer is associated with
		/// </summary>
		public Type AssociatedType
		{
			get
			{
				return _associatedType;
			}
		}

		#endregion
	}
}
