using Avalonia.Controls;
using Avalonia.Labs.Panels;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="FlexPanel"/>
    /// </summary>
    /// <remarks>
    /// <see cref="FlexPanel"/> is sealed, so this base is not generic.
    /// </remarks>
    [ProcessNode]
    public abstract partial class FlexPanelNodeBase : PanelNodeBase<FlexPanel>
    {
        [Fragment]
        public FlexPanelNodeBase([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext)
            : base(nodeContext) { }

        /// <summary>Sets the direction of the main-axis, determining the orientation in which child controls are laid out.</summary>
        [ImplementProperty(
            typeof(FlexPanel),
            nameof(FlexPanel.DirectionProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<FlexDirection> _direction;

        /// <summary>Sets the main-axis alignment of child items inside a line.</summary>
        [ImplementProperty(
            typeof(FlexPanel),
            nameof(FlexPanel.JustifyContentProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<JustifyContent> _justifyContent;

        /// <summary>Sets the cross-axis alignment of all child items inside a line.</summary>
        [ImplementProperty(
            typeof(FlexPanel),
            nameof(FlexPanel.AlignItemsProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<AlignItems> _alignItems;

        /// <summary>Sets the cross-axis alignment of lines when there is extra space.</summary>
        [ImplementProperty(
            typeof(FlexPanel),
            nameof(FlexPanel.AlignContentProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<AlignContent> _alignContent;

        /// <summary>Sets the wrap mode, controlling whether the panel is single-line or multi-line.</summary>
        [ImplementProperty(
            typeof(FlexPanel),
            nameof(FlexPanel.WrapProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<FlexWrap> _wrap;

        /// <summary>Sets the minimum horizontal spacing between child items or lines.</summary>
        [ImplementProperty(
            typeof(FlexPanel),
            nameof(FlexPanel.ColumnSpacingProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<float> _columnSpacing;

        /// <summary>Sets the minimum vertical spacing between child items or lines.</summary>
        [ImplementProperty(
            typeof(FlexPanel),
            nameof(FlexPanel.RowSpacingProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<float> _rowSpacing;
    }

    /// <summary>
    /// Wrapper for <see cref="FlexPanel"/>. A panel that arranges child controls using CSS FlexBox principles.
    /// </summary>
    [ProcessNode(Name = "FlexPanel")]
    public class FlexPanelNode : FlexPanelNodeBase
    {
        [Fragment]
        public FlexPanelNode([Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext)
            : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetChildren(
            [Pin(PinGroupKind = PinGroupKind.Collection, PinGroupDefaultCount = 1)]
                Spread<Control> children
        )
        {
            base.SetChildren(children);
        }
    }

    /// <inheritdoc cref="FlexPanelNode"/>
    [ProcessNode(Name = "FlexPanel (Spectral)")]
    public class FlexPanelSpectralNode : FlexPanelNodeBase
    {
        [Fragment]
        public FlexPanelSpectralNode(
            [Pin(Visibility = PinVisibility.Hidden)] NodeContext nodeContext
        )
            : base(nodeContext) { }

        [Fragment(Order = PinOrder.Main)]
        public override void SetChildren(IReadOnlyList<Control> children)
        {
            base.SetChildren(children);
        }
    }
}
