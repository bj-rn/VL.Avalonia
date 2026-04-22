using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Stride.Core.Mathematics;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="ScrollViewer"/>.
    /// </summary>
    public static class ScrollViewerExtensions
    {
        /// <inheritdoc cref="ScrollViewer.Offset"/>
        public static Vector2 Offset(this ScrollViewer scrollViewer) =>
            scrollViewer != null
                ? new Vector2((float)scrollViewer.Offset.X, (float)scrollViewer.Offset.Y)
                : Vector2.Zero;

        /// <inheritdoc cref="ScrollViewer.Offset"/>
        public static void SetOffset(this ScrollViewer scrollViewer, Vector2 offset)
        {
            if (scrollViewer is not null)
                scrollViewer.Offset = new Vector(offset.X, offset.Y);
        }

        /// <inheritdoc cref="ScrollViewer.Extent"/>
        public static Vector2 Extent(this ScrollViewer scrollViewer) =>
            scrollViewer != null
                ? new Vector2((float)scrollViewer.Extent.Width, (float)scrollViewer.Extent.Height)
                : Vector2.Zero;

        /// <inheritdoc cref="ScrollViewer.Viewport"/>
        public static Vector2 Viewport(this ScrollViewer scrollViewer) =>
            scrollViewer != null
                ? new Vector2(
                    (float)scrollViewer.Viewport.Width,
                    (float)scrollViewer.Viewport.Height
                )
                : Vector2.Zero;

        /// <inheritdoc cref="ScrollViewer.LargeChange"/>
        public static Vector2 LargeChange(this ScrollViewer scrollViewer) =>
            scrollViewer != null
                ? new Vector2(
                    (float)scrollViewer.LargeChange.Width,
                    (float)scrollViewer.LargeChange.Height
                )
                : Vector2.Zero;

        /// <inheritdoc cref="ScrollViewer.SmallChange"/>
        public static Vector2 SmallChange(this ScrollViewer scrollViewer) =>
            scrollViewer != null
                ? new Vector2(
                    (float)scrollViewer.SmallChange.Width,
                    (float)scrollViewer.SmallChange.Height
                )
                : Vector2.Zero;

        /// <inheritdoc cref="ScrollViewer.ScrollBarMaximum"/>
        public static Vector2 ScrollBarMaximum(this ScrollViewer scrollViewer) =>
            scrollViewer != null
                ? new Vector2(
                    (float)scrollViewer.ScrollBarMaximum.X,
                    (float)scrollViewer.ScrollBarMaximum.Y
                )
                : Vector2.Zero;

        /// <inheritdoc cref="ScrollViewer.IsExpanded"/>
        public static bool IsExpanded(this ScrollViewer scrollViewer) =>
            scrollViewer != null ? scrollViewer.IsExpanded : false;

        /// <inheritdoc cref="ScrollViewer.CurrentAnchor"/>
        public static Control? CurrentAnchor(this ScrollViewer scrollViewer) =>
            scrollViewer != null ? scrollViewer.CurrentAnchor : null;

        // AttachedProperties:

        /// <inheritdoc cref="ScrollViewer.BringIntoViewOnFocusChangeProperty"/>
        public static bool ScrollViewerBringIntoViewOnFocusChange(this Control control) =>
            control != null ? ScrollViewer.GetBringIntoViewOnFocusChange(control) : true;

        /// <inheritdoc cref="ScrollViewer.BringIntoViewOnFocusChangeProperty"/>
        public static void SetScrollViewerBringIntoViewOnFocusChange(
            this Control control,
            bool value
        )
        {
            if (control is not null)
                ScrollViewer.SetBringIntoViewOnFocusChange(control, value);
        }

        /// <inheritdoc cref="ScrollViewer.HorizontalScrollBarVisibilityProperty"/>
        public static ScrollBarVisibility ScrollViewerHorizontalScrollBarVisibility(
            this Control control
        ) =>
            control != null
                ? ScrollViewer.GetHorizontalScrollBarVisibility(control)
                : ScrollBarVisibility.Disabled;

        /// <inheritdoc cref="ScrollViewer.HorizontalScrollBarVisibilityProperty"/>
        public static void SetScrollViewerHorizontalScrollBarVisibility(
            this Control control,
            ScrollBarVisibility value
        )
        {
            if (control is not null)
                ScrollViewer.SetHorizontalScrollBarVisibility(control, value);
        }

        /// <inheritdoc cref="ScrollViewer.VerticalScrollBarVisibilityProperty"/>
        public static ScrollBarVisibility ScrollViewerVerticalScrollBarVisibility(
            this Control control
        ) =>
            control != null
                ? ScrollViewer.GetVerticalScrollBarVisibility(control)
                : ScrollBarVisibility.Auto;

        /// <inheritdoc cref="ScrollViewer.VerticalScrollBarVisibilityProperty"/>
        public static void SetScrollViewerVerticalScrollBarVisibility(
            this Control control,
            ScrollBarVisibility value
        )
        {
            if (control is not null)
                ScrollViewer.SetVerticalScrollBarVisibility(control, value);
        }

        /// <inheritdoc cref="ScrollViewer.HorizontalSnapPointsTypeProperty"/>
        public static SnapPointsType ScrollViewerHorizontalSnapPointsType(this Control control) =>
            control != null ? ScrollViewer.GetHorizontalSnapPointsType(control) : default;

        /// <inheritdoc cref="ScrollViewer.HorizontalSnapPointsTypeProperty"/>
        public static void SetScrollViewerHorizontalSnapPointsType(
            this Control control,
            SnapPointsType value
        )
        {
            if (control is not null)
                ScrollViewer.SetHorizontalSnapPointsType(control, value);
        }

        /// <inheritdoc cref="ScrollViewer.VerticalSnapPointsTypeProperty"/>
        public static SnapPointsType ScrollViewerVerticalSnapPointsType(this Control control) =>
            control != null ? ScrollViewer.GetVerticalSnapPointsType(control) : default;

        /// <inheritdoc cref="ScrollViewer.VerticalSnapPointsTypeProperty"/>
        public static void SetScrollViewerVerticalSnapPointsType(
            this Control control,
            SnapPointsType value
        )
        {
            if (control is not null)
                ScrollViewer.SetVerticalSnapPointsType(control, value);
        }

        /// <inheritdoc cref="ScrollViewer.HorizontalSnapPointsAlignmentProperty"/>
        public static SnapPointsAlignment ScrollViewerHorizontalSnapPointsAlignment(
            this Control control
        ) => control != null ? ScrollViewer.GetHorizontalSnapPointsAlignment(control) : default;

        /// <inheritdoc cref="ScrollViewer.HorizontalSnapPointsAlignmentProperty"/>
        public static void SetScrollViewerHorizontalSnapPointsAlignment(
            this Control control,
            SnapPointsAlignment value
        )
        {
            if (control is not null)
                ScrollViewer.SetHorizontalSnapPointsAlignment(control, value);
        }

        /// <inheritdoc cref="ScrollViewer.VerticalSnapPointsAlignmentProperty"/>
        public static SnapPointsAlignment ScrollViewerVerticalSnapPointsAlignment(
            this Control control
        ) => control != null ? ScrollViewer.GetVerticalSnapPointsAlignment(control) : default;

        /// <inheritdoc cref="ScrollViewer.VerticalSnapPointsAlignmentProperty"/>
        public static void SetScrollViewerVerticalSnapPointsAlignment(
            this Control control,
            SnapPointsAlignment value
        )
        {
            if (control is not null)
                ScrollViewer.SetVerticalSnapPointsAlignment(control, value);
        }

        /// <inheritdoc cref="ScrollViewer.AllowAutoHideProperty"/>
        public static bool ScrollViewerAllowAutoHide(this Control control) =>
            control != null ? ScrollViewer.GetAllowAutoHide(control) : true;

        /// <inheritdoc cref="ScrollViewer.AllowAutoHideProperty"/>
        public static void SetScrollViewerAllowAutoHide(this Control control, bool value)
        {
            if (control is not null)
                ScrollViewer.SetAllowAutoHide(control, value);
        }

        /// <inheritdoc cref="ScrollViewer.IsScrollChainingEnabledProperty"/>
        public static bool ScrollViewerIsScrollChainingEnabled(this Control control) =>
            control != null ? ScrollViewer.GetIsScrollChainingEnabled(control) : true;

        /// <inheritdoc cref="ScrollViewer.IsScrollChainingEnabledProperty"/>
        public static void SetScrollViewerIsScrollChainingEnabled(this Control control, bool value)
        {
            if (control is not null)
                ScrollViewer.SetIsScrollChainingEnabled(control, value);
        }

        /// <inheritdoc cref="ScrollViewer.IsScrollInertiaEnabledProperty"/>
        public static bool ScrollViewerIsScrollInertiaEnabled(this Control control) =>
            control != null ? ScrollViewer.GetIsScrollInertiaEnabled(control) : true;

        /// <inheritdoc cref="ScrollViewer.IsScrollInertiaEnabledProperty"/>
        public static void SetScrollViewerIsScrollInertiaEnabled(this Control control, bool value)
        {
            if (control is not null)
                ScrollViewer.SetIsScrollInertiaEnabled(control, value);
        }

        /// <inheritdoc cref="ScrollViewer.IsDeferredScrollingEnabledProperty"/>
        public static bool ScrollViewerIsDeferredScrollingEnabled(this Control control) =>
            control != null ? ScrollViewer.GetIsDeferredScrollingEnabled(control) : false;

        /// <inheritdoc cref="ScrollViewer.IsDeferredScrollingEnabledProperty"/>
        public static void SetScrollViewerIsDeferredScrollingEnabled(
            this Control control,
            bool value
        )
        {
            if (control is not null)
                ScrollViewer.SetIsDeferredScrollingEnabled(control, value);
        }
    }
}
