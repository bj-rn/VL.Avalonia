using Avalonia.Controls;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="TreeViewItem"/>.
    /// </summary>
    public static class TreeViewItemExtensions
    {
        /// <inheritdoc cref="TreeViewItem.IsExpanded"/>
        public static bool IsExpanded(this TreeViewItem treeViewItem) =>
            treeViewItem?.IsExpanded ?? false;

        /// <inheritdoc cref="TreeViewItem.IsExpanded"/>
        public static void SetIsExpanded(this TreeViewItem treeViewItem, bool isExpanded)
        {
            if (treeViewItem is not null)
                treeViewItem.IsExpanded = isExpanded;
        }

        /// <inheritdoc cref="TreeViewItem.IsSelected"/>
        public static bool IsSelected(this TreeViewItem treeViewItem) =>
            treeViewItem?.IsSelected ?? false;

        /// <inheritdoc cref="TreeViewItem.IsSelected"/>
        public static void SetIsSelected(this TreeViewItem treeViewItem, bool isSelected)
        {
            if (treeViewItem is not null)
                treeViewItem.IsSelected = isSelected;
        }

        /// <inheritdoc cref="TreeViewItem.Level"/>
        public static int Level(this TreeViewItem treeViewItem) => treeViewItem?.Level ?? 0;
    }
}
