using Avalonia.Controls.Primitives;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="UniformGrid"/>.
    /// </summary>
    public static class UniformGridExtensions
    {
        /// <inheritdoc cref="UniformGrid.Rows"/>
        public static int Rows(this UniformGrid uniformGrid) =>
            uniformGrid != null ? uniformGrid.Rows : 0;

        /// <inheritdoc cref="UniformGrid.Rows"/>
        public static void SetRows(this UniformGrid uniformGrid, int rows)
        {
            if (uniformGrid is not null)
                uniformGrid.Rows = rows;
        }

        /// <inheritdoc cref="UniformGrid.Columns"/>
        public static int Columns(this UniformGrid uniformGrid) =>
            uniformGrid != null ? uniformGrid.Columns : 0;

        /// <inheritdoc cref="UniformGrid.Columns"/>
        public static void SetColumns(this UniformGrid uniformGrid, int columns)
        {
            if (uniformGrid is not null)
                uniformGrid.Columns = columns;
        }

        /// <inheritdoc cref="UniformGrid.FirstColumn"/>
        public static int FirstColumn(this UniformGrid uniformGrid) =>
            uniformGrid != null ? uniformGrid.FirstColumn : 0;

        /// <inheritdoc cref="UniformGrid.FirstColumn"/>
        public static void SetFirstColumn(this UniformGrid uniformGrid, int firstColumn)
        {
            if (uniformGrid is not null)
                uniformGrid.FirstColumn = firstColumn;
        }
    }
}
