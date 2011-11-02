using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using log4net;
using System.Security.AccessControl;

namespace Shared.Utilities
{
	/// <summary>
	/// A few helper methods that makes dealing with reading/writing files 
	/// a little less painful
	/// 
	/// EXPERIMENTAL - DO NOT USE
	/// </summary>
	internal static class FileEx
	{
		private static ILog _log = LogManager.GetLogger(typeof(FileEx));

		#region TryDelete

		public static OperationResult TryDelete(string path)
		{
			try
			{
				File.Delete(path);
				return OperationResult.Succeeded;
			}
			catch (Exception e) { return new OperationResult(e.Message); }
		}

		#endregion

		#region TryCopy

		public static OperationResult TryCopy(string sourceFileName, string destinationFileName)
		{
			try
			{
				File.Copy(sourceFileName, destinationFileName);
				return OperationResult.Succeeded;
			}
			catch (Exception e) { return new OperationResult(e.Message); }
		}

		public static OperationResult TryCopy(string sourceFileName, string destinationFileName, bool overwrite)
		{
			try
			{
				File.Copy(sourceFileName, destinationFileName, overwrite);
				return OperationResult.Succeeded;
			}
			catch (Exception e) { return new OperationResult(e.Message); }
		}

		#endregion

		#region TrySetAttributes

		public static OperationResult TrySetAttributes(string path, FileAttributes attributes)
		{
			try
			{
				File.SetAttributes(path, attributes);
				return OperationResult.Succeeded;
			}
			catch (Exception e) { return new OperationResult(e.Message); }
		}

		#endregion

		#region TryOpen

		public static OperationResult TryOpen(string path, FileMode mode, out FileStream stream)
		{
			stream = null;
			try
			{
				stream = File.Open(path, mode);
				return OperationResult.Succeeded;
			}
			catch (Exception e) { return new OperationResult(e.Message); }
		}

		public static OperationResult TryOpen(string path, FileMode mode, FileAccess access, out FileStream stream)
		{
			stream = null;
			try
			{
				stream = File.Open(path, mode, access);
				return OperationResult.Succeeded;
			}
			catch (Exception e) { return new OperationResult(e.Message); }
		}

		public static OperationResult TryOpen(string path, FileMode mode, FileAccess access, FileShare share, out FileStream stream)
		{
			stream = null;
			try
			{
				stream = File.Open(path, mode, access, share);
				return OperationResult.Succeeded;
			}
			catch (Exception e) { return new OperationResult(e.Message); }
		}

		#endregion

		#region TryCreate

		public static OperationResult TryCreate(string path, out FileStream stream)
		{
			stream = null;
			try
			{
				stream = File.Create(path);
				return OperationResult.Succeeded;
			}
			catch (Exception e) { return new OperationResult(e.Message); }
		}

		public static OperationResult TryCreate(string path, int bufferSize, out FileStream stream)
		{
			stream = null;
			try
			{
				stream = File.Create(path, bufferSize);
				return OperationResult.Succeeded;
			}
			catch (Exception e) { return new OperationResult(e.Message); }
		}

		public static OperationResult TryCreate(string path, int bufferSize, FileOptions options, out FileStream stream)
		{
			stream = null;
			try
			{
				stream = File.Create(path, bufferSize, options);
				return OperationResult.Succeeded;
			}
			catch (Exception e) { return new OperationResult(e.Message); }
		}

		public static OperationResult TryCreate(string path, int bufferSize, FileOptions options, FileSecurity security, out FileStream stream)
		{
			stream = null;
			try
			{
				stream = File.Create(path, bufferSize, options, security);
				return OperationResult.Succeeded;
			}
			catch (Exception e) { return new OperationResult(e.Message); }
		}

		#endregion

		#region TryOpenRead

		public static OperationResult TryOpenRead(string path, out FileStream stream)
		{
			stream = null;
			try
			{
				stream = File.OpenRead(path);
				return OperationResult.Succeeded;
			}
			catch (Exception e) { return new OperationResult(e.Message); }
		}

		#endregion

		#region TryOpenWrite

		public static OperationResult TryOpenWrite(string path, out FileStream stream)
		{
			stream = null;
			try
			{
				stream = File.OpenWrite(path);
				return OperationResult.Succeeded;
			}
			catch (Exception e) { return new OperationResult(e.Message); }
		}

		#endregion

		#region TryMove

		public static OperationResult TryMove(string sourceFileName, string destinationFileName)
		{
			try
			{
				File.Move(sourceFileName, destinationFileName);
				return OperationResult.Succeeded;
			}
			catch (Exception e) { return new OperationResult(e.Message); }
		}

		#endregion
	}
}
