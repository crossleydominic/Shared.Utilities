using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if !DOTNET35
namespace Shared.Utilities.ExtensionMethods.Delegates
{
	/// <summary>
	/// A set of extension methods that allows a delegate
	/// to be attempted multiple times. The number of attempts
	/// and interval between attempts is configurable.
	/// </summary>
	public static class FuncExtensions
	{
		#region Func<TResult> Extensions

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// </summary>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<TResult>(this Func<TResult> functionToAttempt, out TResult output)
		{
			return Attempt<TResult>(functionToAttempt, null, out output);
		}

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// A second delegate must also be supplied that will be used to determine
		/// whether or not the Func delegate succeeded.
		/// </summary>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="successCondition">
		/// A delegate that will be invoked after running the Func delegate.
		/// This delegate will be passed the return value from the Func delegate
		/// and will be used to determine whether or not the invokation of the
		/// Func delegate succeeded or not.
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<TResult>(this Func<TResult> functionToAttempt, Func<TResult, bool> successCondition, out TResult output)
		{
            return Attempt<TResult>(functionToAttempt, successCondition, Retry.DEFAULT_ATTEMPTS, Retry.DEFAULT_INTERVAL, RetryExceptionBehaviour.HandleAndCollate, out output);
		}

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// A second delegate must also be supplied that will be used to determine
		/// whether or not the Func delegate succeeded.
		/// A configurable number of attempts and interval beteween attempts can also
		/// be specified.
		/// Optionally opt-out of automatic exception handling.
		/// </summary>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="successCondition">
		/// A delegate that will be invoked after running the Func delegate.
		/// This delegate will be passed the return value from the Func delegate
		/// and will be used to determine whether or not the invokation of the
		/// Func delegate succeeded or not.
		/// </param>
		/// <param name="numberOfAttempts">
		/// The number of times the Func delegate should be run before giving up.
		/// </param>
		/// <param name="interval">
		/// The amount of time to wait inbetween each attempt
		/// </param>
		/// <param name="handleExceptions">
		/// Whether or not we want to automatically handle any exceptions that get 
		/// thrown by the Fund delegate. If this is True then calling code must
		/// be prepared to handle any exceptions that can be thrown by the 
		/// Func delegate
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<TResult>(this Func<TResult> functionToAttempt, Func<TResult, bool> successCondition, int numberOfAttempts, TimeSpan interval, RetryExceptionBehaviour exceptionBehaviour, out TResult output)
		{
            return DoAttempt<object, object, object, object, object, object, object, TResult>(0, functionToAttempt, null, successCondition, numberOfAttempts, interval, exceptionBehaviour, out output);
		}

		#endregion

		#region Func<T1, TResult> Extensions

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<T1, TResult>(this Func<T1, TResult> functionToAttempt, T1 arg1, out TResult output)
		{
			return Attempt<T1, TResult>(functionToAttempt, arg1, null, out output);
		}

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// A second delegate must also be supplied that will be used to determine
		/// whether or not the Func delegate succeeded.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="successCondition">
		/// A delegate that will be invoked after running the Func delegate.
		/// This delegate will be passed the return value from the Func delegate
		/// and will be used to determine whether or not the invokation of the
		/// Func delegate succeeded or not.
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<T1, TResult>(this Func<T1, TResult> functionToAttempt, T1 arg1, Func<TResult, bool> successCondition, out TResult output)
		{
            return Attempt<T1, TResult>(functionToAttempt, arg1, successCondition, Retry.DEFAULT_ATTEMPTS, Retry.DEFAULT_INTERVAL, RetryExceptionBehaviour.HandleAndCollate, out output);
		}

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// A second delegate must also be supplied that will be used to determine
		/// whether or not the Func delegate succeeded.
		/// A configurable number of attempts and interval beteween attempts can also
		/// be specified.
		/// Optionally opt-out of automatic exception handling.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="successCondition">
		/// A delegate that will be invoked after running the Func delegate.
		/// This delegate will be passed the return value from the Func delegate
		/// and will be used to determine whether or not the invokation of the
		/// Func delegate succeeded or not.
		/// </param>
		/// <param name="numberOfAttempts">
		/// The number of times the Func delegate should be run before giving up.
		/// </param>
		/// <param name="interval">
		/// The amount of time to wait inbetween each attempt
		/// </param>
		/// <param name="handleExceptions">
		/// Whether or not we want to automatically handle any exceptions that get 
		/// thrown by the Fund delegate. If this is True then calling code must
		/// be prepared to handle any exceptions that can be thrown by the 
		/// Func delegate
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
        public static OperationResult Attempt<T1, TResult>(this Func<T1, TResult> functionToAttempt, T1 arg1, Func<TResult, bool> successCondition, int numberOfAttempts, TimeSpan interval, RetryExceptionBehaviour exceptionBehaviour, out TResult output)
		{
			Tuple<T1, object, object, object, object, object, object> args = new Tuple<T1, object, object, object, object, object, object>(arg1, null, null, null, null, null, null);
			return DoAttempt<T1, object, object, object, object, object, object, TResult>(1, functionToAttempt, args, successCondition, numberOfAttempts, interval, exceptionBehaviour, out output);
		}

