using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.ExtensionMethods.Collections
{
    public static class IEnumerableExtensions
    {
        public static IList<IList<T>> Chunk<T>(this IEnumerable<T> data, int chunkSize)
        {
            #region Input validation

            Insist.IsAtLeast(chunkSize, 1, "chunkSize");

            if (!data.Any())
            {
                return new List<IList<T>>();
            }

            #endregion

            IList<IList<T>> chunks = new List<IList<T>>();
            int currentIndex = 0;

            List<T> currentChunk = null;

            foreach (T element in data)
            {
                int offset = currentIndex % chunkSize;

                if (offset == 0)
                {
                    currentChunk = new List<T>();
                    chunks.Add(currentChunk);
                }

                currentChunk.Add(element);

                currentIndex++;
            }

            return chunks;
        }
    }
}
