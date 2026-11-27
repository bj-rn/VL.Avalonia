using System.Collections.Specialized;
using Avalonia.Collections;
using VL.Avalonia.Helpers;

namespace VL.Avalonia.Tests;

public class ItemsReconcilerTests
{
    private sealed class Item(string name)
    {
        public string Name { get; } = name;

        public override string ToString() => Name;
    }

    private sealed record Rec(int X);

    /// <summary>
    /// Runs the sync and checks that the target ends up as the source (by identity) and that
    /// replaying the raised events onto the old items gives the same result.
    /// </summary>
    private static List<NotifyCollectionChangedEventArgs> Sync<T>(
        AvaloniaList<T> target,
        IReadOnlyList<T>? source
    )
    {
        var before = target.ToList();
        var events = new List<NotifyCollectionChangedEventArgs>();
        void Record(object? sender, NotifyCollectionChangedEventArgs e) => events.Add(e);

        target.CollectionChanged += Record;
        try
        {
            ItemsReconciler.Sync(target, source);
        }
        finally
        {
            target.CollectionChanged -= Record;
        }

        var expected = source ?? Array.Empty<T>();
        AssertSameItems(expected, target);
        AssertSameItems(expected, Replay(before, events));
        return events;
    }

    private static List<T> Replay<T>(List<T> items, List<NotifyCollectionChangedEventArgs> events)
    {
        var result = items.ToList();
        foreach (var e in events)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Assert.True(e.NewStartingIndex >= 0 && e.NewStartingIndex <= result.Count);
                    result.InsertRange(e.NewStartingIndex, e.NewItems!.Cast<T>());
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var removed = e.OldItems!.Cast<T>().ToList();
                    AssertSameItems(removed, result.GetRange(e.OldStartingIndex, removed.Count));
                    result.RemoveRange(e.OldStartingIndex, removed.Count);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    result.Clear();
                    break;
                default:
                    Assert.Fail($"Unexpected {e.Action} event: it rebuilds the containers.");
                    break;
            }
        }
        return result;
    }

    private static void AssertSameItems<T>(IReadOnlyList<T> expected, IReadOnlyList<T> actual)
    {
        Assert.Equal(expected.Count, actual.Count);
        for (var i = 0; i < expected.Count; i++)
            Assert.True(ItemIdentity.Same(expected[i], actual[i]), $"Item {i} differs.");
    }

    private static int RemovedCount(List<NotifyCollectionChangedEventArgs> events) =>
        events
            .Where(e => e.Action == NotifyCollectionChangedAction.Remove)
            .Sum(e => e.OldItems!.Count);

    private static Item[] Items(int count) =>
        Enumerable.Range(0, count).Select(i => new Item(((char)('a' + i % 26)).ToString() + i)).ToArray();

    [Fact]
    public void Same_reference_raises_no_events()
    {
        var items = Items(3);
        var list = new AvaloniaList<Item>(items);

        Assert.Empty(Sync(list, items));
    }

    [Fact]
    public void Equal_content_in_a_new_list_raises_no_events()
    {
        var items = Items(3);
        var list = new AvaloniaList<Item>(items);

        Assert.Empty(Sync(list, items.ToList()));
    }

    [Fact]
    public void Fill_from_empty_raises_one_add()
    {
        var list = new AvaloniaList<Item>();

        var events = Sync(list, Items(4));

        var e = Assert.Single(events);
        Assert.Equal(NotifyCollectionChangedAction.Add, e.Action);
        Assert.Equal(4, e.NewItems!.Count);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(4)]
    public void Insert_of_a_range_raises_one_add_at_its_index(int index)
    {
        var items = Items(4);
        var list = new AvaloniaList<Item>(items);
        var inserted = Items(2);
        var next = items.ToList();
        next.InsertRange(index, inserted);

        var events = Sync(list, next);

        var e = Assert.Single(events);
        Assert.Equal(NotifyCollectionChangedAction.Add, e.Action);
        Assert.Equal(index, e.NewStartingIndex);
        Assert.Equal(inserted, e.NewItems!.Cast<Item>());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    public void Removal_of_a_range_raises_one_remove_at_its_index(int index)
    {
        var items = Items(5);
        var list = new AvaloniaList<Item>(items);
        var next = items.ToList();
        next.RemoveRange(index, 2);

        var events = Sync(list, next);

        var e = Assert.Single(events);
        Assert.Equal(NotifyCollectionChangedAction.Remove, e.Action);
        Assert.Equal(index, e.OldStartingIndex);
        Assert.Equal(2, e.OldItems!.Count);
    }

    [Fact]
    public void Replacing_an_item_raises_remove_and_add_and_keeps_the_others()
    {
        var items = Items(3);
        var list = new AvaloniaList<Item>(items);
        var replacement = new Item("x");

        var events = Sync(list, new[] { items[0], replacement, items[2] });

        Assert.Equal(
            new[] { NotifyCollectionChangedAction.Remove, NotifyCollectionChangedAction.Add },
            events.Select(e => e.Action)
        );
        Assert.Same(items[1], Assert.Single(events[0].OldItems!));
        Assert.Same(replacement, Assert.Single(events[1].NewItems!));
    }

    [Fact]
    public void Swap_keeps_one_of_the_two_items()
    {
        var items = Items(2);
        var list = new AvaloniaList<Item>(items);

        var events = Sync(list, new[] { items[1], items[0] });

        Assert.Equal(1, RemovedCount(events));
    }

    [Fact]
    public void Reverse_keeps_one_item()
    {
        var items = Items(6);
        var list = new AvaloniaList<Item>(items);

        var events = Sync(list, items.Reverse().ToArray());

        Assert.Equal(5, RemovedCount(events));
    }

    [Fact]
    public void Move_of_one_item_removes_only_that_item()
    {
        var items = Items(6);
        var list = new AvaloniaList<Item>(items);

        var events = Sync(list, new[] { items[1], items[2], items[3], items[4], items[0], items[5] });

        Assert.Equal(1, RemovedCount(events));
        Assert.Same(items[0], Assert.Single(events[0].OldItems!));
    }

    [Fact]
    public void Null_source_clears()
    {
        var list = new AvaloniaList<Item>(Items(3));

        var events = Sync(list, null);

        Assert.Empty(list);
        Assert.Equal(3, RemovedCount(events));
    }

    [Fact]
    public void Duplicates_are_matched_in_turn()
    {
        var a = new Item("a");
        var b = new Item("b");
        var list = new AvaloniaList<Item>(new[] { a, b, a, b });

        var events = Sync(list, new[] { a, a, b });

        Assert.Equal(1, RemovedCount(events));
    }

    [Fact]
    public void Null_items_are_matched()
    {
        var a = new Item("a");
        var list = new AvaloniaList<Item?>(new[] { null, a, null });

        var events = Sync(list, new Item?[] { a, null, null });

        Assert.Equal(1, RemovedCount(events));
    }

    [Fact]
    public void Equal_strings_of_other_instances_raise_no_events()
    {
        var list = new AvaloniaList<string>(new[] { "alpha", "beta" });

        var events = Sync(list, new[] { new string("alpha".ToCharArray()), new string("beta".ToCharArray()) });

        Assert.Empty(events);
    }

    [Fact]
    public void Equal_boxed_values_raise_no_events()
    {
        var list = new AvaloniaList<object>(new object[] { 1, 2.5f, "x" });

        Assert.Empty(Sync(list, new object[] { 1, 2.5f, "x" }));
    }

    [Fact]
    public void Equal_records_of_other_instances_are_replaced()
    {
        var list = new AvaloniaList<Rec>(new[] { new Rec(1), new Rec(2) });

        var events = Sync(list, new[] { new Rec(1), new Rec(2) });

        Assert.Equal(2, RemovedCount(events));
    }

    [Fact]
    public void Value_type_items_are_matched_by_value()
    {
        var list = new AvaloniaList<int>(new[] { 1, 2, 3, 4 });

        var events = Sync(list, new[] { 1, 3, 4, 5 });

        Assert.Equal(1, RemovedCount(events));
    }

    [Fact]
    public void Random_changes_of_unique_items_remove_only_what_is_needed()
    {
        var random = new Random(1234);
        var pool = Items(40);
        var list = new AvaloniaList<Item>();

        for (var round = 0; round < 500; round++)
        {
            var next = Mutate(random, list.ToList(), pool, allowDuplicates: false);
            var before = list.ToList();

            var events = Sync(list, next);

            Assert.Equal(before.Count - CommonSubsequenceLength(before, next), RemovedCount(events));
        }
    }

    [Fact]
    public void Random_changes_with_duplicates_and_nulls_stay_consistent()
    {
        var random = new Random(4321);
        var pool = Items(6).Append(null).ToArray();
        var list = new AvaloniaList<Item?>();

        for (var round = 0; round < 500; round++)
            Sync(list, Mutate(random, list.ToList(), pool, allowDuplicates: true));
    }

    private static List<T> Mutate<T>(Random random, List<T> items, T[] pool, bool allowDuplicates)
    {
        var unused = allowDuplicates ? pool.ToList() : pool.Except(items).ToList();
        var edits = random.Next(1, 5);
        for (var e = 0; e < edits; e++)
        {
            switch (random.Next(4))
            {
                case 0 when unused.Count > 0:
                    var add = unused[random.Next(unused.Count)];
                    if (!allowDuplicates)
                        unused.Remove(add);
                    items.Insert(random.Next(items.Count + 1), add);
                    break;
                case 1 when items.Count > 0:
                    var at = random.Next(items.Count);
                    if (!allowDuplicates)
                        unused.Add(items[at]);
                    items.RemoveAt(at);
                    break;
                case 2 when items.Count > 1:
                    var from = random.Next(items.Count);
                    var moved = items[from];
                    items.RemoveAt(from);
                    items.Insert(random.Next(items.Count + 1), moved);
                    break;
                case 3 when random.Next(10) == 0:
                    if (!allowDuplicates)
                        unused.AddRange(items);
                    items.Clear();
                    break;
            }
        }
        return items;
    }

    private static int CommonSubsequenceLength<T>(List<T> a, List<T> b)
    {
        var lengths = new int[a.Count + 1, b.Count + 1];
        for (var i = 1; i <= a.Count; i++)
        for (var j = 1; j <= b.Count; j++)
            lengths[i, j] = ItemIdentity.Same(a[i - 1], b[j - 1])
                ? lengths[i - 1, j - 1] + 1
                : Math.Max(lengths[i - 1, j], lengths[i, j - 1]);
        return lengths[a.Count, b.Count];
    }
}
