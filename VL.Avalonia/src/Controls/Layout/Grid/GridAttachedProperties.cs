using Avalonia.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    // TODO: REFACTOR

    /// <summary>
    /// Row property.
    /// Grid defines Row, so that it can be set on any element treated as a cell. Row property specifies child's position with respect to rows. Rows are 0 - based. In order to appear in first row, element should have Row property set to 0. Default value for the property is 0
    /// Grid Attached Property.
    /// </summary>
    [ProcessNode(Name = "Row (Grid)")]
    public partial class GridRowProperty : AttachedPropertyBase
    {
        private Optional<int> _row;

        public void SetRow(Optional<int> row)
        {
            if (_row != row)
            {
                _row = row;
                UpdateSetters();
            }
        }

        protected override void UpdateSetters()
        {
            if (_input.HasNoValue)
            {
                return;
            }

            if (_row.HasValue)
            {
                Grid.SetRow(_input.Value, _row.Value);
            }
            else
            {
                _input.Value.ClearValue(Grid.RowProperty);
            }
        }
    }

    /// <summary>
    /// RowSpan property. This is an attached property. Grid defines RowSpan, so that it can be set on any element treated as a cell. RowSpan property specifies child's height with respect to row grid lines. Example, RowSpan == 3 means that child will span across three rows.
    /// Grid Attached Property.
    /// </summary>
    [ProcessNode(Name = "RowSpan (Grid)")]
    public partial class GridRowSpanProperty : AttachedPropertyBase
    {
        private Optional<int> _rowSpan;

        public void SetRowSpan(Optional<int> rowSpan)
        {
            if (_rowSpan != rowSpan)
            {
                _rowSpan = rowSpan;
                UpdateSetters();
            }
        }

        protected override void UpdateSetters()
        {
            if (_input.HasNoValue)
            {
                return;
            }

            if (_rowSpan.HasValue)
            {
                Grid.SetRowSpan(_input.Value, _rowSpan.Value);
            }
            else
            {
                _input.Value.ClearValue(Grid.RowSpanProperty);
            }
        }
    }

    /// <summary>
    /// Column property.
    /// Grid defines Column, so that it can be set on any element treated as a cell. Column property specifies child's position with respect to columns. Columns are 0 - based. In order to appear in first column, element should have Column property set to 0. Default value for the property is 0
    /// Grid Attached Property.
    /// </summary>
    [ProcessNode(Name = "Column (Grid)")]
    public partial class GridColumnProperty : AttachedPropertyBase
    {
        private Optional<int> _column;

        public void SetColumn(Optional<int> column)
        {
            if (_column != column)
            {
                _column = column;
                UpdateSetters();
            }
        }

        protected override void UpdateSetters()
        {
            if (_input.HasNoValue)
            {
                return;
            }

            if (_column.HasValue)
            {
                Grid.SetColumn(_input.Value, _column.Value);
            }
            else
            {
                _input.Value.ClearValue(Grid.ColumnProperty);
            }
        }
    }

    /// <summary>
    /// ColumnSpan property. This is an attached property. Grid defines ColumnSpan, so that it can be set on any element treated as a cell. ColumnSpan property specifies child's width with respect to column grid lines. Example, ColumnSpan == 3 means that child will span across three columns.
    /// Grid Attached Property.
    /// </summary>
    [ProcessNode(Name = "ColumnSpan (Grid)")]
    public partial class GridColumnSpanProperty : AttachedPropertyBase
    {
        private Optional<int> _columnSpan;

        public void SetColumnSpan(Optional<int> columnSpan)
        {
            if (_columnSpan != columnSpan)
            {
                _columnSpan = columnSpan;
                UpdateSetters();
            }
        }

        protected override void UpdateSetters()
        {
            if (_input.HasNoValue)
            {
                return;
            }

            if (_columnSpan.HasValue)
            {
                Grid.SetColumnSpan(_input.Value, _columnSpan.Value);
            }
            else
            {
                _input.Value.ClearValue(Grid.ColumnSpanProperty);
            }
        }
    }

    /// <summary>
    /// Whether this element is the root of a shared size scope for column/row sizing
    /// Grid Attached Property
    /// </summary>
    [ProcessNode(Name = "IsSharedSizeScope (Grid)")]
    public partial class GridIsSharedSizeScopeProperty : AttachedPropertyBase
    {
        protected Optional<bool> _isSharedSizeScope;

        /// <param name="isSharedSizeScope">
        /// (Attached) Whether this element is the root of a shared size scope for column/row sizing
        /// </param>
        public void SetIsSharedSizeScope(Optional<bool> isSharedSizeScope)
        {
            if (_isSharedSizeScope != isSharedSizeScope)
            {
                _isSharedSizeScope = isSharedSizeScope;

                UpdateSetters();
            }
        }

        protected override void UpdateSetters()
        {
            if (_input.HasNoValue)
            {
                return;
            }

            if (_isSharedSizeScope.HasValue)
            {
                Grid.SetIsSharedSizeScope(_input.Value, _isSharedSizeScope.Value);
            }
            else
            {
                _input.Value.ClearValue(Grid.IsSharedSizeScopeProperty);
            }
        }
    }
}
