using Avalonia;
using Stride.Core.Mathematics;
using AMatrix = Avalonia.Matrix;
using Point = Avalonia.Point;
using SMatrix = Stride.Core.Mathematics.Matrix;

namespace VL.Avalonia;

public static partial class Extensions
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
    public static Point ToPoint(this Vector2 point) =>
        new Point(point.X, point.Y);

    /// <inheritdoc cref="ToPoint(Vector2)"/>
    public static Point? ToPoint(Vector2? point) =>
    point.HasValue ? point.Value.ToPoint() : null;

    /// <summary>
    /// Converts Avalonia.Rect to RectangleF
    /// </summary>
    public static RectangleF FromRect(this Rect rect) =>
        new RectangleF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);

    /// <inheritdoc cref="FromRect(Rect)"/>
    public static RectangleF? FromRect(Rect? rect) =>
    rect.HasValue ? rect.Value.FromRect() : null;

    /// <summary>
    /// Converts RectangleF to Avalonia.Rect
    /// </summary>
    public static Rect ToRect(this RectangleF rect) =>
           new Rect((double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height);

    /// <inheritdoc cref="ToRect(RectangleF)"/>
    public static Rect? ToRect(RectangleF? rect) =>
    rect.HasValue ? rect.Value.ToRect() : null;

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
      new AMatrix(
          matrix.M11, matrix.M12,
          matrix.M21, matrix.M22,
          matrix.M41, matrix.M42
      );

    public static SMatrix FromAvaloniaMatrix(this AMatrix matrix) =>
    new SMatrix(
        (float)matrix.M11, (float)matrix.M12, 0, 0,
        (float)matrix.M21, (float)matrix.M22, 0, 0,
        0, 0, 1, 0,
        (float)matrix.M31, (float)matrix.M32, 0, 1
    );
}
