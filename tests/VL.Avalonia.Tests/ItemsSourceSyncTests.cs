using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Headless.XUnit;
using Avalonia.Threading;
using VL.Avalonia.Helpers;
using VL.Lib.Reactive;

namespace VL.Avalonia.Tests;

public class ItemsSourceSyncTests
{
    private sealed class Item(string name)
    {
        public string Name { get; } = name;

        public override string ToString() => Name;
    }

    private sealed class Host : AvaloniaObject
    {
        public static readonly StyledProperty<IEnumerable?> ItemsProperty =
            AvaloniaProperty.Register<Host, IEnumerable?>("Items");

        public IEnumerable? Items => GetValue(ItemsProperty);
    }

    private readonly Host _host = new();
    private readonly ItemsSourceSync<Item> _sync;
    private readonly Item _a = new("a");
    private readonly Item _b = new("b");
    private readonly Item _c = new("c");

    public ItemsSourceSyncTests()
    {
        _sync = new ItemsSourceSync<Item>(_host, Host.ItemsProperty);
    }

    private static IChannel<IReadOnlyList<Item>> NewChannel() =>
        ChannelHelpers.CreateChannelOfType<IReadOnlyList<Item>>();

    private AvaloniaList<Item> AssignedList(params Item[] expected)
    {
        var list = Assert.IsType<AvaloniaList<Item>>(_host.Items);
        Assert.Equal(expected, list);
        return list;
    }

    /// <summary>
    /// Runs on a new thread; unlike Task.Run(...).Wait() it can't be inlined on the UI thread.
    /// </summary>
    private static void OnOtherThread(Action action)
    {
        var thread = new Thread(() => action());
        thread.Start();
        thread.Join();
    }

    private static List<NotifyCollectionChangedEventArgs> Record(AvaloniaList<Item> list)
    {
        var events = new List<NotifyCollectionChangedEventArgs>();
        list.CollectionChanged += (_, e) => events.Add(e);
        return events;
    }

    [AvaloniaFact]
    public void Items_are_handed_on_through_one_persistent_list()
    {
        _sync.SetItems(new[] { _a, _b });
        var list = AssignedList(_a, _b);
        var events = Record(list);

        _sync.SetItems(new[] { _a, _c, _b });

        Assert.Same(list, AssignedList(_a, _c, _b));
        var e = Assert.Single(events);
        Assert.Equal(NotifyCollectionChangedAction.Add, e.Action);
    }

    [AvaloniaFact]
    public void Same_items_instance_is_ignored()
    {
        var items = new List<Item> { _a };
        _sync.SetItems(items);
        var events = Record(AssignedList(_a));

        items.Add(_b);
        _sync.SetItems(items);

        Assert.Empty(events);
        AssignedList(_a);
    }

    [AvaloniaFact]
    public void Null_items_clear_the_property_and_later_items_assign_it_again()
    {
        _sync.SetItems(new[] { _a });

        _sync.SetItems(null);
        Assert.False(_host.IsSet(Host.ItemsProperty));

        _sync.SetItems(new[] { _b });
        AssignedList(_b);
    }

    [AvaloniaFact]
    public void Channel_value_is_applied_on_set_and_on_change()
    {
        var channel = NewChannel();
        channel.Value = new[] { _a };

        _sync.SetChannel(channel);
        var list = AssignedList(_a);

        channel.Value = new[] { _a, _b };
        Assert.Same(list, AssignedList(_a, _b));
    }

    [AvaloniaFact]
    public void Null_channel_clears_the_property()
    {
        var channel = NewChannel();
        channel.Value = new[] { _a };
        _sync.SetChannel(channel);

        _sync.SetChannel(null);

        Assert.False(_host.IsSet(Host.ItemsProperty));
    }

    [AvaloniaFact]
    public void Null_channel_value_clears_the_property()
    {
        var channel = NewChannel();
        channel.Value = new[] { _a };
        _sync.SetChannel(channel);

        channel.Value = null!;

        Assert.Null(_host.Items);
    }

    [AvaloniaFact]
    public void Previous_channel_is_ignored_after_a_switch()
    {
        var first = NewChannel();
        first.Value = new[] { _a };
        var second = NewChannel();
        second.Value = new[] { _b };
        _sync.SetChannel(first);
        _sync.SetChannel(second);

        first.Value = new[] { _c };

        AssignedList(_b);
    }

    [AvaloniaFact]
    public void Items_after_a_channel_stop_its_updates()
    {
        var channel = NewChannel();
        channel.Value = new[] { _a };
        _sync.SetChannel(channel);

        _sync.SetItems(new[] { _b });
        channel.Value = new[] { _c };

        AssignedList(_b);
    }

    [AvaloniaFact]
    public void Collection_that_reports_its_changes_is_handed_on_as_is()
    {
        var collection = new ObservableCollection<Item> { _a };

        _sync.SetItems(collection);
        Assert.Same(collection, _host.Items);

        _sync.SetItems(new[] { _b });
        AssignedList(_b);
    }

    [AvaloniaFact]
    public void New_items_take_the_property_back_after_an_outside_change()
    {
        _sync.SetItems(new[] { _a });
        _host.SetValue(Host.ItemsProperty, new[] { "other" });

        _sync.SetItems(new[] { _a, _b });

        AssignedList(_a, _b);
    }

    [AvaloniaFact]
    public void Off_thread_values_apply_on_the_ui_thread_and_only_the_latest()
    {
        var channel = NewChannel();
        channel.Value = new[] { _a };
        _sync.SetChannel(channel);
        var events = Record(AssignedList(_a));

        OnOtherThread(() =>
        {
            channel.Value = new[] { _a, _b };
            channel.Value = new[] { _a, _c };
        });

        AssignedList(_a);
        Dispatcher.UIThread.RunJobs();

        AssignedList(_a, _c);
        var e = Assert.Single(events);
        Assert.Same(_c, Assert.Single(e.NewItems!));
    }

    [AvaloniaFact]
    public void Off_thread_value_is_dropped_after_the_channel_is_unset()
    {
        var channel = NewChannel();
        channel.Value = new[] { _a };
        _sync.SetChannel(channel);

        OnOtherThread(() => channel.Value = new[] { _b });
        _sync.SetChannel(null);
        Dispatcher.UIThread.RunJobs();

        Assert.False(_host.IsSet(Host.ItemsProperty));
    }

    [AvaloniaFact]
    public void Off_thread_value_is_dropped_after_dispose()
    {
        var channel = NewChannel();
        channel.Value = new[] { _a };
        _sync.SetChannel(channel);

        OnOtherThread(() => channel.Value = new[] { _b });
        _sync.Dispose();
        Dispatcher.UIThread.RunJobs();

        AssignedList(_a);
    }

    [AvaloniaFact]
    public void Dispose_stops_channel_updates()
    {
        var channel = NewChannel();
        channel.Value = new[] { _a };
        _sync.SetChannel(channel);

        _sync.Dispose();
        channel.Value = new[] { _b };

        AssignedList(_a);
    }
}
