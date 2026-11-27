using System.Globalization;
using Avalonia;
using Avalonia.Headless.XUnit;
using VL.Avalonia.Data;
using VL.Lib.Reactive;

namespace VL.Avalonia.Tests;

public class DefaultBindingConverterTests
{
    private sealed class Target : AvaloniaObject
    {
        public static readonly StyledProperty<int?> NullableIntProperty =
            AvaloniaProperty.Register<Target, int?>("NullableInt");

        public static readonly StyledProperty<TimeSpan?> TimeProperty =
            AvaloniaProperty.Register<Target, TimeSpan?>("Time");

        public static readonly StyledProperty<double> NumberProperty =
            AvaloniaProperty.Register<Target, double>("Number");
    }

    private readonly Target _target = new();

    private static void WithCommaDecimalCulture(Action action)
    {
        var previous = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");
        try
        {
            action();
        }
        finally
        {
            CultureInfo.CurrentCulture = previous;
        }
    }

    [AvaloniaFact]
    public void Two_way_nullable_value_flows_both_ways()
    {
        var channel = ChannelHelpers.CreateChannelOfType<int?>();
        using var binding = new TwoWayBinding<int?>(_target, Target.NullableIntProperty);
        binding.Bind(channel);

        channel.Value = 5;
        Assert.Equal(5, _target.GetValue(Target.NullableIntProperty));

        _target.SetValue(Target.NullableIntProperty, 7);
        Assert.Equal(7, channel.Value);
    }

    [AvaloniaFact]
    public void Two_way_nullable_non_convertible_value_flows_both_ways()
    {
        var channel = ChannelHelpers.CreateChannelOfType<TimeSpan?>();
        using var binding = new TwoWayBinding<TimeSpan?>(_target, Target.TimeProperty);
        binding.Bind(channel);

        channel.Value = TimeSpan.FromMinutes(1);
        Assert.Equal(TimeSpan.FromMinutes(1), _target.GetValue(Target.TimeProperty));

        _target.SetValue(Target.TimeProperty, TimeSpan.FromMinutes(2));
        Assert.Equal(TimeSpan.FromMinutes(2), channel.Value);
    }

    [AvaloniaFact]
    public void One_way_nullable_value_reaches_the_channel()
    {
        var channel = ChannelHelpers.CreateChannelOfType<int?>();
        using var binding = new OneWayBinding<int?>(_target, Target.NullableIntProperty);
        binding.Bind(channel);

        _target.SetValue(Target.NullableIntProperty, 3);

        Assert.Equal(3, channel.Value);
    }

    [AvaloniaFact]
    public void String_to_number_ignores_the_current_culture()
    {
        WithCommaDecimalCulture(() =>
        {
            var channel = ChannelHelpers.CreateChannelOfType<string>();
            using var binding = new TwoWayBinding<string, double>(_target, Target.NumberProperty);
            binding.Bind(channel);

            channel.Value = "1.5";

            Assert.Equal(1.5, _target.GetValue(Target.NumberProperty));
        });
    }

    [AvaloniaFact]
    public void Number_to_string_ignores_the_current_culture()
    {
        WithCommaDecimalCulture(() =>
        {
            var channel = ChannelHelpers.CreateChannelOfType<string>();
            using var binding = new OneWayBinding<string, double>(_target, Target.NumberProperty);
            binding.Bind(channel);

            _target.SetValue(Target.NumberProperty, 1.5);

            Assert.Equal("1.5", channel.Value);
        });
    }

    [AvaloniaFact]
    public void Unconvertible_value_becomes_default_instead_of_throwing()
    {
        var channel = ChannelHelpers.CreateChannelOfType<string>();
        using var binding = new TwoWayBinding<string, double>(_target, Target.NumberProperty);
        binding.Bind(channel);
        _target.SetValue(Target.NumberProperty, 2.0);

        channel.Value = "abc";

        Assert.Equal(0.0, _target.GetValue(Target.NumberProperty));
    }
}
