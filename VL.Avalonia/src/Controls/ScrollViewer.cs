using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Stride.Core.Mathematics;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>ScrollViewer</c> is a content control that provides scrolling for its child content when it overflows the available space. It supports both horizontal and vertical scrolling, snap points, auto-hide scrollbars, scroll chaining, inertia, deferred scrolling, and change events.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/scrollviewer">ScrollViewer</see>
/// </summary>
[ProcessNode(Name = "ScrollViewer")]
public partial class ScrollViewerWrapper : ContentControlWrapperBase<ScrollViewer>
{
    #region ScrollViewer Properties

    protected ChannelTwoWayBinding<Vector2, Vector> _offsetBinding;
    public ScrollViewerWrapper()
    {
        _offsetBinding = new ChannelTwoWayBinding<Vector2, Vector>(_output, ScrollViewer.OffsetProperty, x => x.ToVector(), x => x.FromVector());
    }

    /// <param name="offsetChannel">
    /// The current scroll offset as a Vector (horizontal, vertical).
    /// </param>
    [Fragment(Order = -7)]
    public virtual void SetOffsetChannel(IChannel<Vector2> offsetChannel) =>
        _offsetBinding.SetChannel(offsetChannel);

    /// <param name="horizontalScrollBarVisibility">
    /// Controls the visibility of the horizontal scrollbar (Disabled, Auto, Visible, Hidden).
    /// </param>
    [ImplementProperty("ScrollViewer.HorizontalScrollBarVisibilityProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<ScrollBarVisibility> _horizontalScrollBarVisibility;

    /// <param name="verticalScrollBarVisibility">
    /// Controls the visibility of the vertical scrollbar (Disabled, Auto, Visible, Hidden).
    /// </param>
    [ImplementProperty("ScrollViewer.VerticalScrollBarVisibilityProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<ScrollBarVisibility> _verticalScrollBarVisibility;

    /// <param name="bringIntoViewOnFocusChange">
    /// Whether to automatically scroll focused elements into view (default: true).
    /// </param>
    [ImplementProperty("ScrollViewer.BringIntoViewOnFocusChangeProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _bringIntoViewOnFocusChange;

    /// <param name="allowAutoHide">
    /// Whether scrollbars auto-hide when not in use (default: true).
    /// </param>
    [ImplementProperty("ScrollViewer.AllowAutoHideProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _allowAutoHide;

    /// <param name="isScrollChainingEnabled">
    /// Whether scroll chaining is enabled (default: true).
    /// </param>
    [ImplementProperty("ScrollViewer.IsScrollChainingEnabledProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isScrollChainingEnabled;

    /// <param name="isScrollInertiaEnabled">
    /// Whether scroll gestures use inertia (default: true).
    /// </param>
    [ImplementProperty("ScrollViewer.IsScrollInertiaEnabledProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isScrollInertiaEnabled;

    /// <param name="isDeferredScrollingEnabled">
    /// Whether deferred scrolling (thumb drag updates offset only on release) is enabled.
    /// </param>
    [ImplementProperty("ScrollViewer.IsDeferredScrollingEnabledProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isDeferredScrollingEnabled;

    /// <param name="horizontalSnapPointsType">
    /// Controls snap point behavior along the horizontal axis (None, Mandatory, Optional, etc.).
    /// </param>
    [ImplementProperty("ScrollViewer.HorizontalSnapPointsTypeProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<SnapPointsType> _horizontalSnapPointsType;

    /// <param name="verticalSnapPointsType">
    /// Controls snap point behavior along the vertical axis (None, Mandatory, Optional, etc.).
    /// </param>
    [ImplementProperty("ScrollViewer.VerticalSnapPointsTypeProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<SnapPointsType> _verticalSnapPointsType;

    /// <param name="horizontalSnapPointsAlignment">
    /// Controls how horizontal snap points align relative to the viewport.
    /// </param>
    [ImplementProperty("ScrollViewer.HorizontalSnapPointsAlignmentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<SnapPointsAlignment> _horizontalSnapPointsAlignment;

    /// <param name="verticalSnapPointsAlignment">
    /// Controls how vertical snap points align relative to the viewport.
    /// </param>
    [ImplementProperty("ScrollViewer.VerticalSnapPointsAlignmentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<SnapPointsAlignment> _verticalSnapPointsAlignment;

    #endregion
}
