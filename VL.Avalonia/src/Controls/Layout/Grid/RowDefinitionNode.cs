using Avalonia.Controls;
using Avalonia.Data;
using VL.Avalonia.Attributes;
using VL.Core.Import;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Wrapper for <see cref="RowDefinition"/>
    /// </summary>
    [ProcessNode(Name = "RowDefinition")]
    public partial class RowDefinitionNode : DefinitionNodeBase<RowDefinition>
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

        /// <summary>Sets <see cref="RowDefinition"/> max height.</summary>
        [ImplementProperty(
            typeof(RowDefinition),
            nameof(RowDefinition.MaxHeightProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<float> _maxHeight;

        /// <summary>Sets <see cref="RowDefinition"/> min height.</summary>
        [ImplementProperty(
            typeof(RowDefinition),
            nameof(RowDefinition.MinHeightProperty),
            Order = PinOrder.Style,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<float> _minHeight;

        protected virtual void UpdateDefinition()
        {
            _output.SetValue(
                RowDefinition.HeightProperty,
                new GridLength(
                    _gridLengthValue.GetValueOrDefault(0f),
                    _gridUnitType.GetValueOrDefault(GridUnitType.Star)
                )
            );
        }
    }
}
