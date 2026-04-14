using System.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Selection;
using AvaSelectionMode = Avalonia.Controls.SelectionMode;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="ListBox"/>.
    /// </summary>
    public static class ListBoxExtensions
    {
        /// <inheritdoc cref="ListBox.Scroll"/>
        public static IScrollable? Scroll(this ListBox listBox) => listBox?.Scroll;

        /// <inheritdoc cref="ListBox.SelectedItems"/>
        public static IList? SelectedItems(this ListBox listBox) => listBox?.SelectedItems;

        /// <inheritdoc cref="ListBox.SelectedItems"/>
        public static void SetSelectedItems(this ListBox listBox, IList? selectedItems)
        {
            if (listBox is not null)
                listBox.SelectedItems = selectedItems;
        }

        /// <inheritdoc cref="ListBox.Selection"/>
        public static ISelectionModel? Selection(this ListBox listBox) => listBox?.Selection;

        /// <inheritdoc cref="ListBox.Selection"/>
        public static void SetSelection(this ListBox listBox, ISelectionModel selection)
        {
            if (listBox is not null && selection is not null)
                listBox.Selection = selection;
        }

        /// <inheritdoc cref="ListBox.SelectionMode"/>
        public static AvaSelectionMode SelectionMode(this ListBox listBox) =>
            listBox?.SelectionMode ?? AvaSelectionMode.Single;

        /// <inheritdoc cref="ListBox.SelectionMode"/>
        public static void SetSelectionMode(this ListBox listBox, AvaSelectionMode selectionMode)
        {
            if (listBox is not null)
                listBox.SelectionMode = selectionMode;
        }

        /// <inheritdoc cref="ListBox.SelectAll"/>
        public static void SelectAll(this ListBox listBox)
        {
            listBox?.SelectAll();
        }

        /// <inheritdoc cref="ListBox.UnselectAll"/>
        public static void UnselectAll(this ListBox listBox)
        {
            listBox?.UnselectAll();
        }
    }
}
