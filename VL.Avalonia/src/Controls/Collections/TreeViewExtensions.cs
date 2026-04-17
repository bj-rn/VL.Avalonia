using Avalonia.Collections;
using Avalonia.Controls;
using VL.Lib.Collections;
using AvaSelectionMode = Avalonia.Controls.SelectionMode;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="TreeView"/>.
    /// </summary>
    public static class TreeViewExtensions
    {
        /// <inheritdoc cref="TreeView.AutoScrollToSelectedItem"/>
        public static bool AutoScrollToSelectedItem(this TreeView treeView) =>
            treeView != null ? treeView.AutoScrollToSelectedItem : false;

        /// <inheritdoc cref="TreeView.AutoScrollToSelectedItem"/>
        public static void SetAutoScrollToSelectedItem(
            this TreeView treeView,
            bool autoScrollToSelectedItem
        )
        {
            if (treeView is not null)
                treeView.AutoScrollToSelectedItem = autoScrollToSelectedItem;
        }

        /// <inheritdoc cref="TreeView.SelectedItem"/>
        public static object? SelectedItem(this TreeView treeView) =>
            treeView != null ? treeView.SelectedItem : null;

        /// <inheritdoc cref="TreeView.SelectedItem"/>
        public static void SetSelectedItem(this TreeView treeView, object? selectedItem)
        {
            if (treeView is not null)
                treeView.SelectedItem = selectedItem;
        }

        /// <inheritdoc cref="TreeView.SelectionMode"/>
        public static AvaSelectionMode SelectionMode(this TreeView treeView) =>
            treeView != null ? treeView.SelectionMode : AvaSelectionMode.Single;

        /// <inheritdoc cref="TreeView.SelectionMode"/>
        public static void SetSelectionMode(this TreeView treeView, AvaSelectionMode selectionMode)
        {
            if (treeView is not null)
                treeView.SelectionMode = selectionMode;
        }

        /// <inheritdoc cref="TreeView.SelectedItems"/>
        public static IReadOnlyList<T> SelectedItems<T>(this TreeView treeView)
        {
            var items = treeView?.SelectedItems;

            if (items == null || items.Count == 0)
                return Spread<T>.Empty;

            return items.OfType<T>().ToSpread();
        }

        /// <inheritdoc cref="TreeView.SelectedItems"/>
        public static void SetSelectedItems<T>(
            this TreeView treeView,
            IReadOnlyList<T> selectedItems
        )
        {
            if (treeView is not null)
            {
                var mutableList = new AvaloniaList<object>();

                foreach (var item in selectedItems ?? [])
                {
                    // TODO: Not sure if we should filter out nulls here.
                    if (item is not null)
                        mutableList.Add(item);
                }

                treeView.SelectedItems = mutableList;
            }
        }
    }
}
