using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls.Base;

/// <summary>
/// Base class for Flyouts.
/// Implements: <c>Output</c>, <c>Style</c>, <c>Classes</c>, <c>Name</c>.
/// </summary>
/// <typeparam name="T"></typeparam>
[ProcessNode]
public abstract class FlyoutWrapperBase<T, T1> where T : Button, new() where T1 : PopupFlyoutBase, new()
{
    protected readonly T1 _output = new();

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
                _input.Value?.ClearValue(Button.FlyoutProperty);
            }
        }
    }
    public T Output => _input.Value;

    //protected Optional<IAvaloniaStyle> _style;
    //[Fragment(Order = -3)]
    //public void SetStyle(Optional<IAvaloniaStyle> style)
    //{
    //    if (_style != style)
    //    {
    //        _style = style;
    //        _output.TryUpdateStyles(style.Value);
    //    }
    //}

    //protected Optional<string> _classes;
    //[Fragment(Order = -2)]
    //public void SetClasses([Pin(Visibility = Model.PinVisibility.Optional)] Optional<string> classes)
    //{
    //    if (_classes != classes)
    //    {
    //        _classes = classes;
    //        _output.TryUpdateClasses(classes.Value);
    //    }
    //}

    protected Optional<string> _name;
    [Fragment(Order = -1)]
    public void SetName([Pin(Visibility = Model.PinVisibility.Optional)] Optional<string> name)
    {
        if (_name != name)
        {
            _name = name;

            if (name.HasValue)
            {
                _output.SetValue(Control.NameProperty, name.Value);
            }
            else
            {
                _output.ClearValue(Control.NameProperty);
            }
        }
    }

    protected Optional<bool> _isEnabled;
    [Fragment(Order = 9999)]
    public void SetEnabled([Pin(Visibility = Model.PinVisibility.Optional)] Optional<bool> isEnabled)
    {
        if (_isEnabled != isEnabled)
        {
            _isEnabled = isEnabled;
            if (isEnabled.HasValue)
            {
                _output.SetValue(Control.IsEnabledProperty, isEnabled.Value);
            }
            else
            {
                _output.ClearValue(Control.IsEnabledProperty);
            }
        }
    }
}
