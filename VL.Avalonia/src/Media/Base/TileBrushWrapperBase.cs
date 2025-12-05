using Avalonia;
using Avalonia.Media;
using Stride.Core.Mathematics;
using VL.Avalonia.Attributes;
using VL.Avalonia.Extensions;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Media.Base;

[ProcessNode]
public abstract partial class TileBrushWrapperBase<T> : BrushWrapperBase<T>
    where T : TileBrush, new()
{
    [ImplementProperty("TileBrush.AlignmentXProperty")]
    protected Optional<AlignmentX> _aligmentX;

    [ImplementProperty("TileBrush.AlignmentYProperty")]
    protected Optional<AlignmentY> _aligmentY;

    protected Optional<RectangleF> _destinationRect;
    protected Optional<RelativeUnit> _destinationRectRelativeUnit;

    public void SetDestinationRect(
        Optional<RectangleF> destinationRect,
        Optional<RelativeUnit> destinationRectRelativeUnit
    )
    {
        if (
            _destinationRect != destinationRect
            || _destinationRectRelativeUnit != destinationRectRelativeUnit
        )
        {
            if (destinationRect.HasValue)
            {
                _output.SetValue(
                    TileBrush.DestinationRectProperty,
                    destinationRect.Value.ToRelativeRect(
                        destinationRectRelativeUnit.HasValue
                            ? destinationRectRelativeUnit.Value
                            : RelativeUnit.Relative
                    )
                );
            }
            else
            {
                _output.ClearValue(TileBrush.DestinationRectProperty);
            }

            _destinationRect = destinationRect;
            _destinationRectRelativeUnit = destinationRectRelativeUnit;
        }
    }

    protected Optional<RectangleF> _sourceRect;
    protected Optional<RelativeUnit> _sourceRectRelativeUnit;

    public void SetSourceRect(
        Optional<RectangleF> sourceRect,
        Optional<RelativeUnit> sourceRectRelativeUnit
    )
    {
        if (_sourceRect != sourceRect || _sourceRectRelativeUnit != sourceRectRelativeUnit)
        {
            if (sourceRect.HasValue)
            {
                _output.SetValue(
                    TileBrush.SourceRectProperty,
                    sourceRect.Value.ToRelativeRect(
                        sourceRectRelativeUnit.HasValue
                            ? sourceRectRelativeUnit.Value
                            : RelativeUnit.Relative
                    )
                );
            }
            else
            {
                _output.ClearValue(TileBrush.SourceRectProperty);
            }

            _sourceRect = sourceRect;
            _sourceRectRelativeUnit = sourceRectRelativeUnit;
        }
    }

    [ImplementProperty("TileBrush.StretchProperty")]
    protected Optional<Stretch> _stretch;

    [ImplementProperty("TileBrush.TileModeProperty")]
    protected Optional<TileMode> _tileMode;
}
