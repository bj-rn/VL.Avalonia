using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "ToggleSplitButton")]
public partial class ToggleSplitButtonWrapper : ControlWrapperBase<ToggleSplitButton>
{
    [ImplementProperty("ToggleSplitButton.ContentProperty", Order = -5)]
    protected Optional<object?> _content;

    // TODO: REFACTOR
    private ChannelFlange<bool> _isCheckedFlange = new ChannelFlange<bool>(false);
    private IChannel<bool>? _isCheckedChannel;
    public void SetIsCheckedChannel(IChannel<bool>? isCheckedChannel)
    {
        if (_isCheckedChannel != isCheckedChannel)
        {
            _isCheckedChannel = isCheckedChannel;

            if (isCheckedChannel != null)
            {
                _isCheckedFlange.Value = isCheckedChannel.Value;
                _isCheckedFlange.Update(_isCheckedChannel);

                _isCheckedChannel?.Subscribe(x => _output.SetValue(ToggleButton.IsCheckedProperty, x));
                _output.IsCheckedChanged += IsCheckedChanged;

                _output.SetValue(ToggleButton.IsCheckedProperty, _isCheckedFlange.Value);
            }
            else
            {
                _output.IsCheckedChanged -= IsCheckedChanged;
            }
        }
    }

    private void IsCheckedChanged(object? sender, global::Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_isCheckedFlange != null)
        {
            _isCheckedFlange.Value = _output.IsChecked;
            _isCheckedFlange.Update(_isCheckedChannel);
        }
    }

    [ImplementProperty("ToggleSplitButton.FlyoutProperty", Order = -3)]
    protected Optional<FlyoutBase> _flyout;
}
