using Avalonia.Controls;
using System.Reactive;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls.Buttons;

[ProcessNode(Name = "ButtonSpinner")]
public partial class ButtonSpinnerWrapper : ControlWrapperBase<ButtonSpinner>
{
    [ImplementProperty("Button.ContentProperty", Order = -5)]
    protected Optional<object?> _content;

    protected IChannel<Unit>? _onSpinIncrease;
    public void SetOnSpinIncrease(IChannel<Unit>? onSpinIncrease)
    {
        if (_onSpinIncrease != onSpinIncrease)
        {
            _onSpinIncrease = onSpinIncrease;
        }
    }

    protected IChannel<Unit>? _onSpinDecrease;
    public void SetOnSpinDecrease(IChannel<Unit>? onSpinDecrease)
    {
        if (_onSpinDecrease != onSpinDecrease)
        {
            _onSpinDecrease = onSpinDecrease;
        }
    }

    public ButtonSpinnerWrapper()
    {
        _output.Spin += (s, a) =>
        {
            if (a.Direction == SpinDirection.Increase)
            {
                _onSpinIncrease?.OnNext(new Unit());
            }
            else if (a.Direction == SpinDirection.Decrease)
            {
                _onSpinDecrease?.OnNext(new Unit());
            }
        };
    }

    [ImplementProperty("ButtonSpinner.ButtonSpinnerLocationProperty")]
    protected Optional<Location> _buttonSpinnerLocation;

    [ImplementProperty("ButtonSpinner.ValidSpinDirectionProperty")]
    protected Optional<ValidSpinDirections> _validSpinDirection;

    [ImplementProperty("ButtonSpinner.AllowSpinProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _allowSpin;
}
