using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities
{
    public enum RetryExceptionBehaviour
    {
        /// <summary>
        /// Exceptions are not handled at all and are immeditately
        /// thrown to calling code
        /// </summary>
        DoNotHandle = 1,

        /// <summary>
        /// Exceptions are caught by Retry and added to a list of thrown exceptions
        /// that is available in the OperationResult
        /// </summary>
        HandleAndCollate = 2,

        /// <summary>
        /// Exceptions are caught but they discarded. A summary of the number of exceptions
        /// thrown is added to the OperationResult.
        /// </summary>
        HandleOnly = 3
    }
}
