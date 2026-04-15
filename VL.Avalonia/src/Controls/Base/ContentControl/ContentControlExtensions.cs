using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Templates;
using Avalonia.Layout;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="ContentControl"/>.
    /// </summary>
    public static class ContentControlExtensions
    {
        /// <inheritdoc cref="ContentControl.Content"/>
        public static object? Content(this ContentControl contentControl) =>
            contentControl?.Content;

        /// <inheritdoc cref="ContentControl.Content"/>
        public static void SetContent(this ContentControl contentControl, object? content)
        {
            if (contentControl is not null)
                contentControl.Content = content;
        }

        /// <inheritdoc cref="ContentControl.ContentTemplate"/>
        public static IDataTemplate? ContentTemplate(this ContentControl contentControl) =>
            contentControl?.ContentTemplate;

        /// <inheritdoc cref="ContentControl.ContentTemplate"/>
        public static void SetContentTemplate(
            this ContentControl contentControl,
            IDataTemplate? contentTemplate
        )
        {
            if (contentControl is not null)
                contentControl.ContentTemplate = contentTemplate;
        }

        /// <inheritdoc cref="ContentControl.Presenter"/>
        public static ContentPresenter? Presenter(this ContentControl contentControl) =>
            contentControl?.Presenter;

        /// <inheritdoc cref="ContentControl.HorizontalContentAlignment"/>
        public static HorizontalAlignment HorizontalContentAlignment(
            this ContentControl contentControl
        ) => contentControl?.HorizontalContentAlignment ?? HorizontalAlignment.Stretch;

        /// <inheritdoc cref="ContentControl.HorizontalContentAlignment"/>
        public static void SetHorizontalContentAlignment(
            this ContentControl contentControl,
            HorizontalAlignment horizontalContentAlignment
        )
        {
            if (contentControl is not null)
                contentControl.HorizontalContentAlignment = horizontalContentAlignment;
        }

        /// <inheritdoc cref="ContentControl.VerticalContentAlignment"/>
        public static VerticalAlignment VerticalContentAlignment(
            this ContentControl contentControl
        ) => contentControl?.VerticalContentAlignment ?? VerticalAlignment.Stretch;

        /// <inheritdoc cref="ContentControl.VerticalContentAlignment"/>
        public static void SetVerticalContentAlignment(
            this ContentControl contentControl,
            VerticalAlignment verticalContentAlignment
        )
        {
            if (contentControl is not null)
                contentControl.VerticalContentAlignment = verticalContentAlignment;
        }
    }
}
