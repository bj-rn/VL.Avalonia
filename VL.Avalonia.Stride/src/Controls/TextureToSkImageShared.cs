using Microsoft.Extensions.DependencyInjection;
using SkiaSharp;
using Stride.Engine;
using Stride.Graphics;
using VL.Core;
using VL.Skia;
using StrideRenderContext = Stride.Rendering.RenderContext;

namespace VL.Avalonia.Stride.Controls
{
    /// <summary>
    /// Converts a Stride Texture to SKImage using VL.Skia.FromSharedHandle.
    /// Handles creating a shared copy if the source texture is not shared.
    /// </summary>
    public class TextureToSkImageShared : IDisposable
    {
        private readonly FromSharedHandle _sharedHandleConverter;

        // Intermediate shared texture (used if input texture isn't shared)
        private Texture? _sharedCopy;

        public TextureToSkImageShared()
        {
            _sharedHandleConverter = new FromSharedHandle();
        }

        public SKImage? Update(Texture? texture)
        {
            if (texture == null)
            {
                _sharedHandleConverter.Update(IntPtr.Zero);
                return null;
            }

            // 1. Get the handle
            IntPtr handle = texture.SharedHandle;

            // 2. If no handle, we must copy to a shared texture
            if (handle == IntPtr.Zero)
            {
                // Recreate shared copy if dimensions/format changed
                if (
                    _sharedCopy == null
                    || _sharedCopy.Width != texture.Width
                    || _sharedCopy.Height != texture.Height
                    || _sharedCopy.Format != texture.Format
                )
                {
                    _sharedCopy?.Dispose();

                    var desc = texture.Description;
                    desc.Options = TextureOptions.Shared; // Force Shared
                    desc.Usage = GraphicsResourceUsage.Default;

                    _sharedCopy = Texture.New(texture.GraphicsDevice, desc);
                }

                // Copy content
                var cmd = GetCommandList();
                if (cmd != null)
                {
                    cmd.Copy(texture, _sharedCopy);
                }

                handle = _sharedCopy.SharedHandle;

                if (handle == IntPtr.Zero)
                    return null;
            }
            else
            {
                // Input is already shared, free our copy if we had one
                if (_sharedCopy != null)
                {
                    _sharedCopy.Dispose();
                    _sharedCopy = null;
                }
            }

            // 3. AGGRESSIVE UPDATE:
            // The FromSharedHandle class caches the SKImage if the handle pointer is the same.
            // Since our texture content changes (video/game) but the handle (address) stays the same,
            // we MUST invalidate the cache by passing Zero first.
            // This forces the EGL/GL backend to recreate the image from the latest content.
            _sharedHandleConverter.Update(IntPtr.Zero);

            // 4. Update with the real handle
            return _sharedHandleConverter.Update(handle);
        }

        private static CommandList? GetCommandList()
        {
            var game = AppHost.Current.Services.GetService<Game>();
            return game != null
                ? StrideRenderContext.GetShared(game.Services).GetThreadContext().CommandList
                : null;
        }

        public void Dispose()
        {
            _sharedHandleConverter.Dispose();
            _sharedCopy?.Dispose();
        }
    }
}
