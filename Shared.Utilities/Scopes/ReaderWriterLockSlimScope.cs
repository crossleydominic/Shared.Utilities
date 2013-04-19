using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Shared.Utilities.Scopes
{
    /// <summary>
    /// Scope to provide Enter/Exit scopes for ReaderWriterLockSlim objects.
    /// </summary>
    public class ReaderWriterLockSlimScope : Scope<ReaderWriterLockSlim>
    {
        #region Constructors

        /// <summary>
        /// Create new ReaderWriterLockSlimScope wrapping a new ReaderWriterLockSlim
        /// </summary>
        public ReaderWriterLockSlimScope() : this(new ReaderWriterLockSlim()) {}

        /// <summary>
        /// Create new ReaderWriterLockSlimScope wrapping the supplied ReaderWriterLockSlim
        /// </summary>
        public ReaderWriterLockSlimScope(ReaderWriterLockSlim @lock) : base(@lock) {}

        #endregion

        #region Public properties

        /// <summary>
        /// Get disposer to provide EnterRead/ExitRead semantics
        /// </summary>
        public Disposer<ReaderWriterLockSlim> Read
        {
            get { return Create(v => v.EnterReadLock(), v => v.ExitReadLock()); }
        }

        /// <summary>
        /// Get disposer to provide EnterUpgradeableReadLock/ExitUpgradeableReadLock semantics
        /// </summary>
        public Disposer<ReaderWriterLockSlim> UpgradeableRead
        {
            get { return Create(v => v.EnterUpgradeableReadLock(), v => v.ExitUpgradeableReadLock()); }
        }

        /// <summary>
        /// Get disposer to provide EnterWriteLock/ExitWriteLock semantics
        /// </summary>
        public Disposer<ReaderWriterLockSlim> Write
        {
            get { return Create(v => v.EnterWriteLock(), v => v.ExitWriteLock()); }
        }

        #endregion
    }
}
