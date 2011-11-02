using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.Threading;
using Shared.Utilities.Wpf.Tests.TestVisuals;
using System.Windows.Controls;
using Shared.Utilities.Wpf.Converters;
using System.Windows;

namespace Shared.Utilities.Wpf.Tests.Converters
{
	[TestFixture]
	public class BoolToVisibilityConverterTests
	{
		#region Private members

		/// <summary>
		/// A list of object that the converter will recognize as "true" for its negation parameter
		/// </summary>
		private List<object> _trueValues = new List<object>(new object[] { "true", "-1", "1", true });

		#endregion

		#region Helper classes

		public class BoolToVisibilityConverterBindableBoolClass
		{
			public bool IsVisible1 { get; set; }
			public bool IsVisible2 { get; set; }
		}

		public class BoolToVisibilityConverterBindableNonBoolClass
		{
			public string IsVisible1 { get; set; }
			public string IsVisible2 { get; set; }
		}

		#endregion

		#region Convert Tests

		/// <summary>
		/// Tests the converter when the target type is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException=typeof(InvalidOperationException))]
		public void Convert_NullTargetType()
		{
			BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
			converter.Convert(true, null, null, null);
		}

		/// <summary>
		/// Tests the converter when the target type is not Visibility
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(InvalidOperationException))]
		public void Convert_InvalidTargetType()
		{
			BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
			converter.Convert(true, typeof(Uri), null, null);
		}

		/// <summary>
		/// Tests the converter when converting a null value and not specifying whether or not to negate that value
		/// </summary>
		[Test]
		public void Convert_NullValue_NullNegateValue()
		{
			BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
			Visibility visibility = (Visibility)converter.Convert(null, typeof(Visibility), null, null);
			Assert.IsTrue(visibility == Visibility.Collapsed);

			visibility = (Visibility)converter.Convert((bool?)null, typeof(Visibility), null, null);
			Assert.IsTrue(visibility == Visibility.Collapsed);
		}

		/// <summary>
		/// Tests the converter when converting a true value and not specifying whether or not to negate that value
		/// </summary>
		[Test]
		public void Convert_TrueValue_NullNegateValue()
		{
			bool boolVal = true;
			bool? nullableBoolVal = true;

			BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
			Visibility visibility = (Visibility)converter.Convert(boolVal, typeof(Visibility), null, null);
			Assert.IsTrue(visibility == Visibility.Visible);

			visibility = (Visibility)converter.Convert(nullableBoolVal, typeof(Visibility), null, null);
			Assert.IsTrue(visibility == Visibility.Visible);
		}

		/// <summary>
		/// Tests the converter when converting a false value and not specifying whether or not to negate that value
		/// </summary>
		public void Convert_FalseValue_NullNegateValue()
		{
			bool boolVal = false;
			bool? nullableBoolVal = false;

			BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
			Visibility visibility = (Visibility)converter.Convert(boolVal, typeof(Visibility), null, null);
			Assert.IsTrue(visibility == Visibility.Collapsed);

			visibility = (Visibility)converter.Convert(nullableBoolVal, typeof(Visibility), null, null);
			Assert.IsTrue(visibility == Visibility.Collapsed);
		}

		/// <summary>
		/// Tests the converter when converting a null value and specifically not negating that value
		/// </summary>
		[Test]
		public void Convert_NullValue_FalseNegateValue()
		{
			BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
			Visibility visibility = (Visibility)converter.Convert(null, typeof(Visibility), false, null);
			Assert.IsTrue(visibility == Visibility.Collapsed);

			visibility = (Visibility)converter.Convert((bool?)null, typeof(Visibility), false, null);
			Assert.IsTrue(visibility == Visibility.Collapsed);
		}

		/// <summary>
		/// Tests the converter when converting a false value and specifically not negating that value
		/// </summary>
		[Test]
		public void Convert_TrueValue_FalseNegateValue()
		{
			bool boolVal = true;
			bool? nullableBoolVal = true;

			BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
			Visibility visibility = (Visibility)converter.Convert(boolVal, typeof(Visibility), false, null);
			Assert.IsTrue(visibility == Visibility.Visible);

			visibility = (Visibility)converter.Convert(nullableBoolVal, typeof(Visibility), false, null);
			Assert.IsTrue(visibility == Visibility.Visible);
		}

		/// <summary>
		/// Tests the converter when converting a false value and specifically not negating that value
		/// </summary>
		public void Convert_FalseValue_FalseNegateValue()
		{
			bool boolVal = false;
			bool? nullableBoolVal = false;

			BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
			Visibility visibility = (Visibility)converter.Convert(boolVal, typeof(Visibility), false, null);
			Assert.IsTrue(visibility == Visibility.Collapsed);

			visibility = (Visibility)converter.Convert(nullableBoolVal, typeof(Visibility), false, null);
			Assert.IsTrue(visibility == Visibility.Collapsed);
		}

		/// <summary>
		/// Tests the converter when converting a null value and specifically negating that value
		/// </summary>
		[Test]
		public void Convert_NullValue_TrueNegateValue()
		{
			foreach (object truthinessObject in _trueValues)
			{
				BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
				Visibility visibility = (Visibility)converter.Convert(null, typeof(Visibility), truthinessObject, null);
				Assert.IsTrue(visibility == Visibility.Visible);

				visibility = (Visibility)converter.Convert((bool?)null, typeof(Visibility), truthinessObject, null);
				Assert.IsTrue(visibility == Visibility.Visible);
			}
		}

		/// <summary>
		/// Tests the converter when converting a true value and specifically negating that value
		/// </summary>
		[Test]
		public void Convert_TrueValue_TrueNegateValue()
		{
			foreach (object truthinessObject in _trueValues)
			{
				bool boolVal = true;
				bool? nullableBoolVal = true;

				BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
				Visibility visibility = (Visibility)converter.Convert(boolVal, typeof(Visibility), truthinessObject, null);
				Assert.IsTrue(visibility == Visibility.Collapsed);

				visibility = (Visibility)converter.Convert(nullableBoolVal, typeof(Visibility), truthinessObject, null);
				Assert.IsTrue(visibility == Visibility.Collapsed);
			}
		}

		/// <summary>
		/// Tests the converter when converting a false value and specifically negating that value
		/// </summary>
		public void Convert_FalseValue_TrueNegateValue()
		{
			foreach (object truthinessObject in _trueValues)
			{
				bool boolVal = false;
				bool? nullableBoolVal = false;

				BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
				Visibility visibility = (Visibility)converter.Convert(boolVal, typeof(Visibility), truthinessObject, null);
				Assert.IsTrue(visibility == Visibility.Visible);

				visibility = (Visibility)converter.Convert(nullableBoolVal, typeof(Visibility), truthinessObject, null);
				Assert.IsTrue(visibility == Visibility.Visible);
			}
		}

		#endregion

		#region ConvertBack Tests


		/// <summary>
		/// Tests the converter when converting back and the target type is null
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(InvalidOperationException))]
		public void ConvertBack_NullTargetType()
		{
			BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
			converter.ConvertBack(true, null, null, null);
		}

		/// <summary>
		/// Tests the converter when converting back and the target type is not a bool or bool?
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(InvalidOperationException))]
		public void ConvertBack_InvalidTargetType()
		{
			BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
			converter.ConvertBack(true, typeof(Uri), null, null);
		}

		/// <summary>
		/// Tests the converter when converting back a null value and not specifying whether or not to negate that value
		/// </summary>
		[Test]
		public void ConvertBack_NullValue_NullNegateValue()
		{
			BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
			bool result = (bool)converter.ConvertBack(null, typeof(bool), null, null);
			Assert.IsFalse(result);

			bool? nullableResult = (bool?)converter.ConvertBack(null, typeof(bool?), null, null);
			Assert.IsTrue(nullableResult.HasValue && nullableResult == false);
		}

		/// <summary>
		/// Tests the converter when converting back a Visibility.Visible value and not specifying whether or not to negate that value
		/// </summary>
		[Test]
		public void ConvertBack_VisibleValue_NullNegateValue()
		{
			BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
			bool result = (bool)converter.ConvertBack(Visibility.Visible, typeof(bool), null, null);
			Assert.IsTrue(result);

			bool? nullableResult = (bool?)converter.ConvertBack(Visibility.Visible, typeof(bool?), null, null);
			Assert.IsTrue(nullableResult.HasValue && nullableResult == true);
		}

		/// <summary>
		/// Tests the converter when converting back a Visibility.Collapsed value and not specifying whether or not to negate that value
		/// </summary>
		public void ConvertBack_CollapsedValue_NullNegateValue()
		{
			BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
			bool result = (bool)converter.ConvertBack(Visibility.Collapsed, typeof(bool), null, null);
			Assert.IsFalse(result);

			bool? nullableResult = (bool?)converter.ConvertBack(Visibility.Collapsed, typeof(bool?), null, null);
			Assert.IsTrue(nullableResult.HasValue && nullableResult == false);
		}

		/// <summary>
		/// Tests the converter when converting back a null value and specifically not negating that value
		/// </summary>
		[Test]
		public void ConvertBack_NullValue_FalseNegateValue()
		{
			BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
			bool result = (bool)converter.ConvertBack(null, typeof(bool), false, null);
			Assert.IsFalse(result);

			bool? nullableResult = (bool?)converter.ConvertBack(null, typeof(bool?), false, null);
			Assert.IsTrue(nullableResult.HasValue && nullableResult == false);
		}

		/// <summary>
		/// Tests the converter when converting back a Visibility.Visible value and specifically not negating that value
		/// </summary>
		[Test]
		public void ConvertBack_VisibleValue_FalseNegateValue()
		{
			BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
			bool result = (bool)converter.ConvertBack(Visibility.Visible, typeof(bool), false, null);
			Assert.IsTrue(result);

			bool? nullableResult = (bool?)converter.ConvertBack(Visibility.Visible, typeof(bool?), false, null);
			Assert.IsTrue(nullableResult.HasValue && nullableResult == true);
		}

		/// <summary>
		/// Tests the converter when converting back a Visibility.Collapsed value and specifically not negating that value
		/// </summary>
		public void ConvertBack_CollapsedValue_FalseNegateValue()
		{
			BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
			bool result = (bool)converter.ConvertBack(Visibility.Collapsed, typeof(bool), false, null);
			Assert.IsFalse(result);

			bool? nullableResult = (bool?)converter.ConvertBack(Visibility.Collapsed, typeof(bool?), false, null);
			Assert.IsTrue(nullableResult.HasValue && nullableResult == false);
		}

		/// <summary>
		/// Tests the converter when converting back a null value and specifically negating that value
		/// </summary>
		[Test]
		public void ConvertBack_NullValue_TrueNegateValue()
		{
			foreach (object truthinessObject in _trueValues)
			{
				BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
				bool result = (bool)converter.ConvertBack(null, typeof(bool), truthinessObject, null);
				Assert.IsTrue(result);

				bool? nullableResult = (bool?)converter.ConvertBack(null, typeof(bool?), truthinessObject, null);
				Assert.IsTrue(nullableResult.HasValue && nullableResult == true);
			}
		}

		/// <summary>
		/// Tests the converter when converting back a Visibility.Visible value and specifically negating that value
		/// </summary>
		[Test]
		public void ConvertBack_VisibleValue_TrueNegateValue()
		{
			foreach (object truthinessObject in _trueValues)
			{
				BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
				bool result = (bool)converter.ConvertBack(Visibility.Visible, typeof(bool), truthinessObject, null);
				Assert.IsFalse(result);

				bool? nullableResult = (bool?)converter.ConvertBack(Visibility.Visible, typeof(bool?), truthinessObject, null);
				Assert.IsTrue(nullableResult.HasValue && nullableResult == false);
			}
		}

		/// <summary>
		/// Tests the converter when converting back a Visibility.Collapsed value and specifically negating that value
		/// </summary>
		public void ConvertBack_CollapsedValue_TrueNegateValue()
		{
			foreach (object truthinessObject in _trueValues)
			{
				BoolToVisibilityConverter converter = new BoolToVisibilityConverter();
				bool result = (bool)converter.ConvertBack(Visibility.Collapsed, typeof(bool), truthinessObject, null);
				Assert.IsTrue(result);

				bool? nullableResult = (bool?)converter.ConvertBack(Visibility.Collapsed, typeof(bool?), truthinessObject, null);
				Assert.IsTrue(nullableResult.HasValue && nullableResult == true);
			}
		}


		#endregion

		#region DataBinding Tests

		[Test]
		public void DataBinding_BindToBooleanMember()
		{
			ThreadRunner.RunInSTA(delegate {

				//Create the visual
				BoolToVisibilityConverterTestVisual testVisual = new BoolToVisibilityConverterTestVisual();
				
				//Create the object that the visual will databind to.
				BoolToVisibilityConverterBindableBoolClass dataContext = new BoolToVisibilityConverterBindableBoolClass();
				dataContext.IsVisible1 = true;
				dataContext.IsVisible2 = true;
				testVisual.DataContext = dataContext;

				//Render the visual
				RenderUtility.RenderVisual(testVisual);

				//Check the visibility of the controls
				Assert.IsTrue(testVisual.nonInvertingVisibilityControl.Visibility == Visibility.Visible);
				Assert.IsTrue(testVisual.invertingVisibilityControl.Visibility == Visibility.Collapsed);

				//Negate the visibilities to check that ConvertBack works ok.
				testVisual.NegateCurrentVisbilities();
				RenderUtility.RenderVisual(testVisual);

				//Check the visibility flags in our datacontext
				Assert.IsTrue(dataContext.IsVisible1 == false);
				Assert.IsTrue(dataContext.IsVisible2 == false);

			});
		}

		[Test]
		[ExpectedException(ExpectedException=(typeof(InvalidOperationException)))]
		public void DataBinding_BindToNonBooleanMember()
		{
			ThreadRunner.RunInSTA(delegate
			{

				//Create the visual
				BoolToVisibilityConverterTestVisual testVisual = new BoolToVisibilityConverterTestVisual();

				//Create the object that the visual will databind to.
				BoolToVisibilityConverterBindableNonBoolClass dataContext = new BoolToVisibilityConverterBindableNonBoolClass();
				dataContext.IsVisible1 = "some string 1";
				dataContext.IsVisible2 = "some string 2";
				testVisual.DataContext = dataContext;

				//Render the visual
				RenderUtility.RenderVisual(testVisual);
			});
		}
		

		#endregion
	}
}
