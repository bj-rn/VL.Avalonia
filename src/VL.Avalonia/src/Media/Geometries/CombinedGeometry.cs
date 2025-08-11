using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Media;

[ProcessNode(Name = "CombinedGeometry")]
public partial class CombinedGeometryWrapper : GeometryWrapperBase<CombinedGeometry>
{
    [ImplementProperty("CombinedGeometry.Geometry1Property", Order = PinOrder.Main)]
    protected Optional<Geometry> _geometry1;

    [ImplementProperty("CombinedGeometry.Geometry2Property", Order = PinOrder.Main)]
    protected Optional<Geometry> _geometry2;

    [ImplementProperty("CombinedGeometry.GeometryCombineModeProperty", Order = PinOrder.Secondary)]
    protected Optional<GeometryCombineMode> _geometryCombineMode;
}
