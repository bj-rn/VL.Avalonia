using Avalonia.Controls;
using Avalonia.Input;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

/// <summary>
/// Base class for exposing Controls properties and methods
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Focuses control inherited from InputElement
    /// </summary>
    /// <param name="input"></param>
    /// <param name="method"></param>
    /// <param name="keyModifiers"></param>
    /// <returns></returns>
    public static void Focus(this InputElement? input, NavigationMethod method = NavigationMethod.Unspecified, KeyModifiers keyModifiers = KeyModifiers.None) =>
        input?.Focus(method, keyModifiers);

    /// <param name="result">
    /// Whether the menu is currently open.
    /// </param>
    public static void IsOpen(this MenuBase? menu, out bool result) =>
        result = menu?.IsOpen ?? false;

    #region TreeView

    /// <summary>
    /// Gets TreeView selected item.
    /// </summary>
    public static object? SelectedItem(TreeView treeView) => treeView.SelectedItem;

    /// <summary>
    /// Gets TreeView selected items.
    /// </summary>
    public static Spread<T> SelectedItems<T>(TreeView treeView)
    {
        var selectedItems = treeView.SelectedItems;
        if (selectedItems == null || selectedItems.Count == 0)
            return Spread<T>.Empty;

        return selectedItems.Cast<T>().ToSpread();
    }

    #endregion

    #region TreeViewItem

    /// <summary>
    /// Gets the level/indentation of the item.
    /// </summary>
    public static int Level(this TreeViewItem? input) => input?.Level ?? -1;

    #endregion
}
