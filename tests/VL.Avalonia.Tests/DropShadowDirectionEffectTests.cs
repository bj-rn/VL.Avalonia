using Avalonia.Headless.XUnit;
using Avalonia.Media;
using VL.Avalonia.Media.Effects;
using VL.Core;

namespace VL.Avalonia.Tests;

public class DropShadowDirectionEffectTests
{
    private readonly DropShadowDirectionEffectWrapper _node = new();

    private bool IsSet => _node.Output.IsSet(DropShadowDirectionEffect.DirectionProperty);

    private double Direction => _node.Output.Direction;

    [AvaloniaFact]
    public void First_direction_is_set_in_degrees()
    {
        _node.SetDirection(new Optional<float>(0.25f));

        Assert.True(IsSet);
        Assert.Equal(90.0, Direction, 6);
    }

    [AvaloniaFact]
    public void Changed_direction_is_set()
    {
        _node.SetDirection(new Optional<float>(0.25f));

        _node.SetDirection(new Optional<float>(0.5f));

        Assert.Equal(180.0, Direction, 6);
    }

    [AvaloniaFact]
    public void Unset_direction_clears_the_property()
    {
        _node.SetDirection(new Optional<float>(0.25f));

        _node.SetDirection(default);

        Assert.False(IsSet);
    }

    [AvaloniaFact]
    public void Direction_after_an_unset_is_set_again()
    {
        _node.SetDirection(new Optional<float>(0.25f));
        _node.SetDirection(default);

        _node.SetDirection(new Optional<float>(0.75f));

        Assert.Equal(270.0, Direction, 6);
    }
}
