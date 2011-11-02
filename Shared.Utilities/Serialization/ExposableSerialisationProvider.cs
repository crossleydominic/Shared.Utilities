using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.Serialization
{
	/// <summary>
	/// A SerializationProvider that exposes the underlying serializer so that 
	/// consuming code can modify the serializer before performing a serialization
	/// operation.
	/// </summary>
	/// <typeparam name="TUnderlyingSerializerType">
	/// The actual underlying serializer type.
	/// </typeparam>
	public abstract class ExposableSerializationProvider<TUnderlyingSerializerType> : SerializationProvider where TUnderlyingSerializerType : class
	{
		/// <summary>
		/// The actual underlying serializer that will be used to perform (de)serialization.
		/// </summary>
		/// <remarks>
		/// Purposefully marked as Private.  We want any derviving types to go through the 
		/// Get accessor to obtain a reference to the serializer.
		/// </remarks>
		private TUnderlyingSerializerType _underlyingSerializer;

		/// <summary>
		/// Provide access to the serializer to external code.
		/// </summary>
		public TUnderlyingSerializerType UnderlyingSerializer
		{
			get
			{
				return _underlyingSerializer;
			}
			protected set
			{
				_underlyingSerializer = value;
			}
		}
	}
}
