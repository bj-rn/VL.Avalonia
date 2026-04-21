using System.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="AutoCompleteBox"/>.
    /// </summary>
    public static class AutoCompleteBoxExtensions
    {
        /// <inheritdoc cref="AutoCompleteBox.CaretIndex"/>
        public static int CaretIndex(this AutoCompleteBox autoCompleteBox) =>
            autoCompleteBox != null ? autoCompleteBox.CaretIndex : 0;

        /// <inheritdoc cref="AutoCompleteBox.CaretIndex"/>
        public static void SetCaretIndex(this AutoCompleteBox autoCompleteBox, int caretIndex)
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.CaretIndex = caretIndex;
        }

        /// <inheritdoc cref="AutoCompleteBox.Watermark"/>
        public static string? Watermark(this AutoCompleteBox autoCompleteBox) =>
            autoCompleteBox != null ? autoCompleteBox.Watermark : null;

        /// <inheritdoc cref="AutoCompleteBox.Watermark"/>
        public static void SetWatermark(this AutoCompleteBox autoCompleteBox, string? watermark)
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.Watermark = watermark;
        }

        /// <inheritdoc cref="AutoCompleteBox.MinimumPrefixLength"/>
        public static int MinimumPrefixLength(this AutoCompleteBox autoCompleteBox) =>
            autoCompleteBox != null ? autoCompleteBox.MinimumPrefixLength : 1;

        /// <inheritdoc cref="AutoCompleteBox.MinimumPrefixLength"/>
        public static void SetMinimumPrefixLength(
            this AutoCompleteBox autoCompleteBox,
            int minimumPrefixLength
        )
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.MinimumPrefixLength = minimumPrefixLength;
        }

        /// <inheritdoc cref="AutoCompleteBox.MinimumPopulateDelay"/>
        public static TimeSpan MinimumPopulateDelay(this AutoCompleteBox autoCompleteBox) =>
            autoCompleteBox != null ? autoCompleteBox.MinimumPopulateDelay : TimeSpan.Zero;

        /// <inheritdoc cref="AutoCompleteBox.MinimumPopulateDelay"/>
        public static void SetMinimumPopulateDelay(
            this AutoCompleteBox autoCompleteBox,
            TimeSpan minimumPopulateDelay
        )
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.MinimumPopulateDelay = minimumPopulateDelay;
        }

        /// <inheritdoc cref="AutoCompleteBox.MaxDropDownHeight"/>
        public static float MaxDropDownHeight(this AutoCompleteBox autoCompleteBox) =>
            autoCompleteBox != null
                ? (float)autoCompleteBox.MaxDropDownHeight
                : float.PositiveInfinity;

        /// <inheritdoc cref="AutoCompleteBox.MaxDropDownHeight"/>
        public static void SetMaxDropDownHeight(
            this AutoCompleteBox autoCompleteBox,
            float maxDropDownHeight
        )
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.MaxDropDownHeight = maxDropDownHeight;
        }

        /// <inheritdoc cref="AutoCompleteBox.IsTextCompletionEnabled"/>
        public static bool IsTextCompletionEnabled(this AutoCompleteBox autoCompleteBox) =>
            autoCompleteBox != null ? autoCompleteBox.IsTextCompletionEnabled : false;

        /// <inheritdoc cref="AutoCompleteBox.IsTextCompletionEnabled"/>
        public static void SetIsTextCompletionEnabled(
            this AutoCompleteBox autoCompleteBox,
            bool isTextCompletionEnabled
        )
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.IsTextCompletionEnabled = isTextCompletionEnabled;
        }

        /// <inheritdoc cref="AutoCompleteBox.ItemTemplate"/>
        public static IDataTemplate? ItemTemplate(this AutoCompleteBox autoCompleteBox) =>
            autoCompleteBox != null ? autoCompleteBox.ItemTemplate : null;

        /// <inheritdoc cref="AutoCompleteBox.ItemTemplate"/>
        public static void SetItemTemplate(
            this AutoCompleteBox autoCompleteBox,
            IDataTemplate? itemTemplate
        )
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.ItemTemplate = itemTemplate;
        }

        /// <inheritdoc cref="AutoCompleteBox.IsDropDownOpen"/>
        public static bool IsDropDownOpen(this AutoCompleteBox autoCompleteBox) =>
            autoCompleteBox != null ? autoCompleteBox.IsDropDownOpen : false;

        /// <inheritdoc cref="AutoCompleteBox.IsDropDownOpen"/>
        public static void SetIsDropDownOpen(
            this AutoCompleteBox autoCompleteBox,
            bool isDropDownOpen
        )
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.IsDropDownOpen = isDropDownOpen;
        }

        /// <inheritdoc cref="AutoCompleteBox.ValueMemberBinding"/>
        public static IBinding? ValueMemberBinding(this AutoCompleteBox autoCompleteBox) =>
            autoCompleteBox != null ? autoCompleteBox.ValueMemberBinding : null;

        /// <inheritdoc cref="AutoCompleteBox.ValueMemberBinding"/>
        public static void SetValueMemberBinding(
            this AutoCompleteBox autoCompleteBox,
            IBinding? valueMemberBinding
        )
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.ValueMemberBinding = valueMemberBinding;
        }

        /// <inheritdoc cref="AutoCompleteBox.SelectedItem"/>
        public static object? SelectedItem(this AutoCompleteBox autoCompleteBox) =>
            autoCompleteBox != null ? autoCompleteBox.SelectedItem : null;

        /// <inheritdoc cref="AutoCompleteBox.SelectedItem"/>
        public static void SetSelectedItem(
            this AutoCompleteBox autoCompleteBox,
            object? selectedItem
        )
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.SelectedItem = selectedItem;
        }

        /// <inheritdoc cref="AutoCompleteBox.Text"/>
        public static string? Text(this AutoCompleteBox autoCompleteBox) =>
            autoCompleteBox != null ? autoCompleteBox.Text : null;

        /// <inheritdoc cref="AutoCompleteBox.Text"/>
        public static void SetText(this AutoCompleteBox autoCompleteBox, string? text)
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.Text = text;
        }

        /// <inheritdoc cref="AutoCompleteBox.SearchText"/>
        public static string? SearchText(this AutoCompleteBox autoCompleteBox) =>
            autoCompleteBox != null ? autoCompleteBox.SearchText : null;

        // Note: SetSearchText is omitted because the SearchText property has a private setter.

        /// <inheritdoc cref="AutoCompleteBox.FilterMode"/>
        public static AutoCompleteFilterMode FilterMode(this AutoCompleteBox autoCompleteBox) =>
            autoCompleteBox != null
                ? autoCompleteBox.FilterMode
                : AutoCompleteFilterMode.StartsWith;

        /// <inheritdoc cref="AutoCompleteBox.FilterMode"/>
        public static void SetFilterMode(
            this AutoCompleteBox autoCompleteBox,
            AutoCompleteFilterMode filterMode
        )
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.FilterMode = filterMode;
        }

        /// <inheritdoc cref="AutoCompleteBox.ItemFilter"/>
        public static AutoCompleteFilterPredicate<object?>? ItemFilter(
            this AutoCompleteBox autoCompleteBox
        ) => autoCompleteBox != null ? autoCompleteBox.ItemFilter : null;

        /// <inheritdoc cref="AutoCompleteBox.ItemFilter"/>
        public static void SetItemFilter(
            this AutoCompleteBox autoCompleteBox,
            AutoCompleteFilterPredicate<object?>? itemFilter
        )
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.ItemFilter = itemFilter;
        }

        /// <inheritdoc cref="AutoCompleteBox.TextFilter"/>
        public static AutoCompleteFilterPredicate<string?>? TextFilter(
            this AutoCompleteBox autoCompleteBox
        ) => autoCompleteBox != null ? autoCompleteBox.TextFilter : null;

        /// <inheritdoc cref="AutoCompleteBox.TextFilter"/>
        public static void SetTextFilter(
            this AutoCompleteBox autoCompleteBox,
            AutoCompleteFilterPredicate<string?>? textFilter
        )
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.TextFilter = textFilter;
        }

        /// <inheritdoc cref="AutoCompleteBox.ItemSelector"/>
        public static AutoCompleteSelector<object>? ItemSelector(
            this AutoCompleteBox autoCompleteBox
        ) => autoCompleteBox != null ? autoCompleteBox.ItemSelector : null;

        /// <inheritdoc cref="AutoCompleteBox.ItemSelector"/>
        public static void SetItemSelector(
            this AutoCompleteBox autoCompleteBox,
            AutoCompleteSelector<object>? itemSelector
        )
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.ItemSelector = itemSelector;
        }

        /// <inheritdoc cref="AutoCompleteBox.TextSelector"/>
        public static AutoCompleteSelector<string?>? TextSelector(
            this AutoCompleteBox autoCompleteBox
        ) => autoCompleteBox != null ? autoCompleteBox.TextSelector : null;

        /// <inheritdoc cref="AutoCompleteBox.TextSelector"/>
        public static void SetTextSelector(
            this AutoCompleteBox autoCompleteBox,
            AutoCompleteSelector<string?>? textSelector
        )
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.TextSelector = textSelector;
        }

        /// <inheritdoc cref="AutoCompleteBox.AsyncPopulator"/>
        public static Func<string?, CancellationToken, Task<IEnumerable<object>>>? AsyncPopulator(
            this AutoCompleteBox autoCompleteBox
        ) => autoCompleteBox != null ? autoCompleteBox.AsyncPopulator : null;

        /// <inheritdoc cref="AutoCompleteBox.AsyncPopulator"/>
        public static void SetAsyncPopulator(
            this AutoCompleteBox autoCompleteBox,
            Func<string?, CancellationToken, Task<IEnumerable<object>>>? asyncPopulator
        )
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.AsyncPopulator = asyncPopulator;
        }

        /// <inheritdoc cref="AutoCompleteBox.ItemsSource"/>
        public static IEnumerable? ItemsSource(this AutoCompleteBox autoCompleteBox) =>
            autoCompleteBox != null ? autoCompleteBox.ItemsSource : null;

        /// <inheritdoc cref="AutoCompleteBox.ItemsSource"/>
        public static void SetItemsSource(
            this AutoCompleteBox autoCompleteBox,
            IEnumerable? itemsSource
        )
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.ItemsSource = itemsSource;
        }

        /// <inheritdoc cref="AutoCompleteBox.MaxLength"/>
        public static int MaxLength(this AutoCompleteBox autoCompleteBox) =>
            autoCompleteBox != null ? autoCompleteBox.MaxLength : 0;

        /// <inheritdoc cref="AutoCompleteBox.MaxLength"/>
        public static void SetMaxLength(this AutoCompleteBox autoCompleteBox, int maxLength)
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.MaxLength = maxLength;
        }

        /// <inheritdoc cref="AutoCompleteBox.InnerLeftContent"/>
        public static object? InnerLeftContent(this AutoCompleteBox autoCompleteBox) =>
            autoCompleteBox != null ? autoCompleteBox.InnerLeftContent : null;

        /// <inheritdoc cref="AutoCompleteBox.InnerLeftContent"/>
        public static void SetInnerLeftContent(
            this AutoCompleteBox autoCompleteBox,
            object? innerLeftContent
        )
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.InnerLeftContent = innerLeftContent;
        }

        /// <inheritdoc cref="AutoCompleteBox.InnerRightContent"/>
        public static object? InnerRightContent(this AutoCompleteBox autoCompleteBox) =>
            autoCompleteBox != null ? autoCompleteBox.InnerRightContent : null;

        /// <inheritdoc cref="AutoCompleteBox.InnerRightContent"/>
        public static void SetInnerRightContent(
            this AutoCompleteBox autoCompleteBox,
            object? innerRightContent
        )
        {
            if (autoCompleteBox is not null)
                autoCompleteBox.InnerRightContent = innerRightContent;
        }
    }
}
