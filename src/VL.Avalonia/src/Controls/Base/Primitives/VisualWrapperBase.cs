using Avalonia;
using VL.Avalonia.Attributes;
using VL.Core;

namespace VL.Avalonia.Controls.Primitives;

/// <summary>
/// Visual base wrapper, based on https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Base/Visual.cs 
/// Gonna comment few props for convenience, use Style Setters instead.
/// </summary>
/// <typeparam name="T"></typeparam>
[Obsolete("Overcomplicated")]
public abstract partial class VisualWrapperBase<T> : StyledElementWrapperBase<T> where T : Visual
{
    #region Visual Properties (from Visual)

    /*
    /// <param name="isVisible">
    /// Whether the control is visible in the user interface
    /// </param>
    [ImplementProperty("Visual.IsVisibleProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isVisible;

    /// <param name="opacity">
    /// The opacity (transparency) of the control from 0.0 (fully transparent) to 1.0 (fully opaque)
    /// </param>
    [ImplementProperty("Visual.OpacityProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _opacity;

    /*
    /// <param name="renderTransform">
    /// The transform applied to the control's rendering (scaling, rotation, translation, skew)
    /// </param>
    [ImplementProperty("Visual.RenderTransformProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<ITransform> _renderTransform;
    */

    /*
    /// <param name="renderTransformOrigin">
    /// The origin point for render transformations as a relative point (0,0 = top-left, 1,1 = bottom-right)
    /// </param>
    [ImplementProperty("Visual.RenderTransformOriginProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<RelativePoint> _renderTransformOrigin;
    */

    /*
    /// <param name="flowDirection">
    /// The direction of text and other UI elements flow (LeftToRight or RightToLeft)
    /// </param>
    [ImplementProperty("Visual.FlowDirectionProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<FlowDirection> _flowDirection;
    */

    /// <param name="zIndex">
    /// The z-order (layering) of the control relative to its siblings
    /// </param>
    [ImplementProperty("Visual.ZIndexProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _zIndex;

    /*
    /// <param name="clipToBounds">
    /// Whether child elements are clipped to the bounds of this control
    /// </param>
    [ImplementProperty("Visual.ClipToBoundsProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _clipToBounds;
    */

    /*
    /// <param name="clip">
    /// The geometry used to clip the control's content
    /// </param>
    [ImplementProperty("Visual.ClipProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<Geometry> _clip;
    */

    /*
    /// <param name="opacityMask">
    /// A brush that represents the opacity mask for the control
    /// </param>
    [ImplementProperty("Visual.OpacityMaskProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<IBrush> _opacityMask;
    */

    /*
    /// <param name="effect">
    /// Visual effect applied to the control (drop shadow, blur, etc.)
    /// </param>
    [ImplementProperty("Visual.EffectProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<IEffect> _effect;
    */

    #endregion
}
