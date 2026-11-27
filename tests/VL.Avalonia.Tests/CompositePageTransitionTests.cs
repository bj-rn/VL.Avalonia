using Avalonia.Animation;
using Avalonia.Headless.XUnit;
using VL.Avalonia.Animation.PageTransitions;
using VL.Lib.Collections;

namespace VL.Avalonia.Tests;

public class CompositePageTransitionTests
{
    private readonly CompositePageTransitionWrapper<CompositePageTransition> _node = new();

    [AvaloniaFact]
    public void Unchanged_spread_keeps_the_same_list()
    {
        var transitions = Spread.Create<IPageTransition>(new CrossFade());
        _node.SetPageTransitions(transitions);
        var first = _node.Output.PageTransitions;

        _node.SetPageTransitions(transitions);

        Assert.Same(first, _node.Output.PageTransitions);
    }

    [AvaloniaFact]
    public void New_spread_replaces_the_transitions()
    {
        var crossFade = new CrossFade();
        var slide = new PageSlide();
        _node.SetPageTransitions(Spread.Create<IPageTransition>(crossFade));

        _node.SetPageTransitions(Spread.Create<IPageTransition>(slide));

        Assert.Equal(new IPageTransition[] { slide }, _node.Output.PageTransitions);
    }

    [AvaloniaFact]
    public void Null_entries_are_skipped()
    {
        var crossFade = new CrossFade();

        _node.SetPageTransitions(Spread.Create<IPageTransition>(null!, crossFade, null!));

        Assert.Equal(new IPageTransition[] { crossFade }, _node.Output.PageTransitions);
    }

    [AvaloniaFact]
    public void Null_spread_clears_the_transitions()
    {
        _node.SetPageTransitions(Spread.Create<IPageTransition>(new CrossFade()));

        _node.SetPageTransitions(null!);

        Assert.Empty(_node.Output.PageTransitions);
    }
}
