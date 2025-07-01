using Avalonia.Controls;
using Avalonia.Input;

namespace VL.Avalonia.Controls;

/// <summary>
/// Base class for exposing Controls properties and methods
/// </summary>
public static class ControlExtensions
{
    /// <summary>
    /// Focuses control inherited from InputElement
    /// </summary>
    /// <param name="input"></param>
    /// <param name="method"></param>
    /// <param name="keyModifiers"></param>
    /// <returns></returns>
    public static void Focus(this InputElement? input, NavigationMethod method = NavigationMethod.Unspecified, KeyModifiers keyModifiers = KeyModifiers.None) =>
        input?.Focus(method, keyModifiers);

    /// <param name="result">
    /// Whether the menu is currently open.
    /// </param>
    public static void IsOpen(this MenuBase? menu, out bool result) =>
        result = menu?.IsOpen ?? false;
}
