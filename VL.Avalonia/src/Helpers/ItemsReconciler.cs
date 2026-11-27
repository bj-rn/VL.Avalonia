using System.Runtime.CompilerServices;
using Avalonia.Collections;

namespace VL.Avalonia.Helpers
{
    /// <summary>
    /// Brings an <see cref="AvaloniaList{T}"/> in line with a new list of items through removes and
    /// inserts, so the containers of the items that stay are kept (with their expanded state,
    /// selection and template instances).
    /// </summary>
    /// <remarks>
    /// Items are matched by identity: objects by reference, strings and values by
    /// <see cref="object.Equals(object)"/>. Of the matched items, the largest set already in the
    /// right order stays; the others are removed and inserted again, as Avalonia rebuilds the
    /// container of a moved item anyway.
    /// </remarks>
    internal static class ItemsReconciler
    {
        public static void Sync<T>(AvaloniaList<T> target, IReadOnlyList<T>? source)
        {
            source ??= Array.Empty<T>();

            var oldCount = target.Count;
            var newCount = source.Count;

            var start = 0;
            while (start < oldCount && start < newCount && ItemIdentity.Same(target[start], source[start]))
                start++;

            var oldEnd = oldCount;
            var newEnd = newCount;
            while (
                oldEnd > start
                && newEnd > start
                && ItemIdentity.Same(target[oldEnd - 1], source[newEnd - 1])
            )
            {
                oldEnd--;
                newEnd--;
            }

            var oldLength = oldEnd - start;
            var newLength = newEnd - start;

            if (oldLength == 0 && newLength == 0)
                return;

            if (oldLength == 0)
            {
                target.InsertRange(start, Slice(source, start, newLength));
                return;
            }

            if (newLength == 0)
            {
                target.RemoveRange(start, oldLength);
                return;
            }

            var keepOld = new bool[oldLength];
            var keepNew = new bool[newLength];
            KeepInOrder(target, source, start, oldLength, newLength, keepOld, keepNew);

            for (var i = oldLength - 1; i >= 0; )
            {
                if (keepOld[i])
                {
                    i--;
                    continue;
                }

                var end = i;
                while (i >= 0 && !keepOld[i])
                    i--;
                target.RemoveRange(start + i + 1, end - i);
            }

            for (var j = 0; j < newLength; )
            {
                if (keepNew[j])
                {
                    j++;
                    continue;
                }

                var first = j;
                while (j < newLength && !keepNew[j])
                    j++;
                target.InsertRange(start + first, Slice(source, start + first, j - first));
            }
        }

        /// <summary>
        /// Marks the items of the changed middle part that can stay: each new item is matched to an
        /// old one (duplicates in turn), and of those the longest run in increasing old order stays.
        /// </summary>
        private static void KeepInOrder<T>(
            AvaloniaList<T> target,
            IReadOnlyList<T> source,
            int start,
            int oldLength,
            int newLength,
            bool[] keepOld,
            bool[] keepNew
        )
        {
            var oldIndices = new Dictionary<ItemKey<T>, Queue<int>>(oldLength, ItemKeyComparer<T>.Instance);
            for (var i = 0; i < oldLength; i++)
            {
                var key = new ItemKey<T>(target[start + i]);
                if (!oldIndices.TryGetValue(key, out var queue))
                    oldIndices[key] = queue = new Queue<int>();
                queue.Enqueue(i);
            }

            var matchedNew = new List<int>();
            var matchedOld = new List<int>();
            for (var j = 0; j < newLength; j++)
            {
                if (
                    oldIndices.TryGetValue(new ItemKey<T>(source[start + j]), out var queue)
                    && queue.Count > 0
                )
                {
                    matchedNew.Add(j);
                    matchedOld.Add(queue.Dequeue());
                }
            }

            foreach (var k in LongestIncreasingRun(matchedOld))
            {
                keepOld[matchedOld[k]] = true;
                keepNew[matchedNew[k]] = true;
            }
        }

        /// <summary>
        /// The positions of the longest strictly increasing subsequence of distinct values.
        /// </summary>
        private static IEnumerable<int> LongestIncreasingRun(List<int> values)
        {
            var count = values.Count;
            if (count == 0)
                return Array.Empty<int>();

            var previous = new int[count];
            var tails = new int[count];
            var length = 0;

            for (var k = 0; k < count; k++)
            {
                int low = 0,
                    high = length;
                while (low < high)
                {
                    var mid = (low + high) >> 1;
                    if (values[tails[mid]] < values[k])
                        low = mid + 1;
                    else
                        high = mid;
                }

                previous[k] = low > 0 ? tails[low - 1] : -1;
                tails[low] = k;
                if (low == length)
                    length++;
            }

            var result = new int[length];
            for (int k = tails[length - 1], n = length - 1; n >= 0; k = previous[k], n--)
                result[n] = k;
            return result;
        }

        private static List<T> Slice<T>(IReadOnlyList<T> source, int start, int count)
        {
            var slice = new List<T>(count);
            for (var i = 0; i < count; i++)
                slice.Add(source[start + i]);
            return slice;
        }

        private readonly struct ItemKey<T>(T value)
        {
            public readonly T Value = value;
        }

        private sealed class ItemKeyComparer<T> : IEqualityComparer<ItemKey<T>>
        {
            public static readonly ItemKeyComparer<T> Instance = new();

            public bool Equals(ItemKey<T> x, ItemKey<T> y) => ItemIdentity.Same(x.Value, y.Value);

            public int GetHashCode(ItemKey<T> key) => ItemIdentity.Hash(key.Value);
        }
    }

    /// <summary>
    /// When two items are the same item: objects by reference, strings and values by
    /// <see cref="object.Equals(object)"/>.
    /// </summary>
    internal static class ItemIdentity
    {
        public static bool Same<T>(T a, T b)
        {
            if (typeof(T).IsValueType)
                return EqualityComparer<T>.Default.Equals(a, b);

            object? x = a,
                y = b;
            if (ReferenceEquals(x, y))
                return true;
            if (x is null || y is null)
                return false;
            return ByValue(x) && x.Equals(y);
        }

        public static int Hash<T>(T value)
        {
            if (typeof(T).IsValueType)
                return EqualityComparer<T>.Default.GetHashCode(value!);

            object? x = value;
            if (x is null)
                return 0;
            return ByValue(x) ? x.GetHashCode() : RuntimeHelpers.GetHashCode(x);
        }

        private static bool ByValue(object x) => x is string || x.GetType().IsValueType;
    }
}
