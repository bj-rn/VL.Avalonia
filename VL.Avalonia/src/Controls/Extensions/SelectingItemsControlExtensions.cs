using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="SelectingItemsControl"/>.
    /// </summary>
    public static class SelectingItemsControlExtensions
    {
        /// <inheritdoc cref="SelectingItemsControl.AutoScrollToSelectedItem"/>
        public static bool AutoScrollToSelectedItem(
            this SelectingItemsControl selectingItemsControl
        ) => selectingItemsControl?.AutoScrollToSelectedItem ?? true; // Default is true

        /// <inheritdoc cref="SelectingItemsControl.AutoScrollToSelectedItem"/>
        public static void SetAutoScrollToSelectedItem(
            this SelectingItemsControl selectingItemsControl,
            bool autoScrollToSelectedItem
        )
        {
            if (selectingItemsControl is not null)
                selectingItemsControl.AutoScrollToSelectedItem = autoScrollToSelectedItem;
        }

        /// <inheritdoc cref="SelectingItemsControl.SelectedIndex"/>
        public static int SelectedIndex(this SelectingItemsControl selectingItemsControl) =>
            selectingItemsControl?.SelectedIndex ?? -1;

        /// <inheritdoc cref="SelectingItemsControl.SelectedIndex"/>
        public static void SetSelectedIndex(
            this SelectingItemsControl selectingItemsControl,
            int selectedIndex
        )
        {
            if (selectingItemsControl is not null)
                selectingItemsControl.SelectedIndex = selectedIndex;
        }

        /// <inheritdoc cref="SelectingItemsControl.SelectedItem"/>
        public static object? SelectedItem(this SelectingItemsControl selectingItemsControl) =>
            selectingItemsControl?.SelectedItem;

        /// <inheritdoc cref="SelectingItemsControl.SelectedItem"/>
        public static void SetSelectedItem(
            this SelectingItemsControl selectingItemsControl,
            object? selectedItem
        )
        {
            if (selectingItemsControl is not null)
                selectingItemsControl.SelectedItem = selectedItem;
        }

        /// <inheritdoc cref="SelectingItemsControl.SelectedValue"/>
        public static object? SelectedValue(this SelectingItemsControl selectingItemsControl) =>
            selectingItemsControl?.SelectedValue;

        /// <inheritdoc cref="SelectingItemsControl.SelectedValue"/>
        public static void SetSelectedValue(
            this SelectingItemsControl selectingItemsControl,
            object? selectedValue
        )
        {
            if (selectingItemsControl is not null)
                selectingItemsControl.SelectedValue = selectedValue;
        }

        /// <inheritdoc cref="SelectingItemsControl.SelectedValueBinding"/>
        public static IBinding? SelectedValueBinding(
            this SelectingItemsControl selectingItemsControl
        ) => selectingItemsControl?.SelectedValueBinding;

        /// <inheritdoc cref="SelectingItemsControl.SelectedValueBinding"/>
        public static void SetSelectedValueBinding(
            this SelectingItemsControl selectingItemsControl,
            IBinding? selectedValueBinding
        )
        {
            if (selectingItemsControl is not null)
                selectingItemsControl.SelectedValueBinding = selectedValueBinding;
        }

        /// <inheritdoc cref="SelectingItemsControl.IsTextSearchEnabled"/>
        public static bool IsTextSearchEnabled(this SelectingItemsControl selectingItemsControl) =>
            selectingItemsControl?.IsTextSearchEnabled ?? false;

        /// <inheritdoc cref="SelectingItemsControl.IsTextSearchEnabled"/>
        public static void SetIsTextSearchEnabled(
            this SelectingItemsControl selectingItemsControl,
            bool isTextSearchEnabled
        )
        {
            if (selectingItemsControl is not null)
                selectingItemsControl.IsTextSearchEnabled = isTextSearchEnabled;
        }

        /// <inheritdoc cref="SelectingItemsControl.WrapSelection"/>
        public static bool WrapSelection(this SelectingItemsControl selectingItemsControl) =>
            selectingItemsControl?.WrapSelection ?? false;

        /// <inheritdoc cref="SelectingItemsControl.WrapSelection"/>
        public static void SetWrapSelection(
            this SelectingItemsControl selectingItemsControl,
            bool wrapSelection
        )
        {
            if (selectingItemsControl is not null)
                selectingItemsControl.WrapSelection = wrapSelection;
        }

        /// <inheritdoc cref="SelectingItemsControl.GetIsSelected(Control)"/>
        public static bool IsSelected(this Control control)
        {
            if (control is not null)
                return SelectingItemsControl.GetIsSelected(control);

            return false;
        }

        /// <inheritdoc cref="SelectingItemsControl.SetIsSelected(Control, bool)"/>
        public static void SetIsSelected(this Control control, bool isSelected)
        {
            if (control is not null)
                SelectingItemsControl.SetIsSelected(control, isSelected);
        }
    }
}
