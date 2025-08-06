using Avalonia.Media;
using SkiaSharp;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;


namespace VL.Avalonia.Controls.Media
{
    //[ProcessNode(HasStateOutput = true, Name = "TextureViewer", FragmentSelection = FragmentSelection.Explicit)]

    /// <summary>
    /// The <c>SkiaTextureViewer</c> control to see display an SKImage
    /// </summary>
    [ProcessNode(Name = "TextureViewer")]

    public partial class SkiaTextureViewer : ControlWrapperBase<TextureViewer>
    {
        [ImplementProperty("TextureViewer.SourceProperty", Order = -10)]
        protected Optional<SKImage> _source;

        [ImplementProperty("TextureViewer.StretchProperty", Order = -10)]
        protected Optional<Stretch> _stretch;

        [ImplementProperty("TextureViewer.StretchDirectionProperty", Order = -10)]
        protected Optional<StretchDirection> _stretchDirection;
    }
}
