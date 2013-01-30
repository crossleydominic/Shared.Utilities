using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data;
using Shared.Utilities.ExtensionMethods.Data;

namespace Shared.Utilities.Tests.ExtensionMethods.Data
{
	[TestFixture]
	public class DataSetExtensionsTests
	{
		#region Global test data objects

		/// <summary>
		/// A dataset that contains no tables
		/// </summary>
		private static DataSet _dataSetNoTables;
		
		/// <summary>
		/// A dataset with one table that has at least 1 row
		/// </summary>
		private static DataSet _dataSetOneTableWithRows;

		/// <summary>
		/// A dataset with 1 table but no rows
		/// </summary>
		private static DataSet _dataSetOneTableWithoutRows;

		/// <summary>
		/// A dataset with 3 tables, all tables have at least 1 row
		/// </summary>
		private static DataSet _dataSetThreeTablesAllWithRows;
		
		/// <summary>
		/// A dataset with 3 tables, some of the tables have rows, others don't
		/// </summary>
		private static DataSet _dataSetThreeTablesSomeWithRows;
		
		/// <summary>
		/// A dataset with 3 tables, all tables do not have rows.
		/// </summary>
		private static DataSet _dataSetThreeTablesAllWithoutRows;

		#endregion

		#region Test setup

		[TestFixtureSetUp]
		public static void InitialiseData()
		{
			//Setup dataset without tables.
			_dataSetNoTables = new DataSet();

			//Setup dataset with 1 table with no rows
			_dataSetOneTableWithoutRows = new DataSet();
			_dataSetOneTableWithoutRows.Tables.Add(new DataTable());
			_dataSetOneTableWithoutRows.Tables[0].Columns.Add(new DataColumn("col1", typeof(string)));

			//Setup dataset with 1 table with some rows.
			_dataSetOneTableWithRows = new DataSet();
			_dataSetOneTableWithRows.Tables.Add(new DataTable());
			_dataSetOneTableWithRows.Tables[0].Columns.Add(new DataColumn("Col1", typeof(string)));
			_dataSetOneTableWithRows.Tables[0].Columns.Add(new DataColumn("Col2", typeof(string)));
			_dataSetOneTableWithRows.Tables[0].Rows.Add("val1", "val2");
			_dataSetOneTableWithRows.Tables[0].Rows.Add("val3", "val4");

			//Setup dataset with 3 tables, all tables have rows.
			_dataSetThreeTablesAllWithRows = new DataSet();
			_dataSetThreeTablesAllWithRows.Tables.Add(new DataTable());
			_dataSetThreeTablesAllWithRows.Tables[0].Columns.Add(new DataColumn("Col1", typeof(string)));
			_dataSetThreeTablesAllWithRows.Tables[0].Columns.Add(new DataColumn("Col2", typeof(string)));
			_dataSetThreeTablesAllWithRows.Tables[0].Rows.Add("val1", "val2");
			_dataSetThreeTablesAllWithRows.Tables[0].Rows.Add("val3", "val4");
			_dataSetThreeTablesAllWithRows.Tables.Add(new DataTable());
			_dataSetThreeTablesAllWithRows.Tables[1].Columns.Add(new DataColumn("Col1", typeof(string)));
			_dataSetThreeTablesAllWithRows.Tables[1].Columns.Add(new DataColumn("Col2", typeof(string)));
			_dataSetThreeTablesAllWithRows.Tables[1].Rows.Add("val1", "val2");
			_dataSetThreeTablesAllWithRows.Tables[1].Rows.Add("val3", "val4");
			_dataSetThreeTablesAllWithRows.Tables.Add(new DataTable());
			_dataSetThreeTablesAllWithRows.Tables[2].Columns.Add(new DataColumn("Col1", typeof(string)));
			_dataSetThreeTablesAllWithRows.Tables[2].Columns.Add(new DataColumn("Col2", typeof(string)));
			_dataSetThreeTablesAllWithRows.Tables[2].Rows.Add("val1", "val2");
			_dataSetThreeTablesAllWithRows.Tables[2].Rows.Add("val3", "val4");

			//Setup dataset with 3 tables, all without rows.
			_dataSetThreeTablesAllWithoutRows = new DataSet();
			_dataSetThreeTablesAllWithoutRows.Tables.Add(new DataTable());
			_dataSetThreeTablesAllWithoutRows.Tables.Add(new DataTable());
			_dataSetThreeTablesAllWithoutRows.Tables.Add(new DataTable());

			//Setup dataset with 3 tables, tables 1 and 3 have rows.
			_dataSetThreeTablesSomeWithRows = new DataSet();
			_dataSetThreeTablesSomeWithRows.Tables.Add(new DataTable());
			_dataSetThreeTablesSomeWithRows.Tables[0].Columns.Add(new DataColumn("Col1", typeof(string)));
			_dataSetThreeTablesSomeWithRows.Tables[0].Columns.Add(new DataColumn("Col2", typeof(string)));
			_dataSetThreeTablesSomeWithRows.Tables[0].Rows.Add("val1", "val2");
			_dataSetThreeTablesSomeWithRows.Tables[0].Rows.Add("val3", "val4");
			_dataSetThreeTablesSomeWithRows.Tables.Add(new DataTable());
			_dataSetThreeTablesSomeWithRows.Tables.Add(new DataTable());
			_dataSetThreeTablesSomeWithRows.Tables[2].Columns.Add(new DataColumn("Col1", typeof(string)));
			_dataSetThreeTablesSomeWithRows.Tables[2].Columns.Add(new DataColumn("Col2", typeof(string)));
			_dataSetThreeTablesSomeWithRows.Tables[2].Rows.Add("val1", "val2");
			_dataSetThreeTablesSomeWithRows.Tables[2].Rows.Add("val3", "val4");
		}

