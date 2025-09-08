using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using VL.Avalonia.Controls;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Custom.Controls.Value
{
    /// <summary>
    /// ColorView
    /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/colorpicker/">ColorView</see>
    /// </summary>
    [ProcessNode(Name = "ColorView")]
    public partial class ColorViewWrapper<T> : ControlWrapperBase<T> where T : ColorView, new()
    {
        protected ChannelTwoWayBinding<HsvColor> _hsvBinding;
        public ColorViewWrapper()
        {
            _hsvBinding = new ChannelTwoWayBinding<HsvColor>(_output, ColorPreviewer.HsvColorProperty);
        }

        /// <param name="hsvColorChannel">
        /// Gets or sets the current value
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetHsvColorChannel(IChannel<HsvColor> hsvColorChannel) =>
            _hsvBinding.SetChannel(hsvColorChannel);




        protected Optional<ColorModel> _colorModel;
        [Fragment(Order = PinOrder.Main)]
        public void SetColorModel([Pin(Visibility = Model.PinVisibility.Visible)] Optional<ColorModel> colorModel)
        {
            if (_colorModel != colorModel)
            {
                if (colorModel.HasValue)
                {
                    _output.SetValue(ColorView.ColorModelProperty, colorModel.Value);
                }
                else
                {
                    _output.ClearValue(ColorView.ColorModelProperty);
                }

                _colorModel = colorModel;
            }
        }


        protected Optional<ColorSpectrumComponents> _colorSpectrumComponents;
        [Fragment(Order = PinOrder.Main)]
        public void SetColorSpectrumComponent([Pin(Visibility = Model.PinVisibility.Visible)] Optional<ColorSpectrumComponents> colorSpectrumComponents)
        {
            if (_colorSpectrumComponents != colorSpectrumComponents)
            {
                if (colorSpectrumComponents.HasValue)
                {
                    _output.SetValue(ColorView.ColorSpectrumComponentsProperty, colorSpectrumComponents.Value);
                }
                else
                {
                    _output.ClearValue(ColorView.ColorSpectrumComponentsProperty);
                }

                _colorSpectrumComponents = colorSpectrumComponents;
            }
        }


        protected Optional<ColorSpectrumShape> _alphaComponentPosition;
        [Fragment(Order = PinOrder.Main)]
        public void SetAlphaComponentPosition([Pin(Visibility = Model.PinVisibility.Visible)] Optional<ColorSpectrumShape> alphaComponentPosition)
        {
            if (_alphaComponentPosition != alphaComponentPosition)
            {
                if (alphaComponentPosition.HasValue)
                {
                    _output.SetValue(ColorView.ColorSpectrumShapeProperty, alphaComponentPosition.Value);
                }
                else
                {
                    _output.ClearValue(ColorView.ColorSpectrumShapeProperty);
                }

                _alphaComponentPosition = alphaComponentPosition;
            }
        }


        protected Optional<AlphaComponentPosition> _hexInputAlphaPosition;
        [Fragment(Order = PinOrder.Main)]
        public void SetHexInputAlphaPosition([Pin(Visibility = Model.PinVisibility.Visible)] Optional<AlphaComponentPosition> hexInputAlphaPosition)
        {
            if (_hexInputAlphaPosition != hexInputAlphaPosition)
            {
                if (hexInputAlphaPosition.HasValue)
                {
                    _output.SetValue(ColorView.HexInputAlphaPositionProperty, hexInputAlphaPosition.Value);
                }
                else
                {
                    _output.ClearValue(ColorView.HexInputAlphaPositionProperty);
                }

                _hexInputAlphaPosition = hexInputAlphaPosition;
            }
        }


        protected Optional<bool> _isAccentColorsVisible;
        [Fragment(Order = PinOrder.Main)]
        public void SetIsAccentColorsVisible([Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isAccentColorVisible)
        {
            if (_isAccentColorsVisible != isAccentColorVisible)
            {
                if (isAccentColorVisible.HasValue)
                {
                    _output.SetValue(ColorView.IsAccentColorsVisibleProperty, isAccentColorVisible.Value);
                }
                else
                {
                    _output.ClearValue(ColorView.IsAccentColorsVisibleProperty);
                }

                _isAccentColorsVisible = isAccentColorVisible;
            }
        }


        protected Optional<bool> _isAlphaVisible;
        [Fragment(Order = PinOrder.Main)]
        public void SetIsAlphaVisible([Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isAlphaVisible)
        {
            if (_isAlphaVisible != isAlphaVisible)
            {
                if (isAlphaVisible.HasValue)
                {
                    _output.SetValue(ColorView.IsAlphaVisibleProperty, isAlphaVisible.Value);
                }
                else
                {
                    _output.ClearValue(ColorView.IsAlphaVisibleProperty);
                }

                _isAlphaVisible = isAlphaVisible;
            }
        }


        protected Optional<bool> _isColorComponentsVisible;
        [Fragment(Order = PinOrder.Main)]
        public void SetIsColorComponentVisible([Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isColorComponentVisible)
        {
            if (_isColorComponentsVisible != isColorComponentVisible)
            {
                if (isColorComponentVisible.HasValue)
                {
                    _output.SetValue(ColorView.IsColorComponentsVisibleProperty, isColorComponentVisible.Value);
                }
                else
                {
                    _output.ClearValue(ColorView.IsColorComponentsVisibleProperty);
                }

                _isColorComponentsVisible = isColorComponentVisible;
            }
        }


        protected Optional<bool> _isColorModelVisible;
        [Fragment(Order = PinOrder.Main)]
        public void SetIsColorModelVisible([Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isColorPreviewVisible)
        {
            if (_isColorModelVisible != isColorPreviewVisible)
            {
                if (isColorPreviewVisible.HasValue)
                {
                    _output.SetValue(ColorView.IsColorModelVisibleProperty, isColorPreviewVisible.Value);
                }
                else
                {
                    _output.ClearValue(ColorView.IsColorModelVisibleProperty);
                }

                _isColorModelVisible = isColorPreviewVisible;
            }
        }


        protected Optional<bool> _isColorPreviewVisible;
        [Fragment(Order = PinOrder.Main)]
        public void SetIsColorPreviewVisible([Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isColorPreviewVisible)
        {
            if (_isColorPreviewVisible != isColorPreviewVisible)
            {
                if (isColorPreviewVisible.HasValue)
                {
                    _output.SetValue(ColorView.IsColorPreviewVisibleProperty, isColorPreviewVisible.Value);
                }
                else
                {
                    _output.ClearValue(ColorView.IsColorPreviewVisibleProperty);
                }

                _isColorPreviewVisible = isColorPreviewVisible;
            }
        }


        protected Optional<bool> _isColorSpectrumVisible;
        [Fragment(Order = PinOrder.Main)]
        public void SetIsColorSpectrumVisible([Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isColorSpectrumVisible)
        {
            if (_isColorSpectrumVisible != isColorSpectrumVisible)
            {
                if (isColorSpectrumVisible.HasValue)
                {
                    _output.SetValue(ColorView.IsColorSpectrumVisibleProperty, isColorSpectrumVisible.Value);
                }
                else
                {
                    _output.ClearValue(ColorView.IsColorSpectrumVisibleProperty);
                }

                _isColorSpectrumVisible = isColorSpectrumVisible;
            }
        }


        protected Optional<bool> _isColorSpectrumSliderVisible;
        [Fragment(Order = PinOrder.Main)]
        public void SetIsColorSpectrumSliderVisible([Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isColorSpectrumSliderVisible)
        {
            if (_isColorSpectrumSliderVisible != isColorSpectrumSliderVisible)
            {
                if (isColorSpectrumSliderVisible.HasValue)
                    _output.SetValue(ColorView.IsColorSpectrumSliderVisibleProperty, isColorSpectrumSliderVisible.Value);
                else
                    _output.ClearValue(ColorView.IsColorSpectrumSliderVisibleProperty);

                _isColorSpectrumSliderVisible = isColorSpectrumSliderVisible;
            }
        }


        protected Optional<bool> _isComponentSliderVisible;
        [Fragment(Order = PinOrder.Main)]
        public void SetIsComponentSliderVisible([Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isComponentSliderVisible)
        {
            if (_isComponentSliderVisible != isComponentSliderVisible)
            {
                if (isComponentSliderVisible.HasValue)
                    _output.SetValue(ColorView.IsComponentSliderVisibleProperty, isComponentSliderVisible.Value);
                else
                    _output.ClearValue(ColorView.IsComponentSliderVisibleProperty);

                _isComponentSliderVisible = isComponentSliderVisible;
            }
        }


        protected Optional<bool> _isComponentTextInputVisible;
        [Fragment(Order = PinOrder.Main)]
        public void SetIsComponentTextInputVisible([Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isComponentTextInputVisible)
        {
            if (_isComponentTextInputVisible != isComponentTextInputVisible)
            {
                if (isComponentTextInputVisible.HasValue)
                    _output.SetValue(ColorView.IsComponentTextInputVisibleProperty, isComponentTextInputVisible.Value);
                else
                    _output.ClearValue(ColorView.IsComponentTextInputVisibleProperty);

                _isComponentTextInputVisible = isComponentTextInputVisible;
            }
        }


        protected Optional<bool> _isHexInputVisible;
        [Fragment(Order = PinOrder.Main)]
        public void SetIsHexInputVisible([Pin(Visibility = Model.PinVisibility.Visible)] Optional<bool> isHexInputVisible)
        {
            if (_isHexInputVisible != isHexInputVisible)
            {
                if (isHexInputVisible.HasValue)
                    _output.SetValue(ColorView.IsHexInputVisibleProperty, isHexInputVisible.Value);
                else
                    _output.ClearValue(ColorView.IsHexInputVisibleProperty);

                _isHexInputVisible = isHexInputVisible;
            }
        }
    }
}
