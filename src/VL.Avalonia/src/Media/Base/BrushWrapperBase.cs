using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Media.Base;


/// https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Base/Media/Brush.cs
[ProcessNode]
public partial class BrushWrapperBase<T> where T : Brush, new()
{
    protected T _output = new();
    public T Output => _output;

    [ImplementProperty("Brush.OpacityProperty")]
    protected Optional<float> _opacity;

    /* TODO
    [ImplementProperty("Brush.TransformProperty")]
    protected Optional<Matrix> _opacity;
    */


}
