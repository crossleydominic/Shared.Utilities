using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.Functional;

namespace Shared.Utilities.Tests.Functional
{
    [TestFixture]
    public class UncurryTests
    {
        [Test]
        public void Two_UsesValues()
        {
            var func = Uncurry.Two<int, int, int>(a => b => a + b);

            int value = -1;

            value = func(1, 0);
            Assert.AreEqual(1, value);

            value = func(1, 2);
            Assert.AreEqual(3, value);
        }

        [Test]
        public void Three_UsesValues()
        {
            var func = Uncurry.Three<int, int, int, int>(a => b => c => a + b + c);

            int value = -1;

            value = func(1, 0, 0);
            Assert.AreEqual(1, value);

            value = func(1, 2, 0);
            Assert.AreEqual(3, value);

            value = func(1, 2, 3);
            Assert.AreEqual(6, value);
        }

        [Test]
        public void Four_UsesValues()
        {
            var func = Uncurry.Four<int, int, int, int, int>(a => b => c => d =>a + b + c + d);

            int value = -1;

            value = func(1, 0, 0, 0);
            Assert.AreEqual(1, value);

            value = func(1, 2, 0, 0);
            Assert.AreEqual(3, value);

            value = func(1, 2, 3, 0);
            Assert.AreEqual(6, value);

            value = func(1, 2, 3, 4);
            Assert.AreEqual(10, value);
        }

        [Test]
        public void Five_UsesValues()
        {
            var func = Uncurry.Five<int, int, int, int, int, int>(a => b => c => d => e => a + b + c + d + e);

            int value = -1;

            value = func(1, 0, 0, 0, 0);
            Assert.AreEqual(1, value);

            value = func(1, 2, 0, 0, 0);
            Assert.AreEqual(3, value);

            value = func(1, 2, 3, 0, 0);
            Assert.AreEqual(6, value);

            value = func(1, 2, 3, 4, 0);
            Assert.AreEqual(10, value);

            value = func(1, 2, 3, 4, 5);
            Assert.AreEqual(15, value);
        }

        [Test]
        public void Six_UsesValues()
        {
            var func = Uncurry.Six<int, int, int, int, int, int, int>(a => b => c => d => e => f => a + b + c + d + e + f);

            int value = -1;

            value = func(1, 0, 0, 0, 0, 0);
            Assert.AreEqual(1, value);

            value = func(1, 2, 0, 0, 0, 0);
            Assert.AreEqual(3, value);

            value = func(1, 2, 3, 0, 0, 0);
            Assert.AreEqual(6, value);

            value = func(1, 2, 3, 4, 0, 0);
            Assert.AreEqual(10, value);

            value = func(1, 2, 3, 4, 5, 0);
            Assert.AreEqual(15, value);

            value = func(1, 2, 3, 4, 5, 6);
            Assert.AreEqual(21, value);
        }

        [Test]
        public void Seven_UsesValues()
        {
            var func = Uncurry.Seven<int, int, int, int, int, int, int, int>(a => b => c => d => e => f => g => a + b + c + d + e + f + g);

            int value = -1;

            value = func(1, 0, 0, 0, 0, 0, 0);
            Assert.AreEqual(1, value);

            value = func(1, 2, 0, 0, 0, 0, 0);
            Assert.AreEqual(3, value);

            value = func(1, 2, 3, 0, 0, 0, 0);
            Assert.AreEqual(6, value);

            value = func(1, 2, 3, 4, 0, 0, 0);
            Assert.AreEqual(10, value);

            value = func(1, 2, 3, 4, 5, 0, 0);
            Assert.AreEqual(15, value);

            value = func(1, 2, 3, 4, 5, 6, 0);
            Assert.AreEqual(21, value);

            value = func(1, 2, 3, 4, 5, 6, 7);
            Assert.AreEqual(28, value);
        }

        [Test]
        public void Eight_UsesValues()
        {
            var func = Uncurry.Eight<int, int, int, int, int, int, int, int, int>(a => b => c => d => e => f => g => h => a + b + c + d + e + f + g + h);

            int value = -1;

            value = func(1, 0, 0, 0, 0, 0, 0, 0);
            Assert.AreEqual(1, value);

            value = func(1, 2, 0, 0, 0, 0, 0, 0);
            Assert.AreEqual(3, value);

            value = func(1, 2, 3, 0, 0, 0, 0, 0);
            Assert.AreEqual(6, value);

            value = func(1, 2, 3, 4, 0, 0, 0, 0);
            Assert.AreEqual(10, value);

            value = func(1, 2, 3, 4, 5, 0, 0, 0);
            Assert.AreEqual(15, value);

            value = func(1, 2, 3, 4, 5, 6, 0, 0);
            Assert.AreEqual(21, value);

            value = func(1, 2, 3, 4, 5, 6, 7, 0);
            Assert.AreEqual(28, value);

            value = func(1, 2, 3, 4, 5, 6, 7, 8);
            Assert.AreEqual(36, value);
        }
    }
}
