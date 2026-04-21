using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Layout;
using Avalonia.Media;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="ComboBox"/>.
    /// </summary>
    public static class ComboBoxExtensions
    {
        /// <inheritdoc cref="ComboBox.IsDropDownOpen"/>
        public static bool IsDropDownOpen(this ComboBox comboBox) =>
            comboBox != null ? comboBox.IsDropDownOpen : false;

        /// <inheritdoc cref="ComboBox.IsDropDownOpen"/>
        public static void SetIsDropDownOpen(this ComboBox comboBox, bool isDropDownOpen)
        {
            if (comboBox is not null)
                comboBox.IsDropDownOpen = isDropDownOpen;
        }

        /// <inheritdoc cref="ComboBox.MaxDropDownHeight"/>
        public static float MaxDropDownHeight(this ComboBox comboBox) =>
            comboBox != null ? (float)comboBox.MaxDropDownHeight : 200f;

        /// <inheritdoc cref="ComboBox.MaxDropDownHeight"/>
        public static void SetMaxDropDownHeight(this ComboBox comboBox, float maxDropDownHeight)
        {
            if (comboBox is not null)
                comboBox.MaxDropDownHeight = maxDropDownHeight;
        }

        /// <inheritdoc cref="ComboBox.SelectionBoxItem"/>
        public static object? SelectionBoxItem(this ComboBox comboBox) =>
            comboBox != null ? comboBox.SelectionBoxItem : null;

        // Note: SetSelectionBoxItem is omitted because the SelectionBoxItem property has a protected setter in ComboBox.

        /// <inheritdoc cref="ComboBox.PlaceholderText"/>
        public static string? PlaceholderText(this ComboBox comboBox) =>
            comboBox != null ? comboBox.PlaceholderText : null;

        /// <inheritdoc cref="ComboBox.PlaceholderText"/>
        public static void SetPlaceholderText(this ComboBox comboBox, string? placeholderText)
        {
            if (comboBox is not null)
                comboBox.PlaceholderText = placeholderText;
        }

        /// <inheritdoc cref="ComboBox.PlaceholderForeground"/>
        public static IBrush? PlaceholderForeground(this ComboBox comboBox) =>
            comboBox != null ? comboBox.PlaceholderForeground : null;

        /// <inheritdoc cref="ComboBox.PlaceholderForeground"/>
        public static void SetPlaceholderForeground(
            this ComboBox comboBox,
            IBrush? placeholderForeground
        )
        {
            if (comboBox is not null)
                comboBox.PlaceholderForeground = placeholderForeground;
        }

        /// <inheritdoc cref="ComboBox.HorizontalContentAlignment"/>
        public static HorizontalAlignment HorizontalContentAlignment(this ComboBox comboBox) =>
            comboBox != null ? comboBox.HorizontalContentAlignment : default(HorizontalAlignment);

        /// <inheritdoc cref="ComboBox.HorizontalContentAlignment"/>
        public static void SetHorizontalContentAlignment(
            this ComboBox comboBox,
            HorizontalAlignment horizontalContentAlignment
        )
        {
            if (comboBox is not null)
                comboBox.HorizontalContentAlignment = horizontalContentAlignment;
        }

        /// <inheritdoc cref="ComboBox.VerticalContentAlignment"/>
        public static VerticalAlignment VerticalContentAlignment(this ComboBox comboBox) =>
            comboBox != null ? comboBox.VerticalContentAlignment : default(VerticalAlignment);

        /// <inheritdoc cref="ComboBox.VerticalContentAlignment"/>
        public static void SetVerticalContentAlignment(
            this ComboBox comboBox,
            VerticalAlignment verticalContentAlignment
        )
        {
            if (comboBox is not null)
                comboBox.VerticalContentAlignment = verticalContentAlignment;
        }

        /// <inheritdoc cref="ComboBox.SelectionBoxItemTemplate"/>
        public static IDataTemplate? SelectionBoxItemTemplate(this ComboBox comboBox) =>
            comboBox != null ? comboBox.SelectionBoxItemTemplate : null;

        /// <inheritdoc cref="ComboBox.SelectionBoxItemTemplate"/>
        public static void SetSelectionBoxItemTemplate(
            this ComboBox comboBox,
            IDataTemplate? selectionBoxItemTemplate
        )
        {
            if (comboBox is not null)
                comboBox.SelectionBoxItemTemplate = selectionBoxItemTemplate;
        }

        /// <inheritdoc cref="ComboBox.Clear"/>
        public static void Clear(this ComboBox comboBox)
        {
            comboBox?.Clear();
        }

        // AttachedProperties:
        // No attached properties were found in the provided file.
    }
}
