using Avalonia.Controls;
using Avalonia.Labs.Panels;
using VL.Core;
using VL.Core.Import;
using VL.Avalonia.Controls;

namespace VL.Avalonia.Labs.Panels.Controls
{
    /// <summary>
    /// Cross-axis alignment override for a child item of a <see cref="FlexPanel"/>.
    /// Overrides the panel's AlignItems for this child. Equivalent to CSS align-self property.
    /// FlexPanel Attached Property.
    /// </summary>
    [ProcessNode(Name = "AlignSelf (FlexPanel)")]
    public partial class FlexPanelAlignSelfProperty : AttachedPropertyBase
    {
        private Optional<AlignItems> _alignSelf;

        /// <param name="alignSelf">
        /// (Attached) Cross-axis alignment of the child. When unset, the panel's AlignItems is used.
        /// </param>
        public void SetAlignSelf(Optional<AlignItems> alignSelf)
        {
            if (_alignSelf != alignSelf)
            {
                _alignSelf = alignSelf;
                UpdateSetters();
            }
        }

        protected override void UpdateSetters()
        {
            if (_input.HasNoValue)
            {
                return;
            }

            if (_alignSelf.HasValue)
            {
                Flex.SetAlignSelf(_input.Value, _alignSelf.Value);
            }
            else
            {
                _input.Value?.ClearValue(Flex.AlignSelfProperty);
            }
        }
    }

    /// <summary>
    /// Order in which a child item appears within a <see cref="FlexPanel"/>.
    /// Lower values are positioned earlier; items with the same order keep their source order. Equivalent to CSS order property.
    /// FlexPanel Attached Property.
    /// </summary>
    [ProcessNode(Name = "Order (FlexPanel)")]
    public partial class FlexPanelOrderProperty : AttachedPropertyBase
    {
        private Optional<int> _order;

        /// <param name="order">
        /// (Attached) Order of the child within the panel. Default is 0.
        /// </param>
        public void SetOrder(Optional<int> order)
        {
            if (_order != order)
            {
                _order = order;
                UpdateSetters();
            }
        }

        protected override void UpdateSetters()
        {
            if (_input.HasNoValue)
            {
                return;
            }

            if (_order.HasValue)
            {
                Flex.SetOrder(_input.Value, _order.Value);
            }
            else
            {
                _input.Value?.ClearValue(Flex.OrderProperty);
            }
        }
    }

    /// <summary>
    /// Initial main-axis size of a child item of a <see cref="FlexPanel"/>, before free space is distributed according to the flex factors.
    /// Equivalent to CSS flex-basis property.
    /// FlexPanel Attached Property.
    /// </summary>
    [ProcessNode(Name = "Basis (FlexPanel)")]
    public partial class FlexPanelBasisProperty : AttachedPropertyBase
    {
        private Optional<float> _basis;
        private Optional<FlexBasisKind> _kind;

        /// <param name="basis">
        /// (Attached) Initial main-axis size. Absolute: device independent pixels. Relative: percentage (0-100) of the panel's main-axis size. Negative or NaN values are treated as 0.
        /// </param>
        /// <param name="kind">
        /// (Attached) How Basis is interpreted. Auto ignores Basis and uses the child's desired size. Defaults to Absolute when only Basis is set.
        /// </param>
        public void SetBasis(Optional<float> basis, Optional<FlexBasisKind> kind)
        {
            if (_basis != basis || _kind != kind)
            {
                _basis = basis;
                _kind = kind;
                UpdateSetters();
            }
        }

        protected override void UpdateSetters()
        {
            if (_input.HasNoValue)
            {
                return;
            }

            var basis = FlexValueHelper.ToBasis(_basis, _kind);
            if (basis.HasValue)
            {
                Flex.SetBasis(_input.Value, basis.Value);
            }
            else
            {
                _input.Value?.ClearValue(Flex.BasisProperty);
            }
        }
    }

    /// <summary>
    /// Factor by which a child item can shrink along the main-axis, relative to other items in a <see cref="FlexPanel"/>.
    /// Equivalent to CSS flex-shrink property.
    /// FlexPanel Attached Property.
    /// </summary>
    [ProcessNode(Name = "Shrink (FlexPanel)")]
    public partial class FlexPanelShrinkProperty : AttachedPropertyBase
    {
        private Optional<float> _shrink;

        /// <param name="shrink">
        /// (Attached) Shrink factor. Default is 1. Negative or NaN values are treated as the default.
        /// </param>
        public void SetShrink(Optional<float> shrink)
        {
            if (_shrink != shrink)
            {
                _shrink = shrink;
                UpdateSetters();
            }
        }

        protected override void UpdateSetters()
        {
            if (_input.HasNoValue)
            {
                return;
            }

            if (_shrink.HasValue)
            {
                Flex.SetShrink(_input.Value, FlexValueHelper.ToFactor(_shrink.Value, FlexValueHelper.DefaultShrink));
            }
            else
            {
                _input.Value?.ClearValue(Flex.ShrinkProperty);
            }
        }
    }

    /// <summary>
    /// Factor by which a child item can grow along the main-axis, relative to other items in a <see cref="FlexPanel"/>.
    /// Equivalent to CSS flex-grow property.
    /// FlexPanel Attached Property.
    /// </summary>
    [ProcessNode(Name = "Grow (FlexPanel)")]
    public partial class FlexPanelGrowProperty : AttachedPropertyBase
    {
        private Optional<float> _grow;

        /// <param name="grow">
        /// (Attached) Grow factor. Default is 0. Negative or NaN values are treated as the default.
        /// </param>
        public void SetGrow(Optional<float> grow)
        {
            if (_grow != grow)
            {
                _grow = grow;
                UpdateSetters();
            }
        }

        protected override void UpdateSetters()
        {
            if (_input.HasNoValue)
            {
                return;
            }

            if (_grow.HasValue)
            {
                Flex.SetGrow(_input.Value, FlexValueHelper.ToFactor(_grow.Value, FlexValueHelper.DefaultGrow));
            }
            else
            {
                _input.Value?.ClearValue(Flex.GrowProperty);
            }
        }
    }

    /// <summary>
    /// Sanitizes VL pin values so the validating <see cref="Flex"/> setters and the <see cref="FlexBasis"/> constructor never throw.
    /// </summary>
    internal static class FlexValueHelper
    {
        public const double DefaultShrink = 1.0;
        public const double DefaultGrow = 0.0;

        public static double ToFactor(float value, double fallback) =>
            float.IsNaN(value) || value < 0f ? fallback : value;

        public static FlexBasis? ToBasis(Optional<float> basis, Optional<FlexBasisKind> kind)
        {
            if (kind.HasValue && kind.Value == FlexBasisKind.Auto)
                return FlexBasis.Auto;

            if (basis.HasNoValue)
                return null;

            return ToBasis(basis.Value, kind.HasValue ? kind.Value : FlexBasisKind.Absolute);
        }

        public static FlexBasis ToBasis(float basis, FlexBasisKind kind)
        {
            if (kind == FlexBasisKind.Auto)
                return FlexBasis.Auto;

            var value = float.IsFinite(basis) && basis > 0f ? basis : 0.0;
            return new FlexBasis(value, kind);
        }
    }
}