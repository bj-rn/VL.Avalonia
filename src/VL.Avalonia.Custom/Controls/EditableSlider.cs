using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace VL.Avalonia.Custom.Controls
{
    public partial class EditableSlider : Slider
    {
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            Border SliderBorder = e.NameScope.Find<Border>("PART_MainBorder");
            SliderBorder.AddHandler(PointerReleasedEvent, Button_OnPointerReleased, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);

            InputValue = e.NameScope.Find<TextBox>("textInput");
            if (InputValue != null)
            {
                InputValue.PointerExited += InputValue_PointerExited;
                InputValue.PointerEntered += InputValue_PointerEntered;
                InputValue.KeyDown += InputValue_KeyDown;
            }

            Focusable = true;
            IsEditing = false;
        }

        private double oldValue;
        private TextBox InputValue { get; set; }

        private void AddParentWindowHandlers()
        {
            var topLevel = TopLevel.GetTopLevel(this);
            topLevel?.AddHandler(PointerReleasedEvent, TopLevel_OnPointerReleased, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
            topLevel?.AddHandler(PointerPressedEvent, TopLevel_OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        }

        private void RemoveParentWindowHandlers()
        {
            var topLevel = TopLevel.GetTopLevel(this);
            topLevel?.RemoveHandler(PointerReleasedEvent, TopLevel_OnPointerReleased);
            topLevel?.RemoveHandler(PointerPressedEvent, TopLevel_OnPointerPressed);
        }


        private void OnSwitchToNormalMode()
        {
            IsEditing = false;

            if (InputValue != null)
                oldValue = this.Value;

            RemoveParentWindowHandlers();
        }


        private void InputValue_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                IsEditing = false;
                OnSwitchToNormalMode();
            }
            else if (e.Key == Key.Escape)
            {
                IsEditing = false;
                CancelUpdateValue();
                OnSwitchToNormalMode();
            }
        }

        private void InputValue_PointerEntered(object? sender, PointerEventArgs e)
        {
            RemoveParentWindowHandlers();
        }

        private void InputValue_PointerExited(object? sender, PointerEventArgs e)
        {
            if (IsEditing == false)
                return;

            AddParentWindowHandlers();
            e.Handled = false;
        }


        private void Button_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.PointerUpdateKind == PointerUpdateKind.MiddleButtonReleased)
            {
                IsEditing = true;
                InputValue.Focus();
                InputValue.SelectAll();
                var topLevel = TopLevel.GetTopLevel(this)?.FocusManager?.GetFocusedElement();
                AddParentWindowHandlers();
            }
            e.Handled = false;
        }

        private void TopLevel_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (IsEditing == false)
                return;

            OnSwitchToNormalMode();
            e.Handled = true;
        }

        private void TopLevel_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            RemoveParentWindowHandlers();
            e.Handled = true;
        }

        private void CancelUpdateValue()
        {
            InputValue.Text = oldValue.ToString();
        }
    }
}
