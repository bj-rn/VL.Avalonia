using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="HeaderedItemsControl"/>.
    /// </summary>
    public static class HeaderedItemsControlExtensions
    {
        /// <inheritdoc cref="HeaderedItemsControl.Header"/>
        public static object? Header(this HeaderedItemsControl headeredItemsControl) =>
            headeredItemsControl?.Header;

        /// <inheritdoc cref="HeaderedItemsControl.Header"/>
        public static void SetHeader(this HeaderedItemsControl headeredItemsControl, object? header)
        {
            if (headeredItemsControl is not null)
                headeredItemsControl.Header = header;
        }

        /// <inheritdoc cref="HeaderedItemsControl.HeaderTemplate"/>
        public static IDataTemplate? HeaderTemplate(
            this HeaderedItemsControl headeredItemsControl
        ) => headeredItemsControl?.HeaderTemplate;

        /// <inheritdoc cref="HeaderedItemsControl.HeaderTemplate"/>
        public static void SetHeaderTemplate(
            this HeaderedItemsControl headeredItemsControl,
            IDataTemplate? headerTemplate
        )
        {
            if (headeredItemsControl is not null)
                headeredItemsControl.HeaderTemplate = headerTemplate;
        }

        /// <inheritdoc cref="HeaderedItemsControl.HeaderPresenter"/>
        public static ContentPresenter? HeaderPresenter(
            this HeaderedItemsControl headeredItemsControl
        ) => headeredItemsControl?.HeaderPresenter;
    }
}
