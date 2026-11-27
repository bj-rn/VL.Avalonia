using System.Collections;
using System.Collections.Specialized;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Headless.XUnit;
using VL.Avalonia.Controls;
using VL.Core.PublicAPI;
using VL.Lib.Collections;

namespace VL.Avalonia.Tests;

public class TreeDataTemplateNodeTests
{
    // Stands in for the patch inside the region; Children is read on every call.
    private sealed class Inlay(TreeDataTemplateNodeTests test) : ITreeDataTemplateInlay<string, string>, IDisposable
    {
        public int Calls;
        public int Disposed;

        public Control? Update(string input, out IReadOnlyList<string>? children)
        {
            Calls++;
            children = test.Children;
            return new TextBlock { Text = input };
        }

        public void Dispose() => Disposed++;
    }

    private sealed class Recorder : IObserver<object?>
    {
        public readonly List<object?> Values = new();
        public void OnNext(object? value) => Values.Add(value);
        public void OnError(Exception error) { }
        public void OnCompleted() { }
        public IEnumerable Last => (IEnumerable)Values[^1]!;
    }

    private IReadOnlyList<string>? Children = new[] { "a", "b" }.ToSpread();
    private readonly List<Inlay> _inlays = new();
    private StackPanel? _host;

    // A logical tree needs a root, so built controls are shown in a window.
    private StackPanel Panel
    {
        get
        {
            if (_host is null)
                new Window { Content = _host = new StackPanel() }.Show();
            return _host;
        }
    }

    private TNode Create<TNode>() where TNode : IRegion<ITreeDataTemplateInlay<string, string>>, new()
    {
        var node = new TNode();
        node.SetPatchInlayFactory(() =>
        {
            var inlay = new Inlay(this);
            _inlays.Add(inlay);
            return inlay;
        });
        return node;
    }

    private Control Show(ITreeDataTemplate template, string item)
    {
        var control = template.Build(item)!;
        Panel.Children.Add(control);
        return control;
    }

    private static (Recorder, IDisposable) Subscribe(ITreeDataTemplate template, string item)
    {
        var recorder = new Recorder();
        return (recorder, template.ItemsSelector(item)!.Source!.Subscribe(recorder));
    }

    private static string[] ChildrenOf(ITreeDataTemplate template, string item)
    {
        var (recorder, subscription) = Subscribe(template, item);
        subscription.Dispose();
        return recorder.Last.Cast<string>().ToArray();
    }

    [Fact]
    public void Matches_only_data_of_its_type()
    {
        var template = new TreeDataTemplateNode<string, string>().Output;

        Assert.True(template.Match("a"));
        Assert.False(template.Match(null));
        Assert.False(template.Match(1));
        Assert.Null(template.ItemsSelector(1));
    }

    [AvaloniaFact]
    public void Children_reach_an_item_that_asked_before_its_control_was_built()
    {
        var node = Create<TreeDataTemplateNode<string, string>>();
        var (recorder, _) = Subscribe(node.Output, "root");
        Assert.Empty(recorder.Last);

        Show(node.Output, "root");

        Assert.Single(recorder.Values);
        Assert.Equal(new[] { "a", "b" }, recorder.Last.Cast<string>());
    }

    [AvaloniaFact]
    public void No_children_means_an_empty_list()
    {
        Children = null;
        var node = Create<TreeDataTemplateNode<string, string>>();
        Show(node.Output, "root");

        Assert.Empty(ChildrenOf(node.Output, "root"));
    }

    [AvaloniaFact]
    public void A_new_list_updates_the_shown_list_in_place()
    {
        var node = Create<TreeDataTemplateExperimentalNode<string, string>>();
        var (recorder, _) = Subscribe(node.Output, "root");
        Show(node.Output, "root");
        var shown = (INotifyCollectionChanged)recorder.Last;
        var changes = 0;
        shown.CollectionChanged += (_, _) => changes++;

        node.Update();
        Assert.Equal(0, changes);

        Children = new[] { "a", "c" }.ToSpread();
        node.Update();

        Assert.Single(recorder.Values);
        Assert.Equal(new[] { "a", "c" }, recorder.Last.Cast<string>());
        Assert.True(changes > 0);
    }

    [AvaloniaFact]
    public void A_list_that_reports_its_own_changes_is_handed_on_as_is()
    {
        var own = new AvaloniaList<string> { "x" };
        Children = own;
        var node = Create<TreeDataTemplateExperimentalNode<string, string>>();
        var (recorder, _) = Subscribe(node.Output, "root");
        Show(node.Output, "root");
        Assert.Same(own, recorder.Last);

        Children = new[] { "y" }.ToSpread();
        node.Update();

        Assert.NotSame(own, recorder.Last);
        Assert.Equal(new[] { "y" }, recorder.Last.Cast<string>());
    }

    [AvaloniaFact]
    public void Shown_children_do_not_find_an_item_of_another_type()
    {
        var node = Create<TreeDataTemplateNode<string, string>>();
        var (recorder, _) = Subscribe(node.Output, "root");
        Show(node.Output, "root");

        // A tree view asks every expanded children list for the clicked item.
        Assert.Equal(-1, ((IList)recorder.Last).IndexOf(new object()));
    }

    [AvaloniaFact]
    public void Children_are_kept_while_one_of_two_controls_for_the_item_is_shown()
    {
        var node = Create<TreeDataTemplateNode<string, string>>();
        var first = Show(node.Output, "root");
        var second = Show(node.Output, "root");

        Panel.Children.Remove(first);
        Assert.Equal(new[] { "a", "b" }, ChildrenOf(node.Output, "root"));

        Panel.Children.Remove(second);
        Assert.Empty(ChildrenOf(node.Output, "root"));
    }

    [AvaloniaFact]
    public void Children_are_kept_while_a_tree_item_still_listens()
    {
        var node = Create<TreeDataTemplateNode<string, string>>();
        var (_, subscription) = Subscribe(node.Output, "root");
        var control = Show(node.Output, "root");

        Panel.Children.Remove(control);
        Assert.Equal(new[] { "a", "b" }, ChildrenOf(node.Output, "root"));

        subscription.Dispose();
        Assert.Empty(ChildrenOf(node.Output, "root"));
    }

    [AvaloniaFact]
    public void Leaving_the_logical_tree_disposes_the_patch_once()
    {
        var node = Create<TreeDataTemplateNode<string, string>>();
        var control = Show(node.Output, "root");

        Panel.Children.Remove(control);
        node.Dispose();

        Assert.Equal(1, _inlays.Single().Disposed);
    }

    [AvaloniaFact]
    public void Dispose_disposes_every_live_patch_once()
    {
        var node = Create<TreeDataTemplateNode<string, string>>();
        Show(node.Output, "a");
        Show(node.Output, "b");

        node.Dispose();

        Assert.All(_inlays, i => Assert.Equal(1, i.Disposed));
    }

    [AvaloniaFact]
    public void Experimental_node_runs_only_the_live_patches_again()
    {
        var node = Create<TreeDataTemplateExperimentalNode<string, string>>();
        Show(node.Output, "a");
        var b = Show(node.Output, "b");
        Panel.Children.Remove(b);

        node.Update();

        Assert.Equal(new[] { 2, 1 }, _inlays.Select(i => i.Calls));
    }

    [AvaloniaFact]
    public void Invalidate_hands_out_a_new_template()
    {
        var node = Create<TreeDataTemplateNode<string, string>>();
        var before = node.Output;

        node.Update(invalidate: false);
        Assert.Same(before, node.Output);

        node.Update(invalidate: true);
        Assert.NotSame(before, node.Output);
    }
}
