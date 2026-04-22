using System.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Styling;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="MenuFlyout"/>.
    /// </summary>
    public static class MenuFlyoutExtensions
    {
        /// <inheritdoc cref="MenuFlyout.FlyoutPresenterClasses"/>
        public static Classes? FlyoutPresenterClasses(this MenuFlyout menuFlyout) =>
            menuFlyout != null ? menuFlyout.FlyoutPresenterClasses : null;

        /// <inheritdoc cref="MenuFlyout.Items"/>
        public static IReadOnlyList<T> Items<T>(this MenuFlyout menuFlyout)
        {
            var items = menuFlyout?.Items;

            if (items == null || items.Count == 0)
                return Spread<T>.Empty;

            return items.OfType<T>().ToSpread();
        }

        /// <inheritdoc cref="MenuFlyout.ItemsSource"/>
        public static IEnumerable? ItemsSource(this MenuFlyout menuFlyout) =>
            menuFlyout != null ? menuFlyout.ItemsSource : null;

        /// <inheritdoc cref="MenuFlyout.ItemsSource"/>
        public static void SetItemsSource(this MenuFlyout menuFlyout, IEnumerable? itemsSource)
        {
            if (menuFlyout is not null)
                menuFlyout.ItemsSource = itemsSource;
        }

        /// <inheritdoc cref="MenuFlyout.ItemTemplate"/>
        public static IDataTemplate? ItemTemplate(this MenuFlyout menuFlyout) =>
            menuFlyout != null ? menuFlyout.ItemTemplate : null;

        /// <inheritdoc cref="MenuFlyout.ItemTemplate"/>
        public static void SetItemTemplate(this MenuFlyout menuFlyout, IDataTemplate? itemTemplate)
        {
            if (menuFlyout is not null)
                menuFlyout.ItemTemplate = itemTemplate;
        }

        /// <inheritdoc cref="MenuFlyout.ItemContainerTheme"/>
        public static ControlTheme? ItemContainerTheme(this MenuFlyout menuFlyout) =>
            menuFlyout != null ? menuFlyout.ItemContainerTheme : null;

        /// <inheritdoc cref="MenuFlyout.ItemContainerTheme"/>
        public static void SetItemContainerTheme(
            this MenuFlyout menuFlyout,
            ControlTheme? itemContainerTheme
        )
        {
            if (menuFlyout is not null)
                menuFlyout.ItemContainerTheme = itemContainerTheme;
        }

        /// <inheritdoc cref="MenuFlyout.FlyoutPresenterTheme"/>
        public static ControlTheme? FlyoutPresenterTheme(this MenuFlyout menuFlyout) =>
            menuFlyout != null ? menuFlyout.FlyoutPresenterTheme : null;

        /// <inheritdoc cref="MenuFlyout.FlyoutPresenterTheme"/>
        public static void SetFlyoutPresenterTheme(
            this MenuFlyout menuFlyout,
            ControlTheme? flyoutPresenterTheme
        )
        {
            if (menuFlyout is not null)
                menuFlyout.FlyoutPresenterTheme = flyoutPresenterTheme;
        }
    }
}
