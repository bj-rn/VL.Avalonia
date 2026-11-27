using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Templates;
using Avalonia.Headless.XUnit;
using VL.Avalonia.Controls;
using VL.Core.PublicAPI;

namespace VL.Avalonia.Tests;

public class DataTemplateNodeTests
{
    // Stands in for the patch inside the region.
    private sealed class Inlay(Func<string, Control?> make) : IDataTemplateInlay<string>, IDisposable
    {
        public readonly List<string> Calls = new();
        public Control? Control;
        public int Disposed;

        public Control? Update(string input)
        {
            Calls.Add(input);
            var control = make(input);
            Control ??= control;
            return control;
        }

        public void Dispose() => Disposed++;
    }

    private static List<Inlay> WithPatch(IRegion<IDataTemplateInlay<string>> node, Func<string, Control?>? make = null)
    {
        var inlays = new List<Inlay>();
        node.SetPatchInlayFactory(() =>
        {
            var inlay = new Inlay(make ?? (s => new TextBlock { Text = s }));
            inlays.Add(inlay);
            return inlay;
        });
        return inlays;
    }

    // A logical tree needs a root, so the panel lives in a window.
    private static Panel ShowPanel()
    {
        var panel = new StackPanel();
        new Window { Content = panel }.Show();
        return panel;
    }

    // The headless test app has no theme, so give the ItemsControl a presenter to realize items.
    private static ItemsControl ShowItemsControl(IDataTemplate template, AvaloniaList<string> items)
    {
        var itemsControl = new ItemsControl
        {
            Template = new FuncControlTemplate<ItemsControl>((_, _) => new ItemsPresenter { Name = "PART_ItemsPresenter" }),
            ItemTemplate = template,
            ItemsSource = items,
        };
        var window = new Window { Content = itemsControl };
        window.Show();
        window.UpdateLayout();
        return itemsControl;
    }

    private static Control?[] Shown(ItemsControl itemsControl)
    {
        itemsControl.UpdateLayout();
        return itemsControl.GetRealizedContainers().Select(c => ((ContentPresenter)c).Child).ToArray();
    }

    [Fact]
    public void Matches_only_data_of_its_type()
    {
        var template = new DataTemplateNode<string>().Output;

        Assert.True(template.Match("a"));
        Assert.False(template.Match(null));
        Assert.False(template.Match(1));
    }

    [AvaloniaFact]
    public void Builds_nothing_without_a_patch()
    {
        Assert.Null(new DataTemplateNode<string>().Output.Build("a"));
    }

    [AvaloniaFact]
    public void Data_of_another_type_creates_no_patch_instance()
    {
        var node = new DataTemplateNode<string>();
        var inlays = WithPatch(node);

        Assert.Null(node.Output.Build(1));
        Assert.Empty(inlays);
    }

    [AvaloniaFact]
    public void Every_built_control_gets_its_own_patch_instance()
    {
        var node = new DataTemplateNode<string>();
        var inlays = WithPatch(node);

        var a = node.Output.Build("a");
        var b = node.Output.Build("b");

        Assert.Equal(2, inlays.Count);
        Assert.Equal(new[] { "a" }, inlays[0].Calls);
        Assert.Equal(new[] { "b" }, inlays[1].Calls);
        Assert.Same(inlays[0].Control, a);
        Assert.Same(inlays[1].Control, b);
    }

    [AvaloniaFact]
    public void Patch_returning_no_control_is_disposed_right_away()
    {
        var node = new DataTemplateNode<string>();
        var inlays = WithPatch(node, _ => null);

        Assert.Null(node.Output.Build("a"));
        Assert.Equal(1, inlays[0].Disposed);

        node.Dispose();
        Assert.Equal(1, inlays[0].Disposed);
    }

    [AvaloniaFact]
    public void Leaving_the_logical_tree_disposes_the_patch_once()
    {
        var node = new DataTemplateNode<string>();
        var inlays = WithPatch(node);
        var panel = ShowPanel();
        var control = node.Output.Build("a")!;
        panel.Children.Add(control);
        Assert.Equal(0, inlays[0].Disposed);

        panel.Children.Remove(control);
        Assert.Equal(1, inlays[0].Disposed);

        node.Dispose();
        Assert.Equal(1, inlays[0].Disposed);
    }

