using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;


namespace VL.Avalonia.Controls;
/// <summary>
/// The <c>RadioButton</c> control presents a group of options from which only one may be selected at a time. A selected option is drawn as a filled circle, and an unselected option as an empty circle.
/// </summary>
[ProcessNode(Name = "RadioButton")]
public partial class RadioButtonWrapper : ControlWrapperBase<RadioButton>
{
    [ImplementProperty("RadioButton.ContentProperty", Order = -10)]
    protected Optional<object?> _content;

    [ImplementProperty("RadioButton.GroupNameProperty")]
    protected Optional<string> _groupName;

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

                _isCheckedChannel?.Subscribe(x => _output.SetValue(RadioButton.IsCheckedProperty, x));
                _output.IsCheckedChanged += IsCheckedChanged;

                _output.SetValue(RadioButton.IsCheckedProperty, _isCheckedFlange.Value);
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
            _isCheckedFlange.Value = _output.IsChecked ?? false;
            _isCheckedFlange.Update(_isCheckedChannel);
        }
    }

    public bool IsChecked => _output.IsChecked ?? false;
}
