using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls.Panels;

/// <summary>
/// The UniformGrid divides the available space evenly in both directions, into cells. You can specify how many divisions to use, and these can be different in either direction.
/// </summary>
[ProcessNode(Name = "UniformGrid (Spectral)")]
public partial class UniformGridSpectralWrapper
{
    [ImplementOutput]
    protected readonly UniformGrid _output = new UniformGrid();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    [ImplementClasses]
    protected Optional<string> _classes;

    [ImplementChildren]
    protected Spread<Control?> _children;

    [ImplementProperty("Control.NameProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<string> _name;

    /// <summary>
    /// Specifies the column count. If set to 0, column count will be calculated automatically.
    /// </summary>
    [ImplementProperty("UniformGrid.ColumnsProperty")]
    protected Optional<int> _columns;

    /// <summary>
    /// Specifies the row count. If set to 0, row count will be calculated automatically.
    /// </summary>
    [ImplementProperty("UniformGrid.RowsProperty")]
    protected Optional<int> _rows;

    /// <summary>
    /// Specifies, for the first row, the column where the items should start.
    /// </summary>
    [ImplementProperty("UniformGrid.FirstColumnProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _firstColumn;

    /*
     * NOT IMPLEMENTED ON 11.2.1
     * 
    /// <summary>
    /// Specifies the spacing between rows.
    /// </summary>
    [ImplementProperty("UniformGrid.RowSpacingProperty")]
    protected Optional<float> _rowSpacing;


    /// <summary>
    /// Specifies the spacing between columns.
    /// </summary>
    [ImplementProperty("UniformGrid.ColumnSpacingProperty")]
    protected Optional<float> _columnSpacing;
    */
}

[ProcessNode(Name = "UniformGrid")]
public partial class UniformGridWrapper : UniformGridSpectralWrapper
{
    [ImplementChildren(IsPinGroup = true)]
    protected Spread<Control?> _children;
}
