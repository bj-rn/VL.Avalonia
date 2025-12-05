using Avalonia.Controls;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls
{
    public static partial class Extensions
    {
        /// <summary>
        /// Gets TreeView selected item.
        /// </summary>
        public static object? SelectedItem(TreeView? treeView) => treeView?.SelectedItem;

        /// <summary>
        /// Gets TreeView selected items.
        /// </summary>
        public static Spread<T> SelectedItems<T>(TreeView? treeView)
        {
            var selectedItems = treeView?.SelectedItems;
            if (selectedItems == null || selectedItems.Count == 0)
                return Spread<T>.Empty;

            return selectedItems.Cast<T>().ToSpread();
        }
    }

    public static class TreeViewItemExtensions
    {
        public static int Level(TreeViewItem? input) => input?.Level ?? -1;
    }
}
