using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using log4net.ObjectRenderer;
using System.IO;

namespace Shared.Utilities.ExtensionMethods.Logging
{
	/// <summary>
	/// A class that provides some extension methods to make loggin easier.
	/// </summary>
	public static class ILogExtensions
	{
		#region Private constants

		/// <summary>
		/// The format pattern for outputing a "method called" message
		/// </summary>
		private const string METHOD_CALLED_PATTERN = "Method '{0}' called with arguments: ";

		/// <summary>
		/// The format pattern for outputting a "method returning" message
		/// </summary>
		private const string METHOD_RETURNING_PATTERN = "Method '{0}' returning with value: ";

		/// <summary>
		/// The format pattern for outputting individual argument values
		/// </summary>
		private const string ARGUMENT_PATTERN = "'{0}'";

		/// <summary>
		/// The format pattern for outputting enumerables
		/// </summary>
		private const string ENUMERABLE_ARGUMENT_PATTERN = "'{{{0}}}'";

		/// <summary>
		/// A character to use to seperate arguments in the output message
		/// </summary>
		private const string ARGUMENT_SEPERATOR = ", ";

		/// <summary>
		/// The string that will be used for arguments that are null
		/// </summary>
		private const string NULL_ARGUMENT = "null";

		/// <summary>
		/// The string that will be used if no arguments where supplied to log
		/// </summary>
		private const string NO_ARGUMENTS = "No arguments supplied";

		/// <summary>
		/// The format pattern for constructing the full name of the method called.
		/// </summary>
		private const string TYPE_AND_METHOD_FORMAT = "{0}.{1}";

		#endregion

		#region Private static members

		/// <summary>
		/// The type of this class. Cache it here so that it doesn't have to
		/// be evaluated constantly.
		/// </summary>
		private static readonly Type ilogExtensionsType = typeof(ILogExtensions);

		#endregion

		#region Public methods

		/// <summary>
		/// Output a "method called" debug message and also dumps the .ToString() representation
		/// of all objects supplied in the args array.
		/// </summary>
		/// <param name="logger">
		/// The logger to log to
		/// </param>
		/// <param name="args">
		/// The list of arguments that need to be logged
		/// </param>
		public static void DebugMethodCalled(this ILog logger, params object[] args)
		{
			DebugMethodCalled(logger, EnumerableExpansion.DoNotExpand, args);
		}

		/// <summary>
		/// Output a "method called" debug message and also dumps the .ToString() representation
		/// of all objects supplied in the args array.
		/// </summary>
		/// <param name="logger">
		/// The logger to log to
		/// </param>
		/// <param name="expandEnumerables">
		/// If EnumerableExpansion.Expand and the args collection contains any IEnumerables then we'll print .ToString for every
		/// item in the collection and not just the collection itself.
		/// </param>
		/// <param name="args">
		/// The list of arguments that need to be logged
		/// </param>
		public static void DebugMethodCalled(this ILog logger, EnumerableExpansion expandEnumerables, params object[] args)
		{
			#region Input validation

			Insist.IsNotNull(logger, "logger");
			Insist.IsDefined<EnumerableExpansion>(expandEnumerables, "expandEnumerables");

			#endregion

			if (logger.IsDebugEnabled)
			{
				logger.Debug(BuildMessage(logger, string.Format(METHOD_CALLED_PATTERN, GetCallingMethod()), expandEnumerables, args));
			}
		}

		/// <summary>
		/// Output a "method returning" debug message and also dumps the .ToString() representation
		/// of the val object.
		/// </summary>
		/// <param name="logger">
		/// The logger to log to
		/// </param>
		/// <param name="val">
		/// The value that is being returned that should be logged.
		/// </param>
		public static void DebugMethodReturning(this ILog logger, object val)
		{
			DebugMethodReturning(logger, EnumerableExpansion.DoNotExpand, val);
		}

		/// <summary>
		/// Output a "method returning" debug message and also dumps the .ToString() representation
		/// of the val object.
		/// </summary>
		/// <param name="logger">
		/// The logger to log to
		/// </param>
		/// <param name="expandEnumerables">
		/// If EnumerableExpansion.Expand and the val parameter is an IEnumerable then we'll print .ToString for every
		/// item in the collection and not just the collection itself.
		/// </param>
		/// <param name="val">
		/// The value that is being returned that should be logged.
		/// </param>
		public static void DebugMethodReturning(this ILog logger, EnumerableExpansion expandEnumerables, object val)
		{
			#region Input validation

			Insist.IsNotNull(logger, "logger");
			Insist.IsDefined<EnumerableExpansion>(expandEnumerables, "expandEnumerables");

			#endregion

			if (logger.IsDebugEnabled)
			{
				logger.Debug(BuildMessage(logger, string.Format(METHOD_RETURNING_PATTERN, GetCallingMethod()), expandEnumerables, new object[] { val }));
			}
		}

