using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Styling;
using VL.Avalonia.Styles;
using VL.Core;

namespace VL.Avalonia.Tests;

public class StyleSettersTests
{
    public static TheoryData<Type> Setters()
    {
        var data = new TheoryData<Type>();
        foreach (
            var type in typeof(StyleSetterBase<>)
                .Assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition && IsStyleSetter(t))
                .OrderBy(t => t.FullName)
        )
            data.Add(type);
        return data;
    }

    private static bool IsStyleSetter(Type type)
    {
        for (var t = type.BaseType; t is not null; t = t.BaseType)
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(StyleSetterBase<>))
                return true;
        return false;
    }

    [Fact]
    public void Style_setters_are_found()
    {
        Assert.True(Setters().Count > 100);
    }

    [Theory]
    [MemberData(nameof(Setters))]
    public void Every_style_setter_has_a_value_input(Type setter)
    {
        Assert.NotNull(setter.GetMethod("SetValue"));
    }

    private static object? BuiltValue(IAvaloniaStyle output, StyledElement owner, AvaloniaProperty property)
    {
        var style = output.BuildStyle(owner, new Style());
        var setter = Assert.Single(style.Setters.OfType<Setter>(), s => s.Property == property);
        return setter.Value;
    }

    [AvaloniaFact]
    public void SetZIndex_sets_the_z_index()
    {
        var node = new SetZIndex();
        node.SetValue(new Optional<int>(5));

        Assert.Equal(5, BuiltValue(node.Output, new Border(), Visual.ZIndexProperty));
    }

    [AvaloniaFact]
    public void SetRenderTransformOrigin_sets_the_origin()
    {
        var origin = new RelativePoint(0.25, 0.75, RelativeUnit.Relative);
        var node = new SetRenderTransformOrigin();
        node.SetValue(new Optional<RelativePoint>(origin));

        Assert.Equal(origin, BuiltValue(node.Output, new Border(), Visual.RenderTransformOriginProperty));
    }

    [AvaloniaFact]
    public void SetBorderThickness_sets_the_thickness()
    {
        var thickness = new Thickness(1, 2, 3, 4);
        var node = new SetBorderThickness();
        node.SetValue(new Optional<Thickness>(thickness));

        Assert.Equal(thickness, BuiltValue(node.Output, new Border(), Border.BorderThicknessProperty));
    }
}
