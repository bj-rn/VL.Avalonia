using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace VL.Avalonia.Custom.Controls.Value
{
    public partial class NumberField : NumericUpDown
    {
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            ValueButton = e.NameScope.Find<Button>("PART_ValueButton");

            Border mainBorder = e.NameScope.Find<Border>("PART_MainBorder");
            mainBorder.AddHandler(PointerReleasedEvent, Button_OnPointerReleased, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
            mainBorder.PointerPressed += MainBorder_PointerPressed;
            mainBorder.PointerMoved += MainBorder_PointerMoved;
            mainBorder.PointerReleased += MainBorder_PointerReleased;

            InputValue = e.NameScope.Find<TextBox>("PART_TextBox");
            if (InputValue != null)
            {
                InputValue.PointerExited += InputValue_PointerExited;
                InputValue.PointerEntered += InputValue_PointerEntered;
                InputValue.KeyDown += InputValue_KeyDown;
            }
            IsEditing = false;
        }

        private TextBox InputValue { get; set; }
        private Button ValueButton { get; set; }
        private decimal? oldValue;
        Point pressedPosition;

        private void MainBorder_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (IsEditing == true)
                return;

            if (e.GetCurrentPoint(this).Properties.PointerUpdateKind == PointerUpdateKind.LeftButtonPressed)
            {
                oldValue = this.Value;
                pressedPosition = e.GetPosition(this);
            }
        }

        private void MainBorder_PointerMoved(object? sender, PointerEventArgs e)
        {
            if (ValueButton.IsPressed)
            {
                var position = e.GetPosition(this);
                var offset = position - pressedPosition;
                var newValue = Convert.ToDecimal(this.Value) + (decimal)(offset.X * 0.01);
                this.Value = newValue;
                oldValue = newValue;
                pressedPosition = position;
            }
        }

        private void MainBorder_PointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            pressedPosition = e.GetPosition(this);
        }

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
                this.Value = oldValue;
                OnSwitchToNormalMode();
            }
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
                this.oldValue = this.Value;
                IsEditing = true;
                InputValue.Focus();
                InputValue.SelectAll();
                var topLevel = TopLevel.GetTopLevel(this)?.FocusManager?.GetFocusedElement();
                AddParentWindowHandlers();
            }
            e.Handled = false;
        }



    }
}
