using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;

/// <summary>
/// Based on my analysis of the Avalonia codebase, it appears that the <b>FlyoutProperty</b> is primarily implemented on the <b>Button</b> class.
/// </summary>
/// <typeparam name="T">Button</typeparam>
[ProcessNode(Name = "ButtonFlyout")]
public partial class ButtonFlyout<T> where T : Button
{
    protected readonly Flyout _output = new Flyout();

    protected Optional<T> _input;
    public void SetInput(Optional<T> input)
    {
        if (_input != input)
        {
            _input = input;

            if (_input.HasValue)
            {
                _input.Value?.SetValue(Button.FlyoutProperty, _output);
            }
            else
            {
                _input.Value?.ClearValue(Control.ContextFlyoutProperty);
            }
        }
    }
    public T Output => _input.Value;

    [ImplementProperty("Flyout.ContentProperty")]
    protected Optional<object> _content;
}
