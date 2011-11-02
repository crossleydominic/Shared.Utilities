using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities
{
	/// <summary>
	/// A struct that prevents null references.
	/// </summary>
	public struct NonNullable<T> where T : class
	{
		#region Private members

		/// <summary>
		/// The wrapped reference type
		/// </summary>
		private readonly T _value;

		#endregion

		#region Constructors

		/// <summary>
		/// Create a new NonNullable type that wraps the passed reference type
		/// </summary>
		/// <param name="value">
		/// The reference type to wrap
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if value is null
		/// </exception>
		public NonNullable(T value)
		{
			Insist.IsNotNull(value, "value");

			_value = value;
		}

		#endregion

		#region Type casting

		/// <summary>
		/// Implicitly cast a reference type into a NonNullable
		/// </summary>
		public static implicit operator NonNullable<T>(T value)
		{
			return new NonNullable<T>(value);
		}

		/// <summary>
		/// Implicitly cast a NonNullable to a reference type.
		/// </summary>
		public static implicit operator T(NonNullable<T> wrapper)
		{
			return wrapper.Value;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Gets access to the wrapped value.
		/// </summary>
		public T Value
		{
			get
			{
				if (_value == null)
				{
					throw new InvalidOperationException("Cannot retrieve the value because it is null. This type may have been created with the default struct constructor.");
				}

				return _value;
			}
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Compares two NonNullables for equality
		/// </summary>
		public override bool Equals(object obj)
		{
			return _value.Equals(obj);
		}

		/// <summary>
		/// Gets the hashcode for this nonnullable type
		/// </summary>
		public override int GetHashCode()
		{
			return _value.GetHashCode();
		}

		/// <summary>
		/// Converts this nonnullable type to a string
		/// </summary>
		public override string ToString()
		{
			return _value.ToString();
		}

		#endregion
	}
}
