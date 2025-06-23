using Avalonia.Controls.Shapes;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using static VL.Avalonia.Styles;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "Rectangle")]
public partial class RectangleWrapper
{
    [ImplementOutput]
    protected Rectangle _output = new Rectangle();

    [ImplementStyle]
    protected Optional<IAvaloniaStyle> _style;

    [ImplementClasses]
    protected Optional<string> _classes;

    [ImplementProperty("Rectangle.NameProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<string> _name;

    [ImplementProperty("Rectangle.FillProperty")]
    protected Optional<IBrush> _fill;

    [ImplementProperty("Rectangle.WidthProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _width;

    [ImplementProperty("Rectangle.HeightProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _height;

    [ImplementProperty("Rectangle.OpacityProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _opacity;
}
