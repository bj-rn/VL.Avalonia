using Avalonia.Media;
using VL.Avalonia.Media.Base;
using VL.Core;
using VL.Core.Import;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using Path = VL.Lib.IO.Path;

namespace VL.Avalonia.Media.Brushes;

[ProcessNode(Name = "ImageBrush")]
public partial class ImageBrushWrapper : TileBrushWrapperBase<ImageBrush>
{
    protected Optional<Path> _source;
    /// <param name="source">
    /// Sets image source path, and loads it as Avalonia bitmap
    /// </param>
    [Fragment(Order = PinOrder.Main)]
    public void SetSource(Optional<Path> source)
    {
        if (_source != source)
        {
            if (source.HasValue && source.Value.IsFile)
            {
                var bitmap = new Bitmap(source.Value.ToString());

                _output.SetValue(ImageBrush.SourceProperty, bitmap);
            }
            else
            {
                _output.ClearValue(ImageBrush.SourceProperty);
            }

            _source = source;
        }
    }
}
