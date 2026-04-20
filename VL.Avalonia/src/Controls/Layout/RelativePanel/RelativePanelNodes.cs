using Avalonia.Controls;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="RelativePanel"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class RelativePanelNodeBase<T> : PanelNodeBase<T>
        where T : RelativePanel, new() { }

    /// <summary>
    /// Wrapper for <see cref="RelativePanel"/>
    /// </summary>
    [ProcessNode(Name = "RelativePanel")]
    public class RelativePanelNode : RelativePanelNodeBase<RelativePanel>
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

    /// <inheritdoc cref="RelativePanelNode"/>
    [ProcessNode(Name = "RelativePanel (Spectral)")]
    public class RelativePanelSpectralNode : RelativePanelNodeBase<RelativePanel>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetChildren(IReadOnlyList<Control> children)
        {
            base.SetChildren(children);
        }
    }
}
