using Avalonia.Animation;
using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "Expander")]
public partial class ExpanderWrapper : HeaderedControlWrapperBase<Expander>
{
    private ChannelTwoWayBinding<bool> _isExpandedBinding;
    public ExpanderWrapper() : base()
    {
        _isExpandedBinding = new ChannelTwoWayBinding<bool>(_output, Expander.IsExpandedProperty);
    }

    /// <param name="isExpandedChannel">
    /// IsExpanded channel
    /// </param>
    [Fragment(Order = -7)]
    public void SetIsExpandedChannel(IChannel<bool> isExpandedChannel) =>
        _isExpandedBinding.SetChannel(isExpandedChannel);

    [ImplementProperty("Expander.ExpandDirectionProperty")]
    protected Optional<ExpandDirection> _expandDirection;

    [ImplementProperty("Expander.ContentTransitionProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<IPageTransition> _contentTransition;
}
