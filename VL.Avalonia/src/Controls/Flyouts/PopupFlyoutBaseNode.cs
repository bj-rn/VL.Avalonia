using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Primitives.PopupPositioning;
using Avalonia.Input;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="PopupFlyoutBase"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class PopupFlyoutBaseNode<TControl> : FlyoutBaseNode<TControl>
        where TControl : PopupFlyoutBase, new()
    {
        /// <summary>Sets the desired placement of the flyout in relation to the PlacementTarget.</summary>
        [ImplementProperty(
            typeof(PopupFlyoutBase),
            nameof(PopupFlyoutBase.PlacementProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<PlacementMode> _placement;

        /// <summary>Sets the Horizontal offset of the flyout in relation to the PlacementTarget.</summary>
        [ImplementProperty(
            typeof(PopupFlyoutBase),
            nameof(PopupFlyoutBase.HorizontalOffsetProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<float> _horizontalOffset;

        /// <summary>Sets the Vertical offset of the flyout in relation to the PlacementTarget.</summary>
        [ImplementProperty(
            typeof(PopupFlyoutBase),
            nameof(PopupFlyoutBase.VerticalOffsetProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<float> _verticalOffset;

        /// <summary>Sets the anchor point on the PlacementRect when Placement is AnchorAndGravity.</summary>
        [ImplementProperty(
            typeof(PopupFlyoutBase),
            nameof(PopupFlyoutBase.PlacementAnchorProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<PopupAnchor> _placementAnchor;

        /// <summary>Sets a value which defines in what direction the flyout should open when Placement is AnchorAndGravity.</summary>
        [ImplementProperty(
            typeof(PopupFlyoutBase),
            nameof(PopupFlyoutBase.PlacementGravityProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<PopupGravity> _placementGravity;

        /// <summary>Sets a delegate handler method that positions the Popup control, when Placement is set to Custom.</summary>
        [ImplementProperty(
            typeof(PopupFlyoutBase),
            nameof(PopupFlyoutBase.CustomPopupPlacementCallbackProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<CustomPopupPlacementCallback> _customPopupPlacementCallback;

        /// <summary>Sets the desired ShowMode.</summary>
        [ImplementProperty(
            typeof(PopupFlyoutBase),
            nameof(PopupFlyoutBase.ShowModeProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<FlyoutShowMode> _showMode;

        /// <summary>Sets a value indicating whether the event that closes the flyout is passed through to the parent window.</summary>
        [ImplementProperty(
            typeof(PopupFlyoutBase),
            nameof(PopupFlyoutBase.OverlayDismissEventPassThroughProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _overlayDismissEventPassThrough;

        /// <summary>Sets an element that should receive pointer input events even when underneath the flyout's overlay.</summary>
        [ImplementProperty(
            typeof(PopupFlyoutBase),
            nameof(PopupFlyoutBase.OverlayInputPassThroughElementProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<IInputElement> _overlayInputPassThroughElement;

        /// <summary>Sets a value describing how the flyout position will be adjusted if the unadjusted position would result in the flyout being partly constrained.</summary>
        [ImplementProperty(
            typeof(PopupFlyoutBase),
            nameof(PopupFlyoutBase.PlacementConstraintAdjustmentProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<PopupPositionerConstraintAdjustment> _placementConstraintAdjustment;
    }
}
