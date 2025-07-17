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

    // TODO:
    //[ImplementProperty("Geometry.TransformProperty", PinVisibility = Model.PinVisibility.Optional)]
    //protected Optional<Transform> _transform;
}
