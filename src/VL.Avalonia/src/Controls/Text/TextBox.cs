using Avalonia.Controls;
using Avalonia.Media;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <b>TextBox</b> presents an area for typed (keyboard) input. It can be for a single or multiple lines of input.
/// </summary>
[ProcessNode(Name = "TextBox")]
public partial class TextBoxWrapper : ControlWrapperBase<TextBox>
{
    protected ChannelTwoWayBinding<string> _textBinding;
    public TextBoxWrapper()
    {
        _textBinding = new ChannelTwoWayBinding<string>(_output, TextBox.TextProperty);
    }

    /// <summary>
    /// The text content of the TextBox
    /// </summary>
    /// <param name="textChannel"></param>
    [Fragment(Order = -10)]
    public void SetTextChannel(IChannel<string>? textChannel) =>
        _textBinding.SetChannel(textChannel);

    /*
    // Selection and Navigation Properties
    // This should be handled via channel, so not going to include
    /// <summary>
    /// Current position of the text cursor/caret
    /// </summary>
    [ImplementProperty("TextBox.CaretIndexProperty")]
    protected Optional<int> _caretIndex;

    /// <summary>
    /// Starting position of selected text
    /// </summary>
    [ImplementProperty("TextBox.SelectionStartProperty")]
    protected Optional<int> _selectionStart;

    /// <summary>
    /// Ending position of selected text
    /// </summary>
    [ImplementProperty("TextBox.SelectionEndProperty")]
    protected Optional<int> _selectionEnd;
    */


    /// <summary>
    /// Placeholder text displayed when TextBox is empty
    /// </summary>
    [ImplementProperty("TextBox.WatermarkProperty")]
    private Optional<string> _watermark;

    /// <summary>
    /// Whether watermark floats above text when typing
    /// </summary>
    [ImplementProperty("TextBox.UseFloatingWatermarkProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _useFloatingWatermark;

    /// <summary>
    /// Whether the TextBox is read-only (cannot be edited)
    /// </summary>
    [ImplementProperty("TextBox.IsReadOnlyProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isReadOnly;

    /// <summary>
    /// Whether the TextBox allows and displays newline characters
    /// </summary>
    [ImplementProperty("TextBox.AcceptsReturnProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _acceptsReturn;

    /// <summary>
    /// Whether the TextBox allows and displays tab characters
    /// </summary>
    [ImplementProperty("TextBox.AcceptsTabProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _acceptsTab;

    /// <summary>
    /// Character used for password masking (e.g., '*')
    /// </summary>
    [ImplementProperty("TextBox.PasswordCharProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<char> _passwordChar;

    /// <summary>
    /// Whether password text should be revealed temporarily
    /// </summary>
    [ImplementProperty("TextBox.RevealPasswordProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _revealPassword;

    /// <summary>
    /// Maximum number of characters that can be entered
    /// </summary>
    [ImplementProperty("TextBox.MaxLengthProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _maxLength;

    /// <summary>
    /// How text wraps (NoWrap, Wrap, WrapWithOverflow)
    /// </summary>
    [ImplementProperty("TextBox.TextWrappingProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<TextWrapping> _textWrapping;

    /// <summary>
    /// Maximum number of visible lines for sizing
    /// </summary>
    [ImplementProperty("TextBox.MaxLinesProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _maxLines;

    /// <summary>
    /// Minimum number of visible lines for sizing
    /// </summary>
    [ImplementProperty("TextBox.MinLinesProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _minLines;

    /// <summary>
    /// Characters inserted when Enter is pressed
    /// </summary>
    [ImplementProperty("TextBox.NewLineProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<string> _newLine;


    /// <summary>
    /// Whether undo/redo functionality is enabled
    /// </summary>
    [ImplementProperty("TextBox.IsUndoEnabledProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isUndoEnabled;

    /// <summary>
    /// Maximum number of items in the undo stack
    /// </summary>
    [ImplementProperty("TextBox.UndoLimitProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<int> _undoLimit;

    /// <summary>
    /// Brush used to highlight selected text background
    /// </summary>
    [ImplementProperty("TextBox.SelectionBrushProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<IBrush> _selectionBrush;

    /// <summary>
    /// Brush used for selected text foreground color
    /// </summary>
    [ImplementProperty("TextBox.SelectionForegroundBrushProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<IBrush> _selectionForegroundBrush;

    /// <summary>
    /// Brush used for the text caret/cursor
    /// </summary>
    [ImplementProperty("TextBox.CaretBrushProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<IBrush> _caretBrush;

    /// <summary>
    /// How fast the caret blinks (default 500ms)
    /// </summary>
    [ImplementProperty("TextBox.CaretBlinkIntervalProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<TimeSpan> _caretBlinkInterval;

    /// <summary>
    /// Custom content positioned on the left side of text
    /// </summary>
    [ImplementProperty("TextBox.InnerLeftContentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<object> _innerLeftContent;

    /// <summary>
    /// Custom content positioned on the right side of text
    /// </summary>
    [ImplementProperty("TextBox.InnerRightContentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<object> _innerRightContent;

    /// <summary>
    /// Whether selection is highlighted when not focused
    /// </summary>
    [ImplementProperty("TextBox.IsInactiveSelectionHighlightEnabledProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isInactiveSelectionHighlightEnabled;

    /// <summary>
    /// Whether selection is cleared when focus is lost
    /// </summary>
    [ImplementProperty("TextBox.ClearSelectionOnLostFocusProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _clearSelectionOnLostFocus;

    /// <summary>
    /// Whether the Cut command can be executed
    /// </summary>
    [ImplementProperty("TextBox.CanCutProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _canCut;

    /// <summary>
    /// Whether the Copy command can be executed
    /// </summary>
    [ImplementProperty("TextBox.CanCopyProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _canCopy;

    /// <summary>
    /// Whether the Paste command can be executed
    /// </summary>
    [ImplementProperty("TextBox.CanPasteProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _canPaste;

    /// <summary>
    /// Whether there are actions that can be undone
    /// </summary>
    [ImplementProperty("TextBox.CanUndoProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _canUndo;

    /// <summary>
    /// Whether there are actions that can be redone
    /// </summary>
    [ImplementProperty("TextBox.CanRedoProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _canRedo;
}

