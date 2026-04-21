using Avalonia.Collections;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;

namespace VL.Avalonia.Media
{
    [ProcessNode(Name = "TextDecoration")]
    public partial class TextDecorationWrapper
    {
        protected readonly TextDecoration _output = new TextDecoration();
        public TextDecoration Output => _output;

        [ImplementProperty("TextDecoration.LocationProperty")]
        protected Optional<TextDecorationLocation> _textLocation;

        [ImplementProperty("TextDecoration.StrokeProperty")]
        protected Optional<IBrush> _stroke;

        [ImplementProperty("TextDecoration.StrokeThicknessUnitProperty")]
        protected Optional<TextDecorationUnit> _strokeThicknessUnit;

        protected Spread<float> _strokeDashArray;

        public void SetStrokeDashArray(Spread<float> strokeDashArray)
        {
            if (_strokeDashArray != strokeDashArray)
            {
                if (strokeDashArray != null)
                {
                    var array = new AvaloniaList<double>();

                    foreach (var strokeDash in strokeDashArray)
                    {
                        array.Add(strokeDash);
                    }

                    _output.SetValue(TextDecoration.StrokeDashArrayProperty, array);
                }
                else
                {
                    _output.ClearValue(TextDecoration.StrokeDashArrayProperty);
                }

                _strokeDashArray = strokeDashArray;
            }
        }

        [ImplementProperty<float, double>("TextDecoration.StrokeDashOffsetProperty")]
        protected Optional<float> _strokeDashOffset;

        [ImplementProperty<float, double>("TextDecoration.StrokeThicknessProperty")]
        protected Optional<float> _strokeThickness;

        [ImplementProperty("TextDecoration.StrokeLineCapProperty")]
        protected Optional<PenLineCap> _strokeLineCap;

        [ImplementProperty<float, double>("TextDecoration.StrokeOffsetProperty")]
        protected Optional<float> _strokeOffset;

        [ImplementProperty("TextDecoration.StrokeOffsetUnitProperty")]
        protected Optional<TextDecorationUnit> _strokeOffsetUnit;
    }
}
