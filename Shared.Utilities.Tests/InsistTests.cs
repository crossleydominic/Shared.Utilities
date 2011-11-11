using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data;

namespace Shared.Utilities.Tests {
	[TestFixture]
	public class InsistTests {

		public const string ARGUMENT_NAME = "MyArg";

		public const string MESSAGE = "*#MyMessage@$";

		#region Test Enums

		private enum TestEnum
		{
			First = 1,
			Second = 2,
			Last = 99
		}

		#endregion

		#region IsNotNull

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void IsNotNull_1_Null_Value_Throws_Exception() {

			Insist.IsNotNull(null, "value");

		}

		[Test]
		public void IsNotNull_1_Non_Null_Value_Does_Not_Throw_Exception() {

            Insist.IsNotNull(new Object(), "value");

		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void IsNotNull_2_Null_Value_Throws_Exception() {

			Insist.IsNotNull(null, ARGUMENT_NAME);

		}

		[Test]
		public void IsNotNull_2_Non_Null_Value_Does_Not_Throw_Exception() {

			Insist.IsNotNull(new Object(), ARGUMENT_NAME);

		}

		[Test]
		public void IsNotNull_2_Thrown_Exception_Has_Correct_Argument_Name() {

			try {

				Insist.IsNotNull(null, ARGUMENT_NAME);

			} catch(ArgumentNullException e) {

				Assert.AreEqual(ARGUMENT_NAME, e.ParamName);

			}

		}

		[Test]
		public void IsNotNull_2_Thrown_Exception_Has_Correct_Message()
		{

			try
			{

				Insist.IsNotNull(null, ARGUMENT_NAME, MESSAGE);

			}
			catch (ArgumentNullException e)
			{

				Assert.IsTrue(e.Message.Contains(MESSAGE));

			}

		}

		#endregion

		#region IsNotNullOrEmpty

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		[TestCase("")]
		[TestCase(null)]
		public void IsNotNullOrEmpty_1_Null_Or_Empty_Value_Throws_Exception(string argValue) {

            Insist.IsNotNullOrEmpty(argValue, "argVlue");

		}

		[Test]
		public void IsNotNullOrEmpty_1_Non_Null_Or_Empty_Value_Does_Not_Throw_Exception() {

            Insist.IsNotNullOrEmpty("This is not null", "value");

		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		[TestCase("")]
		[TestCase(null)]
		public void IsNotNullOrEmpty_2_Null_Or_Empty_Value_Throws_Exception(string argValue) {

			Insist.IsNotNullOrEmpty(argValue, ARGUMENT_NAME);

		}

		[Test]
		public void IsNotNullOrEmpty_2_Non_Null_Or_Empty_Value_Does_Not_Throw_Exception() {

			Insist.IsNotNullOrEmpty("This is not null.", ARGUMENT_NAME);

		}

		[Test]
		public void IsNotNullOrEmpty_Thrown_Exception_Has_Correct_Argument_Name() {

			try {

				Insist.IsNotNullOrEmpty(null, ARGUMENT_NAME);

			} catch(ArgumentException e) {

				Assert.AreEqual(ARGUMENT_NAME, e.ParamName);

			}

		}

		[Test]
		public void IsNotNullOrEmpty_Thrown_Exception_Has_Correct_Message()
		{

			try
			{

				Insist.IsNotNullOrEmpty(null, ARGUMENT_NAME, MESSAGE);

			}
			catch (ArgumentException e)
			{

				Assert.IsTrue(e.Message.Contains(MESSAGE));

			}

		}

		#endregion

		#region IsWithinBounds

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		[TestCase(1, 5, 10)]
		[TestCase(10, 1, 5)]
		public void IsWithinBounds_Value_Out_Of_Bounds_Throws_Exception(int value, int min, int max) {

			Insist.IsWithinBounds(value, min, max, ARGUMENT_NAME);

		}

		[Test]
		public void IsWithinBounds_Value_Witin_Bounds_Does_Not_Throw_Exception() {

			Insist.IsWithinBounds(5, 1, 10, ARGUMENT_NAME);

		}

		[Test]
		public void IsWithinBounds_Thrown_Exception_Has_Correct_Argument_Name() {

			try {

				Insist.IsWithinBounds(1, 5, 10, ARGUMENT_NAME);

			} catch(ArgumentException e) {

				Assert.AreEqual(ARGUMENT_NAME, e.ParamName);

			}

		}

		[Test]
		public void IsWithinBounds_Thrown_Exception_Has_Correct_Message()
		{

			try
			{

				Insist.IsWithinBounds(1, 5, 10, ARGUMENT_NAME, MESSAGE);

			}
			catch (ArgumentException e)
			{

				Assert.IsTrue(e.Message.Contains(MESSAGE));

			}

		}

		#endregion

		#region IsAtLeast

		[Test]
		[ExpectedException(typeof(ArgumentException))]		
		public void IsAtLeast_Value_Out_Of_Bounds_Throws_Exception() {

			Insist.IsAtLeast(1, 5, ARGUMENT_NAME);

		}

		[Test]
		public void IsAtLeast_Value_Within_Bounds_Does_Not_Throw_Exception() {

			Insist.IsAtLeast(10, 5, ARGUMENT_NAME);

		}

		[Test]
		public void IsAtLeast_Thrown_Exception_Has_Correct_Argument_Name() {

			try {

				Insist.IsAtLeast(1, 5, ARGUMENT_NAME);

			} catch(ArgumentException e) {

				Assert.AreEqual(ARGUMENT_NAME, e.ParamName);

			}

		}

		[Test]
		public void IsAtLeast_Thrown_Exception_Has_Correct_Message()
		{

			try
			{

				Insist.IsAtLeast(1, 5, ARGUMENT_NAME, MESSAGE);

			}
			catch (ArgumentException e)
			{

				Assert.IsTrue(e.Message.Contains(MESSAGE));

			}

		}

		#endregion

		#region IsAtMost

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IsAtMost_Value_Out_Of_Bounds_Throws_Exception()
		{
			Insist.IsAtMost(10, 5, ARGUMENT_NAME);
		}

		[Test]
		public void IsAtMost_Value_Within_Bounds_Does_Not_Throw_Exception()
		{
			Insist.IsAtMost(1, 5, ARGUMENT_NAME);
		}

		[Test]
		public void IsAtMost_Thrown_Exception_Has_Correct_Argument_Name()
		{
			try
			{
				Insist.IsAtMost(10, 5, ARGUMENT_NAME);
			}
			catch (ArgumentException e)
			{
				Assert.AreEqual(ARGUMENT_NAME, e.ParamName);
			}
		}

		[Test]
		public void IsAtMost_Thrown_Exception_Has_Correct_Message()
		{
			try
			{
				Insist.IsAtMost(10, 5, ARGUMENT_NAME, MESSAGE);
			}
			catch (ArgumentException e)
			{
				Assert.IsTrue(e.Message.Contains(MESSAGE));
			}
		}

		#endregion

		#region IsAssignableFrom

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IsAssignableFrom_Unassignable_Type_Throws_Exception() {

			Insist.IsAssignableFrom<IDisposable>(typeof(Object), ARGUMENT_NAME);

		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void IsAssignableFrom_Null_Type_Throws_Exception() {

			Insist.IsAssignableFrom<IDisposable>(null, ARGUMENT_NAME);

		}

		[Test]
		public void IsAssignableFrom_Assignable_Type_Does_Not_Throw_Exception() {

			Insist.IsAssignableFrom<IDisposable>(typeof(IDataReader), ARGUMENT_NAME);

		}

		[Test]
		public void IsAssignableFrom_Thrown_Exception_Has_Correct_Argument_Name() {

			try {

				Insist.IsAssignableFrom<IDisposable>(typeof(Object), ARGUMENT_NAME);

			} catch(ArgumentException e) {

				Assert.AreEqual(ARGUMENT_NAME, e.ParamName);

			}

		}

		[Test]
		public void IsAssignableFrom_Thrown_Exception_Has_Correct_Message()
		{

			try
			{

				Insist.IsAssignableFrom<IDisposable>(typeof(Object), ARGUMENT_NAME, MESSAGE);

			}
			catch (ArgumentException e)
			{

				Assert.IsTrue(e.Message.Contains(MESSAGE));

			}

		}

		#endregion

		#region Equality

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Equality_Non_Matching_Values_Throws_Exception() {

			Insist.Equality("ABC123", "DEF456", ARGUMENT_NAME);

		}

		[Test]
		[TestCase("ABC123")]
		[TestCase(12345)]
		[TestCase(12345D)]
		[TestCase(12345L)]		
		public void Equality_Matching_Values_Does_Not_Throw_Exception(Object expected) {

			Insist.Equality(expected, expected, ARGUMENT_NAME);

		}

		[Test]
		public void Equality_Thrown_Exception_Has_Correct_Argument_Name() {

			try {

				Insist.Equality("ABC123", "DEF456", ARGUMENT_NAME);

			} catch(ArgumentException e) {

				Assert.AreEqual(ARGUMENT_NAME, e.ParamName);

			}

		}

		[Test]
		public void Equality_Thrown_Exception_Has_Correct_Message()
		{

			try
			{

				Insist.Equality("ABC123", "DEF456", ARGUMENT_NAME, MESSAGE);

			}
			catch (ArgumentException e)
			{

				Assert.IsTrue(e.Message.Contains(MESSAGE));

			}

		}

		#endregion

		#region AllItemsAreNotNull Tests

		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentNullException))]
		public void AllItemsAreNotNull_Null_Collection_Throws_Exception()
		{
			IList<string> list = null;
			Insist.AllItemsAreNotNull(list, "list");
		}

