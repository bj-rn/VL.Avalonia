using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Input;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="MenuItem"/>.
    /// </summary>
    public static class MenuItemExtensions
    {
        /// <inheritdoc cref="MenuItem.Command"/>
        public static ICommand? Command(this MenuItem menuItem) =>
            menuItem != null ? menuItem.Command : null;

        /// <inheritdoc cref="MenuItem.Command"/>
        public static void SetCommand(this MenuItem menuItem, ICommand? command)
        {
            if (menuItem is not null)
                menuItem.Command = command;
        }

        /// <inheritdoc cref="MenuItem.HotKey"/>
        public static KeyGesture? HotKey(this MenuItem menuItem) =>
            menuItem != null ? menuItem.HotKey : null;

        /// <inheritdoc cref="MenuItem.HotKey"/>
        public static void SetHotKey(this MenuItem menuItem, KeyGesture? hotKey)
        {
            if (menuItem is not null)
                menuItem.HotKey = hotKey;
        }

        /// <inheritdoc cref="MenuItem.CommandParameter"/>
        public static object? CommandParameter(this MenuItem menuItem) =>
            menuItem != null ? menuItem.CommandParameter : null;

        /// <inheritdoc cref="MenuItem.CommandParameter"/>
        public static void SetCommandParameter(this MenuItem menuItem, object? commandParameter)
        {
            if (menuItem is not null)
                menuItem.CommandParameter = commandParameter;
        }

        /// <inheritdoc cref="MenuItem.Icon"/>
        public static object? Icon(this MenuItem menuItem) =>
            menuItem != null ? menuItem.Icon : null;

        /// <inheritdoc cref="MenuItem.Icon"/>
        public static void SetIcon(this MenuItem menuItem, object? icon)
        {
            if (menuItem is not null)
                menuItem.Icon = icon;
        }

        /// <inheritdoc cref="MenuItem.InputGesture"/>
        public static KeyGesture? InputGesture(this MenuItem menuItem) =>
            menuItem != null ? menuItem.InputGesture : null;

        /// <inheritdoc cref="MenuItem.InputGesture"/>
        public static void SetInputGesture(this MenuItem menuItem, KeyGesture? inputGesture)
        {
            if (menuItem is not null)
                menuItem.InputGesture = inputGesture;
        }

        /// <inheritdoc cref="MenuItem.IsSelected"/>
        public static bool IsSelected(this MenuItem menuItem) =>
            menuItem != null ? menuItem.IsSelected : false;

        /// <inheritdoc cref="MenuItem.IsSelected"/>
        public static void SetIsSelected(this MenuItem menuItem, bool isSelected)
        {
            if (menuItem is not null)
                menuItem.IsSelected = isSelected;
        }

        /// <inheritdoc cref="MenuItem.IsSubMenuOpen"/>
        public static bool IsSubMenuOpen(this MenuItem menuItem) =>
            menuItem != null ? menuItem.IsSubMenuOpen : false;

        /// <inheritdoc cref="MenuItem.IsSubMenuOpen"/>
        public static void SetIsSubMenuOpen(this MenuItem menuItem, bool isSubMenuOpen)
        {
            if (menuItem is not null)
                menuItem.IsSubMenuOpen = isSubMenuOpen;
        }

        /// <inheritdoc cref="MenuItem.StaysOpenOnClick"/>
        public static bool StaysOpenOnClick(this MenuItem menuItem) =>
            menuItem != null ? menuItem.StaysOpenOnClick : false;

        /// <inheritdoc cref="MenuItem.StaysOpenOnClick"/>
        public static void SetStaysOpenOnClick(this MenuItem menuItem, bool staysOpenOnClick)
        {
            if (menuItem is not null)
                menuItem.StaysOpenOnClick = staysOpenOnClick;
        }

        /// <inheritdoc cref="MenuItem.ToggleType"/>
        public static MenuItemToggleType ToggleType(this MenuItem menuItem) =>
            menuItem != null ? menuItem.ToggleType : default(MenuItemToggleType);

        /// <inheritdoc cref="MenuItem.ToggleType"/>
        public static void SetToggleType(this MenuItem menuItem, MenuItemToggleType toggleType)
        {
            if (menuItem is not null)
                menuItem.ToggleType = toggleType;
        }

        /// <inheritdoc cref="MenuItem.IsChecked"/>
        public static bool IsChecked(this MenuItem menuItem) =>
            menuItem != null ? menuItem.IsChecked : false;

        /// <inheritdoc cref="MenuItem.IsChecked"/>
        public static void SetIsChecked(this MenuItem menuItem, bool isChecked)
        {
            if (menuItem is not null)
                menuItem.IsChecked = isChecked;
        }

        /// <inheritdoc cref="MenuItem.GroupName"/>
        public static string? GroupName(this MenuItem menuItem) =>
            menuItem != null ? menuItem.GroupName : null;

        /// <inheritdoc cref="MenuItem.GroupName"/>
        public static void SetGroupName(this MenuItem menuItem, string? groupName)
        {
            if (menuItem is not null)
                menuItem.GroupName = groupName;
        }

        /// <inheritdoc cref="MenuItem.HasSubMenu"/>
        public static bool HasSubMenu(this MenuItem menuItem) =>
            menuItem != null ? menuItem.HasSubMenu : false;

        /// <inheritdoc cref="MenuItem.IsTopLevel"/>
        public static bool IsTopLevel(this MenuItem menuItem) =>
            menuItem != null ? menuItem.IsTopLevel : false;
    }
}
