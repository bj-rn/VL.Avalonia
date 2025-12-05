using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "SplitView")]
public partial class SplitViewWrapper : ContentControlWrapperBase<SplitView>
{
    /// <param name="pane">An optional object representing the new content for the pane. Can be <see langword="null"/> to clear the pane.</param>
    [ImplementProperty("SplitView.PaneProperty", Order = PinOrder.Exclusive)]
    private Optional<object> _pane;

    protected ChannelTwoWayBinding<bool> _isPaneOpenBinding;

    public SplitViewWrapper()
        : base()
    {
        _isPaneOpenBinding = new ChannelTwoWayBinding<bool>(_output, SplitView.IsPaneOpenProperty);
    }

    /// <param name="isPaneOpenChannel">Gets or sets whether the pane is open or closed</param>
    public void SetIsPaneOpenChannel(IChannel<bool> isPaneOpenChannel) =>
        _isPaneOpenBinding.SetChannel(isPaneOpenChannel);

    /// <param name="compactPaneLength">sets the length of the pane when in <see cref="SplitViewDisplayMode.CompactOverlay"/> or <see cref="SplitViewDisplayMode.CompactInline"/> mode</param>
    [ImplementProperty<float, double>(
        "SplitView.CompactPaneLengthProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    private Optional<float> _compactPaneLength;

    /// <param name="displayMode">Sets the <see cref="SplitViewDisplayMode"/> for the SplitView</param>
    [ImplementProperty(
        "SplitView.DisplayModeProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    private Optional<SplitViewDisplayMode> _displayMode;

    /// <summary><param name="openPaneLength">Sets the length of the pane when open</param></summary>
    [ImplementProperty<float, double>(
        "SplitView.OpenPaneLengthProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    private Optional<float> _openPaneLength;

    /// <param name="paneBackground">Sets the <see cref="SplitView.PaneBackground"/> for the SplitView</param>
    [ImplementProperty(
        "SplitView.PaneBackgroundProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    private Optional<IBrush> _paneBackground;

    /// <param name="panePlacement">Sets the <see cref="SplitViewPanePlacement"/> for the SplitView</param>
    [ImplementProperty(
        "SplitView.PanePlacementProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    private Optional<SplitViewPanePlacement> _panePlacement;

    /// <param name="paneTemplate">Sets the <see cref="SplitView.PaneTemplate"/> for the SplitView</param>
    [ImplementProperty(
        "SplitView.PaneTemplateProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    private Optional<IDataTemplate> _paneTemplate;

    /// <param name="useLightDismissOverlayMode">Sets the <see cref="SplitView.UseLightDismissOverlayMode"/> for the SplitView</param>
    [ImplementProperty(
        "SplitView.UseLightDismissOverlayModeProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    private Optional<bool> _useLightDismissOverlayMode;
}
