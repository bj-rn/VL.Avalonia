using Avalonia.Controls;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="Canvas"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class CanvasNodeBase<T> : PanelNodeBase<T>
        where T : Canvas, new() { }

    /// <summary>
    /// Wrapper for <see cref="Canvas"/>
    /// </summary>
    [ProcessNode(Name = "Canvas")]
    public class CanvasNode : CanvasNodeBase<Canvas>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetChildren(
            [Pin(PinGroupKind = PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<Control> children
        )
        {
            base.SetChildren(children);
        }
    }

    /// <inheritdoc cref="CanvasNode"/>
    [ProcessNode(Name = "Canvas (Spectral)")]
    public class CanvasNodeSpectral : CanvasNodeBase<Canvas>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetChildren(IReadOnlyList<Control> children)
        {
            base.SetChildren(children);
        }
    }
}
