using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using log4net;
using Shared.Utilities.ExtensionMethods.Logging;

namespace Shared.Utilities.ExtensionMethods.Data
{
	public static class DataSetExtensions
	{
		#region Logging

		/// <summary>
		/// Used to log information about the class
		/// </summary>
		private static ILog _logger = LogManager.GetLogger(typeof(DataSetExtensions));

		#endregion

		/// <summary>
		/// Checks to ensure that the dataset is not null, has at least 1 datatable and
		/// that the first datatable has at least 1 row.
		/// </summary>
		/// <returns>
		/// True if the above conditions are met, otherwise false.
		/// </returns>
		public static bool IsPopulated(this DataSet ds)
		{
			_logger.DebugMethodCalled(ds);

			return IsPopulated(ds, 1);
		}

		/// <summary>
		/// Checks to ensure that the dataset is not null, has the specified number
		/// of datatables in it, and that each datatable has at least 1 row.
		/// </summary>
		/// <param name="requiredNumberOfTables">
		/// The number of tables that the dataset must contain
		/// </param>
		/// <returns>
		/// True if the above conditions are true, otherwise false.
		/// </returns>
		public static bool IsPopulated(this DataSet ds, int requiredNumberOfTables)
		{
			_logger.DebugMethodCalled(ds, requiredNumberOfTables);

			List<bool> flags = new List<bool>();
			
			for (int i = 0; i < requiredNumberOfTables; i++)
			{
				flags.Add(true);
			}

			return IsPopulated(ds, requiredNumberOfTables, flags.ToArray());
		}

		/// <summary>
		/// Checks to ensure that the dataset is not null, has the specified number of tables
		/// and whether or not each table MUST have rows in it.
		/// </summary>
		/// <param name="requiredNumberOfTables">
		/// The number of tables that dataset must contain
		/// </param>
		/// <param name="tableMustHaveRowsFlags">
		/// A list of boolean flags denoting whether each table in the set MUST have some rows in it.
		/// e.g.
		/// 
		/// DataSet ds = GetSomeDataSet();
		/// ds.IsPopulated(3, true, false, true);
		/// 
		/// means that the dataset MUST have 3 tables. The first and third tables MUST have rows in them,
		/// the second table may or may not have rows in it.
		/// </param>
		/// <returns>
		/// True if the the tables required to be populated actually are, otherwise false.
		/// </returns>
		public static bool IsPopulated(this DataSet ds, int requiredNumberOfTables, params bool[] tableMustHaveRowsFlags)
		{
			_logger.DebugMethodCalled(ds, requiredNumberOfTables, tableMustHaveRowsFlags);

			#region Input validation

			Insist.IsAtLeast(requiredNumberOfTables, 1, "requiredNumberOfTables");
			Insist.IsNotNull(tableMustHaveRowsFlags, "tableMustHaveRowsFlags");
			Insist.Equality(tableMustHaveRowsFlags.Length, requiredNumberOfTables, "tableMustHaveRowsFlags", "The number of tableMustHaveRowsFlags must match the number of tables");

			#endregion

			if (ds == null ||
				ds.Tables == null ||
				ds.Tables.Count != requiredNumberOfTables)
			{
				return false;
			}
			else
			{
				for (int i = 0; i < requiredNumberOfTables; i++)
				{
					if (tableMustHaveRowsFlags[i] == true && 
						(
							ds.Tables[i].Rows == null ||
							ds.Tables[i].Rows.Count == 0
						))
					{
						return false;
					}
				}

				return true;
			}
		}

	}
}
