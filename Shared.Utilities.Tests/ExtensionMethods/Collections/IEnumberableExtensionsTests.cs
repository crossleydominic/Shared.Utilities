using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shared.Utilities.ExtensionMethods.Collections;

namespace Shared.Utilities.Tests.ExtensionMethods.Collections
{
    [TestFixture]
    public class IEnumberableExtensionsTests
    {
        #region Chunk

        [Test]
        public void Chunk_NoData()
        {
            List<int> subject = new List<int>();
            var chunks = subject.Chunk(10);

            Assert.IsNotNull(chunks);
            Assert.AreEqual(0, chunks.Count);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Chunk_InvalidChunkSize()
        {
            List<int> subject = new List<int>(){1,2,3,4,5,6};
            var chunks = subject.Chunk(0);
        }

        [Test]
        public void Chunk_ChunkSizeBiggerThanCollection()
        {
            List<int> subject = new List<int>() { 1, 2, 3, 4, 5, 6 };
            var chunks = subject.Chunk(10);

            Assert.IsNotNull(chunks);
            Assert.AreEqual(1, chunks.Count);
            Assert.AreEqual(6, chunks[0].Count);
            Assert.AreEqual(1, chunks[0][0]);
            Assert.AreEqual(2, chunks[0][1]);
            Assert.AreEqual(3, chunks[0][2]);
            Assert.AreEqual(4, chunks[0][3]);
            Assert.AreEqual(5, chunks[0][4]);
            Assert.AreEqual(6, chunks[0][5]);

        }

        [Test]
        public void Chunk_ChunkSizeMultipleOfCollectionSize()
        {
            List<int> subject = new List<int>() { 1, 2, 3, 4, 5, 6 };
            var chunks = subject.Chunk(3);

            Assert.IsNotNull(chunks);
            Assert.AreEqual(2, chunks.Count);

            Assert.AreEqual(3, chunks[0].Count);
            Assert.AreEqual(1, chunks[0][0]);
            Assert.AreEqual(2, chunks[0][1]);
            Assert.AreEqual(3, chunks[0][2]);

            Assert.AreEqual(3, chunks[1].Count);
            Assert.AreEqual(4, chunks[1][0]);
            Assert.AreEqual(5, chunks[1][1]);
            Assert.AreEqual(6, chunks[1][2]);
        }

        [Test]
        public void Chunk_ChunkSizeNotMultipleOfCollectionSize()
        {
            List<int> subject = new List<int>() { 1, 2, 3, 4, 5, 6 };
            var chunks = subject.Chunk(4);

            Assert.IsNotNull(chunks);
            Assert.AreEqual(2, chunks.Count);

            Assert.AreEqual(4, chunks[0].Count);
            Assert.AreEqual(1, chunks[0][0]);
            Assert.AreEqual(2, chunks[0][1]);
            Assert.AreEqual(3, chunks[0][2]);
            Assert.AreEqual(4, chunks[0][3]);

            Assert.AreEqual(2, chunks[1].Count);
            Assert.AreEqual(5, chunks[1][0]);
            Assert.AreEqual(6, chunks[1][1]);
        }

        #endregion
    }
}
