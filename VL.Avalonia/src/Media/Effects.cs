using Avalonia.Media;
using Stride.Core.Mathematics;
using VL.Avalonia.Attributes;
using VL.Avalonia.Controls.Base.Primitives;
using VL.Avalonia.Extensions;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Media.Effects
{
    [ProcessNode]
    public abstract partial class EffectWrapperBase<T> : AnimatableWrapperBase<T> where T : Effect, new()
    {
    }

    [ProcessNode(Name = "BlurEffect")]
    public partial class BlurEffectWrapper : EffectWrapperBase<BlurEffect>
    {
        [ImplementProperty<float, double>("BlurEffect.RadiusProperty")]
        protected Optional<float> _radius;
    }

    [ProcessNode]
    public abstract partial class DropShadowEffectBaseWrapper<T> : EffectWrapperBase<T> where T : DropShadowEffectBase, new()
    {
        [ImplementProperty<float, double>("DropShadowEffectBase.BlurRadiusProperty")]
        protected Optional<float> _blurRadius;

        protected Optional<Color4> _color;
        public void SetColor(Optional<Color4> color)
        {
            if (_color != color)
            {
                if (color.HasValue)
                {
                    _output.SetValue(DropShadowEffectBase.ColorProperty, color.Value.ToColor());
                }
                else
                {
                    _output.ClearValue(DropShadowEffectBase.ColorProperty);
                }

                _color = color;
            }
        }
    }

    [ProcessNode(Name = "DropShadowEffect")]
    public partial class DropShadowEffectWrapper : DropShadowEffectBaseWrapper<DropShadowEffect>
    {
        protected Optional<Vector2> _offset;
        public void SetOffset(Optional<Vector2> offset)
        {
            if (_offset != offset)
            {
                if (offset.HasValue)
                {
                    _output.SetValue(DropShadowEffect.OffsetXProperty, offset.Value.X);
                    _output.SetValue(DropShadowEffect.OffsetYProperty, offset.Value.Y);
                }
                else
                {
                    _output.ClearValue(DropShadowEffect.OffsetXProperty);
                    _output.ClearValue(DropShadowEffect.OffsetYProperty);
                }

                _offset = offset;
            }
        }
    }

    [ProcessNode(Name = "DropShadowDirectionEffect")]
    public partial class DropShadowDirectionEffectWrapper : DropShadowEffectBaseWrapper<DropShadowDirectionEffect>
    {
        [ImplementProperty<float, double>("DropShadowDirectionEffect.ShadowDepthProperty")]
        protected Optional<float> _shadowDepth;

        protected Optional<float> _direction;
        public void SetDirection(Optional<float> direction)
        {
            if (_direction != direction)
            {
                if (_direction.HasValue)
                {
                    _output.SetValue(DropShadowDirectionEffect.DirectionProperty, (double)(direction.Value * 360.0f));
                }
                else
                {
                    _output.ClearValue(DropShadowDirectionEffect.DirectionProperty);
                }

                _direction = direction;
            }
        }
    }
}

