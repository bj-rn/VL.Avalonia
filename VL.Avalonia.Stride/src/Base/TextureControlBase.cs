#nullable enable
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using Avalonia.Threading;
using Microsoft.Extensions.DependencyInjection;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Graphics;
using VL.Avalonia.Controls;
using VL.Avalonia.Extensions;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Mathematics;
using VL.Skia;
using VL.Stride.Textures;
using Point = Avalonia.Point;
using StrideRenderContext = Stride.Rendering.RenderContext;

namespace VL.Avalonia.Stride.Base
{

    public class TextureDrawingOperation : ICustomDrawOperation
    {
        public Rect Bounds { get; }
        public bool Equals(ICustomDrawOperation? other) => false;
        public bool HitTest(Point p) => false;
        public SizeMode Mode { get; }
        public RectangleAnchor Anchor { get; }
        public Texture Texture { get; }
        public TextureToSkImage TextureToSkImage { get; }

        public TextureDrawingOperation(Texture? texture, TextureToSkImage textureToSkImage, Rect bounds, SizeMode mode, RectangleAnchor anchor)
        {
            Texture = texture;
            TextureToSkImage = textureToSkImage;
            Bounds = bounds;
            Mode = mode;
            Anchor = anchor;
        }

        public void Render(ImmediateDrawingContext context)
        {
            var leaseFeature = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            if (leaseFeature != null)
            {
                if (Texture is null)
                    return;

                using var lease = leaseFeature.Lease();
                var canvas = lease.SkCanvas;

                canvas.Save();

                var device = Texture.GraphicsDevice;

                var image = TextureToSkImage.Update(Texture);

                var boundsRectangle = Bounds.FromRect();
                var imageResolution = new Vector2(Texture.Width, Texture.Height);

                AspectRatioUtils.FixAspectRatio(ref boundsRectangle, ref imageResolution, Mode, Anchor, out RectangleF bounds);

                canvas.DrawImage(image, bounds.ToRect().ToSKRect());


                canvas.Restore();
            }
        }
        public void Dispose() { }
    }

    public class TextureControl : Control
    {
        private readonly RenderContextProvider renderContextProvider = AppHost.Current.GetRenderContextProvider();
        private readonly CommandList commandList = GetCommandList();
        public Texture? Texture { get; set; }
        public TextureToSkImage TextureToSkImage { get; } = new TextureToSkImage();
        static CommandList GetCommandList()
        {
            return StrideRenderContext.GetShared(AppHost.Current.Services.GetRequiredService<Game>().Services).GetThreadContext().CommandList;
        }

        public override void Render(DrawingContext context)
        {
            context.Custom(new TextureDrawingOperation(Texture, TextureToSkImage, Bounds, Mode ?? SizeMode.AutoWidth, Anchor ?? RectangleAnchor.Center));
            Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
        }


        public SizeMode? Mode { get; set; }
        public RectangleAnchor? Anchor { get; set; }
    }

    [ProcessNode(Name = "TextureControl")]
    public class TextureControlWrapper : ControlWrapperBase<TextureControl>
    {
        protected Optional<SizeMode> _mode;
        public void SetSizeMode(Optional<SizeMode> mode)
        {
            if (_mode != mode)
            {
                if (mode.HasValue)
                {
                    _output.Mode = mode.Value;
                }
                else
                {
                    _output.Mode = null;
                }

                _mode = mode;
            }
        }

        protected Optional<RectangleAnchor> _anchor;
        public void SetAnchor([Pin(Visibility = Model.PinVisibility.Optional)] Optional<RectangleAnchor> anchor)
        {
            if (_anchor != anchor)
            {
                if (anchor.HasValue)
                {
                    _output.Anchor = anchor.Value;
                }
                else
                {
                    _output.Anchor = null;
                }

                _anchor = anchor;
            }
        }


        [Fragment(Order = PinOrder.Main)]
        public void SetTexture(Optional<Texture> texture)
        {
            texture.Value?.Tags.Clear();
            _output.Texture = texture.Value;
        }
    }

    public static class TextureExtensions
    {
        public static Texture? ClearTags(Texture? texture)
        {
            if (texture is not null)
            {
                texture.Tags.Clear();
            }

            return texture;
        }
    }
}
