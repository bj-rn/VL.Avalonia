using Avalonia.Input;

namespace VL.Avalonia.Controls;

public static partial class Extensions
{
    /// <summary>Focus InputElement (Control)</summary>
    public static void Focus(InputElement? input, NavigationMethod method = NavigationMethod.Unspecified, KeyModifiers keyModifiers = KeyModifiers.None)
        => input?.Focus(method, keyModifiers);

    /// <inheritdoc cref="InputElement.Focusable" />
    public static bool Focusable(InputElement? input)
        => input?.IsEnabled ?? false;

    /// <inheritdoc cref="InputElement.IsEnabled" />
    public static bool IsEnabled(InputElement? input)
        => input?.IsEnabled ?? false;

    /// <inheritdoc cref="InputElement.Cursor" />
    public static Cursor? Cursor(InputElement? input)
        => input?.Cursor;

    /// <inheritdoc cref="InputElement.IsKeyboardFocusWithin" />
    public static bool IsKeyboardFocusWithin(InputElement? input)
        => input?.IsKeyboardFocusWithin ?? false;

    /// <inheritdoc cref="InputElement.IsFocused" />
    public static bool IsFocused(InputElement? input)
        => input?.IsFocused ?? false;

    /// <inheritdoc cref="InputElement.IsPointerOver" />
    public static bool IsPointerOver(InputElement? input)
        => input?.IsPointerOver ?? false;
}
