using System.Globalization;
using Avalonia.Controls;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="MaskedTextBox"/>.
    /// </summary>
    public static class MaskedTextBoxExtensions
    {
        /// <inheritdoc cref="MaskedTextBox.AsciiOnly"/>
        public static bool AsciiOnly(this MaskedTextBox maskedTextBox) =>
            maskedTextBox != null ? maskedTextBox.AsciiOnly : false;

        /// <inheritdoc cref="MaskedTextBox.AsciiOnly"/>
        public static void SetAsciiOnly(this MaskedTextBox maskedTextBox, bool asciiOnly)
        {
            if (maskedTextBox is not null)
                maskedTextBox.AsciiOnly = asciiOnly;
        }

        /// <inheritdoc cref="MaskedTextBox.Culture"/>
        public static CultureInfo? Culture(this MaskedTextBox maskedTextBox) =>
            maskedTextBox != null ? maskedTextBox.Culture : CultureInfo.CurrentCulture;

        /// <inheritdoc cref="MaskedTextBox.Culture"/>
        public static void SetCulture(this MaskedTextBox maskedTextBox, CultureInfo? culture)
        {
            if (maskedTextBox is not null)
                maskedTextBox.Culture = culture;
        }

        /// <inheritdoc cref="MaskedTextBox.HidePromptOnLeave"/>
        public static bool HidePromptOnLeave(this MaskedTextBox maskedTextBox) =>
            maskedTextBox != null ? maskedTextBox.HidePromptOnLeave : false;

        /// <inheritdoc cref="MaskedTextBox.HidePromptOnLeave"/>
        public static void SetHidePromptOnLeave(
            this MaskedTextBox maskedTextBox,
            bool hidePromptOnLeave
        )
        {
            if (maskedTextBox is not null)
                maskedTextBox.HidePromptOnLeave = hidePromptOnLeave;
        }

        /// <inheritdoc cref="MaskedTextBox.MaskCompleted"/>
        public static bool? MaskCompleted(this MaskedTextBox maskedTextBox) =>
            maskedTextBox != null ? maskedTextBox.MaskCompleted : null;

        // Note: MaskCompleted is a read-only DirectProperty, so no setter is generated.

        /// <inheritdoc cref="MaskedTextBox.MaskFull"/>
        public static bool? MaskFull(this MaskedTextBox maskedTextBox) =>
            maskedTextBox != null ? maskedTextBox.MaskFull : null;

        // Note: MaskFull is a read-only DirectProperty, so no setter is generated.

        /// <inheritdoc cref="MaskedTextBox.Mask"/>
        public static string? Mask(this MaskedTextBox maskedTextBox) =>
            maskedTextBox != null ? maskedTextBox.Mask : string.Empty;

        /// <inheritdoc cref="MaskedTextBox.Mask"/>
        public static void SetMask(this MaskedTextBox maskedTextBox, string? mask)
        {
            if (maskedTextBox is not null)
                maskedTextBox.Mask = mask;
        }

        /// <inheritdoc cref="MaskedTextBox.PromptChar"/>
        public static char PromptChar(this MaskedTextBox maskedTextBox) =>
            maskedTextBox != null ? maskedTextBox.PromptChar : '_';

        /// <inheritdoc cref="MaskedTextBox.PromptChar"/>
        public static void SetPromptChar(this MaskedTextBox maskedTextBox, char promptChar)
        {
            if (maskedTextBox is not null)
                maskedTextBox.PromptChar = promptChar;
        }

        /// <inheritdoc cref="MaskedTextBox.ResetOnPrompt"/>
        public static bool ResetOnPrompt(this MaskedTextBox maskedTextBox) =>
            maskedTextBox != null ? maskedTextBox.ResetOnPrompt : true;

        /// <inheritdoc cref="MaskedTextBox.ResetOnPrompt"/>
        public static void SetResetOnPrompt(this MaskedTextBox maskedTextBox, bool resetOnPrompt)
        {
            if (maskedTextBox is not null)
                maskedTextBox.ResetOnPrompt = resetOnPrompt;
        }

        /// <inheritdoc cref="MaskedTextBox.ResetOnSpace"/>
        public static bool ResetOnSpace(this MaskedTextBox maskedTextBox) =>
            maskedTextBox != null ? maskedTextBox.ResetOnSpace : true;

        /// <inheritdoc cref="MaskedTextBox.ResetOnSpace"/>
        public static void SetResetOnSpace(this MaskedTextBox maskedTextBox, bool resetOnSpace)
        {
            if (maskedTextBox is not null)
                maskedTextBox.ResetOnSpace = resetOnSpace;
        }
    }
}
