using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives.PopupPositioning;
using Stride.Core.Mathematics;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="ContextMenu"/>.
    /// </summary>
    public static class ContextMenuExtensions
    {
        /// <inheritdoc cref="ContextMenu.HorizontalOffset"/>
        public static float HorizontalOffset(this ContextMenu contextMenu) =>
            contextMenu != null ? (float)contextMenu.HorizontalOffset : 0f;

        /// <inheritdoc cref="ContextMenu.HorizontalOffset"/>
        public static void SetHorizontalOffset(this ContextMenu contextMenu, float horizontalOffset)
        {
            if (contextMenu is not null)
                contextMenu.HorizontalOffset = horizontalOffset;
        }

        /// <inheritdoc cref="ContextMenu.VerticalOffset"/>
        public static float VerticalOffset(this ContextMenu contextMenu) =>
            contextMenu != null ? (float)contextMenu.VerticalOffset : 0f;

        /// <inheritdoc cref="ContextMenu.VerticalOffset"/>
        public static void SetVerticalOffset(this ContextMenu contextMenu, float verticalOffset)
        {
            if (contextMenu is not null)
                contextMenu.VerticalOffset = verticalOffset;
        }

        /// <inheritdoc cref="ContextMenu.PlacementAnchor"/>
        public static PopupAnchor PlacementAnchor(this ContextMenu contextMenu) =>
            contextMenu != null ? contextMenu.PlacementAnchor : PopupAnchor.None;

        /// <inheritdoc cref="ContextMenu.PlacementAnchor"/>
        public static void SetPlacementAnchor(
            this ContextMenu contextMenu,
            PopupAnchor placementAnchor
        )
        {
            if (contextMenu is not null)
                contextMenu.PlacementAnchor = placementAnchor;
        }

        /// <inheritdoc cref="ContextMenu.PlacementConstraintAdjustment"/>
        public static PopupPositionerConstraintAdjustment PlacementConstraintAdjustment(
            this ContextMenu contextMenu
        ) =>
            contextMenu != null
                ? contextMenu.PlacementConstraintAdjustment
                : PopupPositionerConstraintAdjustment.None;

        /// <inheritdoc cref="ContextMenu.PlacementConstraintAdjustment"/>
        public static void SetPlacementConstraintAdjustment(
            this ContextMenu contextMenu,
            PopupPositionerConstraintAdjustment placementConstraintAdjustment
        )
        {
            if (contextMenu is not null)
                contextMenu.PlacementConstraintAdjustment = placementConstraintAdjustment;
        }

        /// <inheritdoc cref="ContextMenu.PlacementGravity"/>
        public static PopupGravity PlacementGravity(this ContextMenu contextMenu) =>
            contextMenu != null ? contextMenu.PlacementGravity : PopupGravity.None;

        /// <inheritdoc cref="ContextMenu.PlacementGravity"/>
        public static void SetPlacementGravity(
            this ContextMenu contextMenu,
            PopupGravity placementGravity
        )
        {
            if (contextMenu is not null)
                contextMenu.PlacementGravity = placementGravity;
        }

        /// <inheritdoc cref="ContextMenu.Placement"/>
        public static PlacementMode Placement(this ContextMenu contextMenu) =>
            contextMenu != null ? contextMenu.Placement : PlacementMode.Pointer;

        /// <inheritdoc cref="ContextMenu.Placement"/>
        public static void SetPlacement(this ContextMenu contextMenu, PlacementMode placement)
        {
            if (contextMenu is not null)
                contextMenu.Placement = placement;
        }

        /// <inheritdoc cref="ContextMenu.WindowManagerAddShadowHint"/>
        public static bool WindowManagerAddShadowHint(this ContextMenu contextMenu) =>
            contextMenu != null ? contextMenu.WindowManagerAddShadowHint : false;

        /// <inheritdoc cref="ContextMenu.WindowManagerAddShadowHint"/>
        public static void SetWindowManagerAddShadowHint(
            this ContextMenu contextMenu,
            bool windowManagerAddShadowHint
        )
        {
            if (contextMenu is not null)
                contextMenu.WindowManagerAddShadowHint = windowManagerAddShadowHint;
        }

        /// <inheritdoc cref="ContextMenu.PlacementRect"/>
        public static RectangleF? PlacementRect(this ContextMenu contextMenu)
        {
            if (contextMenu?.PlacementRect is { } rect)
                return new RectangleF(
                    (float)rect.X,
                    (float)rect.Y,
                    (float)rect.Width,
                    (float)rect.Height
                );

            return null;
        }

        /// <inheritdoc cref="ContextMenu.PlacementRect"/>
        public static void SetPlacementRect(this ContextMenu contextMenu, RectangleF? placementRect)
        {
            if (contextMenu is not null)
            {
                if (placementRect.HasValue)
                    contextMenu.PlacementRect = new Rect(
                        placementRect.Value.X,
                        placementRect.Value.Y,
                        placementRect.Value.Width,
                        placementRect.Value.Height
                    );
                else
                    contextMenu.PlacementRect = null;
            }
        }

        /// <inheritdoc cref="ContextMenu.PlacementTarget"/>
        public static Control? PlacementTarget(this ContextMenu contextMenu) =>
            contextMenu != null ? contextMenu.PlacementTarget : null;

        /// <inheritdoc cref="ContextMenu.PlacementTarget"/>
        public static void SetPlacementTarget(
            this ContextMenu contextMenu,
            Control? placementTarget
        )
        {
            if (contextMenu is not null)
                contextMenu.PlacementTarget = placementTarget;
        }

        /// <inheritdoc cref="ContextMenu.CustomPopupPlacementCallback"/>
        public static CustomPopupPlacementCallback? CustomPopupPlacementCallback(
            this ContextMenu contextMenu
        ) => contextMenu != null ? contextMenu.CustomPopupPlacementCallback : null;

        /// <inheritdoc cref="ContextMenu.CustomPopupPlacementCallback"/>
        public static void SetCustomPopupPlacementCallback(
            this ContextMenu contextMenu,
            CustomPopupPlacementCallback? customPopupPlacementCallback
        )
        {
            if (contextMenu is not null)
                contextMenu.CustomPopupPlacementCallback = customPopupPlacementCallback;
        }

        // AttachedProperties:
        // No attached properties were found in the provided file.
    }
}
