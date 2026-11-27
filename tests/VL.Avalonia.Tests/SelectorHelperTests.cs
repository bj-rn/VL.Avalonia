using Avalonia.Controls;
using Avalonia.Data.Core;
using VL.Avalonia.Helpers;
using VL.Avalonia.Styles;

namespace VL.Avalonia.Tests;

// SelectorHelper reflects into Avalonia's internal SelectorParser, so these tests also act as a canary for Avalonia upgrades.
public class SelectorHelperTests
{
    [Theory]
    [InlineData("Button")]
    [InlineData("Button.primary:pointerover")]
    [InlineData("Border > TextBlock#title")]
    [InlineData("ListBoxItem:selected /template/ ContentPresenter")]
    [InlineData("ToggleButton:checked")]
    [InlineData("TemplatedControl")]
    [InlineData("Rectangle")]
    [InlineData("StackPanel TextBlock, WrapPanel TextBlock")]
    public void Parses_standard_selectors(string selector)
    {
        Assert.Equal(selector, SelectorHelper.ParseSelector(selector)?.ToString());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Null_or_empty_gives_no_selector(string? selector)
    {
        Assert.Null(SelectorHelper.ParseSelector(selector));
    }

    [Fact]
    public void Invalid_syntax_throws_the_parser_exception_not_a_reflection_wrapper()
    {
        var e = Assert.Throws<ExpressionParseException>(() => SelectorHelper.ParseSelector("Button["));

        Assert.Contains("end of selector", e.Message);
    }

    [Fact]
    public void Unknown_type_throws_with_the_type_name()
    {
        var e = Assert.Throws<InvalidOperationException>(() => SelectorHelper.ParseSelector("NonExistentControl"));

        Assert.Contains("NonExistentControl", e.Message);
    }

    [Fact]
    public void A_per_call_resolver_is_used_and_the_default_comes_back_afterwards()
    {
        var custom = SelectorHelper.ParseSelector("Fancy", (_, name) => name == "Fancy" ? typeof(Border) : typeof(object));

        Assert.Equal("Border", custom?.ToString());
        Assert.Throws<InvalidOperationException>(() => SelectorHelper.ParseSelector("Fancy"));
        Assert.Equal("Button", SelectorHelper.ParseSelector("Button")?.ToString());
    }

    [Fact]
    public void StyleSelector_keeps_the_last_valid_selector_while_the_string_is_invalid()
    {
        var node = new StyleSelector();
        node.SetSelector("Button.primary");

        Assert.Throws<ExpressionParseException>(() => node.SetSelector("Button["));
        Assert.Equal("Button.primary", ((ImmutableSelector)node.Output).Selector?.ToString());

        node.SetSelector("TextBlock");
        Assert.Equal("TextBlock", ((ImmutableSelector)node.Output).Selector?.ToString());
    }
}