    [AvaloniaFact]
    public void Dispose_disposes_every_live_patch_once()
    {
        var node = new DataTemplateNode<string>();
        var inlays = WithPatch(node);
        var panel = ShowPanel();
        var a = node.Output.Build("a")!;
        var b = node.Output.Build("b")!;
        panel.Children.Add(a);
        panel.Children.Add(b);
        panel.Children.Remove(a);

        node.Dispose();

        Assert.All(inlays, i => Assert.Equal(1, i.Disposed));
    }

    [AvaloniaFact]
    public void Invalidate_hands_out_a_new_template_and_the_old_controls_get_rebuilt()
    {
        var node = new DataTemplateNode<string>();
        var inlays = WithPatch(node);
        var host = new ContentPresenter { ContentTemplate = node.Output, Content = "a" };
        new Window { Content = host }.Show();
        host.UpdateLayout();
        var before = node.Output;

        node.Update(invalidate: false);
        Assert.Same(before, node.Output);

        node.Update(invalidate: true);
        Assert.NotSame(before, node.Output);
        host.ContentTemplate = node.Output;
        host.UpdateLayout();

        Assert.Equal(2, inlays.Count);
        Assert.Equal(1, inlays[0].Disposed);
        Assert.Equal(0, inlays[1].Disposed);
        Assert.Same(inlays[1].Control, host.Child);
    }

    [AvaloniaFact]
    public void Experimental_node_runs_every_live_patch_again_each_frame()
    {
        var node = new DataTemplateExperimentalNode<string>();
        var inlays = WithPatch(node);
        var panel = ShowPanel();
        var a = node.Output.Build("a")!;
        var b = node.Output.Build("b")!;
        panel.Children.Add(a);
        panel.Children.Add(b);
        panel.Children.Remove(b);

        node.Update();
        node.Update();

        Assert.Equal(new[] { "a", "a", "a" }, inlays[0].Calls);
        Assert.Equal(new[] { "b" }, inlays[1].Calls);
    }

    [AvaloniaFact]
    public void Experimental_node_keeps_the_first_control()
    {
        var node = new DataTemplateExperimentalNode<string>();
        var inlays = WithPatch(node);
        var items = new AvaloniaList<string> { "a" };
        var itemsControl = ShowItemsControl(node.Output, items);

        node.Update();

        Assert.Same(inlays[0].Control, Shown(itemsControl).Single());
    }

    [AvaloniaFact]
    public void ItemsControl_removing_an_item_disposes_only_its_patch()
    {
        var node = new DataTemplateNode<string>();
        var inlays = WithPatch(node);
        var items = new AvaloniaList<string> { "a", "b", "c" };
        var itemsControl = ShowItemsControl(node.Output, items);
        Assert.Equal(3, inlays.Count);

        items.Remove("b");
        Shown(itemsControl);

        Assert.Equal(new[] { 0, 1, 0 }, inlays.Select(i => i.Disposed));
    }

    [AvaloniaFact]
    public void ItemsControl_every_shown_control_keeps_a_live_patch_when_items_move()
    {
        var node = new DataTemplateNode<string>();
        var inlays = WithPatch(node);
        var items = new AvaloniaList<string> { "a", "b", "c" };
        var itemsControl = ShowItemsControl(node.Output, items);

        items.Move(0, 2);

        Assert.All(Shown(itemsControl), shown =>
            Assert.Equal(0, inlays.Single(i => ReferenceEquals(i.Control, shown)).Disposed));
    }

    [AvaloniaFact]
    public void ItemsControl_disposing_the_node_disposes_every_patch()
    {
        var node = new DataTemplateNode<string>();
        var inlays = WithPatch(node);
        ShowItemsControl(node.Output, new AvaloniaList<string> { "a", "b" });

        node.Dispose();

        Assert.All(inlays, i => Assert.Equal(1, i.Disposed));
    }
}