		[Test]
		public void AllItemsAreNotNull_Empty_Collection_Does_Not_Throw_Exception()
		{
			IList<string> list = new List<string>();
            Insist.AllItemsAreNotNull(list, "list");
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void AllItemsAreNotNull_Collection_Contains_Null_Throws_Exception()
		{
			IList<string> list = new List<string>() { "a", "b", null, "c" };
            Insist.AllItemsAreNotNull(list, "list");
		}

		[Test]
		public void AllItemsAreNotNull_Collection_Does_Not_Contain_Null_Does_Not_Throw_Exception()
		{
			IList<string> list = new List<string>() { "a", "b", "c" };
            Insist.AllItemsAreNotNull(list, "list");
		}

		[Test]
		public void AllItemsAreNotNull_Thrown_Exception_Has_Correct_Argument_Name()
		{
			try
			{
				IList<string> list = new List<string>() { "a", "b", null, "c" };
				Insist.AllItemsAreNotNull(list, ARGUMENT_NAME);
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual(ARGUMENT_NAME, ae.ParamName);
			}
		}

		[Test]
		public void AllItemsAreNotNull_Thrown_Exception_Has_Correct_Message()
		{
			try
			{
				IList<string> list = new List<string>() { "a", "b", null, "c" };
				Insist.AllItemsAreNotNull(list, ARGUMENT_NAME, MESSAGE);
			}
			catch (ArgumentException ae)
			{
				Assert.IsTrue(ae.Message.Contains(MESSAGE));
			}
		}

		#endregion

		#region AllItemsSatisfyCondition Tests

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void AllItemsSatisfyCondition_Null_Collection_Throws_Exception()
		{
			IList<int> list = null;
			Insist.AllItemsSatisfyCondition(list, (i) => { return i > 0; }, "list");
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void AllItemsSatisfyCondition_Null_Predicate_Throws_Exception()
		{
			IList<int> list = new List<int>() { 1, 2, 3, 4, 5 };
            Insist.AllItemsSatisfyCondition(list, null, "list");
		}

		[Test]
		public void AllItemsSatisfyCondition_Empty_Collection_Does_Not_Throw_Exception()
		{
			IList<int> list = new List<int>();
            Insist.AllItemsSatisfyCondition(list, (i) => { return i > 0; }, "list");
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void AllItemsSatisfyCondition_Collection_Contains_Invalid_Value_Throws_Exception()
		{
			IList<int> list = new List<int>() { 1, 2, 3, 0, 4, 5 };
            Insist.AllItemsSatisfyCondition(list, (i) => { return i > 0; }, "list");
		}

		[Test]
		public void AllItemsSatisfyCondition_Collection_Does_Not_Contain_Invalid_Value_Does_Not_Throw_Exception()
		{
			IList<int> list = new List<int>() { 1, 2, 3, 4, 5 };
            Insist.AllItemsSatisfyCondition(list, (i) => { return i > 0; }, "list");
		}

		[Test]
		public void AllItemsSatisfyCondition_Thrown_Exception_Has_Correct_Argument_Name()
		{
			try
			{
				IList<int> list = new List<int>() { 1, 2, 3, 0, 4, 5 };
				Insist.AllItemsSatisfyCondition(list, (i) => { return i > 0; }, ARGUMENT_NAME);
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual(ARGUMENT_NAME, ae.ParamName);
			}
		}

		[Test]
		public void AllItemsSatisfyCondition_Thrown_Exception_Has_Correct_Message()
		{
			try
			{
				IList<int> list = new List<int>() { 1, 2, 3, 0, 4, 5 };
				Insist.AllItemsSatisfyCondition(list, (i) => { return i > 0; }, ARGUMENT_NAME, MESSAGE);
			}
			catch (ArgumentException ae)
			{
				Assert.IsTrue(ae.Message.Contains(MESSAGE));
			}
		}

		#endregion

		#region ContainsAtLeast Tests

		[Test]
		[ExpectedException(ExpectedException=typeof(ArgumentNullException))]
		public void ContainsAtLeast_Null_Collection_Throws_Exception()
		{
			List<string> list = null;
            Insist.ContainsAtLeast(list, 1, "list");
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void ContainsAtLeast_Number_Of_Items_Less_Than_Zero_Throws_Exception()
		{
			List<string> list = new List<string>();
            Insist.ContainsAtLeast(list, -1, "list");
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void ContainsAtLeast_Number_Of_Items_Less_Than_Required_Throws_Exception()
		{
			List<string> list = new List<string>() { "a", "b", "c" };
            Insist.ContainsAtLeast(list, 10, "list");
		}

		[Test]
		public void ContainsAtLeast_Number_Of_Items_Equal_To_Required_Does_Not_Throw_Exception()
		{
			List<string> list = new List<string>() { "a", "b", "c" };
            Insist.ContainsAtLeast(list, 3, "list");
		}

		[Test]
		public void ContainsAtLeast_Number_Of_Items_More_Than_Required_Does_Not_Throw_Exception()
		{
			List<string> list = new List<string>() { "a", "b", "c" };
            Insist.ContainsAtLeast(list, 1, "list");
		}

		[Test]
		public void ContainsAtLeast_Thrown_Exception_Has_Correct_Argument_Name()
		{
			try
			{
				IList<string> list = new List<string>() { "a", "b", "c" };
				Insist.ContainsAtLeast(list, 10, ARGUMENT_NAME);
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual(ARGUMENT_NAME, ae.ParamName);
			}
		}

		[Test]
		public void ContainsAtLeast_Thrown_Exception_Has_Correct_Message()
		{
			try
			{
				IList<string> list = new List<string>() { "a", "b", "c" };
				Insist.ContainsAtLeast(list, 10, ARGUMENT_NAME, MESSAGE);
			}
			catch (ArgumentException ae)
			{
				Assert.IsTrue(ae.Message.Contains(MESSAGE));
			}
		}

		#endregion

		#region ContainsAtLeast Tests

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentNullException))]
		public void ContainsAtMost_Null_Collection_Throws_Exception()
		{
			List<string> list = null;
            Insist.ContainsAtMost(list, 1, "list");
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void ContainsAtMost_Number_Of_Items_Less_Than_Zero_Throws_Exception()
		{
			List<string> list = new List<string>();
            Insist.ContainsAtMost(list, -1, "list");
		}

		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void ContainsAtMost_Number_Of_Items_More_Than_Max_Throws_Exception()
		{
			List<string> list = new List<string>() { "a", "b", "c" };
            Insist.ContainsAtMost(list, 2, "list");
		}

		[Test]
		public void ContainsAtMost_Number_Of_Items_Equal_To_Max_Does_Not_Throw_Exception()
		{
			List<string> list = new List<string>() { "a", "b", "c" };
            Insist.ContainsAtMost(list, 3, "list");
		}

		[Test]
		public void ContainsAtMost_Number_Of_Items_Less_Than_Max_Does_Not_Throw_Exception()
		{
			List<string> list = new List<string>() { "a", "b", "c" };
            Insist.ContainsAtMost(list, 10, "list");
		}

		[Test]
		public void ContainsAtMost_Thrown_Exception_Has_Correct_Argument_Name()
		{
			try
			{
				IList<string> list = new List<string>() { "a", "b", "c" };
				Insist.ContainsAtMost(list, 1, ARGUMENT_NAME);
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual(ARGUMENT_NAME, ae.ParamName);
			}
		}
		[Test]
		public void ContainsAtMost_Thrown_Exception_Has_Correct_Message()
		{
			try
			{
				IList<string> list = new List<string>() { "a", "b", "c" };
				Insist.ContainsAtMost(list, 1, ARGUMENT_NAME, MESSAGE);
			}
			catch (ArgumentException ae)
			{
				Assert.IsTrue(ae.Message.Contains(MESSAGE));
			}
		}


		#endregion

		#region IsAfter (1) Tests

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		[TestCase(null), TestCase("")]
		public void IsAfter_1_Null_Or_Empty_Argument_Name_Throws_Exception(string argName) {

			Insist.IsAfter(DateTime.Now, DateTime.Now, argName);
				
		}

		[Test]
		public void IsAfter_1_Valid_Argument_Does_Not_Throw_Exception() {

			Assert.DoesNotThrow(
				() => {

					DateTime arg = DateTime.Now;
					Insist.IsAfter(arg.AddDays(1), arg, "Dummy");

				}
			);

		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IsAfter_1_Equal_Argument_Throws_Exception() {

			DateTime arg = DateTime.Now;
			Insist.IsAfter(arg, arg, "Dummy");

		}

		#endregion

		#region IsAfter (2) Tests

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		[TestCase(null), TestCase("")]
		public void IsAfter_2_Null_Or_Empty_Argument_Name_Throws_Exception(string argName) {

			Insist.IsAfter(DateTime.Now, DateTime.Now, argName, MESSAGE);

		}

		[Test]
		public void IsAfter_2_Valid_Argument_Does_Not_Throw_Exception() {

			Assert.DoesNotThrow(
				() => {

					DateTime arg = DateTime.Now;
					Insist.IsAfter(arg.AddDays(1), arg, ARGUMENT_NAME, MESSAGE);

				}
			);

		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IsAfter_2_Equal_Arguments_Throws_Exception() {

			DateTime arg = DateTime.Now;
			Insist.IsAfter(arg, arg, ARGUMENT_NAME, MESSAGE);

		}
		
		[Test]
		[TestCase(null), TestCase("")]
		public void IsAfter_2_Null_Or_Empty_User_Message_Uses_Default_Message(string userMessage) {

			string message = null;

			try {
				Insist.IsAfter(DateTime.Now, DateTime.Now.AddDays(1), ARGUMENT_NAME, userMessage);
			} catch(ArgumentException e) {
				message = e.Message;
			}

			Assert.IsNotNullOrEmpty(message);
		}

		#endregion

		#region IsTrue (1)

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		[TestCase(null), TestCase("")]
		public void IsTrue_1_Null_Or_Empty_Argument_Name_Throws_Exception(string argName) {

			Insist.IsTrue(true, argName);

		}

		[Test]
		public void IsTrue_1_True_Does_Not_Throw_Exception() {

			Assert.DoesNotThrow(() => { Insist.IsTrue(true, ARGUMENT_NAME); });

		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IsTrue_1_False_Throws_Exception() {

			Insist.IsTrue(false, ARGUMENT_NAME);

		}

		#endregion

		#region IsTrue (2)

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		[TestCase(null), TestCase("")]
		public void IsTrue_2_Null_Or_Empty_Argument_Name_Throws_Exception(string argName) {

			Insist.IsTrue(true, argName, MESSAGE);

		}

		[Test]
		public void IsTrue_2_True_Does_Not_Throw_Exception() {

			Assert.DoesNotThrow(() => { Insist.IsTrue(true, ARGUMENT_NAME, MESSAGE); });

		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IsTrue_2_False_Throws_Exception() {

			Insist.IsTrue(false, ARGUMENT_NAME, MESSAGE);

		}

		[Test]
		[TestCase(null), TestCase("")]
		public void IsTrue_2_Null_Or_Empty_User_Message_Uses_Default_Message(string userMessage) {

			string message = null;

			try {

				Insist.IsTrue(false, ARGUMENT_NAME, userMessage);

			} catch(ArgumentException e) {
				message = e.Message;
			}

			Assert.IsNotNullOrEmpty(message);
		}

		#endregion

		#region IsFalse (1)

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		[TestCase(null), TestCase("")]
		public void IsFalse_1_Null_Or_Empty_Argument_Name_Throws_Exception(string argName) {

			Insist.IsFalse(false, argName);

		}

		[Test]
		public void IsFalse_1_True_Does_Not_Throw_Exception() {

			Assert.DoesNotThrow(() => { Insist.IsFalse(false, ARGUMENT_NAME); });

		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IsFalse_1_False_Throws_Exception() {

			Insist.IsFalse(true, ARGUMENT_NAME);

		}

		#endregion

		#region IsFalse (2)

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		[TestCase(null), TestCase("")]
		public void IsFalse_2_Null_Or_Empty_Argument_Name_Throws_Exception(string argName) {

			Insist.IsFalse(false, argName, MESSAGE);

		}

		[Test]
		public void IsFalse_2_True_Does_Not_Throw_Exception() {

			Assert.DoesNotThrow(() => { Insist.IsFalse(false, ARGUMENT_NAME, MESSAGE); });

		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IsFalse_2_False_Throws_Exception() {

			Insist.IsFalse(true, ARGUMENT_NAME, MESSAGE);

		}

		[Test]
		[TestCase(null), TestCase("")]
		public void IsFalse_2_Null_Or_Empty_User_Message_Uses_Default_Message(string userMessage) {

			string message = null;

			try {

				Insist.IsFalse(true, ARGUMENT_NAME, userMessage);

			} catch(ArgumentException e) {
				message = e.Message;
			}

			Assert.IsNotNullOrEmpty(message);
		}

		#endregion

		#region IsNotEmpty (1)

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		[TestCase(null), TestCase("")]
		public void IsNotEmpty_1_Null_Or_Empty_Argument_Name_Throws_Exception(string argName) {

			Insist.IsNotEmpty(new string[] { "Hello, World" }, argName);

		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IsNotEmpty_1_Empty_Collection_Throws_Exception() {

			Insist.IsNotEmpty(new string[0], ARGUMENT_NAME);

		}

		[Test]
		public void IsNotEmpty_1_Non_Empty_Collection_Does_Not_Throw_Exception() {

			Assert.DoesNotThrow(() => { Insist.IsNotEmpty(new string[] { "Hello, World" }, ARGUMENT_NAME); });

		}

		#endregion

		#region IsNotEmpty (2)

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		[TestCase(null), TestCase("")]
		public void IsNotEmpty_2_Null_Or_Empty_Argument_Name_Throws_Exception(string argName) {

			Insist.IsNotEmpty(new string[] { "Hello, World" }, argName, MESSAGE);

		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void IsNotEmpty_2_Empty_Collection_Throws_Exception() {

			Insist.IsNotEmpty(new string[0], ARGUMENT_NAME, MESSAGE);

		}

		[Test]
		public void IsNotEmpty_2_Non_Empty_Collection_Does_Not_Throw_Exception() {

			Assert.DoesNotThrow(() => { Insist.IsNotEmpty(new string[] { "Hello, World" }, ARGUMENT_NAME, MESSAGE); });

		}

		[Test]
		[TestCase(null), TestCase("")]
		public void IsNotEmpty_2_Null_Or_Empty_User_Message_Uses_Default_Message(string userMessage) {

			string message = null;

			try {
				Insist.IsNotEmpty(new string[0], ARGUMENT_NAME, userMessage);
			} catch(ArgumentException e) {
				message = e.Message;
			}

			Assert.IsNotNullOrEmpty(message);
		}

		#endregion

		#region In (1)

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		[TestCase(null), TestCase("")]
		public void In_1_Null_Or_Empty_Argument_Name_Throws_Exception(string argName) {

			Insist.In("Hello", new string[] { "Hello" }, argName);

		}

		[Test]		
		public void In_1_Valid_Value_Does_Not_Throw_Exception() {

			Insist.In("Hello", new string[] { "Hello" }, ARGUMENT_NAME);

		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void In_1_Invalid_Value_Throws_Exception() {

			Insist.In("Hello", new string[] { "World" }, ARGUMENT_NAME);

		}

		#endregion

		#region In (2)

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		[TestCase(null), TestCase("")]
		public void In_2_Null_Or_Empty_Argument_Name_Throws_Exception(string argName) {

			Insist.In("Hello", new string[] { "Hello" }, argName, MESSAGE);

		}

		[Test]
		public void In_2_Valid_Value_Does_Not_Throw_Exception() {

			Insist.In("Hello", new string[] { "Hello" }, ARGUMENT_NAME, MESSAGE);

		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void In_2_Invalid_Value_Throws_Exception() {

			Insist.In("Hello", new string[] { "World" }, ARGUMENT_NAME, MESSAGE);

		}

		[Test]
		[TestCase(null), TestCase("")]
		public void In_2_Null_Or_Empty_User_Message_Uses_Default_Message(string userMessage) {

			string message = null;

			try {
				Insist.In("Hello", new string[] { "World" }, ARGUMENT_NAME, userMessage);
			} catch(ArgumentException e) {
				message = e.Message;
			}

			Assert.IsNotNull(message);
		}

		#endregion

        #region NotIn (1)

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [TestCase(null), TestCase("")]
        public void NotIn_1_Null_Or_Empty_Argument_Name_Throws_Exception(string argName) {

            Insist.NotIn("Hello", new string[] { "World" }, argName);

        }

        [Test]
        public void NotIn_1_Valid_Value_Does_Not_Throw_Exception() {

            Assert.DoesNotThrow(
                () => {
                    Insist.NotIn("Hello", new string[] { "World" }, ARGUMENT_NAME);
                }
            );

        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotIn_1_Invalid_Value_Throws_Exception() {

            Insist.NotIn("Hello", new string[] { "Hello" }, ARGUMENT_NAME);

        }

        #endregion

        #region NotIn (2)


        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [TestCase(null), TestCase("")]
        public void NotIn_2_Null_Or_Empty_Argument_Name_Throws_Exception(string argName) {

            Insist.NotIn("Hello", new string[] { "World" }, argName, MESSAGE);

        }

        [Test]
        public void NotIn_2_Valid_Value_Does_Not_Throw_Exception() {

            Assert.DoesNotThrow(
                () => {
                    Insist.NotIn("Hello", new string[] { "World" }, ARGUMENT_NAME, MESSAGE);
                }
            );

        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotIn_2_Invalid_Value_Throws_Exception() {

            Insist.NotIn("Hello", new string[] { "Hello" }, ARGUMENT_NAME, MESSAGE);

        }

        [Test]
        [TestCase(null), TestCase("")]
        public void NotIn_2_Null_Or_Empty_User_Message_Uses_Default_Message(string userMessage) {

            string message = null;

            try {
                Insist.NotIn("Hello", new string[] { "Hello" }, ARGUMENT_NAME, userMessage);
            } catch(ArgumentException e) {
                message = e.Message;
            }

            Assert.IsNotNullOrEmpty(message);
        }

        #endregion

        #region Conforms (1)

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [TestCase(null), TestCase("")]
        public void Conforms_1_Null_Or_Empty_Argument_Name_Throws_Exception(string argName) {

            Insist.Conforms("Hello", s => s.Equals("Hello"), argName);

        }

        [Test]
        public void Conforms_1_Valid_Value_Does_Not_Throw_Exception() {

            Assert.DoesNotThrow(() => { Insist.Conforms("Hello", s => s.Equals("Hello"), ARGUMENT_NAME); });

        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Conforms_1_Invalid_Value_Throws_Exception() {

            Insist.Conforms("Hello", s => s.Equals("World"), ARGUMENT_NAME);


        }

        #endregion

        #region Conforms (2)

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        [TestCase(null), TestCase("")]
        public void Conforms_2_Null_Or_Empty_Argument_Name_Throws_Exception(string argName) {

            Insist.Conforms("Hello", s => s.Equals("Hello"), argName, MESSAGE);

        }

        [Test]
        public void Conforms_2_Valid_Value_Does_Not_Throw_Exception() {

            Assert.DoesNotThrow(() => { Insist.Conforms("Hello", s => s.Equals("Hello"), ARGUMENT_NAME, MESSAGE); });

        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Conforms_2_Invalid_Value_Throws_Exception() {

            Insist.Conforms("Hello", s => s.Equals("World"), ARGUMENT_NAME, MESSAGE);


        }

        [Test]
        [TestCase(null), TestCase("")]
        public void Conforms_2_Null_Or_Empty_User_Message_Uses_Default_Message(string userMessage) {

            string message = null;

            try {
                Insist.Conforms("Hello", s => s.Equals("World"), ARGUMENT_NAME, userMessage);
            } catch(ArgumentException e) {
                message = e.Message;
            }

            Assert.IsNotNullOrEmpty(message);
        }

        #endregion

        #region IsDefined Tests

        [Test]
		[ExpectedException(ExpectedException=typeof(ArgumentException))]
		public void IsDefined_Value_Is_Not_defined()
		{
			TestEnum e = (TestEnum)0;
			Insist.IsDefined<TestEnum>(e, "e");
		}

		[Test]
		public void IsDefined_Value_Is_defined()
		{
			TestEnum e = (TestEnum)99;
            Insist.IsDefined<TestEnum>(e, "e");
		}

		[Test]
		public void IsDefined_Thrown_Exception_Has_Correct_Argument_Name()
		{
			try
			{
				TestEnum e = (TestEnum)0;
				Insist.IsDefined<TestEnum>(e, ARGUMENT_NAME);
			}
			catch (ArgumentException ae)
			{
				Assert.AreEqual(ARGUMENT_NAME, ae.ParamName);
			}
		}

		[Test]
		public void IsDefined_Thrown_Exception_Has_Correct_Message()
		{
			try
			{
				TestEnum e = (TestEnum)0;
				Insist.IsDefined<TestEnum>(e, ARGUMENT_NAME, MESSAGE);
			}
			catch (ArgumentException ae)
			{
				Assert.IsTrue(ae.Message.Contains(MESSAGE));
			}
		}

		#endregion

		#region Is (1)

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		[TestCase(null), TestCase("")]
		public void Is_1_Null_Or_Empty_Argument_Name_Throws_Exception(string argName) {

			Insist.Is<int>(1, 1, (a, b) => a == b, argName);

		}

		[Test]
		public void Is_1_Valid_Value_Does_Not_Throw_Exception() {

			Insist.Is<int>(1, 1, (a, b) => a == b, ARGUMENT_NAME);

		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Is_1_Invalid_Value_Throws_Exception() {

			Insist.Is<int>(1, 2, (a, b) => a == b, ARGUMENT_NAME);

		}

		#endregion

		#region Is (2)

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		[TestCase(null), TestCase("")]
		public void Is_2_Null_Or_Empty_Argument_Name_Throws_Exception(string argName) {

			Insist.Is<int>(1, 1, (a, b) => a == b, argName, MESSAGE);

		}

		[Test]
		public void Is_2_Valid_Value_Does_Not_Throw_Exception() {

			Insist.Is<int>(1, 1, (a, b) => a == b, ARGUMENT_NAME, MESSAGE);

		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void Is_2_Invalid_Value_Throws_Exception() {

			Insist.Is<int>(1, 2, (a, b) => a == b, ARGUMENT_NAME, MESSAGE);

		}

		[Test]
		[TestCase(null), TestCase("")]
		public void Is_2_Null_Or_Empty_User_Message_Uses_Default_Message(string userMessage) {

			string message = null;

			try {
				Insist.Is<int>(1, 2, (a, b) => a == b, ARGUMENT_NAME, userMessage);
			} catch(ArgumentException e) {
				message = e.Message;
			}

			Assert.IsNotNull(message);
		}

		#endregion		
	}
}
