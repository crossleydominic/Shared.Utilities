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
    public class StopwatchScopeTests
    {
        #region RecordDuration tests

        [Test]
        public void RecordDuration_DurationRecorded()
        {
            StopwatchScope subject = new StopwatchScope();

            using (subject.RecordDuration)
            {
                //Yield
                Thread.Sleep(0);
            }

            Assert.Greater(subject.Value.ElapsedTicks, 0);
        }

        #endregion
    }
}
