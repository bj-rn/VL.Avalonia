using Avalonia.Controls;
using Avalonia.Data;
using VL.Avalonia.Attributes;
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
        private Optional<float> _gridLengthValue;
        private Optional<GridUnitType> _gridUnitType;

        /// <param name="gridLengthValue">Size in pixels or star units.</param>
        public void SetGridLengthValue(Optional<float> gridLengthValue)
        {
            if (_gridLengthValue == gridLengthValue)
                return;

            _gridLengthValue = gridLengthValue;

            UpdateDefinition();
        }

        /// <param name="gridUnitType">Type of grid unit (Pixel, Star, Auto).</param>
        public void SetGridUnitType(Optional<GridUnitType> gridUnitType)
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
                new GridLength(
                    _gridLengthValue.GetValueOrDefault(0f),
                    _gridUnitType.GetValueOrDefault(GridUnitType.Star)
                )
            );
        }
    }
}
