using Avalonia.Input;

namespace VL.Avalonia.Controls;

public static partial class Extensions
{
    /// <summary>
    /// Focuses control inherited from InputElement
    /// </summary>
    /// <param name="input"></param>
    /// <param name="method"></param>
    /// <param name="keyModifiers"></param>
    /// <returns></returns>
    public static void Focus(InputElement? input, NavigationMethod method = NavigationMethod.Unspecified, KeyModifiers keyModifiers = KeyModifiers.None) =>
        input?.Focus(method, keyModifiers);
}
