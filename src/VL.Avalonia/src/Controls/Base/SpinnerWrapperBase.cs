using Avalonia.Controls;
using System.Reactive;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>Spinner</c> is an abstract base class for controls that can increment or decrement values through user interaction. It provides the foundation for controls like NumericUpDown by handling spin events, direction validation, and user input. Spinner controls typically display up/down buttons or respond to mouse wheel events to modify values.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/spinner">Spinner</see>
/// </summary>
[ProcessNode]
public abstract partial class SpinnerWrapperBase<T> : ContentControlWrapperBase<T> where T : Spinner, new()
{
    #region Spin Behavior Properties

    protected IChannel<Unit>? _onSpinIncrease;
    /// <param name="onSpinIncrease">
    /// Triggered on spin increase
    /// </param>
    public void SetOnSpinIncrease(IChannel<Unit>? onSpinIncrease)
    {
        if (_onSpinIncrease != onSpinIncrease)
        {
            _onSpinIncrease = onSpinIncrease;
        }
    }

    protected IChannel<Unit>? _onSpinDecrease;
    /// <param name="onSpinDecrease">
    /// Triggered on spin decrease
    /// </param>
    public void SetOnSpinDecrease(IChannel<Unit>? onSpinDecrease)
    {
        if (_onSpinDecrease != onSpinDecrease)
        {
            _onSpinDecrease = onSpinDecrease;
        }
    }

    protected SpinnerWrapperBase()
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

    [ImplementProperty("Spinner.ValidSpinDirectionProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<ValidSpinDirections> _validSpinDirection;

    #endregion
}
