using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Shared.Utilities.Scopes
{
    /// <summary>
    /// Scope to provide simple timings for regions of code.
    /// </summary>
    public class StopwatchScope : Scope<Stopwatch>
    {
        #region Constructors

        /// <summary>
        /// Create new StopwatchScope wrapping a new Stopwatch
        /// </summary>
        public StopwatchScope() : this(new Stopwatch()) {}

        /// <summary>
        /// Create new StopwatchScope wrapping the supplied Stopwatch
        /// </summary>
        public StopwatchScope(Stopwatch stopwatch) : base(stopwatch){}

        #endregion

        #region Public properties

        /// <summary>
        /// Provide start/stop stopwatch semantics for timing a block of code
        /// </summary>
        public Disposer<Stopwatch> RecordDuration
        {
            get { return Disposer<Stopwatch>.Create(Value, v => v.Restart(), v => v.Stop()); }
        }

        #endregion
    }
}
