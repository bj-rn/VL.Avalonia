using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Layout;
using Avalonia.Media;
using System.Globalization;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// The <c>NumericUpDown</c> is an editable numeric input with up and down spinner buttons attached. Non-numeric characters are ignored in the input. The value can also be changed by clicking the buttons, or by using the keyboard arrow keys. The mouse wheel (if present) will also change the value.
/// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/numericupdown">NumericUpDown</see>
/// </summary>
[ProcessNode(Name = "NumericUpDown")]
public partial class NumericUpDownWrapper : ControlWrapperBase<NumericUpDown>
{
    protected ChannelTwoWayBinding<float?, decimal?> _valueBinding;
    protected ChannelTwoWayBinding<string?> _textBinding;
    public NumericUpDownWrapper()
    {
        _valueBinding = new(_output, NumericUpDown.ValueProperty, (x) => (decimal?)x, (x) => (float?)x);
        _textBinding = new(_output, NumericUpDown.TextProperty);
    }

    #region Core Value Properties

    /// <param name="valueChannel">
    /// The current numeric value of the control
    /// </param>
    [Fragment(Order = -10)]
    public void SetValueChannel(IChannel<float?> valueChannel) =>
        _valueBinding.SetChannel(valueChannel);

    /// <param name="textChannel">
    /// The formatted string representation of the value
    /// </param>
    [Fragment(Order = -7)]
    public void SetTextChannel([Pin(Visibility = Model.PinVisibility.Optional)] IChannel<string?> textChannel) =>
        _textBinding.SetChannel(textChannel);

    /// <param name="watermark">
    /// Placeholder text shown when the value is null
    /// </param>
    [ImplementProperty("NumericUpDown.WatermarkProperty")]
    protected Optional<string> _watermark;

    #endregion

    #region Range Properties

    /// <param name="minimum">
    /// The minimum allowed value
    /// </param>
    [ImplementProperty<float, decimal>("NumericUpDown.MinimumProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _minimum;

    /// <param name="maximum">
    /// The maximum allowed value
    /// </param>
    [ImplementProperty<float, decimal>("NumericUpDown.MaximumProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _maximum;

    /// <param name="increment">
    /// The amount by which to increment or decrement the value
    /// </param>
    [ImplementProperty<float, decimal>("NumericUpDown.IncrementProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<float> _increment;

    /// <param name="clipValueToMinMax">
    /// Whether the value should be automatically clipped to the min/max range
    /// </param>
    [ImplementProperty("NumericUpDown.ClipValueToMinMaxProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _clipValueToMinMax;

    #endregion

    #region Spinner Properties

    /// <param name="allowSpin">
    /// Whether increment/decrement operations are allowed via keyboard, buttons, or mouse wheel
    /// </param>
    [ImplementProperty("NumericUpDown.AllowSpinProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _allowSpin;

    /// <param name="showButtonSpinner">
    /// Whether the up/down buttons should be displayed
    /// </param>
    [ImplementProperty("NumericUpDown.ShowButtonSpinnerProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _showButtonSpinner;

    /// <param name="buttonSpinnerLocation">
    /// The location of the spinner buttons (Left or Right)
    /// </param>
    [ImplementProperty("NumericUpDown.ButtonSpinnerLocationProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<Location> _buttonSpinnerLocation;

    #endregion

    #region Behavior Properties

    /// <param name="isReadOnly">
    /// Whether the control is read-only (cannot be edited by user)
    /// </param>
    [ImplementProperty("NumericUpDown.IsReadOnlyProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<bool> _isReadOnly;

    #endregion

    #region Formatting Properties

    /// <param name="formatString">
    /// The format string used to display the value (e.g., "F2", "C", "P")
    /// </param>
    [ImplementProperty("NumericUpDown.FormatStringProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<string> _formatString;

    /// <param name="numberFormat">
    /// The NumberFormatInfo used for formatting and parsing
    /// </param>
    [ImplementProperty("NumericUpDown.NumberFormatProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<NumberFormatInfo> _numberFormat;

    /// <param name="parsingNumberStyle">
    /// The NumberStyles used when parsing text input
    /// </param>
    [ImplementProperty("NumericUpDown.ParsingNumberStyleProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<NumberStyles> _parsingNumberStyle;

    /// <param name="textConverter">
    /// Custom converter for bidirectional text-value conversion
    /// </param>
    [ImplementProperty("NumericUpDown.TextConverterProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<IValueConverter> _textConverter;

    #endregion

    #region Layout and Alignment Properties

    /// <param name="horizontalContentAlignment">
    /// Horizontal alignment of content within the control
    /// </param>
    [ImplementProperty("NumericUpDown.HorizontalContentAlignmentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<HorizontalAlignment> _horizontalContentAlignment;

    /// <param name="verticalContentAlignment">
    /// Vertical alignment of content within the control
    /// </param>
    [ImplementProperty("NumericUpDown.VerticalContentAlignmentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<VerticalAlignment> _verticalContentAlignment;

    /// <param name="textAlignment">
    /// Horizontal alignment of text within the text box
    /// </param>
    [ImplementProperty("NumericUpDown.TextAlignmentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<TextAlignment> _textAlignment;

    #endregion

    #region Inner Content Properties

    /// <param name="innerLeftContent">
    /// Custom content positioned on the left side of the text area
    /// </param>
    [ImplementProperty("NumericUpDown.InnerLeftContentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<object> _innerLeftContent;

    /// <param name="innerRightContent">
    /// Custom content positioned on the right side of the text area
    /// </param>
    [ImplementProperty("NumericUpDown.InnerRightContentProperty", PinVisibility = Model.PinVisibility.Optional)]
    protected Optional<object> _innerRightContent;

    #endregion
}
