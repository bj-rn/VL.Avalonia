using Avalonia.Controls;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="Grid"/>.
    /// </summary>
    public static class GridExtensions
    {
        /// <inheritdoc cref="Grid.ShowGridLines"/>
        public static bool ShowGridLines(this Grid grid) =>
            grid != null ? grid.ShowGridLines : false;

        /// <inheritdoc cref="Grid.ShowGridLines"/>
        public static void SetShowGridLines(this Grid grid, bool showGridLines)
        {
            if (grid is not null)
                grid.ShowGridLines = showGridLines;
        }

        /// <inheritdoc cref="Grid.ColumnDefinitions"/>
        public static ColumnDefinitions? ColumnDefinitions(this Grid grid) =>
            grid != null ? grid.ColumnDefinitions : null;

        /// <inheritdoc cref="Grid.ColumnDefinitions"/>
        public static void SetColumnDefinitions(
            this Grid grid,
            ColumnDefinitions? columnDefinitions
        )
        {
            if (grid is not null && columnDefinitions is not null)
                grid.ColumnDefinitions = columnDefinitions;
        }

        /// <inheritdoc cref="Grid.RowDefinitions"/>
        public static RowDefinitions? RowDefinitions(this Grid grid) =>
            grid != null ? grid.RowDefinitions : null;

        /// <inheritdoc cref="Grid.RowDefinitions"/>
        public static void SetRowDefinitions(this Grid grid, RowDefinitions? rowDefinitions)
        {
            if (grid is not null && rowDefinitions is not null)
                grid.RowDefinitions = rowDefinitions;
        }

        // AttachedProperties:

        /// <inheritdoc cref="Grid.ColumnProperty"/>
        public static int Column(this Control control) =>
            control != null ? Grid.GetColumn(control) : 0;

        /// <inheritdoc cref="Grid.ColumnProperty"/>
        public static void SetColumn(this Control control, int value)
        {
            if (control is not null)
                Grid.SetColumn(control, value);
        }

        /// <inheritdoc cref="Grid.RowProperty"/>
        public static int Row(this Control control) => control != null ? Grid.GetRow(control) : 0;

        /// <inheritdoc cref="Grid.RowProperty"/>
        public static void SetRow(this Control control, int value)
        {
            if (control is not null)
                Grid.SetRow(control, value);
        }

        /// <inheritdoc cref="Grid.ColumnSpanProperty"/>
        public static int ColumnSpan(this Control control) =>
            control != null ? Grid.GetColumnSpan(control) : 1;

        /// <inheritdoc cref="Grid.ColumnSpanProperty"/>
        public static void SetColumnSpan(this Control control, int value)
        {
            if (control is not null)
                Grid.SetColumnSpan(control, value);
        }

        /// <inheritdoc cref="Grid.RowSpanProperty"/>
        public static int RowSpan(this Control control) =>
            control != null ? Grid.GetRowSpan(control) : 1;

        /// <inheritdoc cref="Grid.RowSpanProperty"/>
        public static void SetRowSpan(this Control control, int value)
        {
            if (control is not null)
                Grid.SetRowSpan(control, value);
        }

        /// <inheritdoc cref="Grid.IsSharedSizeScopeProperty"/>
        public static bool IsSharedSizeScope(this Control control) =>
            control != null ? Grid.GetIsSharedSizeScope(control) : false;

        /// <inheritdoc cref="Grid.IsSharedSizeScopeProperty"/>
        public static void SetIsSharedSizeScope(this Control control, bool value)
        {
            if (control is not null)
                Grid.SetIsSharedSizeScope(control, value);
        }
    }
}