		/// <summary>
		/// Dumps the .ToString() representation of each of the supplied arguments
		/// </summary>
		/// <param name="logger">
		/// The logger to write to
		/// </param>
		/// <param name="args">
		/// The list of arguments that need to be logged.
		/// </param>
		public static void DebugDumpArguments(this ILog logger, params object[] args)
		{
			DebugDumpArguments(logger, EnumerableExpansion.DoNotExpand, args);
		}

		/// <summary>
		/// Dumps the .ToString() representation of each of the supplied arguments
		/// </summary>
		/// <param name="logger">
		/// The logger to write to
		/// </param>
		/// <param name="expandEnumerables">
		/// If EnumerableExpansion.Expand and the args collection contains any IEnumerables then we'll print .ToString for every
		/// item in the collection and not just the collection itself.
		/// </param>
		/// <param name="args">
		/// The list of arguments that need to be logged.
		/// </param>
		public static void DebugDumpArguments(this ILog logger, EnumerableExpansion expandEnumerables, params object[] args)
		{
			#region Input validation

			Insist.IsNotNull(logger, "logger");
			Insist.IsDefined<EnumerableExpansion>(expandEnumerables, "expandEnumerables");
		
			#endregion

			if (logger.IsDebugEnabled)
			{
				logger.Debug(BuildMessage(logger, string.Empty, expandEnumerables, args));
			}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Constructs a call stack and goes up the stack to find the method that
		/// called into these extension methods.
		/// </summary>
		private static string GetCallingMethod()
		{
			StackFrame[] frames = (new StackTrace()).GetFrames();

			MethodBase callingMethod = null;

			for (int i = 0; i < frames.Length; i++)
			{
				callingMethod = frames[i].GetMethod();
				if (callingMethod.DeclaringType != ilogExtensionsType)
				{
					break;
				}
			}

			return (string.Format(TYPE_AND_METHOD_FORMAT, callingMethod.DeclaringType.FullName, callingMethod.Name));
		}

		/// <summary>
		/// Builds up the message that will get logged
		/// </summary>
		private static string BuildMessage(ILog logger, string prefix, EnumerableExpansion expandEnumerables, object[] args)
		{
			#region Input validation

			prefix = (prefix == null ? string.Empty : prefix);

			if (args == null || args.Length == 0)
			{
				return prefix + NO_ARGUMENTS;
			}

			#endregion

			StringBuilder builder = new StringBuilder();
			builder.Append(prefix);

			object currentObject = null;

			for (int i = 0; i < args.Length; i++)
			{
				currentObject = args[i];

				if (i > 0) { builder.Append(ARGUMENT_SEPERATOR); }

				if (currentObject == null)
				{
					builder.Append(NULL_ARGUMENT);
				}
				else
				{
					//See if the current object has a renderer associated with it.
					IObjectRenderer renderer = logger.Logger.Repository.RendererMap.Get(currentObject.GetType());

					bool useRenderer = (renderer != null);

					if (expandEnumerables == EnumerableExpansion.DoNotExpand &&
						renderer is DefaultRenderer &&
						currentObject is IEnumerable)
						//The default renderer will expand enumerbales for us, if this 
						//method was called explicitly stating not to expand the enumerables
						//then we'll not use the default renderer and just dump out the ToString()
					{
						useRenderer = false;
					}

                    if (currentObject is string)
                    //Strings are also enumerables but we don't want them
                    //to get expanded.
                    {
                        useRenderer = false;
                    }

					if (useRenderer)
						//We've got a renderer, this is the preferred way of dumping
						//out an object
					{
						using (TextWriter writer = new StringWriter())
						{
							renderer.RenderObject(logger.Logger.Repository.RendererMap, currentObject, writer);
							builder.Append(writer.ToString());
						}
					}
					else
						//No renderer available, just dump out the ToString of the object.
					{
						builder.Append(string.Format(ARGUMENT_PATTERN, currentObject.ToString()));
					}
				}
			}


			return builder.ToString();
		}

		#endregion
	}
}
