using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using VL.Avalonia.Controls;
using VL.Core;
using Matrix = Stride.Core.Mathematics.Matrix;
using Vector3 = Stride.Core.Mathematics.Vector3;

namespace VL.Avalonia.Tests;

public class RenderTransformTests
{
    private sealed class VisualNode : VisualNodeBase<Border> { }

    private readonly VisualNode _node = new();

    private Point Map(Matrix transform, Point point)
    {
        _node.SetRenderTransform(new Optional<Matrix>(transform));
        return point.Transform(_node.Output.RenderTransform!.Value);
    }

    private static Point StrideMap(Matrix transform, Point point)
    {
        var p = Vector3.TransformCoordinate(new Vector3((float)point.X, (float)point.Y, 0), transform);
        return new Point(p.X, p.Y);
    }

    private static void AssertClose(Point expected, Point actual)
    {
        Assert.Equal(expected.X, actual.X, 4);
        Assert.Equal(expected.Y, actual.Y, 4);
    }

    [AvaloniaTheory]
    [InlineData(0.0)]
    [InlineData(0.25)]
    [InlineData(-0.1)]
    [InlineData(0.4)]
    public void Rotation_is_applied_by_its_angle(double cycles)
    {
        var transform = Matrix.RotationZ((float)(cycles * Math.PI * 2));

        AssertClose(StrideMap(transform, new Point(1, 0)), Map(transform, new Point(1, 0)));
    }

    [AvaloniaFact]
    public void Scale_rotation_and_translation_map_points_like_the_matrix()
    {
        var transform =
            Matrix.Scaling(2, 3, 1) * Matrix.RotationZ(0.3f) * Matrix.Translation(10, -5, 0);

        foreach (var point in new[] { new Point(0, 0), new Point(1, 0), new Point(0, 1), new Point(4, -2) })
            AssertClose(StrideMap(transform, point), Map(transform, point));
    }

    [AvaloniaFact]
    public void Unset_transform_clears_the_property()
    {
        _node.SetRenderTransform(new Optional<Matrix>(Matrix.RotationZ(0.5f)));

        _node.SetRenderTransform(default);

        Assert.False(_node.Output.IsSet(Visual.RenderTransformProperty));
    }
}
