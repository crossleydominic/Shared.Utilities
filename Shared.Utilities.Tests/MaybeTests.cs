using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Shared.Utilities.Tests
{
	[TestFixture]
	public class MaybeTests
	{
		#region Test structs

		public struct TestStruct
		{
			public bool ParsedSucceeded { get; set; }

			public static bool TryParse(string toBeParsed, out TestStruct parsedValue)
			{
				
				if (string.IsNullOrEmpty(toBeParsed))
				{
					parsedValue = new TestStruct() { ParsedSucceeded = false };
					return false;
				}
				else
				{
					parsedValue = new TestStruct() { ParsedSucceeded = true };
					return true;
				}
			}
		}

		#endregion

		#region Test Enum

		public enum TestEnum
		{
			NotSet = 0,
			FirstEnumItem = 3,
			SecondEnumItem = 99
		}

		#endregion

		#region ToByte Tests

		[Test]
		public void ToByte_ValueNull()
		{
			string str = null;
			byte? value = Maybe.ToByte(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToByte_ValueEmpty()
		{
			string str = string.Empty;
			byte? value = Maybe.ToByte(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToByte_ValueValid()
		{
			string str = "1";
			byte? value = Maybe.ToByte(str);
			Assert.AreEqual(value, (byte)1);
		}

		#endregion

		#region ToChar Tests

		[Test]
		public void ToChar_ValueNull()
		{
			string str = null;
			Char? value = Maybe.ToChar(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToChar_ValueEmpty()
		{
			string str = string.Empty;
			Char? value = Maybe.ToChar(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToChar_ValueValid()
		{
			string str = "1";
			Char? value = Maybe.ToChar(str);
			Assert.AreEqual(value, '1');
		}

		#endregion

		#region ToSByte Tests

		[Test]
		public void ToSByte_ValueNull()
		{
			string str = null;
			SByte? value = Maybe.ToSByte(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToSByte_ValueEmpty()
		{
			string str = string.Empty;
			SByte? value = Maybe.ToSByte(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToSByte_ValueValid()
		{
			string str = "1";
			SByte? value = Maybe.ToSByte(str);
			Assert.AreEqual(value, (SByte)1);
		}

		#endregion

		#region ToInt Tests

		[Test]
		public void ToInt32_ValueNull()
		{
			string str = null;
			int? value = Maybe.ToInt32(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToInt32_ValueEmpty()
		{
			string str = string.Empty;
			int? value = Maybe.ToInt32(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToInt32_ValueValid()
		{
			string str = "1";
			int? value = Maybe.ToInt32(str);
			Assert.AreEqual(value, 1);
		}

		#endregion

		#region ToUInt32 Tests

		[Test]
		public void ToUInt32_ValueNull()
		{
			string str = null;
			UInt32? value = Maybe.ToUInt32(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToUInt32_ValueEmpty()
		{
			string str = string.Empty;
			UInt32? value = Maybe.ToUInt32(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToUInt32_ValueValid()
		{
			string str = "1";
			UInt32? value = Maybe.ToUInt32(str);
			Assert.AreEqual(value, 1);
		}

		#endregion

		#region ToInt16 Tests

		[Test]
		public void ToInt16_ValueNull()
		{
			string str = null;
			short? value = Maybe.ToInt16(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToInt16_ValueEmpty()
		{
			string str = string.Empty;
			short? value = Maybe.ToInt16(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToInt16_ValueValid()
		{
			string str = "1";
			short? value = Maybe.ToInt16(str);
			Assert.AreEqual(value, 1);
		}

		#endregion

		#region ToUInt16 Tests

		[Test]
		public void ToUInt16_ValueNull()
		{
			string str = null;
			UInt16? value = Maybe.ToUInt16(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToUInt16_ValueEmpty()
		{
			string str = string.Empty;
			UInt16? value = Maybe.ToUInt16(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToUInt16_ValueValid()
		{
			string str = "1";
			UInt16? value = Maybe.ToUInt16(str);
			Assert.AreEqual(value, 1);
		}

		#endregion

		#region ToInt64 Tests

		[Test]
		public void ToInt64_ValueNull()
		{
			string str = null;
			long? value = Maybe.ToInt64(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToInt64_ValueEmpty()
		{
			string str = string.Empty;
			long? value = Maybe.ToInt64(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToInt64_ValueValid()
		{
			string str = "1";
			long? value = Maybe.ToInt64(str);
			Assert.AreEqual(value, 1);
		}

		#endregion

		#region ToUInt64 Tests

		[Test]
		public void ToUInt64_ValueNull()
		{
			string str = null;
			UInt64? value = Maybe.ToUInt64(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToUInt64_ValueEmpty()
		{
			string str = string.Empty;
			UInt64? value = Maybe.ToUInt64(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToUInt64_ValueValid()
		{
			string str = "1";
			UInt64? value = Maybe.ToUInt64(str);
			Assert.AreEqual(value, (UInt64)1);
		}

		#endregion

		#region ToFloat Tests

		[Test]
		public void ToFloat_ValueNull()
		{
			string str = null;
			float? value = Maybe.ToFloat(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToFloat_ValueEmpty()
		{
			string str = string.Empty;
			float? value = Maybe.ToFloat(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToFloat_ValueValid()
		{
			//We can't actually test the parsed float values
			//here because doing a float->string->float round
			//trip might introduce rounding error that would produce
			//different float values.  Stupid Floats.
			//Instead, the best we can do is ensure that the float?
			//has a value and that it is within a very small error margin

			string str = "1.12345";
			float? value = Maybe.ToFloat(str);
			Assert.IsTrue(value.HasValue);
			Assert.GreaterOrEqual(value, value - 0.00005f);
			Assert.LessOrEqual(value, value + 0.00005f);
		}

		#endregion

		#region ToDouble Tests

		[Test]
		public void ToDouble_ValueNull()
		{
			string str = null;
			Double? value = Maybe.ToDouble(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToDouble_ValueEmpty()
		{
			string str = string.Empty;
			Double? value = Maybe.ToDouble(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToDouble_ValueValid()
		{
			//We can't actually test the parsed float values
			//here because doing a float->string->float round
			//trip might introduce rounding error that would produce
			//different float values.  Stupid Floats.
			//Instead, the best we can do is ensure that the float?
			//has a value and that it is within a very small error margin

			string str = "1.12345";
			double? value = Maybe.ToDouble(str);
			Assert.IsTrue(value.HasValue);
			Assert.GreaterOrEqual(value, value - 0.00005f);
			Assert.LessOrEqual(value, value + 0.00005f);
		}

		#endregion

		#region ToDecimal Tests

		[Test]
		public void ToDecimal_ValueNull()
		{
			string str = null;
			Decimal? value = Maybe.ToDecimal(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToDecimal_ValueEmpty()
		{
			string str = string.Empty;
			Decimal? value = Maybe.ToDecimal(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToDecimal_ValueValid()
		{
			string str = "1.12345";
			Decimal? value = Maybe.ToDecimal(str);
			Assert.AreEqual(value, 1.12345);
		}

		#endregion

		#region ToDateTime Tests

		[Test]
		public void ToDateTime_ValueNull()
		{
			string str = null;
			DateTime? value = Maybe.ToDateTime(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToDateTime_ValueEmpty()
		{
			string str = string.Empty;
			DateTime? value = Maybe.ToDateTime(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToDateTime_ValueValid()
		{
			string str = "20/01/2011 13:45:50";
			DateTime? value = Maybe.ToDateTime(str);
			Assert.AreEqual(value, new DateTime(2011, 01, 20, 13, 45, 50));
		}

		#endregion

		#region ToGuid Tests

#if !DOTNET35

		[Test]
		public void ToGuid_ValueNull()
		{
			string str = null;
			Guid? value = Maybe.ToGuid(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToGuid_ValueEmpty()
		{
			string str = string.Empty;
			Guid? value = Maybe.ToGuid(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToGuid_ValueValid()
		{
			Guid initialValue = Guid.NewGuid();
			string str = initialValue.ToString();
			Guid? value = Maybe.ToGuid(str);
			Assert.AreEqual(value, initialValue);
		}

#endif
		#endregion

		#region ToBool Tests

		[Test]
		public void ToBool_ValueZero()
		{
			string str = "0";
			Boolean? value = Maybe.ToBool(str);
			Assert.AreEqual(value, false);
		}
		[Test]
		public void ToBool_ValueFalse()
		{
			string str = "false";
			Boolean? value = Maybe.ToBool(str);
			Assert.AreEqual(value, false);
		}
		[Test]
		public void ToBool_ValueNull()
		{
			string str = null;
			Boolean? value = Maybe.ToBool(str);
			Assert.AreEqual(value, false);
		}

		[Test]
		public void ToBool_ValueEmpty()
		{
			string str = string.Empty;
			Boolean? value = Maybe.ToBool(str);
			Assert.AreEqual(value, false);
		}

		[Test]
		public void ToBool_ValueNo()
		{
			string str = "no";
			Boolean? value = Maybe.ToBool(str);
			Assert.AreEqual(value, false);
		}

		[Test]
		public void ToBool_ValueN()
		{
			string str = "n";
			Boolean? value = Maybe.ToBool(str);
			Assert.AreEqual(value, false);
		}

		[Test]
		public void ToBool_ValueIsOne()
		{
			string str = "1";
			Boolean? value = Maybe.ToBool(str);
			Assert.AreEqual(value, true);
		}

		[Test]
		public void ToBool_ValueIsMinusOne()
		{
			string str = "-1";
			Boolean? value = Maybe.ToBool(str);
			Assert.AreEqual(value, true);
		}

		[Test]
		public void ToBool_ValueIsY()
		{
			string str = "Y";
			Boolean? value = Maybe.ToBool(str);
			Assert.AreEqual(value, true);
		}

		[Test]
		public void ToBool_ValueIsYes()
		{
			string str = "yes";
			Boolean? value = Maybe.ToBool(str);
			Assert.AreEqual(value, true);
		}

		[Test]
		public void ToBool_ValueTrue()
		{
			string str = "true";
			Boolean? value = Maybe.ToBool(str);
			Assert.AreEqual(value, true);
		}

		[Test]
		public void ToBool_ValueNotConvertible()
		{
			string str = "abc";
			Boolean? value = Maybe.ToBool(str);
			Assert.IsNull(value);
		}

		#endregion

		#region ToBoolStrict Tests

		[Test]
		public void ToBoolStrict_ValueNull()
		{
			string str = null;
			Boolean? value = Maybe.ToBoolStrict(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToBoolStrict_ValueEmpty()
		{
			string str = string.Empty;
			Boolean? value = Maybe.ToBoolStrict(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToBoolStrict_ValueIsOne()
		{
			string str = "1";
			Boolean? value = Maybe.ToBoolStrict(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToBoolStrict_ValueIsMinus()
		{
			string str = "-1";
			Boolean? value = Maybe.ToBoolStrict(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToBoolStrict_ValueIsY()
		{
			string str = "Y";
			Boolean? value = Maybe.ToBoolStrict(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToBoolStrict_ValueLowercaseTrue()
		{
			string str = "true";
			Boolean? value = Maybe.ToBoolStrict(str);
			Assert.AreEqual(value, true);
		}

		[Test]
		public void ToBoolStrict_ValueValid()
		{
			string str = "True";
			Boolean? value = Maybe.ToBoolStrict(str);
			Assert.AreEqual(value, true);
		}

		#endregion

		#region ToTimeSpan Tests

		[Test]
		public void ToTimeSpan_ValueNull()
		{
			string str = null;
			TimeSpan? value = Maybe.ToTimeSpan(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToTimeSpan_ValueEmpty()
		{
			string str = string.Empty;
			TimeSpan? value = Maybe.ToTimeSpan(str);
			Assert.IsNull(value);
		}

		[Test]
		public void ToTimeSpan_ValueValid()
		{
			string str = "13:45:50";
			TimeSpan? value = Maybe.ToTimeSpan(str);
			Assert.AreEqual(value, new TimeSpan(13, 45, 50));
		}

		#endregion

		#region ToAny Tests

		[Test]
		public void ToAny_ValueNull()
		{
			string str = null;
			TestStruct? value = Maybe.ToAny<TestStruct>(str, TestStruct.TryParse);
			Assert.IsNull(value);
		}

		[Test]
		public void ToAny_ValueEmpty()
		{
			string str = string.Empty;
			TestStruct? value = Maybe.ToAny<TestStruct>(str, TestStruct.TryParse);
			Assert.IsNull(value);
		}

		[Test]
		public void ToAny_ValueValid()
		{
			string str = "a";
			TestStruct? value = Maybe.ToAny<TestStruct>(str, TestStruct.TryParse);
			Assert.IsNotNull(value);
		}

		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentNullException))]
		public void ToAny_ParseMethodNull()
		{
			string str = "a";
			TestStruct? value = Maybe.ToAny<TestStruct>(str, null);
		}

		#endregion

		#region ToEnum Tests

		[Test]
		public static void ToEnum_ValidValue()
		{
			string str = "FirstEnumItem";
			TestEnum? value = Maybe.ToEnum<TestEnum>(str);

			Assert.IsNotNull(value);
			Assert.IsTrue(Enum.IsDefined(typeof(TestEnum), value));
			Assert.AreEqual(value, TestEnum.FirstEnumItem);
		}

		[Test]
		public static void ToEnum_CaseSensitive()
		{
			string str = "firstenumitem";
			TestEnum? value = Maybe.ToEnum<TestEnum>(str, false);

			Assert.IsNull(value);
		}

		[Test]
		public static void ToEnum_ToEnumCaseInsensitive()
		{
			string str = "secondenumitem";
			TestEnum? value = Maybe.ToEnum<TestEnum>(str);
			
			Assert.IsNotNull(value);
			Assert.IsTrue(Enum.IsDefined(typeof(TestEnum), value));
			Assert.AreEqual(value, TestEnum.SecondEnumItem);
		}

		[Test]
		public static void ToEnum_ValueDoesntExist()
		{
			string str = "blah";
			TestEnum? value = Maybe.ToEnum<TestEnum>(str);

			Assert.IsNull(value);
		}

		#endregion
	}
}
