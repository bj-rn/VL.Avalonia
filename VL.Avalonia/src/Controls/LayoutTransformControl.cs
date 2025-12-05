using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    /// <inheritdoc cref="LayoutTransformControl"/>
    [ProcessNode(Name = "LayoutTransformControl")]
    public partial class LayoutTransformControlWrapper
        : DecoratorBaseWrapper<LayoutTransformControl>
    {
        /// <param name="layoutTransform">Sets the <see cref="LayoutTransformControl.LayoutTransform"/> for the LayoutTransformControl</param>
        [ImplementProperty(
            "LayoutTransformControl.LayoutTransformProperty",
            Order = PinOrder.Action
        )]
        private Optional<ITransform> _layoutTransform;

        /// <param name="useRenderTransform">Sets the <see cref="LayoutTransformControl.UseRenderTransform"/> for the LayoutTransformControl</param>
        [ImplementProperty(
            "LayoutTransformControl.UseRenderTransformProperty",
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _useRenderTransform;
    }
}
