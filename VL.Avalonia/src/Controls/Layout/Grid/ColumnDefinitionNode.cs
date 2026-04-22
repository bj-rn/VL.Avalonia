using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Wrapper for <see cref="ColumnDefinition"/>
    /// </summary>
    [ProcessNode(Name = "ColumnDefinition")]
    public partial class ColumnDefinitionNode : DefinitionNodeBase<ColumnDefinition>
    {
        private float _gridLengthValue = DefaultGridLengthValue;
        private GridUnitType _gridUnitType = DefaultGridUnit;

        /// <param name="gridLengthValue">Size in pixels or star units.</param>
        [Fragment(Order = PinOrder.Main)]
        public void SetGridLengthValue(float gridLengthValue = DefaultGridLengthValue)
        {
            if (_gridLengthValue == gridLengthValue)
                return;

            _gridLengthValue = gridLengthValue;

            UpdateDefinition();
        }

        /// <param name="gridUnitType">Type of grid unit (Pixel, Star, Auto).</param>
        [Fragment(Order = PinOrder.Main)]
        public void SetGridUnitType(GridUnitType gridUnitType = DefaultGridUnit)
        {
            if (_gridUnitType == gridUnitType)
                return;

            _gridUnitType = gridUnitType;

            UpdateDefinition();
        }

        /// <summary>Sets <see cref="ColumnDefinition"/> max width.</summary>
        [ImplementProperty(
            typeof(ColumnDefinition),
            nameof(ColumnDefinition.MaxWidthProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<float> _maxWidth;

        /// <summary>Sets <see cref="ColumnDefinition"/> min width.</summary>
        [ImplementProperty(
            typeof(ColumnDefinition),
            nameof(ColumnDefinition.MinWidthProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<float> _minWidth;

        protected virtual void UpdateDefinition()
        {
            _output.SetValue(
                ColumnDefinition.WidthProperty,
                new GridLength(_gridLengthValue, _gridUnitType)
            );
        }
    }
}
