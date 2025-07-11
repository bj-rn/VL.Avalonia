using Avalonia.Media;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Media;

/// <summary>
/// Represents the geometry of an arbitrarily complex shape.
/// </summary>
[ProcessNode(Name = "StreamGeometry")]
public partial class StreamGeometryWrapper : GeometryWrapperBase<StreamGeometry>
{
    private Optional<string> _data;
    /// <param name="data">The string.</param>
    public void SetData(Optional<string> data)
    {
        if (_data != data)
        {
            if (data.HasValue)
            {
                _output = StreamGeometry.Parse(data.Value);
            }

            _data = data;
        }
    }
}
