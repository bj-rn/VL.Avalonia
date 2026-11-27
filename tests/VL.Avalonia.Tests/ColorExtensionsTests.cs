using Stride.Core.Mathematics;
using VL.Avalonia.Extensions;
using Color = Avalonia.Media.Color;

namespace VL.Avalonia.Tests;

public class ColorExtensionsTests
{
    [Fact]
    public void Nullable_color_with_a_value_converts_to_that_color()
    {
        Color? color = Color.FromArgb(255, 255, 0, 255);

        Assert.Equal(new Color4(1f, 0f, 1f, 1f), color.ToColor());
    }

    [Fact]
    public void Nullable_color_without_a_value_converts_to_white()
    {
        Color? color = null;

        Assert.Equal(Color4.White, color.ToColor());
    }

    [Fact]
    public void Nullable_color_converts_like_the_color_itself()
    {
        var color = Color.FromArgb(128, 10, 20, 30);

        Assert.Equal(color.ToColor(), ((Color?)color).ToColor());
    }
}
