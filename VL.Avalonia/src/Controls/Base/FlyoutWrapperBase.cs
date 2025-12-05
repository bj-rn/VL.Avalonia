using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Primitives.PopupPositioning;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

/// <summary>
/// Base class for FlyoutWrappers.
/// Implements: <c>Output</c>.
/// </summary>
/// <typeparam name="T"></typeparam>
[ProcessNode]
public abstract partial class FlyoutWrapperBase<T>
    where T : PopupFlyoutBase, new()
{
    protected readonly T _output = new();
    public T Output => _output;

    //protected Optional<IAvaloniaStyle> _style;
    //[Fragment(Order = -3)]
    //public void SetStyle(Optional<IAvaloniaStyle> style)
    //{
    //    if (_style != style)
    //    {
    //        _style = style;
    //        _output.TryUpdateStyles(style.Value);
    //    }
    //}

    //protected Optional<string> _classes;
    //[Fragment(Order = -2)]
    //public void SetClasses([Pin(Visibility = Model.PinVisibility.Optional)] Optional<string> classes)
    //{
    //    if (_classes != classes)
    //    {
    //        _classes = classes;
    //        _output.TryUpdateClasses(classes.Value);
    //    }
    //}

    [ImplementProperty("PopupFlyoutBase.PlacementProperty")]
    protected Optional<PlacementMode> _placement;

    [ImplementProperty(
        "PopupFlyoutBase.PlacementGravityProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<PopupGravity> _placementGravity;

    [ImplementProperty(
        "PopupFlyoutBase.PlacementAnchorProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<PopupAnchor> _pacementAnchor;

    [ImplementProperty(
        "PopupFlyoutBase.HorizontalOffsetProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<float> _horizontalOffset;

    [ImplementProperty(
        "PopupFlyoutBase.VerticalOffsetProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<float> _verticalOffset;

    [ImplementProperty(
        "PopupFlyoutBase.ShowModeProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<FlyoutShowMode> _showMode;

    [ImplementProperty(
        "PopupFlyoutBase.OverlayDismissEventPassThroughProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<bool> _overlayDismissEventPassThroughProperty;
}
