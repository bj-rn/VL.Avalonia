using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Headless.XUnit;
using VL.Avalonia.Data;
using VL.Lib.Reactive;

namespace VL.Avalonia.Tests;

public class BindingInitialValueTests
{
    private readonly TextBox _textBox = new() { Text = "control" };
    private readonly IChannel<string> _channel = ChannelHelpers.CreateChannelOfType<string>();

    public BindingInitialValueTests()
    {
        _channel.Value = "channel";
    }

    private IDisposable Bind(BindingMode mode, SupprotedBindingInitialValueHandling handling) =>
        BindingHelpers.Bind(_textBox, TextBox.TextProperty, _channel, mode, initialValueHandling: handling);

    [AvaloniaTheory]
    [InlineData(BindingMode.OneWay)]
    [InlineData(BindingMode.TwoWay)]
    [InlineData(BindingMode.OneTime)]
    [InlineData(BindingMode.OneWayToSource)]
    public void None_copies_neither_value(BindingMode mode)
    {
        using var binding = Bind(mode, SupprotedBindingInitialValueHandling.None);

        Assert.Equal("control", _textBox.Text);
        Assert.Equal("channel", _channel.Value);
    }

    [AvaloniaTheory]
    [InlineData(BindingMode.OneWay)]
    [InlineData(BindingMode.TwoWay)]
    public void None_still_forwards_later_channel_changes(BindingMode mode)
    {
        using var binding = Bind(mode, SupprotedBindingInitialValueHandling.None);

        _channel.Value = "next";

        Assert.Equal("next", _textBox.Text);
    }

    [AvaloniaTheory]
    [InlineData(BindingMode.TwoWay)]
    [InlineData(BindingMode.OneWayToSource)]
    public void None_still_forwards_later_control_changes(BindingMode mode)
    {
        using var binding = Bind(mode, SupprotedBindingInitialValueHandling.None);

        _textBox.Text = "typed";

        Assert.Equal("typed", _channel.Value);
    }

    [AvaloniaTheory]
    [InlineData(BindingMode.OneWay)]
    [InlineData(BindingMode.TwoWay)]
    [InlineData(BindingMode.OneTime)]
    public void Channel_to_control_copies_the_channel_value(BindingMode mode)
    {
        using var binding = Bind(mode, SupprotedBindingInitialValueHandling.ChannelToControl);

        Assert.Equal("channel", _textBox.Text);
        Assert.Equal("channel", _channel.Value);
    }

    [AvaloniaTheory]
    [InlineData(BindingMode.OneWay)]
    [InlineData(BindingMode.TwoWay)]
    [InlineData(BindingMode.OneTime)]
    [InlineData(BindingMode.OneWayToSource)]
    public void Control_to_channel_copies_the_control_value(BindingMode mode)
    {
        using var binding = Bind(mode, SupprotedBindingInitialValueHandling.ControlToChannel);

        Assert.Equal("control", _textBox.Text);
        Assert.Equal("control", _channel.Value);
    }
}
