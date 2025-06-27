using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using VL.Avalonia.Helpers;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>ToggleSplitButton</c> extends SplitButton by adding toggle functionality to the primary button part. The primary button maintains a checked/unchecked state like a ToggleButton, while the secondary button still opens a flyout. This creates a powerful control for scenarios where the primary action has a persistent state that users need to see and control alongside access to related options.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/togglesplitbutton">ToggleSplitButton</see>
/// </summary>
[ProcessNode(Name = "ToggleSplitButton")]
public partial class ToggleSplitButtonWrapper : SplitButtonWrapperBase<ToggleSplitButton>
{
    #region Toggle State Properties

    protected ChannelTwoWayBinding<bool, bool?> _isCheckedBinding;
    protected ToggleSplitButtonWrapper()
    {
        _isCheckedBinding = new(_output, ToggleButton.IsCheckedProperty, (x) => x, (y) => y ?? false);
    }

    /// <param name="isCheckedChannel">
    /// IsChecked channel
    /// </param>
    [Fragment(Order = -7)]
    public void SetIsCheckedChannel(IChannel<bool> isCheckedChannel) =>
        _isCheckedBinding.SetChannel(isCheckedChannel);

    #endregion
}
