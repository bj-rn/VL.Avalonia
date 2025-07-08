using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives.PopupPositioning;
using VL.Avalonia.Attributes;
using VL.Avalonia.Controls.Menus;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

/// <summary>
/// <c>Control</c> is the base class for all Avalonia controls. It adds user data, menu/flyout, focus adorners, loaded/unloaded events, and context menu support.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/control">Control</see>
/// </summary>
[ProcessNode(Name = "ContextMenu (Spectral)")]
public partial class ContextMenuSpectralWrapper : MenuBaseWrapper<ContextMenu, object?>
{
    #region Placement/Popup Properties

    /// <param name="horizontalOffset">
    /// Horizontal offset (in device-independent pixels) for menu placement relative to the anchor/control.
    /// </param>
    [ImplementProperty("ContextMenu.HorizontalOffsetProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _horizontalOffset;

    /// <param name="verticalOffset">
    /// Vertical offset (in device-independent pixels) for menu placement relative to the anchor/control.
    /// </param>
    [ImplementProperty("ContextMenu.VerticalOffsetProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _verticalOffset;

    /// <param name="placementAnchor">
    /// Anchor edge/corner for popup placement (PopupAnchor).
    /// </param>
    [ImplementProperty("ContextMenu.PlacementAnchorProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<PopupAnchor> _placementAnchor;

    /// <param name="placementConstraintAdjustment">
    /// Constraint adjustment for popup placement (PopupPositionerConstraintAdjustment).
    /// </param>
    [ImplementProperty("ContextMenu.PlacementConstraintAdjustmentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<PopupPositionerConstraintAdjustment> _placementConstraintAdjustment;

    /// <param name="placementGravity">
    /// Gravity (direction) for popup placement (PopupGravity).
    /// </param>
    [ImplementProperty("ContextMenu.PlacementGravityProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<PopupGravity> _placementGravity;

    /// <param name="placement">
    /// Placement mode (PlacementMode): Pointer (default), Bottom, Right, etc.
    /// </param>
    [ImplementProperty("ContextMenu.PlacementProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<PlacementMode> _placement;

    /// <param name="placementRect">
    /// Optional rectangle to use as placement reference (Rect?).
    /// </param>
    [ImplementProperty("ContextMenu.PlacementRectProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<Rect?> _placementRect;

    /// <param name="windowManagerAddShadowHint">
    /// Hint to the window manager to add a shadow to the popup menu.
    /// </param>
    [ImplementProperty("ContextMenu.WindowManagerAddShadowHintProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _windowManagerAddShadowHint;

    /// <param name="placementTarget">
    /// The control that acts as the anchor for the context menu (usually set automatically).
    /// </param>
    [ImplementProperty("ContextMenu.PlacementTargetProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<Control> _placementTarget;

    /// <param name="customPopupPlacementCallback">
    /// A callback for custom popup placement logic.
    /// </param>
    [ImplementProperty("ContextMenu.CustomPopupPlacementCallbackProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<CustomPopupPlacementCallback> _customPopupPlacementCallback;

    #endregion
}

/// <inheritdoc cref="ContextMenuSpectralWrapper"/>
[ProcessNode(Name = "ContextMenu")]
public partial class ContextMenuWrapper : ContextMenuSpectralWrapper
{
    /// <param name="items">
    /// The collection of items
    /// </param>
    [Fragment(Order = PinOrder.Main)]
    public override void SetItems([Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<object?> items)
    {
        base.SetItems(items);
    }
}
