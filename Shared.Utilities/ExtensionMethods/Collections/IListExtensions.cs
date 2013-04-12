using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.ExtensionMethods.Collections
{
    public enum IndexOutOfRangeBehaviour
    {
        Ignore,
        ThrowException
    }

    public static class IListExtensions
    {
        public static IList<T> FromIndexes<T>(this IList<T> list, params int[] indexes)
        {
            return FromIndexes(list, IndexOutOfRangeBehaviour.Ignore, indexes);
        }

        public static IList<T> FromIndexes<T>(this IList<T> list, IndexOutOfRangeBehaviour outOfRange, params int[] indexes)
        {
            #region Input validation

            Insist.IsNotNull(list, "list");
            Insist.IsNotNull(indexes, "indexes");
            Insist.IsDefined(outOfRange, "outOfRange");

            #endregion

            List<T> items = new List<T>();

            if (list.Count == 0)
            {
                return items;
            }

            foreach (int index in indexes)
            {
                if (index >= list.Count)
                    //Out of bounds
                {
                    if (outOfRange == IndexOutOfRangeBehaviour.Ignore)
                    {
                        continue;
                    }
                }

                items.Add(list[index]);
            }

            return items;
        }
    }
}
