using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using SkiaSharp;
using VL.Avalonia.Rendering;

namespace VL.Avalonia.Controls
{
    public class TextureViewer : Control
    {
        Rect srcRect;
        Rect dstRect;

        static TextureViewer()
        {
            AffectsRender<TextureViewer>(SourceProperty, StretchDirectionProperty, StretchProperty);
            AffectsMeasure<TextureViewer>(SourceProperty, StretchDirectionProperty, StretchProperty);
            AffectsArrange<TextureViewer>(SourceProperty, StretchDirectionProperty, StretchProperty);
            ClipToBoundsProperty.OverrideDefaultValue<TextureViewer>(true);
        }

        public TextureViewer()
        {
            BoundsProperty.Changed.Subscribe(BoundsChanged);
            SourceProperty.Changed.Subscribe(SourceChanged);
        }


        private void SourceChanged(AvaloniaPropertyChangedEventArgs<SKImage> args)
        {
            var externalImage = args.NewValue.Value;

            if (externalImage == null)
            {
                Source?.Dispose();
                Source = null;
                return;
            }

            Source = externalImage;
        }

        void BoundsChanged(object @obj)
        {
            if (Source == null)
                return;

            Rect viewPort = new Rect(Bounds.Size);
            Size sourceSize = new Size(Source.Width, Source.Height);

            Vector scale = Stretch.CalculateScaling(Bounds.Size, sourceSize, StretchDirection);
            Size scaledSize = sourceSize * scale;

            dstRect = viewPort.CenterRect(new Rect(scaledSize)).Intersect(viewPort);
            srcRect = new Rect(sourceSize).CenterRect(new Rect(dstRect.Size / scale));
        }

        public override void Render(DrawingContext context)
        {
            if(Source == null) 
                return;

            var rect = new Rect(0, 0, 100, 100);
            var render = new TextureViewerRender(Source, dstRect, srcRect);
            context.Custom(render);
        }

        ///<inheritdoc/>
        protected override Size MeasureOverride(Size availableSize)
        {
            if (Source != null)
                return Stretch.CalculateSize(availableSize, new Size(Source.Width, Source.Height), StretchDirection);

            return new Size();
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Source != null)
            {
                Size sourceSize = new Size(Source.Width, Source.Height);
                return Stretch.CalculateSize(finalSize, sourceSize);
            }

            return new Size();
        }


        public readonly static StyledProperty<SKImage> SourceProperty =
            AvaloniaProperty.Register<TextureViewer, SKImage>(nameof(Source));
        public SKImage Source
        {
            get => GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public readonly static StyledProperty<Stretch> StretchProperty =
            Image.StretchProperty.AddOwner<TextureViewer>();
        public Stretch Stretch
        {
            get => GetValue(StretchProperty);
            set => SetValue(StretchProperty, value);
        }

        public readonly static StyledProperty<StretchDirection> StretchDirectionProperty =
            Image.StretchDirectionProperty.AddOwner<TextureViewer>();
        public StretchDirection StretchDirection
        {
            get => GetValue(StretchDirectionProperty);
            set => SetValue(StretchDirectionProperty, value);
        }
    }
}
