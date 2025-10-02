using SkiaSharp;
using VL.Avalonia.Skia;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Mathematics;
namespace VL.Avalonia.Controls
{
    [ProcessNode]
    public abstract class SkiaMediaControlWrapperBase<T> : ControlWrapperBase<T> where T : SkiaMediaControlBase, new()
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
    }

    [ProcessNode(Name = "SkiaImageControl")]
    public partial class SkiaImageControlWrapper : SkiaMediaControlWrapperBase<SkiaImageControl>
    {
        protected Optional<SKImage> _image;
        [Fragment(Order = PinOrder.Main)]
        public void SetImage(Optional<SKImage> image)
        {
            if (_image != image)
            {
                if (image.HasValue)
                {
                    _output.Image = image.Value;
                }
                else
                {
                    _output.Image = null;
                }

                _image = image;
            }
        }
    }

    [ProcessNode(Name = "SkiaPictureControl")]
    public partial class SkiaPictureControlWrapper : SkiaMediaControlWrapperBase<SkiaPictureControl>
    {
        protected Optional<SKPicture> _picture;
        [Fragment(Order = PinOrder.Main)]
        public void SetPicture(Optional<SKPicture> picture)
        {
            if (_picture != picture)
            {
                if (picture.HasValue)
                {
                    _output.Picture = picture.Value;
                }
                else
                {
                    _output.Picture = null;
                }

                _picture = picture;
            }
        }
    }
}
