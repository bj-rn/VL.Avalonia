using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls;


// TODO: 
// Runs https://github.com/AvaloniaUI/Avalonia/blob/ae0573a789f829d1f5d168e313a79fcbcb9ffc83/src/Avalonia.Controls/TextBlock.cs#L166
// Inline's https://github.com/AvaloniaUI/Avalonia/blob/ae0573a789f829d1f5d168e313a79fcbcb9ffc83/src/Avalonia.Controls/TextBlock.cs#L167
// Suspect the best way to do it is to add TextBlock (Advanced) or TextBlock (Runs)


/// <summary>
/// Simple text block control that displays text.
/// More advanced text styling can be performed using Style properties.
/// </summary>
[ProcessNode(Name = "TextBlock")]
public partial class TextBlockWrapper : ControlWrapperBase<TextBlock>
{
    [ImplementProperty("TextBlock.TextProperty")]
    private Optional<string> _text;

    [ImplementProperty("TextBlock.FontFamilyProperty")]
    private Optional<string> _fontFamily;

    [ImplementProperty("TextBlock.FontSizeProperty")]
    private Optional<float> _fontSize;

    [ImplementProperty("TextBlock.FontStyleProperty")]
    private Optional<FontStyle> _fontStyle;

    [ImplementProperty("TextBlock.FontWeightProperty")]
    private Optional<FontWeight> _fontWeight;

    [ImplementProperty("TextBlock.TextAlignmentProperty")]
    private Optional<TextAlignment> _textAlignment;
}
