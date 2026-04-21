using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="TextBox"/>.
    /// </summary>
    public static class TextBoxExtensions
    {
        /// <inheritdoc cref="TextBox.IsInactiveSelectionHighlightEnabled"/>
        public static bool IsInactiveSelectionHighlightEnabled(this TextBox textBox) =>
            textBox != null ? textBox.IsInactiveSelectionHighlightEnabled : true;

        /// <inheritdoc cref="TextBox.IsInactiveSelectionHighlightEnabled"/>
        public static void SetIsInactiveSelectionHighlightEnabled(
            this TextBox textBox,
            bool isInactiveSelectionHighlightEnabled
        )
        {
            if (textBox is not null)
                textBox.IsInactiveSelectionHighlightEnabled = isInactiveSelectionHighlightEnabled;
        }

        /// <inheritdoc cref="TextBox.ClearSelectionOnLostFocus"/>
        public static bool ClearSelectionOnLostFocus(this TextBox textBox) =>
            textBox != null ? textBox.ClearSelectionOnLostFocus : true;

        /// <inheritdoc cref="TextBox.ClearSelectionOnLostFocus"/>
        public static void SetClearSelectionOnLostFocus(
            this TextBox textBox,
            bool clearSelectionOnLostFocus
        )
        {
            if (textBox is not null)
                textBox.ClearSelectionOnLostFocus = clearSelectionOnLostFocus;
        }

        /// <inheritdoc cref="TextBox.AcceptsReturn"/>
        public static bool AcceptsReturn(this TextBox textBox) =>
            textBox != null ? textBox.AcceptsReturn : false;

        /// <inheritdoc cref="TextBox.AcceptsReturn"/>
        public static void SetAcceptsReturn(this TextBox textBox, bool acceptsReturn)
        {
            if (textBox is not null)
                textBox.AcceptsReturn = acceptsReturn;
        }

        /// <inheritdoc cref="TextBox.AcceptsTab"/>
        public static bool AcceptsTab(this TextBox textBox) =>
            textBox != null ? textBox.AcceptsTab : false;

        /// <inheritdoc cref="TextBox.AcceptsTab"/>
        public static void SetAcceptsTab(this TextBox textBox, bool acceptsTab)
        {
            if (textBox is not null)
                textBox.AcceptsTab = acceptsTab;
        }

        /// <inheritdoc cref="TextBox.CaretIndex"/>
        public static int CaretIndex(this TextBox textBox) =>
            textBox != null ? textBox.CaretIndex : 0;

        /// <inheritdoc cref="TextBox.CaretIndex"/>
        public static void SetCaretIndex(this TextBox textBox, int caretIndex)
        {
            if (textBox is not null)
                textBox.CaretIndex = caretIndex;
        }

        /// <inheritdoc cref="TextBox.IsReadOnly"/>
        public static bool IsReadOnly(this TextBox textBox) =>
            textBox != null ? textBox.IsReadOnly : false;

        /// <inheritdoc cref="TextBox.IsReadOnly"/>
        public static void SetIsReadOnly(this TextBox textBox, bool isReadOnly)
        {
            if (textBox is not null)
                textBox.IsReadOnly = isReadOnly;
        }

        /// <inheritdoc cref="TextBox.PasswordChar"/>
        public static char PasswordChar(this TextBox textBox) =>
            textBox != null ? textBox.PasswordChar : '\0';

        /// <inheritdoc cref="TextBox.PasswordChar"/>
        public static void SetPasswordChar(this TextBox textBox, char passwordChar)
        {
            if (textBox is not null)
                textBox.PasswordChar = passwordChar;
        }

        /// <inheritdoc cref="TextBox.SelectionBrush"/>
        public static IBrush? SelectionBrush(this TextBox textBox) =>
            textBox != null ? textBox.SelectionBrush : null;

        /// <inheritdoc cref="TextBox.SelectionBrush"/>
        public static void SetSelectionBrush(this TextBox textBox, IBrush? selectionBrush)
        {
            if (textBox is not null)
                textBox.SelectionBrush = selectionBrush;
        }

        /// <inheritdoc cref="TextBox.SelectionForegroundBrush"/>
        public static IBrush? SelectionForegroundBrush(this TextBox textBox) =>
            textBox != null ? textBox.SelectionForegroundBrush : null;

        /// <inheritdoc cref="TextBox.SelectionForegroundBrush"/>
        public static void SetSelectionForegroundBrush(
            this TextBox textBox,
            IBrush? selectionForegroundBrush
        )
        {
            if (textBox is not null)
                textBox.SelectionForegroundBrush = selectionForegroundBrush;
        }

        /// <inheritdoc cref="TextBox.CaretBrush"/>
        public static IBrush? CaretBrush(this TextBox textBox) =>
            textBox != null ? textBox.CaretBrush : null;

        /// <inheritdoc cref="TextBox.CaretBrush"/>
        public static void SetCaretBrush(this TextBox textBox, IBrush? caretBrush)
        {
            if (textBox is not null)
                textBox.CaretBrush = caretBrush;
        }

        /// <inheritdoc cref="TextBox.CaretBlinkInterval"/>
        public static TimeSpan CaretBlinkInterval(this TextBox textBox) =>
            textBox != null ? textBox.CaretBlinkInterval : TimeSpan.FromMilliseconds(500);

        /// <inheritdoc cref="TextBox.CaretBlinkInterval"/>
        public static void SetCaretBlinkInterval(this TextBox textBox, TimeSpan caretBlinkInterval)
        {
            if (textBox is not null)
                textBox.CaretBlinkInterval = caretBlinkInterval;
        }

        /// <inheritdoc cref="TextBox.SelectionStart"/>
        public static int SelectionStart(this TextBox textBox) =>
            textBox != null ? textBox.SelectionStart : 0;

        /// <inheritdoc cref="TextBox.SelectionStart"/>
        public static void SetSelectionStart(this TextBox textBox, int selectionStart)
        {
            if (textBox is not null)
                textBox.SelectionStart = selectionStart;
        }

        /// <inheritdoc cref="TextBox.SelectionEnd"/>
        public static int SelectionEnd(this TextBox textBox) =>
            textBox != null ? textBox.SelectionEnd : 0;

        /// <inheritdoc cref="TextBox.SelectionEnd"/>
        public static void SetSelectionEnd(this TextBox textBox, int selectionEnd)
        {
            if (textBox is not null)
                textBox.SelectionEnd = selectionEnd;
        }

        /// <inheritdoc cref="TextBox.MaxLength"/>
        public static int MaxLength(this TextBox textBox) =>
            textBox != null ? textBox.MaxLength : 0;

        /// <inheritdoc cref="TextBox.MaxLength"/>
        public static void SetMaxLength(this TextBox textBox, int maxLength)
        {
            if (textBox is not null)
                textBox.MaxLength = maxLength;
        }

        /// <inheritdoc cref="TextBox.MaxLines"/>
        public static int MaxLines(this TextBox textBox) => textBox != null ? textBox.MaxLines : 0;

        /// <inheritdoc cref="TextBox.MaxLines"/>
        public static void SetMaxLines(this TextBox textBox, int maxLines)
        {
            if (textBox is not null)
                textBox.MaxLines = maxLines;
        }

        /// <inheritdoc cref="TextBox.MinLines"/>
        public static int MinLines(this TextBox textBox) => textBox != null ? textBox.MinLines : 0;

        /// <inheritdoc cref="TextBox.MinLines"/>
        public static void SetMinLines(this TextBox textBox, int minLines)
        {
            if (textBox is not null)
                textBox.MinLines = minLines;
        }

        /// <inheritdoc cref="TextBox.LetterSpacing"/>
        public static float LetterSpacing(this TextBox textBox) =>
            textBox != null ? (float)textBox.LetterSpacing : 0f;

        /// <inheritdoc cref="TextBox.LetterSpacing"/>
        public static void SetLetterSpacing(this TextBox textBox, float letterSpacing)
        {
            if (textBox is not null)
                textBox.LetterSpacing = letterSpacing;
        }

        /// <inheritdoc cref="TextBox.LineHeight"/>
        public static float LineHeight(this TextBox textBox) =>
            textBox != null ? (float)textBox.LineHeight : float.NaN;

        /// <inheritdoc cref="TextBox.LineHeight"/>
        public static void SetLineHeight(this TextBox textBox, float lineHeight)
        {
            if (textBox is not null)
                textBox.LineHeight = lineHeight;
        }

        /// <inheritdoc cref="TextBox.Text"/>
        public static string? Text(this TextBox textBox) => textBox != null ? textBox.Text : null;

        /// <inheritdoc cref="TextBox.Text"/>
        public static void SetText(this TextBox textBox, string? text)
        {
            if (textBox is not null)
                textBox.Text = text;
        }

        /// <inheritdoc cref="TextBox.HorizontalContentAlignment"/>
        public static HorizontalAlignment HorizontalContentAlignment(this TextBox textBox) =>
            textBox != null ? textBox.HorizontalContentAlignment : default(HorizontalAlignment);

        /// <inheritdoc cref="TextBox.HorizontalContentAlignment"/>
        public static void SetHorizontalContentAlignment(
            this TextBox textBox,
            HorizontalAlignment horizontalContentAlignment
        )
        {
            if (textBox is not null)
                textBox.HorizontalContentAlignment = horizontalContentAlignment;
        }

        /// <inheritdoc cref="TextBox.VerticalContentAlignment"/>
        public static VerticalAlignment VerticalContentAlignment(this TextBox textBox) =>
            textBox != null ? textBox.VerticalContentAlignment : default(VerticalAlignment);

        /// <inheritdoc cref="TextBox.VerticalContentAlignment"/>
        public static void SetVerticalContentAlignment(
            this TextBox textBox,
            VerticalAlignment verticalContentAlignment
        )
        {
            if (textBox is not null)
                textBox.VerticalContentAlignment = verticalContentAlignment;
        }

        /// <inheritdoc cref="TextBox.TextAlignment"/>
        public static TextAlignment TextAlignment(this TextBox textBox) =>
            textBox != null ? textBox.TextAlignment : default(TextAlignment);

        /// <inheritdoc cref="TextBox.TextAlignment"/>
        public static void SetTextAlignment(this TextBox textBox, TextAlignment textAlignment)
        {
            if (textBox is not null)
                textBox.TextAlignment = textAlignment;
        }

        /// <inheritdoc cref="TextBox.Watermark"/>
        public static string? Watermark(this TextBox textBox) =>
            textBox != null ? textBox.Watermark : null;

        /// <inheritdoc cref="TextBox.Watermark"/>
        public static void SetWatermark(this TextBox textBox, string? watermark)
        {
            if (textBox is not null)
                textBox.Watermark = watermark;
        }

        /// <inheritdoc cref="TextBox.UseFloatingWatermark"/>
        public static bool UseFloatingWatermark(this TextBox textBox) =>
            textBox != null ? textBox.UseFloatingWatermark : false;

        /// <inheritdoc cref="TextBox.UseFloatingWatermark"/>
        public static void SetUseFloatingWatermark(this TextBox textBox, bool useFloatingWatermark)
        {
            if (textBox is not null)
                textBox.UseFloatingWatermark = useFloatingWatermark;
        }

        /// <inheritdoc cref="TextBox.InnerLeftContent"/>
        public static object? InnerLeftContent(this TextBox textBox) =>
            textBox != null ? textBox.InnerLeftContent : null;

        /// <inheritdoc cref="TextBox.InnerLeftContent"/>
        public static void SetInnerLeftContent(this TextBox textBox, object? innerLeftContent)
        {
            if (textBox is not null)
                textBox.InnerLeftContent = innerLeftContent;
        }

        /// <inheritdoc cref="TextBox.InnerRightContent"/>
        public static object? InnerRightContent(this TextBox textBox) =>
            textBox != null ? textBox.InnerRightContent : null;

        /// <inheritdoc cref="TextBox.InnerRightContent"/>
        public static void SetInnerRightContent(this TextBox textBox, object? innerRightContent)
        {
            if (textBox is not null)
                textBox.InnerRightContent = innerRightContent;
        }

        /// <inheritdoc cref="TextBox.RevealPassword"/>
        public static bool RevealPassword(this TextBox textBox) =>
            textBox != null ? textBox.RevealPassword : false;

        /// <inheritdoc cref="TextBox.RevealPassword"/>
        public static void SetRevealPassword(this TextBox textBox, bool revealPassword)
        {
            if (textBox is not null)
                textBox.RevealPassword = revealPassword;
        }

        /// <inheritdoc cref="TextBox.TextWrapping"/>
        public static TextWrapping TextWrapping(this TextBox textBox) =>
            textBox != null ? textBox.TextWrapping : default(TextWrapping);

        /// <inheritdoc cref="TextBox.TextWrapping"/>
        public static void SetTextWrapping(this TextBox textBox, TextWrapping textWrapping)
        {
            if (textBox is not null)
                textBox.TextWrapping = textWrapping;
        }

        /// <inheritdoc cref="TextBox.NewLine"/>
        public static string NewLine(this TextBox textBox) =>
            textBox != null ? textBox.NewLine : Environment.NewLine;

        /// <inheritdoc cref="TextBox.NewLine"/>
        public static void SetNewLine(this TextBox textBox, string newLine)
        {
            if (textBox is not null)
                textBox.NewLine = newLine;
        }

        /// <inheritdoc cref="TextBox.IsUndoEnabled"/>
        public static bool IsUndoEnabled(this TextBox textBox) =>
            textBox != null ? textBox.IsUndoEnabled : true;

        /// <inheritdoc cref="TextBox.IsUndoEnabled"/>
        public static void SetIsUndoEnabled(this TextBox textBox, bool isUndoEnabled)
        {
            if (textBox is not null)
                textBox.IsUndoEnabled = isUndoEnabled;
        }

        /// <inheritdoc cref="TextBox.UndoLimit"/>
        public static int UndoLimit(this TextBox textBox) =>
            textBox != null ? textBox.UndoLimit : -1; // -1 matches UndoRedoHelper.DefaultUndoLimit effectively

        /// <inheritdoc cref="TextBox.UndoLimit"/>
        public static void SetUndoLimit(this TextBox textBox, int undoLimit)
        {
            if (textBox is not null)
                textBox.UndoLimit = undoLimit;
        }

        /// <inheritdoc cref="TextBox.CanCut"/>
        public static bool CanCut(this TextBox textBox) => textBox != null ? textBox.CanCut : false;

        /// <inheritdoc cref="TextBox.CanCopy"/>
        public static bool CanCopy(this TextBox textBox) =>
            textBox != null ? textBox.CanCopy : false;

        /// <inheritdoc cref="TextBox.CanPaste"/>
        public static bool CanPaste(this TextBox textBox) =>
            textBox != null ? textBox.CanPaste : false;

        /// <inheritdoc cref="TextBox.CanUndo"/>
        public static bool CanUndo(this TextBox textBox) =>
            textBox != null ? textBox.CanUndo : false;

        /// <inheritdoc cref="TextBox.CanRedo"/>
        public static bool CanRedo(this TextBox textBox) =>
            textBox != null ? textBox.CanRedo : false;

        /// <inheritdoc cref="TextBox.Clear"/>
        public static void Clear(this TextBox textBox)
        {
            textBox?.Clear();
        }

        /// <inheritdoc cref="TextBox.SelectAll"/>
        public static void SelectAll(this TextBox textBox)
        {
            textBox?.SelectAll();
        }

        /// <inheritdoc cref="TextBox.ClearSelection"/>
        public static void ClearSelection(this TextBox textBox)
        {
            textBox?.ClearSelection();
        }
    }
}
