using SkiaSharp;
using Stride.Graphics;
using VL.Skia;
using VL.Stride.Textures;

namespace VL.Avalonia.Stride.Controls
{
    public class TextureToSkImageShared : IDisposable
    {
        private readonly TextureToSkImage _textureToSkImage;
        private SKImage? _lastRasterImage;
        private bool _disposed;

        public TextureToSkImageShared()
        {
            _textureToSkImage = new TextureToSkImage();
        }

        /// <summary>
        /// Converts the given Stride Texture to an SKImage that can be drawn on Avalonia's canvas.
        /// </summary>
        public SKImage? Update(Texture? texture)
        {
            if (texture == null)
            {
                DisposeLastImage();
                return null;
            }

            // Get the GPU-backed SKImage (live view, cached by TextureToSkImage on texture.Tags)
            SKImage? gpuImage = _textureToSkImage.Update(texture);
            if (gpuImage == null)
                return null;

            // The gpuImage is bound to VL.Skia's EGL GRContext.
            // Avalonia has its own GRContext — drawing a foreign GPU SKImage will either
            // fail silently or show only the first frame.
            // Solution: convert to raster while VL.Skia's context is current.

            // Make VL.Skia's render context current for the GPU readback
            var renderContext = RenderContext.ForCurrentApp();
            using var _ = renderContext.MakeCurrent(forRendering: false);

            // Readback GPU → CPU. This is the per-frame cost but avoids all the
            // EGL reimport overhead of the SharedHandle approach.
            var rasterImage = gpuImage.ToRasterImage(ensurePixelData: true);
            if (rasterImage == null)
                return _lastRasterImage; // fallback to previous frame

            // Dispose previous raster image
            DisposeLastImage();
            _lastRasterImage = rasterImage;

            return _lastRasterImage;
        }

        private void DisposeLastImage()
        {
            _lastRasterImage?.Dispose();
            _lastRasterImage = null;
        }

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;

            DisposeLastImage();
            // TextureToSkImage's SKImages are owned by texture.Tags via DisposeBy — not ours to dispose.
        }
    }
}
