using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>Grid</c> provides a flexible area of rows and columns for arranging child controls. It is the most powerful layout panel in Avalonia, supporting complex arrangements, spanning, and proportional sizing (“star” sizing). Grid supports attached properties for placing and spanning children, spacing properties, and visual debugging with grid lines.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/grid">Grid</see>
/// </summary>
[ProcessNode(Name = "Grid (Spectral)")]
public partial class GridSpectralWrapper : PanelWrapperBase<Grid>
{
    protected Spread<ColumnDefinition?> _columnDefinitions = Spread<ColumnDefinition?>.Empty;
    /// <param name="columnDefinitions">
    /// Sets Grid Column Definitions
    /// </param>
    public void SetColumnDefinitions(Spread<ColumnDefinition?> columnDefinitions)
    {
        if (_columnDefinitions != columnDefinitions)
        {
            _columnDefinitions = columnDefinitions;

            var cd = new ColumnDefinitions();
            foreach (var columnDefinition in _columnDefinitions)
            {
                if (columnDefinition != null)
                {
                    cd.Add(columnDefinition);
                }
            }
            _output.ColumnDefinitions = cd;
        }
    }
    /// <summary>
    /// Sets Grid Row Definitions
    /// </summary>
    protected Spread<RowDefinition?> _rowDefinitions = Spread<RowDefinition?>.Empty;
    public void SetRowDefinitions(Spread<RowDefinition?> rowDefinitions)
    {
        if (_rowDefinitions != rowDefinitions)
        {
            _rowDefinitions = rowDefinitions;
            var rd = new RowDefinitions();
            foreach (var rowDefinition in _rowDefinitions)
            {
                if (rowDefinition != null)
                {
                    rd.Add(rowDefinition);
                }
            }
            _output.RowDefinitions = rd;
        }
    }

    /// <param name="showGridLines">
    /// Show visual grid lines for debugging layout (not intended for production use)
    /// </param>
    [ImplementProperty("Grid.ShowGridLinesProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _showGridLines;

    /*
    * NOT IMPLEMENTED ON 11.2.1
    * 
    /// <param name="rowSpacing">
    /// The amount of space in device-independent pixels between grid rows
    /// </param>
    [ImplementProperty("Grid.RowSpacingProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<double> _rowSpacing;

    /// <param name="columnSpacing">
    /// The amount of space in device-independent pixels between grid columns
    /// </param>
    [ImplementProperty("Grid.ColumnSpacingProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<double> _columnSpacing;
    */
}

[ProcessNode(Name = "Grid")]
public partial class GridWrapper : GridSpectralWrapper
{
    /// <inheritdoc cref="SetChildren(Spread{Control})"/>
    [Fragment(Order = -10)]
    public override void SetChildren([Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)] Spread<Control> children) =>
        base.SetChildren(children);
}

[ProcessNode(Name = "ColumnDefinition")]
public partial class ColumnDefinitionWrapper
{
    [ImplementOutput]
    private readonly ColumnDefinition _output = new ColumnDefinition();

    private float _gridUnitWidth = 1.0f;
    public void SetValue(float value = 1.0f)
    {
        if (_gridUnitWidth != value)
        {
            _gridUnitWidth = value;
            _output.SetValue(ColumnDefinition.WidthProperty, new GridLength(_gridUnitWidth, _gridUnitType));
        }
    }

    private GridUnitType _gridUnitType = GridUnitType.Star;
    public void SetGridUnitType(GridUnitType gridUnitType = GridUnitType.Star)
    {
        if (_gridUnitType != gridUnitType)
        {
            _gridUnitType = gridUnitType;
            _output.SetValue(ColumnDefinition.WidthProperty, new GridLength(_gridUnitWidth, _gridUnitType));
        }
    }

    [ImplementProperty("ColumnDefinition.MaxWidthProperty", PinVisibility = Model.PinVisibility.Optional)]
    private Optional<float> _maxWidth;

    [ImplementProperty("ColumnDefinition.MinWidthProperty", PinVisibility = Model.PinVisibility.Optional)]
    private Optional<float> _minWidth;
}

[ProcessNode(Name = "RowDefinition")]
public partial class RowDefinitionWrapper
{
    [ImplementOutput]
    private readonly RowDefinition _output = new RowDefinition();

    private float _gridUnitHeight = 1.0f;
    public void SetValue(float value = 1.0f)
    {
        if (_gridUnitHeight != value)
        {
            _gridUnitHeight = value;
            _output.SetValue(RowDefinition.HeightProperty, new GridLength(_gridUnitHeight, _gridUnitType));
        }
    }
    private GridUnitType _gridUnitType = GridUnitType.Star;
    public void SetGridUnitType(GridUnitType gridUnitType = GridUnitType.Star)
    {
        if (_gridUnitType != gridUnitType)
        {
            _gridUnitType = gridUnitType;
            _output.SetValue(RowDefinition.HeightProperty, new GridLength(_gridUnitHeight, _gridUnitType));
        }
    }

    [ImplementProperty("RowDefinition.MaxHeightProperty", PinVisibility = Model.PinVisibility.Optional)]
    private Optional<float> _maxHeight;

    [ImplementProperty("RowDefinition.MinHeightProperty", PinVisibility = Model.PinVisibility.Optional)]
    private Optional<float> _minHeight;
}

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