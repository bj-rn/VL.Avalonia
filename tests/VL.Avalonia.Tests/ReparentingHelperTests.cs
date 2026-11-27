using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Templates;
using Avalonia.Headless.XUnit;
using VL.Avalonia.Helpers;
using VL.Lib.Collections;

namespace VL.Avalonia.Tests;

public class ReparentingHelperTests
{
    // The headless test app has no theme, so give the ItemsControl a presenter to realize items.
    private static ItemsControl ShowItemsControl(Action<ItemsControl> fill)
    {
        var itemsControl = new ItemsControl
        {
            Template = new FuncControlTemplate<ItemsControl>(
                (_, _) => new ItemsPresenter { Name = "PART_ItemsPresenter" }),
        };
        fill(itemsControl);
        var window = new Window { Content = itemsControl };
        window.Show();
        window.UpdateLayout();
        return itemsControl;
    }

    [AvaloniaFact]
    public void Item_of_ItemsSource_is_removed_from_the_source()
    {
        var control = new Border();
        var source = new AvaloniaList<Control> { control };
        var itemsControl = ShowItemsControl(ic => ic.ItemsSource = source);
        Assert.Same(itemsControl, control.Parent);

        Assert.True(ReparentingHelper.TryDetach(control));

        Assert.Null(control.Parent);
        Assert.Empty(source);
        new StackPanel().Children.Add(control);
    }

    [AvaloniaFact]
    public void Item_fed_by_ItemsSourceSync_can_be_reparented()
    {
        var control = new Border();
        var itemsControl = ShowItemsControl(ic =>
            new ItemsSourceSync<Control>(ic, ItemsControl.ItemsSourceProperty)
                .SetItems(Spread.Create<Control>(control)));
        itemsControl.UpdateLayout();
        Assert.Same(itemsControl, control.Parent);

        Assert.True(ReparentingHelper.TryDetach(control));

        Assert.Null(control.Parent);
        Assert.Equal(0, itemsControl.ItemCount);
    }

    [AvaloniaFact]
    public void Read_only_ItemsSource_is_left_alone()
    {
        var control = new Border();
        var itemsControl = ShowItemsControl(ic => ic.ItemsSource = new Control[] { control });

        Assert.False(ReparentingHelper.TryDetach(control));

        Assert.Same(itemsControl, control.Parent);
    }

    [AvaloniaFact]
    public void Direct_item_is_removed_from_Items()
    {
        var control = new Border();
        var itemsControl = ShowItemsControl(ic => ic.Items.Add(control));

        Assert.True(ReparentingHelper.TryDetach(control));

        Assert.Null(control.Parent);
        Assert.Equal(0, itemsControl.ItemCount);
    }

    [AvaloniaFact]
    public void Panel_child_is_removed()
    {
        var control = new Border();
        var panel = new StackPanel { Children = { control } };

        Assert.True(ReparentingHelper.TryDetach(control));

        Assert.Null(control.Parent);
        Assert.Empty(panel.Children);
    }

    [AvaloniaFact]
    public void Unparented_control_is_not_detached()
    {
        Assert.False(ReparentingHelper.TryDetach(new Border()));
    }
}
