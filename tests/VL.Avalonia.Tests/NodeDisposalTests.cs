using System.Reactive;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Headless.XUnit;
using VL.Avalonia.Controls;
using VL.Core;
using VL.Lib.Reactive;

namespace VL.Avalonia.Tests;

public class NodeDisposalTests
{
    private sealed class CountingFlyout : FlyoutBase
    {
        public int Shown { get; private set; }

        public override void ShowAt(Control placementTarget) => Shown++;

        public override void Hide() { }
    }

    // Control nodes only store the context (ReparentingHelper reads it on an error path these tests
    // don't hit); a real one needs an AppHost.
    private static readonly NodeContext NoContext = null!;

    private static IChannel<bool> BoolChannel() => ChannelHelpers.CreateChannelOfType<bool>();

    [AvaloniaFact]
    public void Disposed_menu_item_releases_its_channel_bindings()
    {
        var node = new MenuItemNode(NoContext);
        var isSelected = BoolChannel();
        var isSubMenuOpen = BoolChannel();
        var isChecked = BoolChannel();
        node.SetIsSelectedChannel(isSelected);
        node.SetIsSubmenuOpenChannel(isSubMenuOpen);
        node.SetIsCheckedChannel(isChecked);

        node.Dispose();
        node.Output.IsSelected = true;
        node.Output.IsSubMenuOpen = true;
        node.Output.IsChecked = true;

        Assert.False(isSelected.Value);
        Assert.False(isSubMenuOpen.Value);
        Assert.False(isChecked.Value);
    }

    [AvaloniaFact]
    public void Disposed_menu_item_clears_its_command()
    {
        var node = new MenuItemNode(NoContext);
        node.SetCommandChannel(ChannelHelpers.CreateChannelOfType<Unit>());

        node.Dispose();

        Assert.Null(node.Output.Command);
    }

    [AvaloniaFact]
    public void Disposed_auto_complete_box_releases_its_drop_down_binding()
    {
        var node = new AutoCompleteBoxNode(NoContext);
        var isDropDownOpen = BoolChannel();
        node.SetIsDropDownOpenChannel(isDropDownOpen);

        node.Dispose();
        // Without a template the box closes itself right away, so watch for the property
        // changing at all rather than looking at its final value.
        var changes = 0;
        node.Output.PropertyChanged += (_, e) =>
        {
            if (e.Property == AutoCompleteBox.IsDropDownOpenProperty)
                changes++;
        };
        isDropDownOpen.Value = true;

        Assert.Equal(0, changes);
    }

    private static (AttachedFlyoutWrapper<Border> Node, CountingFlyout Flyout) AttachedFlyout()
    {
        var flyout = new CountingFlyout();
        var node = new AttachedFlyoutWrapper<Border>();
        node.SetInput(new Border());
        node.SetFlyout(flyout);
        return (node, flyout);
    }

    [AvaloniaFact]
    public void Attached_flyout_shows_on_push()
    {
        var (node, flyout) = AttachedFlyout();
        var show = ChannelHelpers.CreateChannelOfType<Unit>();
        node.SetShowAttachedFlyoutChannel(show);

        show.OnNext(Unit.Default);

        Assert.Equal(1, flyout.Shown);
    }

    [AvaloniaFact]
    public void Attached_flyout_ignores_a_replaced_channel()
    {
        var (node, flyout) = AttachedFlyout();
        var first = ChannelHelpers.CreateChannelOfType<Unit>();
        node.SetShowAttachedFlyoutChannel(first);
        node.SetShowAttachedFlyoutChannel(ChannelHelpers.CreateChannelOfType<Unit>());

        first.OnNext(Unit.Default);

        Assert.Equal(0, flyout.Shown);
    }

    [AvaloniaFact]
    public void Attached_flyout_ignores_its_channel_after_dispose()
    {
        var (node, flyout) = AttachedFlyout();
        var show = ChannelHelpers.CreateChannelOfType<Unit>();
        node.SetShowAttachedFlyoutChannel(show);

        ((IDisposable)node).Dispose();
        show.OnNext(Unit.Default);

        Assert.Equal(0, flyout.Shown);
    }
}
