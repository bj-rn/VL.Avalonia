using Avalonia.Controls;
using VL.Avalonia.Helpers;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "TextBox")]
public partial class TextBoxWrapper : ControlWrapperBase<TextBox>
{
    protected ChannelTwoWayBinding<string> _textBinding;
    public TextBoxWrapper()
    {
        _textBinding = new ChannelTwoWayBinding<string>(_output, TextBox.TextProperty);
    }

    [Fragment(Order = -10)]
    public void SetTextChannel(IChannel<string>? textChannel) =>
        _textBinding.SetChannel(textChannel);
}

