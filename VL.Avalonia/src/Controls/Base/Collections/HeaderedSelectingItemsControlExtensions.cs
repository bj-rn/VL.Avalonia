using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="HeaderedSelectingItemsControl"/>.
    /// </summary>
    public static class HeaderedSelectingItemsControlExtensions
    {
        /// <inheritdoc cref="HeaderedSelectingItemsControl.Header"/>
        public static object? Header(
            this HeaderedSelectingItemsControl headeredSelectingItemsControl
        ) => headeredSelectingItemsControl != null ? headeredSelectingItemsControl.Header : null;

        /// <inheritdoc cref="HeaderedSelectingItemsControl.Header"/>
        public static void SetHeader(
            this HeaderedSelectingItemsControl headeredSelectingItemsControl,
            object? header
        )
        {
            if (headeredSelectingItemsControl is not null)
                headeredSelectingItemsControl.Header = header;
        }

        /// <inheritdoc cref="HeaderedSelectingItemsControl.HeaderTemplate"/>
        public static IDataTemplate? HeaderTemplate(
            this HeaderedSelectingItemsControl headeredSelectingItemsControl
        ) =>
            headeredSelectingItemsControl != null
                ? headeredSelectingItemsControl.HeaderTemplate
                : null;

        /// <inheritdoc cref="HeaderedSelectingItemsControl.HeaderTemplate"/>
        public static void SetHeaderTemplate(
            this HeaderedSelectingItemsControl headeredSelectingItemsControl,
            IDataTemplate? headerTemplate
        )
        {
            if (headeredSelectingItemsControl is not null)
                headeredSelectingItemsControl.HeaderTemplate = headerTemplate;
        }

        /// <inheritdoc cref="HeaderedSelectingItemsControl.HeaderPresenter"/>
        public static ContentPresenter? HeaderPresenter(
            this HeaderedSelectingItemsControl headeredSelectingItemsControl
        ) =>
            headeredSelectingItemsControl != null
                ? headeredSelectingItemsControl.HeaderPresenter
                : null;
    }
}
