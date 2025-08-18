using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

[ProcessNode]
public abstract partial class TextBoxWrapperBase<T> : ControlWrapperBase<T> where T : TextBox, new()
{
    protected ChannelTwoWayBinding<string> _textBinding;
    public TextBoxWrapperBase()
    {
        _textBinding = new ChannelTwoWayBinding<string>(_output, TextBox.TextProperty);
    }

    /// <param name="textChannel">
    /// The text content of the TextBox
    /// </param>
    [Fragment(Order = -10)]
    public void SetTextChannel(IChannel<string>? textChannel) =>
        _textBinding.SetChannel(textChannel);

    /*
    // Selection and Navigation Properties
    // This should be handled via channel, so not going to include
    /// <param name="_caretIndex">
    /// Current position of the text cursor/caret
    /// </param>
    [ImplementProperty("TextBox.CaretIndexProperty")]
    protected Optional<int> _caretIndex;

    /// <param name="_selectionStart">
    /// Starting position of selected text
    /// </param>
    [ImplementProperty("TextBox.SelectionStartProperty")]
    protected Optional<int> _selectionStart;

    /// <param name="_selectionEnd">
    /// Ending position of selected text
    /// </param>
    [ImplementProperty("TextBox.SelectionEndProperty")]
    protected Optional<int> _selectionEnd;
    */


    /// <param name="watermark">
    /// Placeholder text displayed when TextBox is empty
    /// </param>
    [ImplementProperty("TextBox.WatermarkProperty", Order = -7)]
    private Optional<string> _watermark;

    /// <param name="useFloatingWatermark">
    /// Whether watermark floats above text when typing
    /// </param>
    [ImplementProperty("TextBox.UseFloatingWatermarkProperty", Order = -6, PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _useFloatingWatermark;

    /// <param name="isReadOnly">
    /// Whether the TextBox is read-only (cannot be edited)
    /// </param>
    [ImplementProperty("TextBox.IsReadOnlyProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isReadOnly;

    /// <param name="acceptsReturn">
    /// Whether the TextBox allows and displays newline characters
    /// </param>
    [ImplementProperty("TextBox.AcceptsReturnProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _acceptsReturn;

    /// <param name="acceptsTab">
    /// Whether the TextBox allows and displays tab characters
    /// </param>
    [ImplementProperty("TextBox.AcceptsTabProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _acceptsTab;

    /// <param name="passwordChar">
    /// Character used for password masking (e.g., '*')
    /// </param>
    [ImplementProperty("TextBox.PasswordCharProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<char> _passwordChar;

    /// <param name="revealPassword">
    /// Whether password text should be revealed temporarily
    /// </param>
    [ImplementProperty("TextBox.RevealPasswordProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _revealPassword;

    /// <param name="maxLength">
    /// Maximum number of characters that can be entered
    /// </param>
    [ImplementProperty("TextBox.MaxLengthProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _maxLength;

    /// <param name="textWrapping">
    /// How text wraps (NoWrap, Wrap, WrapWithOverflow)
    /// </param>
    [ImplementProperty("TextBox.TextWrappingProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<TextWrapping> _textWrapping;

    /// <param name="maxLines">
    /// Maximum number of visible lines for sizing
    /// </param>
    [ImplementProperty("TextBox.MaxLinesProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _maxLines;

    /// <param name="minLines">
    /// Minimum number of visible lines for sizing
    /// </param>
    [ImplementProperty("TextBox.MinLinesProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _minLines;

    /// <param name="newLine">
    /// Characters inserted when Enter is pressed
    /// </param>
    [ImplementProperty("TextBox.NewLineProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<string> _newLine;


    /// <param name="isUndoEnabled">
    /// Whether undo/redo functionality is enabled
    /// </param>
    [ImplementProperty("TextBox.IsUndoEnabledProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isUndoEnabled;

    /// <param name="undoLimit">
    /// Maximum number of items in the undo stack
    /// </param>
    [ImplementProperty("TextBox.UndoLimitProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _undoLimit;

    /// <param name="selectionBrush">
    /// Brush used to highlight selected text background
    /// </param>
    [ImplementProperty("TextBox.SelectionBrushProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<IBrush> _selectionBrush;

    /// <param name="selectionForegroundBrush">
    /// Brush used for selected text foreground color
    /// </param>
    [ImplementProperty("TextBox.SelectionForegroundBrushProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<IBrush> _selectionForegroundBrush;

    /// <param name="caretBrush">
    /// Brush used for the text caret/cursor
    /// </param>
    [ImplementProperty("TextBox.CaretBrushProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<IBrush> _caretBrush;

    /// <param name="caretBlinkInterval">
    /// How fast the caret blinks (default 500ms)
    /// </param>
    [ImplementProperty("TextBox.CaretBlinkIntervalProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<TimeSpan> _caretBlinkInterval;

    /// <param name="innerLeftContent">
    /// Custom content positioned on the left side of text
    /// </param>
    [ImplementProperty("TextBox.InnerLeftContentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<object> _innerLeftContent;

    /// <param name="innerRightContent">
    /// Custom content positioned on the right side of text
    /// </param>
    [ImplementProperty("TextBox.InnerRightContentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<object> _innerRightContent;

    /// <param name="isInactiveSelectionHighlightEnabled">
    /// Whether selection is highlighted when not focused
    /// </param>
    [ImplementProperty("TextBox.IsInactiveSelectionHighlightEnabledProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isInactiveSelectionHighlightEnabled;

    /// <param name="clearSelectionOnLostFocus">
    /// Whether selection is cleared when focus is lost
    /// </param>
    [ImplementProperty("TextBox.ClearSelectionOnLostFocusProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _clearSelectionOnLostFocus;
}

/// <summary>
/// The <b>TextBox</b> presents an area for typed (keyboard) input. It can be for a single or multiple lines of input.
/// </summary>
[ProcessNode(Name = "TextBox")]
public partial class TextBoxWrapper : TextBoxWrapperBase<TextBox>
{
}