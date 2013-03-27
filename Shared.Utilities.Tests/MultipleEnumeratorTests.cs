using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.Tests
{
    [TestFixture]
    public class MultipleEnumeratorTests
    {
        #region Constructor tests

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullEnumerables()
        {
            MultipleEnumerator<int> subject = new MultipleEnumerator<int>(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EmptyEnumerables()
        {
            MultipleEnumerator<int> subject = new MultipleEnumerator<int>();
        }

        #endregion

        [Test]
        public void GetEnumerator_OneList()
        {
            List<int> list1 = new List<int>(){1,2,3};

            MultipleEnumerator<int> subject = new MultipleEnumerator<int>(list1);

            int counter = 1;
            foreach (int i in subject)
            {
                Assert.AreEqual(counter, i);
                counter += 1;
            }
        }

        [Test]
        public void GetEnumerator_MultipleLists()
        {
            List<int> list1 = new List<int>() { 1, 2, 3 };
            List<int> list2 = new List<int>() { 4, 5, 6 };
            List<int> list3 = new List<int>() { 7, 8, 9 };

            MultipleEnumerator<int> subject = new MultipleEnumerator<int>(list1, list2, list3);

            int counter = 1;
            foreach (int i in subject)
            {
                Assert.AreEqual(counter, i);
                counter += 1;
            }
        }

        [Test]
        public void GetEnumerator_MultipleListsWithDifferentImplementations()
        {
            List<DerivedA> list1 = new List<DerivedA>() { new DerivedA(1), new DerivedA(2), new DerivedA(3) };
            List<DerivedB> list2 = new List<DerivedB>() { new DerivedB(4), new DerivedB(5), new DerivedB(6) };

            MultipleEnumerator<Base> subject = new MultipleEnumerator<Base>(list1, list2);

            int counter = 1;
            foreach (Base b in subject)
            {
                Assert.AreEqual(counter, b.Value());
                counter += 1;
            } 
        }

        public abstract class Base
        {
            public abstract int Value();
        }

        public class DerivedA : Base
        {
            private int _value;
            public DerivedA(int value)
            {
                _value = value;
            }
            public override int Value()
            {
                return _value;
            }
        }

        public class DerivedB : Base
        {
            private int _value;

            public DerivedB(int value)
            {
                _value = value;
            }

            public override int Value()
            {
                return _value;
            }
        }

    }
}
