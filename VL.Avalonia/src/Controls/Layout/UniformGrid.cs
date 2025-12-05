using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>UniformGrid</c> arranges its children in a grid where all cells are the same size. You can specify the number of rows, columns, and the starting column for the first row. It also allows setting spacing between rows and columns. If rows or columns are set to 0, UniformGrid will automatically calculate the optimal count to fit all children.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/uniformgrid">UniformGrid</see>
/// </summary>
[ProcessNode(Name = "UniformGrid (Spectral)")]
public partial class UniformGridSpectralWrapper : PanelWrapperBase<UniformGrid>
{
    #region Layout Properties

    /// <param name="rows">
    /// The number of rows in the grid. If set to 0, the row count is calculated automatically.
    /// </param>
    [ImplementProperty("UniformGrid.RowsProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _rows;

    /// <param name="columns">
    /// The number of columns in the grid. If set to 0, the column count is calculated automatically.
    /// </param>
    [ImplementProperty("UniformGrid.ColumnsProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _columns;

    /// <param name="firstColumn">
    /// The starting column for the first row of items. Default is 0.
    /// </param>
    [ImplementProperty(
        "UniformGrid.FirstColumnProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
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

    #endregion
}

[ProcessNode(Name = "UniformGrid")]
public partial class UniformGridWrapper : UniformGridSpectralWrapper
{
    /// <inheritdoc cref="SetChildren(Spread{Control})"/>
    [Fragment(Order = -10)]
    public override void SetChildren(
        [Pin(PinGroupKind = Model.PinGroupKind.Collection, PinGroupDefaultCount = 1)]
            Spread<Control> children
    ) => base.SetChildren(children);
}
