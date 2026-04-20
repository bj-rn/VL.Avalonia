using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="DockPanel"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class DockPanelNodeBase<T> : PanelNodeBase<T>
        where T : DockPanel, new()
    {
        [ImplementProperty(
            typeof(DockPanel),
            nameof(DockPanel.LastChildFillProperty),
            Order = PinOrder.Control,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<bool> _lastChildFill;
    }

    /// <summary>
    /// Wrapper for <see cref="DockPanel"/>
    /// </summary>
    [ProcessNode(Name = "DockPanel")]
    public class DockPanelNode : DockPanelNodeBase<DockPanel>
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

    /// <inheritdoc cref="DockPanelNode"/>
    [ProcessNode(Name = "DockPanel (Spectral)")]
    public class DockPanelSpectralNode : DockPanelNodeBase<DockPanel>
    {
        [Fragment(Order = PinOrder.Main)]
        public override void SetChildren(IReadOnlyList<Control> children)
        {
            base.SetChildren(children);
        }
    }
}