		#endregion

		#region Func<T1, T2, TResult> Extensions

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="arg2">
		/// The second argument to the Func delegate
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<T1, T2, TResult>(this Func<T1, T2, TResult> functionToAttempt, T1 arg1, T2 arg2, out TResult output)
		{
			return Attempt<T1, T2, TResult>(functionToAttempt, arg1, arg2, null, out output);
		}

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// A second delegate must also be supplied that will be used to determine
		/// whether or not the Func delegate succeeded.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="arg2">
		/// The second argument to the Func delegate
		/// </param>
		/// <param name="successCondition">
		/// A delegate that will be invoked after running the Func delegate.
		/// This delegate will be passed the return value from the Func delegate
		/// and will be used to determine whether or not the invokation of the
		/// Func delegate succeeded or not.
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<T1, T2, TResult>(this Func<T1, T2, TResult> functionToAttempt, T1 arg1, T2 arg2, Func<TResult, bool> successCondition, out TResult output)
		{
            return Attempt<T1, T2, TResult>(functionToAttempt, arg1, arg2, successCondition, Retry.DEFAULT_ATTEMPTS, Retry.DEFAULT_INTERVAL, RetryExceptionBehaviour.HandleAndCollate, out output);
		}

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// A second delegate must also be supplied that will be used to determine
		/// whether or not the Func delegate succeeded.
		/// A configurable number of attempts and interval beteween attempts can also
		/// be specified.
		/// Optionally opt-out of automatic exception handling.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="arg2">
		/// The second argument to the Func delegate
		/// </param>
		/// <param name="successCondition">
		/// A delegate that will be invoked after running the Func delegate.
		/// This delegate will be passed the return value from the Func delegate
		/// and will be used to determine whether or not the invokation of the
		/// Func delegate succeeded or not.
		/// </param>
		/// <param name="numberOfAttempts">
		/// The number of times the Func delegate should be run before giving up.
		/// </param>
		/// <param name="interval">
		/// The amount of time to wait inbetween each attempt
		/// </param>
		/// <param name="handleExceptions">
		/// Whether or not we want to automatically handle any exceptions that get 
		/// thrown by the Fund delegate. If this is True then calling code must
		/// be prepared to handle any exceptions that can be thrown by the 
		/// Func delegate
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
        public static OperationResult Attempt<T1, T2, TResult>(this Func<T1, T2, TResult> functionToAttempt, T1 arg1, T2 arg2, Func<TResult, bool> successCondition, int numberOfAttempts, TimeSpan interval, RetryExceptionBehaviour exceptionBehaviour, out TResult output)
		{
			Tuple<T1, T2, object, object, object, object, object> args = new Tuple<T1, T2, object, object, object, object, object>(arg1, arg2, null, null, null, null, null);
            return DoAttempt<T1, T2, object, object, object, object, object, TResult>(2, functionToAttempt, args, successCondition, numberOfAttempts, interval, exceptionBehaviour, out output);
		}

		#endregion

