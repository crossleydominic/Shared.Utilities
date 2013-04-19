using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using Shared.Utilities.Scopes;

namespace Shared.Utilities.Tests.Scopes
{
    [TestFixture]
    public class ReaderWriterLockSlimScopeTests
    {
        #region Read Tests

        [Test]
        public void Read_ReadLockTakenAndReleased()
        {
            ReaderWriterLockSlimScope subject = new ReaderWriterLockSlimScope();

            using (subject.Read)
            {
                Assert.IsTrue(subject.Value.IsReadLockHeld);
                Assert.IsFalse(subject.Value.IsUpgradeableReadLockHeld);
                Assert.IsFalse(subject.Value.IsWriteLockHeld);
            }

            Assert.IsFalse(subject.Value.IsReadLockHeld);
            Assert.IsFalse(subject.Value.IsUpgradeableReadLockHeld);
            Assert.IsFalse(subject.Value.IsWriteLockHeld);
        }

        #endregion

        #region UpgradeableRead Tests

        [Test]
        public void UpgradeableRead_UpgradeableReadLockTakenAndReleased()
        {
            ReaderWriterLockSlimScope subject = new ReaderWriterLockSlimScope();

            using (subject.UpgradeableRead)
            {
                Assert.IsFalse(subject.Value.IsReadLockHeld);
                Assert.IsTrue(subject.Value.IsUpgradeableReadLockHeld);
                Assert.IsFalse(subject.Value.IsWriteLockHeld);
            }

            Assert.IsFalse(subject.Value.IsReadLockHeld);
            Assert.IsFalse(subject.Value.IsUpgradeableReadLockHeld);
            Assert.IsFalse(subject.Value.IsWriteLockHeld);
        }

        #endregion

        #region Write Tests

        [Test]
        public void Write_WriteLockTakenAndReleased()
        {
            ReaderWriterLockSlimScope subject = new ReaderWriterLockSlimScope();

            using (subject.Write)
            {
                Assert.IsFalse(subject.Value.IsReadLockHeld);
                Assert.IsFalse(subject.Value.IsUpgradeableReadLockHeld);
                Assert.IsTrue(subject.Value.IsWriteLockHeld);
            }

            Assert.IsFalse(subject.Value.IsReadLockHeld);
            Assert.IsFalse(subject.Value.IsUpgradeableReadLockHeld);
            Assert.IsFalse(subject.Value.IsWriteLockHeld);
        }

        #endregion
    }
}