		#endregion

		#region IsPopulated tests

		/// <summary>
		/// Tests when the dataset is null
		/// </summary>
		[Test]
		public void IsPopulated_DataSetNull()
		{
			DataSet ds = null;
			bool result = ds.IsPopulated();

			Assert.IsFalse(result);
		}

		/// <summary>
		/// Tests when the expected number of tables and the number of flags supplied
		/// do not match
		/// </summary>
		[Test]
		[ExpectedException(ExpectedException = typeof(ArgumentException))]
		public void IsPopulated_NumberOfFlagsMismatch()
		{
			_dataSetThreeTablesSomeWithRows.IsPopulated(3, true);
		}

		/// <summary>
		/// Tests when the expected number of tables is 0
		/// </summary>
		[Test]
        [ExpectedException(ExpectedException = typeof(ArgumentOutOfRangeException))]
		public void IsPopulated_NumberOfRequiredTablesIsZero()
		{
			_dataSetOneTableWithRows.IsPopulated(0);
		}

		/// <summary>
		/// Tests when the number of required tables is less than 0
		/// </summary>
		[Test]
        [ExpectedException(ExpectedException = typeof(ArgumentOutOfRangeException))]
		public void IsPopulated_NumberOfRequiredTablesIsLessThanZero()
		{
			_dataSetOneTableWithRows.IsPopulated(-1);
		}

		/// <summary>
		/// Tests to see if a dataset is populated using datasets that have rows
		/// </summary>
		[Test]
		public void IsPopulated_DataSetWithRows()
		{
			bool result = _dataSetOneTableWithRows.IsPopulated();
			Assert.IsTrue(result);

			result = _dataSetThreeTablesAllWithRows.IsPopulated(3);
			Assert.IsTrue(result);
		}

		/// <summary>
		/// tests to see if a dataset is populated using datasets that have no rows.
		/// </summary>
		[Test]
		public void IsPopulated_DataSetWithoutRows()
		{
			bool result = _dataSetOneTableWithoutRows.IsPopulated();
			Assert.IsFalse(result);

			result = _dataSetThreeTablesAllWithoutRows.IsPopulated(3);
			Assert.IsFalse(result);
		}

		/// <summary>
		/// Tests to see if a dataset is populated using datasets that have a mixture
		/// of tables, some with rows, some without.
		/// </summary>
		public void IsPopulated_DataSetWithSomeRows()
		{
			//Expecting dataset to have 3 tables, first and third must be populated
			bool result = _dataSetThreeTablesSomeWithRows.IsPopulated(3, true, false, true);
			Assert.IsTrue(result);

			//Expecting dataset to have 3 tables, second and third populated
			result = _dataSetThreeTablesSomeWithRows.IsPopulated(3, false, true, true);
			Assert.IsFalse(result);

			//Expecting dataset to have 4 tables, first, third and fourth populated
			result = _dataSetThreeTablesSomeWithRows.IsPopulated(4, true, false, true, true);
			Assert.IsFalse(result);
		}

		#endregion

	}
}
