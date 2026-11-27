using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Labs.Panels;
using VL.Avalonia.Controls;
using VL.Core;

namespace VL.Avalonia.Tests;

// Flex.SetGrow/SetShrink reject NaN and negatives, and FlexBasis rejects non-finite or negative values,
// so every pin value has to be sanitized before it reaches Avalonia.Labs.
public class FlexValueHelperTests
{
    public static TheoryData<float> EdgeValues() => new()
    {
        float.NaN, float.PositiveInfinity, float.NegativeInfinity, -1f, -0.001f, -0f, 0f, 0.5f, 100f, 150f,
    };

    [Theory]
    [InlineData(float.NaN, 1.0)]
    [InlineData(-1f, 1.0)]
    [InlineData(-0.001f, 1.0)]
    [InlineData(float.NegativeInfinity, 1.0)]
    [InlineData(0f, 0.0)]
    [InlineData(2.5f, 2.5)]
    [InlineData(float.PositiveInfinity, double.PositiveInfinity)]
    public void ToFactor_replaces_NaN_and_negatives_with_the_fallback(float value, double expected)
    {
        Assert.Equal(expected, FlexValueHelper.ToFactor(value, 1.0));
    }

    [Theory]
    [InlineData(float.NaN, FlexBasisKind.Absolute)]
    [InlineData(float.PositiveInfinity, FlexBasisKind.Absolute)]
    [InlineData(float.NegativeInfinity, FlexBasisKind.Relative)]
    [InlineData(-5f, FlexBasisKind.Relative)]
    public void ToBasis_clamps_invalid_values_to_zero_and_keeps_the_kind(float value, FlexBasisKind kind)
    {
        var basis = FlexValueHelper.ToBasis(value, kind);

        Assert.Equal(0.0, basis.Value);
        Assert.Equal(kind, basis.Kind);
    }

    [Theory]
    [InlineData(40f, FlexBasisKind.Absolute)]
    [InlineData(50f, FlexBasisKind.Relative)]
    public void ToBasis_passes_valid_values_through(float value, FlexBasisKind kind)
    {
        var basis = FlexValueHelper.ToBasis(value, kind);

        Assert.Equal(value, basis.Value);
        Assert.Equal(kind, basis.Kind);
    }

    [Theory]
    [InlineData(float.NaN)]
    [InlineData(40f)]
    public void ToBasis_with_Auto_kind_ignores_the_value(float value)
    {
        Assert.Equal(FlexBasisKind.Auto, FlexValueHelper.ToBasis(value, FlexBasisKind.Auto).Kind);
        Assert.Equal(FlexBasisKind.Auto, FlexValueHelper.ToBasis(new Optional<float>(value), FlexBasisKind.Auto)!.Value.Kind);
    }

    [Fact]
    public void ToBasis_with_Auto_kind_and_no_value_is_Auto()
    {
        Assert.Equal(FlexBasisKind.Auto, FlexValueHelper.ToBasis(default(Optional<float>), FlexBasisKind.Auto)!.Value.Kind);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ToBasis_without_a_value_is_null_so_the_property_gets_cleared(bool withAbsoluteKind)
    {
        var kind = withAbsoluteKind ? new Optional<FlexBasisKind>(FlexBasisKind.Absolute) : default;

        Assert.Null(FlexValueHelper.ToBasis(default, kind));
    }

    [Fact]
    public void ToBasis_without_a_kind_defaults_to_Absolute()
    {
        var basis = FlexValueHelper.ToBasis(new Optional<float>(30f), default);

        Assert.Equal(FlexBasisKind.Absolute, basis!.Value.Kind);
        Assert.Equal(30.0, basis.Value.Value);
    }

    [AvaloniaTheory]
    [MemberData(nameof(EdgeValues))]
    public void Grow_node_accepts_any_float(float value)
    {
        var control = new Border();
        var node = new FlexPanelGrowProperty();
        node.SetInput(control);

        node.SetGrow(value);

        Assert.Equal(FlexValueHelper.ToFactor(value, FlexValueHelper.DefaultGrow), Flex.GetGrow(control));
    }

    [AvaloniaTheory]
    [MemberData(nameof(EdgeValues))]
    public void Shrink_node_accepts_any_float(float value)
    {
        var control = new Border();
        var node = new FlexPanelShrinkProperty();
        node.SetInput(control);

        node.SetShrink(value);

        Assert.Equal(FlexValueHelper.ToFactor(value, FlexValueHelper.DefaultShrink), Flex.GetShrink(control));
    }

    [AvaloniaTheory]
    [MemberData(nameof(EdgeValues))]
    public void Basis_node_accepts_any_float_for_every_kind(float value)
    {
        foreach (var kind in Enum.GetValues<FlexBasisKind>())
        {
            var control = new Border();
            var node = new FlexPanelBasisProperty();
            node.SetInput(control);

            node.SetBasis(value, kind);

            Assert.Equal(FlexValueHelper.ToBasis(value, kind), Flex.GetBasis(control));
        }
    }

    [AvaloniaFact]
    public void Disconnecting_the_basis_value_clears_it()
    {
        var control = new Border();
        var node = new FlexPanelBasisProperty();
        node.SetInput(control);
        node.SetBasis(40f, default);

        node.SetBasis(default, default);

        Assert.False(control.IsSet(Flex.BasisProperty));
    }
}
