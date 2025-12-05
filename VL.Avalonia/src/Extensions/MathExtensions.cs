using Avalonia;
using Stride.Core.Mathematics;
using AMatrix = Avalonia.Matrix;
using Point = Avalonia.Point;
using SMatrix = Stride.Core.Mathematics.Matrix;

namespace VL.Avalonia.Extensions;

public static partial class MathExtensions
{
    /// <summary>
    /// Converts Avalonia Point to Vector2
    /// </summary>
    public static Vector2 FromPoint(this Point point) =>
        new Vector2((float)point.X, (float)point.Y);

    /// <inheritdoc cref="FromPoint(Point)"/>
    public static Vector2? FromPoint(Point? point) =>
        point.HasValue ? point.Value.FromPoint() : null;

    /// <summary>
    /// Converts Vector2 to Avalonia.Point
    /// </summary>
    public static Point ToPoint(this Vector2 point) => new Point(point.X, point.Y);

    /// <inheritdoc cref="ToPoint(Vector2)"/>
    public static Point? ToPoint(Vector2? point) => point.HasValue ? point.Value.ToPoint() : null;

    /// <summary>
    /// Converts Avalonia.RelativePoint to Vector2
    /// </summary>
    public static Vector2 FromRelativePoint(this RelativePoint relativePoint) =>
        relativePoint.Point.FromPoint();

    /// <inheritdoc cref="FromRelativePoint(RelativePoint)"/>
    public static Vector2? FromRelativePoint(RelativePoint? relativePoint) =>
        relativePoint.HasValue ? relativePoint.Value.FromRelativePoint() : null;

    /// <summary>
    /// Converts Vector2 to Avalonia.RelativePoint
    /// </summary>
    public static RelativePoint ToRelativePoint(
        this Vector2 vector,
        RelativeUnit relativeUnit = RelativeUnit.Relative
    ) => new RelativePoint(vector.ToPoint(), relativeUnit);

    /// <inheritdoc cref="ToRelativePoint(Vector2, RelativeUnit)"/>
    public static RelativePoint? ToRelativePoint(
        Vector2? vector,
        RelativeUnit relativeUnit = RelativeUnit.Relative
    ) => vector.HasValue ? vector.Value.ToRelativePoint(relativeUnit) : null;

    /// <summary>
    /// Converts Avalonia.RelativeScalar to float
    /// </summary>
    public static float FromRelativeScalar(this RelativeScalar relativeScalar) =>
        (float)relativeScalar.Scalar;

    /// <inheritdoc cref="FromRelativeScalar(RelativeScalar)"/>
    public static float? FromRelativeScalar(RelativeScalar? relativeScalar) =>
        relativeScalar.HasValue ? relativeScalar.Value.FromRelativeScalar() : null;

    /// <summary>
    /// Converts float to Avalonia.RelativeScalar
    /// </summary>
    public static RelativeScalar ToRelativeScalar(
        this float value,
        RelativeUnit relativeUnit = RelativeUnit.Relative
    ) => new RelativeScalar((double)value, relativeUnit);

    /// <inheritdoc cref="ToRelativeScalar(float, RelativeUnit)"/>
    public static RelativeScalar? ToRelativeScalar(
        float? value,
        RelativeUnit relativeUnit = RelativeUnit.Relative
    ) => value.HasValue ? value.Value.ToRelativeScalar(relativeUnit) : null;

    /// <summary>
    /// Converts RectangleF to Avalonia.RelativeRect
    /// </summary>
    public static RelativeRect ToRelativeRect(
        this RectangleF rectangle,
        RelativeUnit relativeUnit = RelativeUnit.Relative
    ) => new RelativeRect(rectangle.ToRect(), relativeUnit);

    /// <inheritdoc cref="ToRelativeRect(RectangleF, RelativeUnit)"/>
    public static RelativeRect? ToRelativeRect(
        RectangleF? rectangle,
        RelativeUnit relativeUnit = RelativeUnit.Relative
    ) => rectangle.HasValue ? rectangle.Value.ToRelativeRect(relativeUnit) : null;

    /// <summary>
    /// Converts Avalonia.Rect to RectangleF
    /// </summary>
    public static RectangleF FromRect(this Rect rect) =>
        new RectangleF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);

    /// <inheritdoc cref="FromRect(Rect)"/>
    public static RectangleF? FromRect(Rect? rect) => rect.HasValue ? rect.Value.FromRect() : null;

    public static RectangleF FromRectDIP(this Rect rect, float dip = 0.01f) =>
        new RectangleF(
            (float)rect.X * dip,
            (float)rect.Y * dip,
            (float)rect.Width * dip,
            (float)rect.Height * dip
        );

    public static RectangleF? FromRectDIP(Rect? rect, float dip = 0.01f) =>
        rect.HasValue ? rect.Value.FromRectDIP(dip) : null;

    /// <summary>
    /// Converts RectangleF to Avalonia.Rect
    /// </summary>
    public static Rect ToRect(this RectangleF rect) =>
        new Rect((double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height);

    /// <inheritdoc cref="ToRect(RectangleF)"/>
    public static Rect? ToRect(RectangleF? rect) => rect.HasValue ? rect.Value.ToRect() : null;

    /// <summary>
    /// Converts Avalonia.Vector to Vector2
    /// </summary>
    public static Vector2 FromVector(this Vector vector) =>
        new Vector2((float)vector.X, (float)vector.Y);

    /// <inheritdoc cref="FromVector(Vector)"/>
    public static Vector2? FromVector(Vector? vector) =>
        vector.HasValue ? vector.Value.FromVector() : null;

    /// <summary>
    /// Converts Vector2 to Avalonia.Vector
    /// </summary>
    public static Vector ToVector(this Vector2 vector) =>
        new Vector((double)vector.X, (double)vector.Y);

    /// <inheritdoc cref="ToVector(Vector2)"/>
    public static Vector? ToVector(Vector2? vector) =>
        vector.HasValue ? vector.Value.ToVector() : null;

    public static AMatrix ToAvaloniaMatrix(this SMatrix matrix) =>
        new AMatrix(matrix.M11, matrix.M12, matrix.M21, matrix.M22, matrix.M41, matrix.M42);

    public static SMatrix FromAvaloniaMatrix(this AMatrix matrix) =>
        new SMatrix(
            (float)matrix.M11,
            (float)matrix.M12,
            0,
            0,
            (float)matrix.M21,
            (float)matrix.M22,
            0,
            0,
            0,
            0,
            1,
            0,
            (float)matrix.M31,
            (float)matrix.M32,
            0,
            1
        );

    public static Vector2 ToVector(this Size size) =>
        new Vector2((float)size.Width, (float)size.Height);
}
