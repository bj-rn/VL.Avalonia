using Avalonia;
using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base class for controls which decorate a single child control.
    /// </summary>
    [ProcessNode]
    public partial class DecoratorBaseWrapper<T> : ControlNodeBase<T>
        where T : Decorator, new()
    {
        /// <param name="child">Sets the <see cref="Decorator.Child"/> for the Decorator</param>
        [ImplementProperty("Decorator.ChildProperty", Order = PinOrder.Main)]
        private Optional<Control> _child;

        /// <param name="padding">Sets the <see cref="Decorator.Padding"/> for the Decorator</param>
        [ImplementProperty(
            "Decorator.PaddingProperty",
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<Thickness> _padding;
    }
}

