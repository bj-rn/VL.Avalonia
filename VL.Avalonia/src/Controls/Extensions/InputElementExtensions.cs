using Avalonia.Input;

namespace VL.Avalonia.Input
{
    /// <summary>
    /// Extension methods for <see cref="InputElement"/>.
    /// </summary>
    public static class InputElementExtensions
    {
        /// <inheritdoc cref="InputElement.Focus(NavigationMethod, KeyModifiers)"/>
        public static void Focus(this InputElement input)
        {
            if (input is not null)
                input.Focus();
        }

        /// <inheritdoc cref="InputElement.Focusable"/>
        public static bool Focusable(this InputElement input) => input?.Focusable ?? false;

        /// <inheritdoc cref="InputElement.Focusable"/>
        public static void SetIsFocusable(this InputElement input, bool focusable = true)
        {
            if (input is not null)
                input.Focusable = focusable;
        }

        /// <inheritdoc cref="InputElement.IsEnabled"/>
        public static bool IsEnabled(this InputElement input) => input?.IsEnabled ?? false;

        /// <inheritdoc cref="InputElement.IsEnabled"/>
        public static void SetIsEnabled(this InputElement input, bool isEnabled = true)
        {
            if (input is not null)
                input.IsEnabled = isEnabled;
        }

        /// <inheritdoc cref="InputElement.IsEffectivelyEnabled"/>
        public static bool IsEffectivelyEnabled(this InputElement input) =>
            input?.IsEffectivelyEnabled ?? false;

        /// <inheritdoc cref="InputElement.Cursor"/>
        public static Cursor? Cursor(this InputElement input) => input?.Cursor;

        /// <inheritdoc cref="InputElement.Cursor"/>
        public static void SetCursor(this InputElement input, Cursor cursor)
        {
            if (input is not null)
                input.Cursor = cursor;
        }

        /// <inheritdoc cref="InputElement.IsHitTestVisible"/>
        public static bool IsHitTestVisible(this InputElement input) =>
            input?.IsHitTestVisible ?? false;

        /// <inheritdoc cref="InputElement.IsHitTestVisible"/>
        public static void SetIsHitTestVisible(this InputElement input, bool isHitTestVisible)
        {
            if (input is not null)
                input.IsHitTestVisible = isHitTestVisible;
        }

        /// <inheritdoc cref="InputElement.IsTabStop"/>
        public static bool IsTabStop(this InputElement input) => input?.IsTabStop ?? false;

        /// <inheritdoc cref="InputElement.IsTabStop"/>
        public static void SetIsTabStop(this InputElement input, bool isTabStop)
        {
            if (input is not null)
                input.IsTabStop = isTabStop;
        }

        /// <inheritdoc cref="InputElement.TabIndex"/>
        public static int TabIndex(this InputElement input) => input?.TabIndex ?? 0;

        /// <inheritdoc cref="InputElement.TabIndex"/>
        public static void SetTabIndex(this InputElement input, int tabIndex)
        {
            if (input is not null)
                input.TabIndex = tabIndex;
        }

        /// <inheritdoc cref="InputElement.IsFocused"/>
        public static bool IsFocused(this InputElement input) => input?.IsFocused ?? false;

        /// <inheritdoc cref="InputElement.IsKeyboardFocusWithin"/>
        public static bool IsKeyboardFocusWithin(this InputElement input) =>
            input?.IsKeyboardFocusWithin ?? false;

        /// <inheritdoc cref="InputElement.IsPointerOver"/>
        public static bool IsPointerOver(this InputElement input) => input?.IsPointerOver ?? false;
    }
}
