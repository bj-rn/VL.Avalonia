using Avalonia.Controls;
using Avalonia.Media;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "PathIcon")]
public partial class PathIconWrapper : ControlWrapperBase<PathIcon>
{
    protected Optional<Geometry> _data;
    [Fragment(Order = -5)]
    public void SetData(Optional<Geometry> data)
    {
        if (_data != data)
        {
            _data = data;

            _output.SetValue(PathIcon.DataProperty, _data.Value);
        }
    }
}

