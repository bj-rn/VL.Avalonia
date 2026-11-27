using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Headless.XUnit;
using VL.Avalonia.Data;
using VL.Lib.Reactive;

namespace VL.Avalonia.Tests;

// The binding paths the initial-value, converter and name tests don't reach.
public class BindingEngineTests
{
    private readonly TextBox _textBox = new() { Text = "control" };
    private readonly IChannel<string> _channel = ChannelHelpers.CreateChannelOfType<string>();

    public BindingEngineTests()
    {
        _channel.Value = "channel";
    }

    private int CountChannelValues(Action action)
    {
        var count = 0;
        using (_channel.Subscribe(_ => count++))
            action();
        return count;
    }

    private int CountTextChanges(Action action)
    {
        var count = 0;
        void OnChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property == TextBox.TextProperty)
                count++;
        }
        _textBox.PropertyChanged += OnChanged;
        action();
        _textBox.PropertyChanged -= OnChanged;
        return count;
    }

    [AvaloniaFact]
    public void Default_mode_is_two_way_for_TextBox_Text()
    {
        using var binding = BindingHelpers.Bind(_textBox, TextBox.TextProperty, _channel, BindingMode.Default);

        _textBox.Text = "typed";

        Assert.Equal("typed", _channel.Value);
    }

    [AvaloniaFact]
    public void Default_mode_is_one_way_for_TextBlock_Text()
    {
        var textBlock = new TextBlock();
        using var binding = BindingHelpers.Bind(textBlock, TextBlock.TextProperty, _channel, BindingMode.Default);
        Assert.Equal("channel", textBlock.Text);

        textBlock.Text = "typed";

        Assert.Equal("channel", _channel.Value);
    }

    [AvaloniaFact]
    public void One_way_does_not_write_control_changes_to_the_channel()
    {
        using var binding = BindingHelpers.Bind(_textBox, TextBox.TextProperty, _channel, BindingMode.OneWay);

        _textBox.Text = "typed";

        Assert.Equal("channel", _channel.Value);
    }

    [AvaloniaFact]
    public void One_way_to_source_does_not_write_channel_changes_to_the_control()
    {
        using var binding = BindingHelpers.Bind(_textBox, TextBox.TextProperty, _channel, BindingMode.OneWayToSource);
        Assert.Equal("control", _channel.Value);

        _channel.Value = "next";

        Assert.Equal("control", _textBox.Text);
    }

    [AvaloniaFact]
    public void Two_way_change_from_either_side_arrives_once_without_echo()
    {
        using var binding = BindingHelpers.Bind(_textBox, TextBox.TextProperty, _channel, BindingMode.TwoWay);

        Assert.Equal(1, CountTextChanges(() => Assert.Equal(1, CountChannelValues(() => _channel.Value = "next"))));
        Assert.Equal("next", _textBox.Text);

        Assert.Equal(1, CountChannelValues(() => Assert.Equal(1, CountTextChanges(() => _textBox.Text = "typed"))));
        Assert.Equal("typed", _channel.Value);
    }

    // A channel re-publishes a value that is set again, e.g. every frame by a patch.
    [AvaloniaFact]
    public void Republishing_the_same_channel_value_converts_it_once()
    {
        var conversions = 0;
        using var binding = BindingHelpers.Bind<string, string>(
            _textBox, TextBox.TextProperty, _channel, BindingMode.OneWay,
            toProperty: v => { conversions++; return v; });

        _channel.Value = "channel";
        _channel.Value = "channel";

        Assert.Equal(1, conversions);
    }

    [AvaloniaFact]
    public void Throwing_converter_does_not_tear_down_the_binding()
    {
        var channel = ChannelHelpers.CreateChannelOfType<int>();
        using var binding = BindingHelpers.Bind<int, string>(
            _textBox, TextBox.TextProperty, channel, BindingMode.OneWay,
            toProperty: v => v < 0 ? throw new InvalidOperationException("negative") : v.ToString());

        channel.Value = -1;
        channel.Value = 5;

        Assert.Equal("5", _textBox.Text);
    }

    [AvaloniaFact]
    public void Failing_reverse_conversion_leaves_the_channel_and_keeps_the_binding()
    {
        var channel = ChannelHelpers.CreateChannelOfType<int>();
        channel.Value = 1;
        using var binding = BindingHelpers.Bind<int, string>(
            _textBox, TextBox.TextProperty, channel, BindingMode.TwoWay,
            toProperty: v => v.ToString(),
            toValue: s => int.Parse(s!));

        _textBox.Text = "not a number";
        Assert.Equal(1, channel.Value);

        _textBox.Text = "7";
        Assert.Equal(7, channel.Value);
    }

    [AvaloniaFact]
    public void A_local_value_wins_over_a_style_priority_binding()
    {
        using var binding = BindingHelpers.Bind(
            _textBox, TextBox.TextProperty, _channel, BindingMode.OneWay, priority: BindingPriority.Style);

        _textBox.Text = "local";
        _channel.Value = "next";
        Assert.Equal("local", _textBox.Text);

        _textBox.ClearValue(TextBox.TextProperty);
        Assert.Equal("next", _textBox.Text);
    }

    // Avalonia resets the property when its binding is removed; that reset must not reach the channel.
    [AvaloniaTheory]
    [InlineData(BindingMode.OneWay)]
    [InlineData(BindingMode.TwoWay)]
    public void Disposing_leaves_the_channel_value(BindingMode mode)
    {
        BindingHelpers.Bind(_textBox, TextBox.TextProperty, _channel, mode).Dispose();

        Assert.Equal("channel", _channel.Value);
    }

    [AvaloniaFact]
    public void Disposing_stops_both_directions()
    {
        var binding = BindingHelpers.Bind(_textBox, TextBox.TextProperty, _channel, BindingMode.TwoWay);

        binding.Dispose();
        _textBox.Text = "typed";
        Assert.Equal("channel", _channel.Value);

        _channel.Value = "next";
        Assert.Equal("typed", _textBox.Text);
    }

    [AvaloniaFact]
    public void Missing_target_property_or_channel_is_a_no_op()
    {
        BindingHelpers.Bind(null, TextBox.TextProperty, _channel).Dispose();
        BindingHelpers.Bind(_textBox, (AvaloniaProperty?)null, _channel).Dispose();
        BindingHelpers.Bind<string>(_textBox, TextBox.TextProperty, null).Dispose();

        Assert.Equal("control", _textBox.Text);
        Assert.Equal("channel", _channel.Value);
    }
}
