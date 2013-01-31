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

        [Test]
        public void Apply_Two()
        {
            Func<int, int, int> factorial =
                YCombinator.Apply<int, int, int>(func => (a, b) => a == 0 ? 1 : a * func(a - b, b));

            int value = -1;

            value = factorial(10, 1);
            Assert.AreEqual(3628800, value);
        }

        [Test]
        public void Apply_Three()
        {
            Func<int, int, int, int> factorial =
                YCombinator.Apply<int, int, int, int>(func => (a, b, c) => a == 0 ? 1 : a * func(a - b - c, b, c));

            int value = -1;

            value = factorial(10, 1, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 1);
            Assert.AreEqual(3628800, value);
        }

        [Test]
        public void Apply_Four()
        {
            Func<int, int, int, int, int > factorial =
                YCombinator.Apply<int, int, int, int, int>(func => (a, b, c, d) => a == 0 ? 1 : a * func(a - b - c - d, b, c, d));

            int value = -1;

            value = factorial(10, 1, 0, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 1, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 0, 1);
            Assert.AreEqual(3628800, value);
        }

        [Test]
        public void Apply_Five()
        {
            Func<int, int, int, int, int, int> factorial =
                YCombinator.Apply<int, int, int, int, int, int>(func => (a, b, c, d, e) => a == 0 ? 1 : a * func(a - b - c - d - e, b, c, d, e));

            int value = -1;

            value = factorial(10, 1, 0, 0, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 1, 0, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 0, 1, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 0, 0, 1);
            Assert.AreEqual(3628800, value);
        }

        [Test]
        public void Apply_Six()
        {
            Func<int, int, int, int, int, int, int> factorial =
                YCombinator.Apply<int, int, int, int, int, int, int>(func => (a, b, c, d, e, f) => a == 0 ? 1 : a * func(a - b - c - d - e - f, b, c, d, e, f));

            int value = -1;

            value = factorial(10, 1, 0, 0, 0, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 1, 0, 0, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 0, 1, 0, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 0, 0, 1, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 0, 0, 0, 1);
            Assert.AreEqual(3628800, value); 
        }

        [Test]
        public void Apply_Seven()
        {
            Func<int, int, int, int, int, int, int, int> factorial =
                YCombinator.Apply<int, int, int, int, int, int, int, int>(func => (a, b, c, d, e, f, g) => a == 0 ? 1 : a * func(a - b - c - d - e - f - g, b, c, d, e, f, g));

            int value = -1;

            value = factorial(10, 1, 0, 0, 0, 0, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 1, 0, 0, 0, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 0, 1, 0, 0, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 0, 0, 1, 0, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 0, 0, 0, 1, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 0, 0, 0, 0, 1);
            Assert.AreEqual(3628800, value);
        }

        [Test]
        public void Apply_Eight()
        {
            Func<int, int, int, int, int, int, int, int, int> factorial =
                YCombinator.Apply<int, int, int, int, int, int, int, int, int>(func => (a, b, c, d, e, f, g, h) => a == 0 ? 1 : a*func(a - b - c - d - e - f - g - h, b, c, d, e, f, g, h));

            int value = -1;

            value = factorial(10, 1, 0, 0, 0, 0, 0, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 1, 0, 0, 0, 0, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 0, 1, 0, 0, 0, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 0, 0, 1, 0, 0, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 0, 0, 0, 1, 0, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 0, 0, 0, 0, 1, 0);
            Assert.AreEqual(3628800, value);

            value = factorial(10, 0, 0, 0, 0, 0, 0, 1);
            Assert.AreEqual(3628800, value);
        }
    }
}
