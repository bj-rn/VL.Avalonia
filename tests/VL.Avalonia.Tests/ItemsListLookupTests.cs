using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Themes.Fluent;
using VL.Avalonia.Controls;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Lib.Collections;

namespace VL.Avalonia.Tests;

// Avalonia looks items up through IList with any object: a tree view asks every expanded
// items list for the clicked item. AvaloniaList<T> casts that object to T.
public class ItemsListLookupTests
{
    private static readonly NodeContext NoContext = null!;

    [AvaloniaFact]
    public void Tree_view_finds_a_plain_item_past_a_list_of_typed_tree_items()
    {
        var typed = new TreeViewItemNode<TreeViewItem>(NoContext);
        typed.SetItems(Spread.Create<TreeViewItem?>(new TreeViewItem { Header = "typed child" }));
        typed.Output.IsExpanded = true;

        var leaf = new object();
        var plain = new TreeViewItemNode(NoContext);
        plain.SetItems(Spread.Create<object?>(leaf));
        plain.Output.IsExpanded = true;

        var tree = new TreeViewNode(NoContext);
        tree.SetItems(Spread.Create<object?>(typed.Output, plain.Output));

        // A window is a root: its content is styled when set, so the theme has to come first.
        var window = new Window();
        window.Styles.Add(new FluentTheme());
        window.Content = tree.Output;
        window.Show();
        window.UpdateLayout();

        Assert.NotNull(tree.Output.TreeContainerFromItem(leaf));

        tree.Dispose();
        plain.Dispose();
        typed.Dispose();
    }

    [AvaloniaFact]
    public void Items_list_does_not_find_items_of_another_type()
    {
        System.Collections.IList list = new ItemsList<TreeViewItem> { new TreeViewItem() };

        Assert.Equal(-1, list.IndexOf(new object()));
        Assert.False(list.Contains(new object()));
        list.Remove(new object());
        Assert.Equal(1, list.Count);
    }

    [AvaloniaFact]
    public void Items_list_finds_items_and_null_like_an_avalonia_list()
    {
        var item = new TreeViewItem();
        System.Collections.IList list = new ItemsList<TreeViewItem?> { null, item };

        Assert.Equal(1, list.IndexOf(item));
        Assert.Equal(0, list.IndexOf(null));
        Assert.True(list.Contains(item));
        list.Remove(item);
        Assert.Equal(1, list.Count);
    }

    [AvaloniaFact]
    public void Items_list_of_values_does_not_find_null()
    {
        System.Collections.IList list = new ItemsList<int> { 0 };

        Assert.Equal(-1, list.IndexOf(null));
        Assert.False(list.Contains(null));
    }
}
