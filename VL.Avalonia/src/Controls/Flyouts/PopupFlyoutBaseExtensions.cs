using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Primitives.PopupPositioning;
using Avalonia.Input;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="PopupFlyoutBase"/>.
    /// </summary>
    public static class PopupFlyoutBaseExtensions
    {
        /// <inheritdoc cref="PopupFlyoutBase.Placement"/>
        public static PlacementMode Placement(this PopupFlyoutBase popupFlyoutBase) =>
            popupFlyoutBase != null ? popupFlyoutBase.Placement : PlacementMode.Bottom;

        /// <inheritdoc cref="PopupFlyoutBase.Placement"/>
        public static void SetPlacement(
            this PopupFlyoutBase popupFlyoutBase,
            PlacementMode placement
        )
        {
            if (popupFlyoutBase is not null)
                popupFlyoutBase.Placement = placement;
        }

        /// <inheritdoc cref="PopupFlyoutBase.PlacementGravity"/>
        public static PopupGravity PlacementGravity(this PopupFlyoutBase popupFlyoutBase) =>
            popupFlyoutBase != null ? popupFlyoutBase.PlacementGravity : PopupGravity.None;

        /// <inheritdoc cref="PopupFlyoutBase.PlacementGravity"/>
        public static void SetPlacementGravity(
            this PopupFlyoutBase popupFlyoutBase,
            PopupGravity placementGravity
        )
        {
            if (popupFlyoutBase is not null)
                popupFlyoutBase.PlacementGravity = placementGravity;
        }

        /// <inheritdoc cref="PopupFlyoutBase.PlacementAnchor"/>
        public static PopupAnchor PlacementAnchor(this PopupFlyoutBase popupFlyoutBase) =>
            popupFlyoutBase != null ? popupFlyoutBase.PlacementAnchor : PopupAnchor.None;

        /// <inheritdoc cref="PopupFlyoutBase.PlacementAnchor"/>
        public static void SetPlacementAnchor(
            this PopupFlyoutBase popupFlyoutBase,
            PopupAnchor placementAnchor
        )
        {
            if (popupFlyoutBase is not null)
                popupFlyoutBase.PlacementAnchor = placementAnchor;
        }

        /// <inheritdoc cref="PopupFlyoutBase.HorizontalOffset"/>
        public static float HorizontalOffset(this PopupFlyoutBase popupFlyoutBase) =>
            popupFlyoutBase != null ? (float)popupFlyoutBase.HorizontalOffset : 0f;

        /// <inheritdoc cref="PopupFlyoutBase.HorizontalOffset"/>
        public static void SetHorizontalOffset(
            this PopupFlyoutBase popupFlyoutBase,
            float horizontalOffset
        )
        {
            if (popupFlyoutBase is not null)
                popupFlyoutBase.HorizontalOffset = horizontalOffset;
        }

        /// <inheritdoc cref="PopupFlyoutBase.VerticalOffset"/>
        public static float VerticalOffset(this PopupFlyoutBase popupFlyoutBase) =>
            popupFlyoutBase != null ? (float)popupFlyoutBase.VerticalOffset : 0f;

        /// <inheritdoc cref="PopupFlyoutBase.VerticalOffset"/>
        public static void SetVerticalOffset(
            this PopupFlyoutBase popupFlyoutBase,
            float verticalOffset
        )
        {
            if (popupFlyoutBase is not null)
                popupFlyoutBase.VerticalOffset = verticalOffset;
        }

        /// <inheritdoc cref="PopupFlyoutBase.CustomPopupPlacementCallback"/>
        public static CustomPopupPlacementCallback? CustomPopupPlacementCallback(
            this PopupFlyoutBase popupFlyoutBase
        ) => popupFlyoutBase != null ? popupFlyoutBase.CustomPopupPlacementCallback : null;

        /// <inheritdoc cref="PopupFlyoutBase.CustomPopupPlacementCallback"/>
        public static void SetCustomPopupPlacementCallback(
            this PopupFlyoutBase popupFlyoutBase,
            CustomPopupPlacementCallback? customPopupPlacementCallback
        )
        {
            if (popupFlyoutBase is not null)
                popupFlyoutBase.CustomPopupPlacementCallback = customPopupPlacementCallback;
        }

        /// <inheritdoc cref="PopupFlyoutBase.ShowMode"/>
        public static FlyoutShowMode ShowMode(this PopupFlyoutBase popupFlyoutBase) =>
            popupFlyoutBase != null ? popupFlyoutBase.ShowMode : FlyoutShowMode.Standard;

        /// <inheritdoc cref="PopupFlyoutBase.ShowMode"/>
        public static void SetShowMode(
            this PopupFlyoutBase popupFlyoutBase,
            FlyoutShowMode showMode
        )
        {
            if (popupFlyoutBase is not null)
                popupFlyoutBase.ShowMode = showMode;
        }

        /// <inheritdoc cref="PopupFlyoutBase.OverlayDismissEventPassThrough"/>
        public static bool OverlayDismissEventPassThrough(this PopupFlyoutBase popupFlyoutBase) =>
            popupFlyoutBase != null ? popupFlyoutBase.OverlayDismissEventPassThrough : false;

        /// <inheritdoc cref="PopupFlyoutBase.OverlayDismissEventPassThrough"/>
        public static void SetOverlayDismissEventPassThrough(
            this PopupFlyoutBase popupFlyoutBase,
            bool overlayDismissEventPassThrough
        )
        {
            if (popupFlyoutBase is not null)
                popupFlyoutBase.OverlayDismissEventPassThrough = overlayDismissEventPassThrough;
        }

        /// <inheritdoc cref="PopupFlyoutBase.OverlayInputPassThroughElement"/>
        public static IInputElement? OverlayInputPassThroughElement(
            this PopupFlyoutBase popupFlyoutBase
        ) => popupFlyoutBase != null ? popupFlyoutBase.OverlayInputPassThroughElement : null;

        /// <inheritdoc cref="PopupFlyoutBase.OverlayInputPassThroughElement"/>
        public static void SetOverlayInputPassThroughElement(
            this PopupFlyoutBase popupFlyoutBase,
            IInputElement? overlayInputPassThroughElement
        )
        {
            if (popupFlyoutBase is not null)
                popupFlyoutBase.OverlayInputPassThroughElement = overlayInputPassThroughElement;
        }

        /// <inheritdoc cref="PopupFlyoutBase.PlacementConstraintAdjustment"/>
        public static PopupPositionerConstraintAdjustment PlacementConstraintAdjustment(
            this PopupFlyoutBase popupFlyoutBase
        ) =>
            popupFlyoutBase != null
                ? popupFlyoutBase.PlacementConstraintAdjustment
                : PopupPositionerConstraintAdjustment.None;

        /// <inheritdoc cref="PopupFlyoutBase.PlacementConstraintAdjustment"/>
        public static void SetPlacementConstraintAdjustment(
            this PopupFlyoutBase popupFlyoutBase,
            PopupPositionerConstraintAdjustment placementConstraintAdjustment
        )
        {
            if (popupFlyoutBase is not null)
                popupFlyoutBase.PlacementConstraintAdjustment = placementConstraintAdjustment;
        }
    }
}
