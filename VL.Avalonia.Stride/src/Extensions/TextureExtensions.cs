using Stride.Graphics;

namespace VL.Avalonia.Stride.Extensions
{
    public static class TextureExtensions
    {
        /// <summary>
        /// Extension method that clears tags on texture to trigger cache
        /// </summary>
        public static Texture? TextureTagsClear(Texture? texture)
        {
            if (texture is not null)
            {
                texture.Tags.Clear();
            }

            return texture;
        }
    }
}
