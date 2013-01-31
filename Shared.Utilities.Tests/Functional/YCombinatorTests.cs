using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.Functional;

namespace Shared.Utilities.Tests.Functional
{
    [TestFixture]
    public class YCombinatorTests
    {
        [Test]
        public void Apply_One()
        {
            Func<int, int> factorial = YCombinator.Apply<int,int>(f => n => n == 0 ? 1 : n * f(n - 1));

            int value = factorial(10);

            Assert.AreEqual(3628800, value);
        }
    }
}
