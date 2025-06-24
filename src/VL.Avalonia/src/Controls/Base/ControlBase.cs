using Avalonia.Controls;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;


/// <summary>
/// Base class for all controls, seems more flexible then using CodeGen for each prop.
/// Implements: <c>Output</c>, <c>Style</c>, <c>Classes</c>, <c>Name</c>.
/// </summary>
/// <typeparam name="T"></typeparam>
[ProcessNode]
public abstract class ControlWrapperBase<T> where T : Control, new()
{
    protected readonly T _output = new();
    public T Output => _output;

    protected Optional<IAvaloniaStyle> _style;
    [Fragment(Order = -3)]
    public void SetStyle(Optional<IAvaloniaStyle> style)
    {
        if (_style != style)
        {
            _style = style;
            _output.TryUpdateStyles(style.Value);
        }
    }

    protected Optional<string> _classes;
    [Fragment(Order = -2)]
    public void SetClasses([Pin(Visibility = Model.PinVisibility.Optional)] Optional<string> classes)
    {
        if (_classes != classes)
        {
            _classes = classes;
            _output.TryUpdateClasses(classes.Value);
        }
    }

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

}
