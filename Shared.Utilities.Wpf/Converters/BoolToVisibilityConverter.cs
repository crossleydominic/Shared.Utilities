using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using log4net;
using Shared.Utilities.ExtensionMethods.Logging;

namespace Shared.Utilities.Wpf.Converters
{
	/// <summary>
	/// A Converter that can convert a bool or bool? to a Visibility.
	/// Can be used as a straight converter e.g
	/// true = visible, false = collapsed
	/// or can be used inverted
	/// true = collapsed, false = visible
	/// by passing a true/false boolean to the "negateValueObject" parameter.
	/// </summary>
	public class BoolToVisibilityConverter : IValueConverter
	{		
		#region Logging

		/// <summary>
		/// Used to log information about the class
		/// </summary>
		private static ILog _logger = LogManager.GetLogger(typeof(BoolToVisibilityConverter));

		#endregion

		#region IValueConverter Members

		/// <summary>
		/// Converts the boolean passed in into a Visibility.
		/// </summary>
		/// <param name="value">
		/// Can be either a bool or a bool?.
		/// If "negateValueObject" is not set or false then
		///		true = Visible, false = collapsed, null = collapsed
		///	If "negateValueObject" evaluates to true then
		///		true = collapsed, false = Visible, null = Visible
		/// </param>
		/// <param name="negateValueObject">
		/// Whether or not to invert the flag passed in in "value".
		/// Can be a bool, bool? or a string. Strings with the values "true", "1", "-1" will be treated as true.
		/// </param>
		/// <returns>
		/// A Visiblity
		/// </returns>
		public object Convert(object value, Type targetType, object negateValueObject, System.Globalization.CultureInfo culture)
		{
			_logger.DebugMethodCalled(false, value, targetType, negateValueObject, culture);

			#region Input Validation

			if (value != null &&
				value.GetType() != typeof(bool) &&
				value.GetType() != typeof(bool?))
				//The value object must be a bool or bool? if it has a value
			{
				throw new InvalidOperationException("This converter can only be bound to bool or bool? sources.");
			}

			if (targetType == null || 
				targetType != typeof(Visibility))
				//The target must be a Visibility
			{
				throw new InvalidOperationException("This converter can only be applied to targets of Type Visibility");
			}

			#endregion

			bool visibleFlag = ConvertObjectToBoolean(value);
			bool invertVisibility = ConvertObjectToBoolean(negateValueObject);

			if (invertVisibility)
			{
				visibleFlag = !visibleFlag;
			}

			return (visibleFlag ? Visibility.Visible : Visibility.Collapsed);
		}

		/// <summary>
		/// Converts the supplied visibility back into a bool
		/// </summary>
		/// <param name="value">
		/// Must be a Visibility. 
		/// If "negateValueObject" is not set or false then
		///		Visible = true, collapsed = false, Collapsed = false
		///	If "negateValueObject" evaluates to true then
		///		Visible = false, collapsed = true, Collapsed = true
		/// </param>
		/// <param name="negateValueObject">
		/// Whether or not to invert the Visibility passed in in "value".
		/// Can be a bool, bool? or a string. Strings with the values "true", "1", "-1" will be treated as true.
		/// </param>
		/// <returns>
		/// A boolean
		/// </returns>
		public object ConvertBack(object value, Type targetType, object negateValueObject, System.Globalization.CultureInfo culture)
		{
			_logger.DebugMethodCalled(false, value, targetType, negateValueObject, culture);

			#region Input Validation

			if (targetType == null ||
				(
					targetType.IsAssignableFrom(typeof(bool)) == false &&
					targetType.IsAssignableFrom(typeof(bool?)) == false
				))
				//The target must either be a bool or bool? OR be assignable to a bool or bool?
			{
				throw new InvalidOperationException("This converter can only be applied from sources that are assignable from either bool or bool?");
			}

			#endregion

			Visibility visibility = ConvertObjectToVisibility(value);
			bool invertVisibility = ConvertObjectToBoolean(negateValueObject);

			bool returnVal = (visibility == Visibility.Visible ? true : false);

			if (invertVisibility)
			{
				returnVal = !returnVal;
			}

			return returnVal;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Helper method to convert a Visibility into a boolean
		/// </summary>
		private Visibility ConvertObjectToVisibility(object value)
		{
			_logger.DebugMethodCalled(false, value);

			Visibility defaultVisibility = Visibility.Collapsed;

			if (value is Visibility)
			{
				defaultVisibility = (Visibility)value;
			}

			return defaultVisibility;
		}

		/// <summary>
		/// Helper method to convert an object into a boolean
		/// </summary>
		private bool ConvertObjectToBoolean(object value)
		{
			_logger.DebugMethodCalled(false, value);

			bool defaultVal = false;

			if (value != null)
			{
				if (value is bool)
				{
					defaultVal = (bool)value;
				}
				else if (value is bool?)
				{
					defaultVal = ((bool?)value).HasValue ? ((bool?)value).Value : false;
				}
				else if (value is string)
				{
					if ("true".Equals((string)value, StringComparison.OrdinalIgnoreCase) ||
						"1".Equals((string)value, StringComparison.OrdinalIgnoreCase) ||
						"-1".Equals((string)value, StringComparison.OrdinalIgnoreCase))
					{
						defaultVal = true;
					}
				}
			}

			return defaultVal;
		}

		#endregion
	}
}
