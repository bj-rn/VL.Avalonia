using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Media;

/// <summary>
///  Represents a composite geometry, composed of other Geometry objects.
/// </summary>
[ProcessNode(Name = "GeometryGroup (Spectral)")]
public partial class GeometryGroupSpectralWrapper : GeometryWrapperBase<GeometryGroup>
{
    #region Core Properties

    protected Spread<Geometry> _children;
    /// <param name="children">
    /// Geometry instances
    /// </param>
    public virtual void SetChildren(Spread<Geometry> children)
    {
        if (_children != children)
        {
            if (children != null)
            {
                var group = new GeometryCollection(children);

                _output.SetValue(GeometryGroup.ChildrenProperty, group);
            }
            else
            {
                _output.ClearValue(GeometryGroup.ChildrenProperty);
            }

            _children = children;
        }
    }

    [ImplementProperty("GeometryGroup.FillRuleProperty")]
    protected Optional<FillRule> _fillRule;

    #endregion
}

/// <inheritdoc cref="GeometryGroupSpectralWrapper"/>
[ProcessNode(Name = "GeometryGroup")]
public partial class GeometryGroupWrapper : GeometryGroupSpectralWrapper
{
    /// <param name="children">
    /// Geometry instances
    /// </param>
    public override void SetChildren([Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<Geometry> children)
    {
        base.SetChildren(children);
    }
}
