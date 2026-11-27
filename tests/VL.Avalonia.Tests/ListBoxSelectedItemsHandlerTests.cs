using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using VL.Avalonia.Controls;

namespace VL.Avalonia.Tests;

public class ListBoxSelectedItemsHandlerTests
{
    private static ListBox ListBox(params int[] selected)
    {
        var listBox = new ListBox
        {
            ItemsSource = new object[] { "A", "B", "C", 42 },
            SelectionMode = SelectionMode.Multiple,
        };

        foreach (var index in selected)
            listBox.Selection.Select(index);

        return listBox;
    }

    [Fact]
    public void Handler_is_disposable_so_vl_releases_it()
    {
        Assert.IsAssignableFrom<IDisposable>(new ListBoxSelectedItemsHandler<string>());
    }

    [AvaloniaFact]
    public void Selection_changes_update_the_output()
    {
        var listBox = ListBox();
        var handler = new ListBoxSelectedItemsHandler<string>();
        handler.SetInput(listBox);

        listBox.Selection.Select(0);
        listBox.Selection.Select(2);
        listBox.Selection.Deselect(0);

        Assert.Equal(new[] { "C" }, handler.Output);
        handler.Dispose();
    }

    [AvaloniaFact]
    public void Items_of_other_types_are_filtered_out()
    {
        var listBox = ListBox();
        var handler = new ListBoxSelectedItemsHandler<string>();
        handler.SetInput(listBox);

        listBox.Selection.Select(1);
        listBox.Selection.Select(3);

        Assert.Equal(new[] { "B" }, handler.Output);
        handler.Dispose();
    }

    [AvaloniaFact]
    public void Existing_selection_shows_up_when_input_is_set()
    {
        var handler = new ListBoxSelectedItemsHandler<string>();

        handler.SetInput(ListBox(0, 1));

        Assert.Equal(new[] { "A", "B" }, handler.Output);
        handler.Dispose();
    }

    [AvaloniaFact]
    public void Removing_the_input_clears_the_output()
    {
        var listBox = ListBox();
        var handler = new ListBoxSelectedItemsHandler<string>();
        handler.SetInput(listBox);
        listBox.Selection.Select(0);

        handler.SetInput(null);

        Assert.Empty(handler.Output);
        handler.Dispose();
    }

    [AvaloniaFact]
    public void Switching_input_shows_the_new_controls_selection()
    {
        var first = ListBox();
        var handler = new ListBoxSelectedItemsHandler<string>();
        handler.SetInput(first);
        first.Selection.Select(0);

        handler.SetInput(ListBox(2));

        Assert.Equal(new[] { "C" }, handler.Output);
        handler.Dispose();
    }

    [AvaloniaFact]
    public void Previous_input_no_longer_affects_the_output()
    {
        var first = ListBox();
        var handler = new ListBoxSelectedItemsHandler<string>();
        handler.SetInput(first);
        handler.SetInput(ListBox(1));

        first.Selection.Select(0);

        Assert.Equal(new[] { "B" }, handler.Output);
        handler.Dispose();
    }
}
