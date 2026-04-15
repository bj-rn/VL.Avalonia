using System.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Styling;

namespace VL.Avalonia.Controls.Base.Items
{
    /// <summary>
    /// Extension methods for <see cref="ItemsControl"/>.
    /// </summary>
    public static class ItemsControlExtensions
    {
        /// <inheritdoc cref="ItemsControl.ItemContainerGenerator"/>
        public static ItemContainerGenerator? ItemContainerGenerator(
            this ItemsControl itemsControl
        ) => itemsControl?.ItemContainerGenerator;

        /// <inheritdoc cref="ItemsControl.Items"/>
        public static ItemCollection? Items(this ItemsControl itemsControl) => itemsControl?.Items;

        /// <inheritdoc cref="ItemsControl.ItemContainerTheme"/>
        public static ControlTheme? ItemContainerTheme(this ItemsControl itemsControl) =>
            itemsControl?.ItemContainerTheme;

        /// <inheritdoc cref="ItemsControl.ItemContainerTheme"/>
        public static void SetItemContainerTheme(
            this ItemsControl itemsControl,
            ControlTheme? itemContainerTheme
        )
        {
            if (itemsControl is not null)
                itemsControl.ItemContainerTheme = itemContainerTheme;
        }

        /// <inheritdoc cref="ItemsControl.ItemCount"/>
        public static int ItemCount(this ItemsControl itemsControl) => itemsControl?.ItemCount ?? 0;

        /// <inheritdoc cref="ItemsControl.ItemsPanel"/>
        public static ITemplate<Panel?>? ItemsPanel(this ItemsControl itemsControl) =>
            itemsControl?.ItemsPanel;

        /// <inheritdoc cref="ItemsControl.ItemsPanel"/>
        public static void SetItemsPanel(
            this ItemsControl itemsControl,
            ITemplate<Panel?>? itemsPanel
        )
        {
            if (itemsControl is not null)
                itemsControl.ItemsPanel = itemsPanel;
        }

        /// <inheritdoc cref="ItemsControl.ItemsSource"/>
        public static IEnumerable? ItemsSource(this ItemsControl itemsControl) =>
            itemsControl?.ItemsSource;

        /// <inheritdoc cref="ItemsControl.ItemsSource"/>
        public static void SetItemsSource(this ItemsControl itemsControl, IEnumerable? itemsSource)
        {
            if (itemsControl is not null)
                itemsControl.ItemsSource = itemsSource;
        }

        /// <inheritdoc cref="ItemsControl.ItemTemplate"/>
        public static IDataTemplate? ItemTemplate(this ItemsControl itemsControl) =>
            itemsControl?.ItemTemplate;

        /// <inheritdoc cref="ItemsControl.ItemTemplate"/>
        public static void SetItemTemplate(
            this ItemsControl itemsControl,
            IDataTemplate? itemTemplate
        )
        {
            if (itemsControl is not null)
                itemsControl.ItemTemplate = itemTemplate;
        }

        /// <inheritdoc cref="ItemsControl.DisplayMemberBinding"/>
        public static IBinding? DisplayMemberBinding(this ItemsControl itemsControl) =>
            itemsControl?.DisplayMemberBinding;

        /// <inheritdoc cref="ItemsControl.DisplayMemberBinding"/>
        public static void SetDisplayMemberBinding(
            this ItemsControl itemsControl,
            IBinding? displayMemberBinding
        )
        {
            if (itemsControl is not null)
                itemsControl.DisplayMemberBinding = displayMemberBinding;
        }

        /// <inheritdoc cref="ItemsControl.Presenter"/>
        public static ItemsPresenter? Presenter(this ItemsControl itemsControl) =>
            itemsControl?.Presenter;

        /// <inheritdoc cref="ItemsControl.ItemsPanelRoot"/>
        public static Panel? ItemsPanelRoot(this ItemsControl itemsControl) =>
            itemsControl?.ItemsPanelRoot;

        /// <inheritdoc cref="ItemsControl.ItemsView"/>
        public static ItemsSourceView? ItemsView(this ItemsControl itemsControl) =>
            itemsControl?.ItemsView;

        /// <inheritdoc cref="ItemsControl.ContainerFromIndex(int)"/>
        public static Control? ContainerFromIndex(this ItemsControl itemsControl, int index)
        {
            return itemsControl?.ContainerFromIndex(index);
        }

        /// <inheritdoc cref="ItemsControl.ContainerFromItem(object)"/>
        public static Control? ContainerFromItem(this ItemsControl itemsControl, object item)
        {
            if (itemsControl is not null && item is not null)
            {
                return itemsControl.ContainerFromItem(item);
            }
            return null;
        }

        /// <inheritdoc cref="ItemsControl.IndexFromContainer(Control)"/>
        public static int IndexFromContainer(this ItemsControl itemsControl, Control container)
        {
            if (itemsControl is not null && container is not null)
            {
                return itemsControl.IndexFromContainer(container);
            }
            return -1;
        }

        /// <inheritdoc cref="ItemsControl.ItemFromContainer(Control)"/>
        public static object? ItemFromContainer(this ItemsControl itemsControl, Control container)
        {
            if (itemsControl is not null && container is not null)
            {
                return itemsControl.ItemFromContainer(container);
            }
            return null;
        }

        /// <inheritdoc cref="ItemsControl.GetRealizedContainers"/>
        public static IEnumerable<Control> GetRealizedContainers(this ItemsControl itemsControl)
        {
            return itemsControl?.GetRealizedContainers() ?? System.Array.Empty<Control>();
        }

        /// <inheritdoc cref="ItemsControl.ScrollIntoView(int)"/>
        public static void ScrollIntoView(this ItemsControl itemsControl, int index)
        {
            itemsControl?.ScrollIntoView(index);
        }

        /// <inheritdoc cref="ItemsControl.ScrollIntoView(object)"/>
        public static void ScrollIntoView(this ItemsControl itemsControl, object item)
        {
            if (itemsControl is not null && item is not null)
            {
                itemsControl.ScrollIntoView(item);
            }
        }

        /// <summary>
        /// Returns the <see cref="ItemsControl"/> that owns the specified container control.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns>The owning <see cref="ItemsControl"/> or null if the control is not an items container.</returns>
        public static ItemsControl? GetOwningItemsControl(this Control container)
        {
            if (container is not null)
            {
                return ItemsControl.ItemsControlFromItemContainer(container);
            }
            return null;
        }
    }
}
