using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Shared.Utilities.Tests
{
	[TestFixture]
	public class NonNullableTests
	{
		#region Construction Tests

		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentNullException))]
		public void Constructor_ConstructViaImplicitTypeCast_NullReference()
		{
			NonNullable<string> str = null;
		}

		[Test]
		public void Constructor_ConstructViaImplicitTypeCast_NonNullReference()
		{
			NonNullable<string> str = "abc";

			Assert.AreEqual(str.Value, "abc");
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void Constructor_ConstructViaConstructor_NullReference()
		{
			NonNullable<string> str = new NonNullable<string>(null);
		}

		[Test]
		public void Constructor_ConstructViaConstructor_NonNullReference()
		{
			NonNullable<string> str = new NonNullable<string>("abc");

			Assert.AreEqual(str.Value, "abc");
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(InvalidOperationException))]
		public void Constructor_DefaultConstructorCausesExceptionWhenValueRetrieved()
		{
			NonNullable<string> str = new NonNullable<string>();
			string str2 = str.Value;
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(InvalidOperationException))]
		public void Constructor_DefaultConstructorCausesExceptionWhenCast()
		{
			NonNullable<string> str = new NonNullable<string>();
			string str2 = str;
		}

		#endregion

		#region Implicit type casting tests

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void Implicit_ImplicitCastNullReferenceToNonNullable()
		{
			string str = null;
			ImplicitCastMethod(str);
		}

		[Test]
		public void Implicit_ImplicitCastNonNullReferenceToNonNullable()
		{
			string str = "abc";
			bool success = ImplicitCastMethod(str);

			Assert.IsTrue(success);
		}

		private bool ImplicitCastMethod(NonNullable<string> str)
		{
			return true;
		}

		[Test]
		public void Implicit_ImplicitCastNonNullableToReferenceType()
		{
			string str = ImplicitReturnMethod("abc");
			Assert.AreEqual(str, "abc");
		}

		private NonNullable<string> ImplicitReturnMethod(string str)
		{
			return new NonNullable<string>(str);
		}

		#endregion 

		#region Value Tests

		[Test]
		[ExpectedException(ExpectedException = typeof(InvalidOperationException))]
		public void Value_ThrowExceptionIfValueNull()
		{
			//This is the only way to create a NonNullable that contains a null reference.
			//C# does not allow overriding the default constructor on structs.
			NonNullable<string> str = new NonNullable<string>();
			string s = str.Value;
		}

		#endregion

		#region ToString tests

		[Test]
		public void ToString_ReturnsUnderlyingToString()
		{
			string str = "abc";
			NonNullable<string> str2 = new NonNullable<string>(str);

			Assert.AreEqual(str2.ToString(), str);
		}

		#endregion

		#region Equality Tests

		[Test]
		public void Equals_ValuesAreTheSame()
		{
			string str = "abc";
			NonNullable<string> str2 = "abc";

			Assert.IsTrue(str2.Equals(str));
		}

		#endregion
	}
}
