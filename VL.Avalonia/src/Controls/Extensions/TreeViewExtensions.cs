using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
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
            treeView?.AutoScrollToSelectedItem ?? false;

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
        public static object? SelectedItem(this TreeView treeView) => treeView?.SelectedItem;

        /// <inheritdoc cref="TreeView.SelectedItem"/>
        public static void SetSelectedItem(this TreeView treeView, object? selectedItem)
        {
            if (treeView is not null)
                treeView.SelectedItem = selectedItem;
        }

        /// <inheritdoc cref="TreeView.SelectedItems"/>
        public static IReadOnlyList<T> SelectedItems<T>(this TreeView treeView)
        {
            var items = treeView?.SelectedItems;

            if (items == null || items.Count == 0)
                return Array.Empty<T>();

            return items.OfType<T>().ToArray();
        }

        /// <inheritdoc cref="TreeView.SelectedItems"/>
        public static void SetSelectedItems<T>(
            this TreeView treeView,
            IReadOnlyList<T> selectedItems
        )
        {
            if (treeView is not null && selectedItems is not null)
            {
                var mutableList = new AvaloniaList<object>();

                for (int i = 0; i < selectedItems.Count; i++)
                {
                    if (selectedItems[i] is not null)
                    {
                        mutableList.Add(selectedItems[i]!);
                    }
                }

                treeView.SelectedItems = mutableList;
            }
        }

        /// <inheritdoc cref="TreeView.SelectionMode"/>
        public static AvaSelectionMode SelectionMode(this TreeView treeView) =>
            treeView?.SelectionMode ?? AvaSelectionMode.Single;

        /// <inheritdoc cref="TreeView.SelectionMode"/>
        public static void SetSelectionMode(this TreeView treeView, AvaSelectionMode selectionMode)
        {
            if (treeView is not null)
                treeView.SelectionMode = selectionMode;
        }

        /// <inheritdoc cref="TreeView.ItemContainerGenerator"/>
        public static TreeItemContainerGenerator? ItemContainerGenerator(this TreeView treeView) =>
            treeView?.ItemContainerGenerator;

        /// <inheritdoc cref="TreeView.ExpandSubTree(TreeViewItem)"/>
        public static void ExpandSubTree(this TreeView treeView, TreeViewItem item)
        {
            if (treeView is not null && item is not null)
                treeView.ExpandSubTree(item);
        }

        /// <inheritdoc cref="TreeView.CollapseSubTree(TreeViewItem)"/>
        public static void CollapseSubTree(this TreeView treeView, TreeViewItem item)
        {
            if (treeView is not null && item is not null)
                treeView.CollapseSubTree(item);
        }

        /// <inheritdoc cref="TreeView.SelectAll"/>
        public static void SelectAll(this TreeView treeView)
        {
            treeView?.SelectAll();
        }

        /// <inheritdoc cref="TreeView.UnselectAll"/>
        public static void UnselectAll(this TreeView treeView)
        {
            treeView?.UnselectAll();
        }

        /// <inheritdoc cref="TreeView.GetRealizedTreeContainers"/>
        public static IReadOnlyList<Control> GetRealizedTreeContainers(this TreeView treeView)
        {
            return treeView?.GetRealizedTreeContainers().ToSpread() ?? Spread<Control>.Empty;
        }

        /// <inheritdoc cref="TreeView.TreeContainerFromItem(object)"/>
        public static Control? TreeContainerFromItem(this TreeView treeView, object item)
        {
            if (treeView is not null && item is not null)
            {
                return treeView.TreeContainerFromItem(item);
            }
            return null;
        }

        /// <inheritdoc cref="TreeView.TreeItemFromContainer(Control)"/>
        public static object? TreeItemFromContainer(this TreeView treeView, Control container)
        {
            if (treeView is not null && container is not null)
            {
                return treeView.TreeItemFromContainer(container);
            }
            return null;
        }
    }
}
