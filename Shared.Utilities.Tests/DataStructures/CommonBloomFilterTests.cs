using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Utilities.DataStructures;
using NUnit.Framework;

namespace Shared.Utilities.Tests.DataStructures
{
    public static class CommonBloomFilterTests
    {
        public static void Add_NullData_Throws<T>(BaseBloomFilter<T> bf)
        {
            bf.Add((byte[])null);
        }

        public static void Add_EmptyData_Throws<T>(BaseBloomFilter<T> bf)
        {
            bf.Add(new byte[0]);
        }

        public static void Add_NullString_Throws<T>(BaseBloomFilter<T> bf)
        {
            bf.Add((string)null);
        }

        public static void Add_EmptyString_Throws<T>(BaseBloomFilter<T> bf)
        {
            bf.Add(string.Empty);
        }

        public static void Add_AddedItemIsPresent<T>(BaseBloomFilter<T> bf)
        {
            bf.Add("a");

            Assert.IsTrue(bf.IsPresent("a"));
        }

        public static void Add_NonAddedItemIsNotPresent<T>(BaseBloomFilter<T> bf)
        {
            bf.Add("a");

            Assert.IsFalse(bf.IsPresent("b"));
        }

        public static void Clear_AddedItemNoLongerPresent<T>(BaseBloomFilter<T> bf)
        {
            bf.Add("a");
            bf.Clear();
            Assert.IsFalse(bf.IsPresent("a"));
        }

        /// <summary>
        /// Tests to make sure the that the requested error rate is achieved
        /// when inserting random strings.
        /// </summary>
        public static void ErrorRateWithinExpectedBounds<T>(BaseBloomFilter<T> bf)
        {
            for (int i = 0; i < bf.ExpectedNumberOfElements; i++)
            {
                bf.Add(StaticRandom.NextString(10));
            }

            int falsePositives = 0;
            for (int i = 0; i < bf.ExpectedNumberOfElements; i++)
            {
                if (bf.IsPresent(StaticRandom.NextString(5)))
                {
                    falsePositives++;
                }
            }

            Assert.LessOrEqual(falsePositives, (1.03d * ((double)bf.ExpectedNumberOfElements) * bf.ErrorRate));
        }
    }
}
