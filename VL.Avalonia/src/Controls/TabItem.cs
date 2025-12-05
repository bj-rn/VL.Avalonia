using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>TabItem</c> represents a selectable item in a <see cref="TabControl"/>. Each TabItem can display a header and content, and can be selected via UI or code. TabItem supports custom header, selection state, and participates in keyboard navigation and automation.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/tabitem">TabItem</see>
/// </summary>
[ProcessNode(Name = "TabItem")]
public partial class TabItemWrapper : HeaderedControlWrapperBase<TabItem>
{
    #region TabItem Properties

    protected ChannelTwoWayBinding<bool> _isSelectedBinding;

    public TabItemWrapper()
        : base()
    {
        _isSelectedBinding = new ChannelTwoWayBinding<bool>(_output, TabItem.IsSelectedProperty);
    }

    /// <param name="isSelectedChannel">
    /// Whether this list item is currently selected
    /// </param>
    public void SetIsSelectedChannel(
        [Pin(Visibility = Model.PinVisibility.Optional)] IChannel<bool> isSelectedChannel
    ) => _isSelectedBinding.SetChannel(isSelectedChannel);

    [ImplementProperty(
        "TabItem.TabStripPlacementProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<Dock> _tabStripPlacement;

    #endregion
}