		#region Func<T1, T2, T3, TResult> Extensions

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="T3">
		/// The type of the third argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="arg2">
		/// The second argument to the Func delegate
		/// </param>
		/// <param name="arg3">
		/// The third argument to the Func delegate
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> functionToAttempt, T1 arg1, T2 arg2, T3 arg3, out TResult output)
		{
			return Attempt<T1, T2, T3, TResult>(functionToAttempt, arg1, arg2, arg3, null, out output);
		}

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// A second delegate must also be supplied that will be used to determine
		/// whether or not the Func delegate succeeded.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="T3">
		/// The type of the third argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="arg2">
		/// The second argument to the Func delegate
		/// </param>
		/// <param name="arg3">
		/// The third argument to the Func delegate
		/// </param>
		/// <param name="successCondition">
		/// A delegate that will be invoked after running the Func delegate.
		/// This delegate will be passed the return value from the Func delegate
		/// and will be used to determine whether or not the invokation of the
		/// Func delegate succeeded or not.
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> functionToAttempt, T1 arg1, T2 arg2, T3 arg3, Func<TResult, bool> successCondition, out TResult output)
		{
            return Attempt<T1, T2, T3, TResult>(functionToAttempt, arg1, arg2, arg3, successCondition, Retry.DEFAULT_ATTEMPTS, Retry.DEFAULT_INTERVAL, RetryExceptionBehaviour.HandleAndCollate, out output);
		}

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// A second delegate must also be supplied that will be used to determine
		/// whether or not the Func delegate succeeded.
		/// A configurable number of attempts and interval beteween attempts can also
		/// be specified.
		/// Optionally opt-out of automatic exception handling.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="T3">
		/// The type of the third argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="arg2">
		/// The second argument to the Func delegate
		/// </param>
		/// <param name="arg3">
		/// The third argument to the Func delegate
		/// </param>
		/// <param name="successCondition">
		/// A delegate that will be invoked after running the Func delegate.
		/// This delegate will be passed the return value from the Func delegate
		/// and will be used to determine whether or not the invokation of the
		/// Func delegate succeeded or not.
		/// </param>
		/// <param name="numberOfAttempts">
		/// The number of times the Func delegate should be run before giving up.
		/// </param>
		/// <param name="interval">
		/// The amount of time to wait inbetween each attempt
		/// </param>
		/// <param name="handleExceptions">
		/// Whether or not we want to automatically handle any exceptions that get 
		/// thrown by the Fund delegate. If this is True then calling code must
		/// be prepared to handle any exceptions that can be thrown by the 
		/// Func delegate
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
        public static OperationResult Attempt<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> functionToAttempt, T1 arg1, T2 arg2, T3 arg3, Func<TResult, bool> successCondition, int numberOfAttempts, TimeSpan interval, RetryExceptionBehaviour exceptionBehaviour, out TResult output)
		{
			Tuple<T1, T2, T3, object, object, object, object> args = new Tuple<T1, T2, T3, object, object, object, object>(arg1, arg2, arg3, null, null, null, null);
            return DoAttempt<T1, T2, T3, object, object, object, object, TResult>(3, functionToAttempt, args, successCondition, numberOfAttempts, interval, exceptionBehaviour, out output);
		}

		#endregion

		#region Func<T1, T2, T3, T4, TResult> Extensions

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="T3">
		/// The type of the third argument to the delegate
		/// </typeparam>
		/// <typeparam name="T4">
		/// The type of the fourth argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="arg2">
		/// The second argument to the Func delegate
		/// </param>
		/// <param name="arg3">
		/// The third argument to the Func delegate
		/// </param>
		/// <param name="arg4">
		/// The fourth argument to the Func delegate
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> functionToAttempt, T1 arg1, T2 arg2, T3 arg3, T4 arg4, out TResult output)
		{
			return Attempt<T1, T2, T3, T4, TResult>(functionToAttempt, arg1, arg2, arg3, arg4, null, out output);
		}

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// A second delegate must also be supplied that will be used to determine
		/// whether or not the Func delegate succeeded.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="T3">
		/// The type of the third argument to the delegate
		/// </typeparam>
		/// <typeparam name="T4">
		/// The type of the fourth argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="arg2">
		/// The second argument to the Func delegate
		/// </param>
		/// <param name="arg3">
		/// The third argument to the Func delegate
		/// </param>
		/// <param name="arg4">
		/// The fourth argument to the Func delegate
		/// </param>
		/// <param name="successCondition">
		/// A delegate that will be invoked after running the Func delegate.
		/// This delegate will be passed the return value from the Func delegate
		/// and will be used to determine whether or not the invokation of the
		/// Func delegate succeeded or not.
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> functionToAttempt, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<TResult, bool> successCondition, out TResult output)
		{
            return Attempt<T1, T2, T3, T4, TResult>(functionToAttempt, arg1, arg2, arg3, arg4, successCondition, Retry.DEFAULT_ATTEMPTS, Retry.DEFAULT_INTERVAL, RetryExceptionBehaviour.HandleAndCollate, out output);
		}

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// A second delegate must also be supplied that will be used to determine
		/// whether or not the Func delegate succeeded.
		/// A configurable number of attempts and interval beteween attempts can also
		/// be specified.
		/// Optionally opt-out of automatic exception handling.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="T3">
		/// The type of the third argument to the delegate
		/// </typeparam>
		/// <typeparam name="T4">
		/// The type of the fourth argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="arg2">
		/// The second argument to the Func delegate
		/// </param>
		/// <param name="arg3">
		/// The third argument to the Func delegate
		/// </param>
		/// <param name="arg4">
		/// The fourth argument to the Func delegate
		/// </param>
		/// <param name="successCondition">
		/// A delegate that will be invoked after running the Func delegate.
		/// This delegate will be passed the return value from the Func delegate
		/// and will be used to determine whether or not the invokation of the
		/// Func delegate succeeded or not.
		/// </param>
		/// <param name="numberOfAttempts">
		/// The number of times the Func delegate should be run before giving up.
		/// </param>
		/// <param name="interval">
		/// The amount of time to wait inbetween each attempt
		/// </param>
		/// <param name="handleExceptions">
		/// Whether or not we want to automatically handle any exceptions that get 
		/// thrown by the Fund delegate. If this is True then calling code must
		/// be prepared to handle any exceptions that can be thrown by the 
		/// Func delegate
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
        public static OperationResult Attempt<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> functionToAttempt, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<TResult, bool> successCondition, int numberOfAttempts, TimeSpan interval, RetryExceptionBehaviour exceptionBehaviour, out TResult output)
		{
			Tuple<T1, T2, T3, T4, object, object, object> args = new Tuple<T1, T2, T3, T4, object, object, object>(arg1, arg2, arg3, arg4, null, null, null);
            return DoAttempt<T1, T2, T3, T4, object, object, object, TResult>(4, functionToAttempt, args, successCondition, numberOfAttempts, interval, exceptionBehaviour, out output);
		}

		#endregion

		#region Func<T1, T2, T3, T4, T5, TResult> Extensions

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="T3">
		/// The type of the third argument to the delegate
		/// </typeparam>
		/// <typeparam name="T4">
		/// The type of the fourth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T5">
		/// The type of the fifth argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="arg2">
		/// The second argument to the Func delegate
		/// </param>
		/// <param name="arg3">
		/// The third argument to the Func delegate
		/// </param>
		/// <param name="arg4">
		/// The fourth argument to the Func delegate
		/// </param>
		/// <param name="arg5">
		/// The fifth argument to the Func delegate
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> functionToAttempt, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, out TResult output)
		{
			return Attempt<T1, T2, T3, T4, T5, TResult>(functionToAttempt, arg1, arg2, arg3, arg4, arg5, null, out output);
		}

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// A second delegate must also be supplied that will be used to determine
		/// whether or not the Func delegate succeeded.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="T3">
		/// The type of the third argument to the delegate
		/// </typeparam>
		/// <typeparam name="T4">
		/// The type of the fourth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T5">
		/// The type of the fifth argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="arg2">
		/// The second argument to the Func delegate
		/// </param>
		/// <param name="arg3">
		/// The third argument to the Func delegate
		/// </param>
		/// <param name="arg4">
		/// The fourth argument to the Func delegate
		/// </param>
		/// <param name="arg5">
		/// The fifth argument to the Func delegate
		/// </param>
		/// <param name="successCondition">
		/// A delegate that will be invoked after running the Func delegate.
		/// This delegate will be passed the return value from the Func delegate
		/// and will be used to determine whether or not the invokation of the
		/// Func delegate succeeded or not.
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> functionToAttempt, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<TResult, bool> successCondition, out TResult output)
		{
            return Attempt<T1, T2, T3, T4, T5, TResult>(functionToAttempt, arg1, arg2, arg3, arg4, arg5, successCondition, Retry.DEFAULT_ATTEMPTS, Retry.DEFAULT_INTERVAL, RetryExceptionBehaviour.HandleAndCollate, out output);
		}

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// A second delegate must also be supplied that will be used to determine
		/// whether or not the Func delegate succeeded.
		/// A configurable number of attempts and interval beteween attempts can also
		/// be specified.
		/// Optionally opt-out of automatic exception handling.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="T3">
		/// The type of the third argument to the delegate
		/// </typeparam>
		/// <typeparam name="T4">
		/// The type of the fourth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T5">
		/// The type of the fifth argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="arg2">
		/// The second argument to the Func delegate
		/// </param>
		/// <param name="arg3">
		/// The third argument to the Func delegate
		/// </param>
		/// <param name="arg4">
		/// The fourth argument to the Func delegate
		/// </param>
		/// <param name="arg5">
		/// The fifth argument to the Func delegate
		/// </param>
		/// <param name="successCondition">
		/// A delegate that will be invoked after running the Func delegate.
		/// This delegate will be passed the return value from the Func delegate
		/// and will be used to determine whether or not the invokation of the
		/// Func delegate succeeded or not.
		/// </param>
		/// <param name="numberOfAttempts">
		/// The number of times the Func delegate should be run before giving up.
		/// </param>
		/// <param name="interval">
		/// The amount of time to wait inbetween each attempt
		/// </param>
		/// <param name="handleExceptions">
		/// Whether or not we want to automatically handle any exceptions that get 
		/// thrown by the Fund delegate. If this is True then calling code must
		/// be prepared to handle any exceptions that can be thrown by the 
		/// Func delegate
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> functionToAttempt, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<TResult, bool> successCondition, int numberOfAttempts, TimeSpan interval, RetryExceptionBehaviour exceptionBehaviour, out TResult output)
		{
			Tuple<T1, T2, T3, T4, T5, object, object> args = new Tuple<T1, T2, T3, T4, T5, object, object>(arg1, arg2, arg3, arg4, arg5, null, null);
			return DoAttempt<T1, T2, T3, T4, T5, object, object, TResult>(5, functionToAttempt, args, successCondition, numberOfAttempts, interval, exceptionBehaviour, out output);
		}

		#endregion

		#region Func<T1, T2, T3, T4, T5, T6, TResult> Extensions

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="T3">
		/// The type of the third argument to the delegate
		/// </typeparam>
		/// <typeparam name="T4">
		/// The type of the fourth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T5">
		/// The type of the fifth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T6">
		/// The type of the sixth argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="arg2">
		/// The second argument to the Func delegate
		/// </param>
		/// <param name="arg3">
		/// The third argument to the Func delegate
		/// </param>
		/// <param name="arg4">
		/// The fourth argument to the Func delegate
		/// </param>
		/// <param name="arg5">
		/// The fifth argument to the Func delegate
		/// </param>
		/// <param name="arg6">
		/// The sixth argument to the Func delegate
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> functionToAttempt, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, out TResult output)
		{
			return Attempt<T1, T2, T3, T4, T5, T6, TResult>(functionToAttempt, arg1, arg2, arg3, arg4, arg5, arg6, null, out output);
		}

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// A second delegate must also be supplied that will be used to determine
		/// whether or not the Func delegate succeeded.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="T3">
		/// The type of the third argument to the delegate
		/// </typeparam>
		/// <typeparam name="T4">
		/// The type of the fourth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T5">
		/// The type of the fifth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T6">
		/// The type of the sixth argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="arg2">
		/// The second argument to the Func delegate
		/// </param>
		/// <param name="arg3">
		/// The third argument to the Func delegate
		/// </param>
		/// <param name="arg4">
		/// The fourth argument to the Func delegate
		/// </param>
		/// <param name="arg5">
		/// The fifth argument to the Func delegate
		/// </param>
		/// <param name="arg6">
		/// The sixth argument to the Func delegate
		/// </param>
		/// <param name="successCondition">
		/// A delegate that will be invoked after running the Func delegate.
		/// This delegate will be passed the return value from the Func delegate
		/// and will be used to determine whether or not the invokation of the
		/// Func delegate succeeded or not.
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> functionToAttempt, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<TResult, bool> successCondition, out TResult output)
		{
			return Attempt<T1, T2, T3, T4, T5, T6, TResult>(functionToAttempt, arg1, arg2, arg3, arg4, arg5, arg6, successCondition, Retry.DEFAULT_ATTEMPTS, Retry.DEFAULT_INTERVAL, RetryExceptionBehaviour.HandleAndCollate, out output);
		}

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// A second delegate must also be supplied that will be used to determine
		/// whether or not the Func delegate succeeded.
		/// A configurable number of attempts and interval beteween attempts can also
		/// be specified.
		/// Optionally opt-out of automatic exception handling.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="T3">
		/// The type of the third argument to the delegate
		/// </typeparam>
		/// <typeparam name="T4">
		/// The type of the fourth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T5">
		/// The type of the fifth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T6">
		/// The type of the sixth argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="arg2">
		/// The second argument to the Func delegate
		/// </param>
		/// <param name="arg3">
		/// The third argument to the Func delegate
		/// </param>
		/// <param name="arg4">
		/// The fourth argument to the Func delegate
		/// </param>
		/// <param name="arg5">
		/// The fifth argument to the Func delegate
		/// </param>
		/// <param name="arg6">
		/// The sixth argument to the Func delegate
		/// </param>
		/// <param name="successCondition">
		/// A delegate that will be invoked after running the Func delegate.
		/// This delegate will be passed the return value from the Func delegate
		/// and will be used to determine whether or not the invokation of the
		/// Func delegate succeeded or not.
		/// </param>
		/// <param name="numberOfAttempts">
		/// The number of times the Func delegate should be run before giving up.
		/// </param>
		/// <param name="interval">
		/// The amount of time to wait inbetween each attempt
		/// </param>
		/// <param name="handleExceptions">
		/// Whether or not we want to automatically handle any exceptions that get 
		/// thrown by the Fund delegate. If this is True then calling code must
		/// be prepared to handle any exceptions that can be thrown by the 
		/// Func delegate
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
        public static OperationResult Attempt<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> functionToAttempt, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<TResult, bool> successCondition, int numberOfAttempts, TimeSpan interval, RetryExceptionBehaviour exceptionBehaviour, out TResult output)
		{
			Tuple<T1, T2, T3, T4, T5, T6, object> args = new Tuple<T1, T2, T3, T4, T5, T6, object>(arg1, arg2, arg3, arg4, arg5, arg6, null);
            return DoAttempt<T1, T2, T3, T4, T5, T6, object, TResult>(6, functionToAttempt, args, successCondition, numberOfAttempts, interval, exceptionBehaviour, out output);
		}

		#endregion

		#region Func<T1, T2, T3, T4, T5, T6, T7, TResult> Extensions

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="T3">
		/// The type of the third argument to the delegate
		/// </typeparam>
		/// <typeparam name="T4">
		/// The type of the fourth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T5">
		/// The type of the fifth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T6">
		/// The type of the sixth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T7">
		/// The type of the seventh argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="arg2">
		/// The second argument to the Func delegate
		/// </param>
		/// <param name="arg3">
		/// The third argument to the Func delegate
		/// </param>
		/// <param name="arg4">
		/// The fourth argument to the Func delegate
		/// </param>
		/// <param name="arg5">
		/// The fifth argument to the Func delegate
		/// </param>
		/// <param name="arg6">
		/// The sixth argument to the Func delegate
		/// </param>
		/// <param name="arg7">
		/// The seveth argument to the Func delegate
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> functionToAttempt, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, out TResult output)
		{
			return Attempt<T1, T2, T3, T4, T5, T6, T7, TResult>(functionToAttempt, arg1, arg2, arg3, arg4, arg5, arg6, arg7, null, out output);
		}

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// A second delegate must also be supplied that will be used to determine
		/// whether or not the Func delegate succeeded.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="T3">
		/// The type of the third argument to the delegate
		/// </typeparam>
		/// <typeparam name="T4">
		/// The type of the fourth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T5">
		/// The type of the fifth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T6">
		/// The type of the sixth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T7">
		/// The type of the seventh argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="arg2">
		/// The second argument to the Func delegate
		/// </param>
		/// <param name="arg3">
		/// The third argument to the Func delegate
		/// </param>
		/// <param name="arg4">
		/// The fourth argument to the Func delegate
		/// </param>
		/// <param name="arg5">
		/// The fifth argument to the Func delegate
		/// </param>
		/// <param name="arg6">
		/// The sixth argument to the Func delegate
		/// </param>
		/// <param name="arg7">
		/// The seveth argument to the Func delegate
		/// </param>
		/// <param name="successCondition">
		/// A delegate that will be invoked after running the Func delegate.
		/// This delegate will be passed the return value from the Func delegate
		/// and will be used to determine whether or not the invokation of the
		/// Func delegate succeeded or not.
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> functionToAttempt, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, Func<TResult, bool> successCondition, out TResult output)
		{
            return Attempt<T1, T2, T3, T4, T5, T6, T7, TResult>(functionToAttempt, arg1, arg2, arg3, arg4, arg5, arg6, arg7, successCondition, Retry.DEFAULT_ATTEMPTS, Retry.DEFAULT_INTERVAL, RetryExceptionBehaviour.HandleAndCollate, out output);
		}

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// A second delegate must also be supplied that will be used to determine
		/// whether or not the Func delegate succeeded.
		/// A configurable number of attempts and interval beteween attempts can also
		/// be specified.
		/// Optionally opt-out of automatic exception handling.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="T3">
		/// The type of the third argument to the delegate
		/// </typeparam>
		/// <typeparam name="T4">
		/// The type of the fourth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T5">
		/// The type of the fifth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T6">
		/// The type of the sixth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T7">
		/// The type of the seventh argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="functionToAttempt">
		/// The Func delegate that we want to attempt multiple times
		/// </param>
		/// <param name="arg1">
		/// The first argument to the Func delegate
		/// </param>
		/// <param name="arg2">
		/// The second argument to the Func delegate
		/// </param>
		/// <param name="arg3">
		/// The third argument to the Func delegate
		/// </param>
		/// <param name="arg4">
		/// The fourth argument to the Func delegate
		/// </param>
		/// <param name="arg5">
		/// The fifth argument to the Func delegate
		/// </param>
		/// <param name="arg6">
		/// The sixth argument to the Func delegate
		/// </param>
		/// <param name="arg7">
		/// The seveth argument to the Func delegate
		/// </param>
		/// <param name="successCondition">
		/// A delegate that will be invoked after running the Func delegate.
		/// This delegate will be passed the return value from the Func delegate
		/// and will be used to determine whether or not the invokation of the
		/// Func delegate succeeded or not.
		/// </param>
		/// <param name="numberOfAttempts">
		/// The number of times the Func delegate should be run before giving up.
		/// </param>
		/// <param name="interval">
		/// The amount of time to wait inbetween each attempt
		/// </param>
		/// <param name="handleExceptions">
		/// Whether or not we want to automatically handle any exceptions that get 
		/// thrown by the Fund delegate. If this is True then calling code must
		/// be prepared to handle any exceptions that can be thrown by the 
		/// Func delegate
		/// </param>
		/// <param name="output">
		/// The return value that the Func delegate will return.  This will only
		/// be set if the invokation of the Func delegate succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not the Func delegate succeeded.
		/// If Func delegate failed then this contains error information
		/// </returns>
		public static OperationResult Attempt<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> functionToAttempt, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, Func<TResult, bool> successCondition, int numberOfAttempts, TimeSpan interval, RetryExceptionBehaviour exceptionBehaviour, out TResult output)
		{
			Tuple<T1, T2, T3, T4, T5, T6, T7> args = new Tuple<T1, T2, T3, T4, T5, T6, T7>(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
			return DoAttempt<T1, T2, T3, T4, T5, T6, T7, TResult>(7, functionToAttempt, args, successCondition, numberOfAttempts, interval, exceptionBehaviour, out output);
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Attempts to run the supplied Func delegate with the supplied arguments.
		/// </summary>
		/// <typeparam name="T1">
		/// The type of the first argument to the delegate
		/// </typeparam>
		/// <typeparam name="T2">
		/// The type of the second argument to the delegate
		/// </typeparam>
		/// <typeparam name="T3">
		/// The type of the third argument to the delegate
		/// </typeparam>
		/// <typeparam name="T4">
		/// The type of the fourth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T5">
		/// The type of the fifth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T6">
		/// The type of the sixth argument to the delegate
		/// </typeparam>
		/// <typeparam name="T7">
		/// The type of the seventh argument to the delegate
		/// </typeparam>
		/// <typeparam name="TResult">
		/// The type of the return argument to the delegate
		/// </typeparam>
		/// <param name="numberOfInputArguments">
		/// The number of input arguments to the delegate
		/// </param>
		/// <param name="functionToAttempt">
		/// The function that we'll attempt to run
		/// </param>
		/// <param name="args">
		/// A Tuple containing all of the arguments to the functionToAttempt
		/// </param>
		/// <param name="successCondition">
		/// A delegate that will be invoked after running the Func delegate.
		/// This delegate will be passed the return value from the Func delegate
		/// and will be used to determine whether or not the invokation of the
		/// Func delegate succeeded or not.
		/// </param>
		/// <param name="numberOfAttempts">
		/// The number of times the functionToAttempt should be run before giving up.
		/// </param>
		/// <param name="interval">
		/// The amount of time to wait inbetween each attempt
		/// </param>
		/// <param name="handleExceptions">
		/// Whether or not we want to automatically handle any exceptions that get 
		/// thrown by functionToHandle. If this is True then calling code must
		/// be prepared to handle any exceptions that can be thrown by the 
		/// functionToAttempt
		/// </param>
		/// <param name="output">
		/// The return value that functionToAttempt will return.  This will only
		/// be set if functionToAttempt succeeded
		/// </param>
		/// <returns>
		/// An OperationResult that tells whether or not functionToAttempt succeeded.
		/// If functionToAttempt failed then this contains error information
		/// </returns>
		private static OperationResult DoAttempt<T1, T2, T3, T4, T5, T6, T7, TResult>(
			int numberOfInputArguments,
			Delegate functionToAttempt,
			Tuple<T1, T2, T3, T4, T5, T6, T7> args,
			Func<TResult, bool> successCondition,
			int numberOfAttempts,
			TimeSpan interval,
			RetryExceptionBehaviour exceptionBehaviour,
			out TResult output)
		{

			TResult localResult = output = default(TResult);

			//Set up a lamdba (closure) that will use the Retry class to 
			//do the attempts for us.
			OperationResult result = Retry.Attempt(
				() =>
				{
					//There is no nice way of converting one Func<> to another 
					//(e.g. converting Func<T1, TResult> to a Func<T1,T2,TResult> 
					//so we rely on some nasty casting and a Tuple to pass the 
					//arguments about).
					switch (numberOfInputArguments)
					{
						case 0:
							localResult = ((Func<TResult>)functionToAttempt)();
							break;
						case 1:
							localResult = ((Func<T1, TResult>)functionToAttempt)(args.Item1);
							break;
						case 2:
							localResult = ((Func<T1, T2, TResult>)functionToAttempt)(args.Item1, args.Item2);
							break;
						case 3:
							localResult = ((Func<T1, T2, T3, TResult>)functionToAttempt)(args.Item1, args.Item2, args.Item3);
							break;
						case 4:
							localResult = ((Func<T1, T2, T3, T4, TResult>)functionToAttempt)(args.Item1, args.Item2, args.Item3, args.Item4);
							break;
						case 5:
							localResult = ((Func<T1, T2, T3, T4, T5, TResult>)functionToAttempt)(args.Item1, args.Item2, args.Item3, args.Item4, args.Item5);
							break;
						case 6:
							localResult = ((Func<T1, T2, T3, T4, T5, T6, TResult>)functionToAttempt)(args.Item1, args.Item2, args.Item3, args.Item4, args.Item5, args.Item6);
							break;
						case 7:
							localResult = ((Func<T1, T2, T3, T4, T5, T6, T7, TResult>)functionToAttempt)(args.Item1, args.Item2, args.Item3, args.Item4, args.Item5, args.Item6, args.Item7);
							break;
					}

					if (successCondition != null)
						//use the successCondition delegate to determine
						//whether or not the value that was returned from 
						//functionToAttempt can be considered a successful invokation
						//or not.
					{
						return successCondition(localResult);
					}
					else
						//If no delegate was supplied that we can use to determine
						//whether or not the functionToAttempt succeeded then we'll just
						//assume success. In this case the functionToAttempt is one
						//of those functions that throws an exception to denote failure.
					{
						return true;
					}
				},
				numberOfAttempts,
				interval,
                exceptionBehaviour);

			//If the functionToAttempt was a success then we need
			//to provide the final output object
			if (result) { output = localResult; }

			return result;
		}

		#endregion
	}
}
#endif