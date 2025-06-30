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
    public static void Focus(this Control? input, NavigationMethod method = NavigationMethod.Unspecified, KeyModifiers keyModifiers = KeyModifiers.None) =>
        input?.Focus(method, keyModifiers);
}
