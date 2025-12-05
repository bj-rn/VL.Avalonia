using Avalonia;
using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;

namespace VL.Avalonia.Controls.Primitives;

/// <summary>
/// Implements styling functionality for a control.
/// StyledElement base, based on https://github.com/AvaloniaUI/Avalonia/blob/master/src/Avalonia.Base/StyledElement.cs
/// Gonna comment few props for convenience, use Style Setters instead.
/// </summary>
[Obsolete("Overcomplicated")]
public abstract partial class StyledElementWrapperBase<T>
    where T : StyledElement
{
    #region Styling Properties (from StyledElement)

    /// <param name="name">
    /// The name identifier of the control for styling and finding purposes
    /// </param>
    [ImplementProperty(
        "StyledElement.NameProperty",
        Order = -1,
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<string> _name;

    /*
    /// <param name="theme">
    /// The control theme that defines the visual appearance
    /// </param>
    [ImplementProperty("StyledElement.ThemeProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<ControlTheme> _theme;
    */

    /// <param name="classes">
    /// Collection of CSS-like class names for styling purposes
    /// </param>
    [ImplementProperty(
        "StyledElement.ClassesProperty",
        Order = -3,
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<Classes> _classes;

    #endregion
}
