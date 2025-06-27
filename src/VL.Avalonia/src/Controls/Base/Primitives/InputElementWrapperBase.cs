using Avalonia.Input;
using VL.Avalonia.Attributes;
using VL.Core;

namespace VL.Avalonia.Controls.Primitives;

/// <summary>
/// Implements input-related functionality for a control.
/// InputElement base, based on https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Base/Input/InputElement.cs
/// Gonna comment few props for convenience, use Style Setters instead.
/// </summary>
[Obsolete("Overcomplicated")]
public abstract partial class InputElementWrapperBase<T> : LayoutableWrapperBase<T> where T : InputElement
{
    #region Input Properties (from InputElement)

    /*
    /// <param name="focusable">
    /// Whether the control can receive keyboard focus
    /// </param>
    [ImplementProperty("InputElement.FocusableProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _focusable;
    */

    /// <param name="isEnabled">
    /// Whether the control responds to user interaction
    /// </param>
    [ImplementProperty("InputElement.IsEnabledProperty", Order = 1000, PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isEnabled;

    /*
    /// <param name="cursor">
    /// The cursor that appears when the mouse hovers over the control
    /// </param>
    [ImplementProperty("InputElement.CursorProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<Cursor> _cursor;
    */

    /*
    /// <param name="isHitTestVisible">
    /// Whether the control can be hit by mouse events
    /// </param>
    [ImplementProperty("InputElement.IsHitTestVisibleProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isHitTestVisible;
    */

    /*
    /// <param name="isTabStop">
    /// Whether the control is included in tab navigation
    /// </param>
    [ImplementProperty("InputElement.IsTabStopProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isTabStop;
    */

    /*
    /// <param name="tabIndex">
    /// The tab order index for keyboard navigation
    /// </param>
    [ImplementProperty("InputElement.TabIndexProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _tabIndex;
    */

    #endregion
}
