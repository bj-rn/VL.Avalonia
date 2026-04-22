using Avalonia.Controls;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="ListBoxItem"/>.
    /// </summary>
    public static class ListBoxItemExtensions
    {
        /// <inheritdoc cref="ListBoxItem.IsSelected"/>
        public static bool IsSelected(this ListBoxItem listBoxItem) =>
            listBoxItem != null ? listBoxItem.IsSelected : false;

        /// <inheritdoc cref="ListBoxItem.IsSelected"/>
        public static void SetIsSelected(this ListBoxItem listBoxItem, bool isSelected)
        {
            if (listBoxItem is not null)
                listBoxItem.IsSelected = isSelected;
        }
    }
}
