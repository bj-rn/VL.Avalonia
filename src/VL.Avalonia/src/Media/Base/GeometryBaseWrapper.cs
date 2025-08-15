using Avalonia.Media;
using VL.Core.Import;

namespace VL.Avalonia.Media;

/// <summary>
/// Avalonia geometry base wrapper
/// <see href="https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Base/Media/Geometry.cs">Geometry</see>
/// </summary>
[ProcessNode]
public abstract partial class GeometryWrapperBase<T> where T : Geometry, new()
{
    protected T _output = new();
    public T Output => _output;

    /* TODO: SEEMS TO NOT WORK CORRECTLY
    protected Optional<Matrix> _transform;
    public void SetTransform([Pin(Visibility = Model.PinVisibility.Optional)] Optional<Matrix> transform)
    {
        if (_transform != transform)
        {
            if (transform.HasValue)
            {
                var t = new MatrixTransform(transform.Value.ToAvaloniaMatrix());
                _output.SetValue(Geometry.TransformProperty, t);
            }
            else
            {
                _output.ClearValue(Geometry.TransformProperty);
            }

            _transform = transform;
        }
    }
    */
}
