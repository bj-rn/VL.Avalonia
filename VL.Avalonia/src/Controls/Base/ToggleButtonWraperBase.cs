using Avalonia.Controls.Primitives;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// Base class for <c>ToggleButton</c> inheriting controls
/// </summary>
[ProcessNode]
public abstract partial class ToggleButtonWrapperBase<T> : ButtonNodeBase<T>
    where T : ToggleButton, new()
{
    protected ChannelTwoWayBinding<bool, bool?> _isCheckedBinding;

    protected ToggleButtonWrapperBase()
        : base()
    {
        _isCheckedBinding = new ChannelTwoWayBinding<bool, bool?>(
            _output,
            ToggleButton.IsCheckedProperty,
            (x) => x,
            (y) => y ?? false
        );
    }

    /// <param name="isCheckedChannel">
    /// IsChecked channel
    /// </param>
    [Fragment(Order = -7)]
    public void SetIsCheckedChannel(IChannel<bool> isCheckedChannel) =>
        _isCheckedBinding.SetChannel(isCheckedChannel);

    /// <param name="isThreeState">
    /// Whether the control supports three states (checked, unchecked, and indeterminate)
    /// </param>
    [ImplementProperty(
        "ToggleButton.IsThreeStateProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<bool> _isThreeState;
}
