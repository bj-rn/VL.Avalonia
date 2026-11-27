using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using VL.Avalonia.Controls;
using VL.Core;
using VL.Lib.Reactive;

namespace VL.Avalonia.Tests;

// A tree can hold items that aren't TreeViewItems, so the selected item can be a value the
// SelectedItem channel can't take.
public class SelectedItemChannelTests
{
    private static readonly NodeContext NoContext = null!;

    private static (Channel<TreeViewItem> Channel, TreeViewItem Initial) TreeViewItemChannel()
    {
        var channel = (Channel<TreeViewItem>)ChannelHelpers.CreateChannelOfType<TreeViewItem>();
        var initial = new TreeViewItem();
        channel.Value = initial;
        return (channel, initial);
    }

    // VL connects a Channel<TreeViewItem> to the object pin of the plain TreeView node.
    [AvaloniaFact]
    public void Plain_object_selection_is_dropped_by_a_narrower_channel_on_an_object_pin()
    {
        var node = new TreeViewNode(NoContext);
        var (channel, initial) = TreeViewItemChannel();
        node.SetSelectedItemChannel((IChannel<object?>)channel);

        node.Output.SelectedItem = new object();
        Assert.Same(initial, channel.Value);

        var next = new TreeViewItem();
        channel.Value = next;
        Assert.Same(next, node.Output.SelectedItem);

        node.Dispose();
    }

    [AvaloniaFact]
    public void Plain_object_selection_is_dropped_by_a_typed_tree_view()
    {
        var node = new TreeViewNode<TreeViewItem>(NoContext);
        var (channel, initial) = TreeViewItemChannel();
        node.SetSelectedItemChannel(channel!);

        node.Output.SelectedItem = new object();
        Assert.Same(initial, channel.Value);

        var item = new TreeViewItem();
        node.Output.SelectedItem = item;
        Assert.Same(item, channel.Value);

        node.Dispose();
    }
}
