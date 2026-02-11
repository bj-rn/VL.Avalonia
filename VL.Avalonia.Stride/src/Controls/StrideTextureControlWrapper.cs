using Stride.Graphics;
using VL.Avalonia.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Stride.Controls
{
    [ProcessNode(Name = "StrideTextureControl")]
    public class StrideTextureControlWrapper : SkiaMediaControlWrapperBase<StrideTextureControl>
    {
        protected Optional<Texture> Texture;

        [Fragment(Order = PinOrder.Main)]
        public void SetTexture(Optional<Texture> texture)
        {
            if (texture.HasValue)
            {
                _output.Texture = texture.Value;
            }
            else
            {
                _output.Texture = null;
            }

            Texture = texture;
        }
    }
}
