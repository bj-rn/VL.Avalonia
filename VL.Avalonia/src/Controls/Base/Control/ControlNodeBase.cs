using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Transformation;
using Stride.Core.Mathematics;
using VL.Avalonia.Animation.Transitions;
using VL.Avalonia.Attributes;
using VL.Avalonia.Styles;
using VL.Core;
using VL.Core.Import;
using Matrix = Stride.Core.Mathematics.Matrix;

namespace VL.Avalonia.Controls
{
    /// <inheritdoc cref="AvaloniaObject"/>
    [ProcessNode]
    public abstract class AvaloniaObjectNodeBase<T>
        where T : AvaloniaObject, new()
    {
        protected readonly T _output = new();
        public T Output => _output;
    }

    /// <inheritdoc cref="Animatable"/>
    [ProcessNode]
    public abstract class AnimatableNodeBase<T> : AvaloniaObjectNodeBase<T>
        where T : Animatable, new()
    {
        private IReadOnlyList<IAvaloniaTransition>? _transitions;

        /// <param name="transitions"><inheritdoc cref="Animatable.Transitions" path="/summary/node()"/></param>
        [Fragment(Order = PinOrder.Animatable)]
        public void SetTransitions(
            [Pin(Visibility = Model.PinVisibility.Optional)]
                IReadOnlyList<IAvaloniaTransition> transitions
        )
        {
            if (ReferenceEquals(_transitions, transitions))
                return;

            _transitions = transitions;

            if (_transitions != null)
            {
                var tc = new Transitions();
                foreach (var builder in _transitions)
                {
                    if (builder.TryBuildTransition(Output, out var transition))
                    {
                        tc.Add(transition);
                    }
                }
                // BUG FIX: Set the property once after building the collection
                Output.SetCurrentValue(Animatable.TransitionsProperty, tc);
            }
            else
            {
                Output.ClearValue(Animatable.TransitionsProperty);
            }
        }
    }

    /// <inheritdoc cref="StyledElement"/>
    [ProcessNode]
    public abstract partial class StyledElementNodeBase<T> : AnimatableNodeBase<T>
        where T : StyledElement, new()
    {
        private Optional<IAvaloniaStyle> _style;

        /// <param name="style">Applies style setters to control</param>
        [Fragment(Order = PinOrder.Styled)]
        public void SetStyle(Optional<IAvaloniaStyle> style)
        {
            if (_style == style)
                return;

            _style = style;

            if (style.HasValue)
            {
                Output.TryUpdateStyles(style.Value);
            }
            else
            {
                Output.Styles.Clear();
            }
        }

        private Optional<string> _classes;

        /// <param name="classes">Applies classes to control</param>
        [Fragment(Order = PinOrder.Styled)]
        public void SetClasses(
            [Pin(Visibility = Model.PinVisibility.Optional)] Optional<string> classes
        )
        {
            if (_classes == classes)
                return;

            _classes = classes;

            if (classes.HasValue)
            {
                Output.TryUpdateClasses(classes.Value);
            }
            else
            {
                Output.Classes.Clear();
            }
        }

        /// <inheritdoc cref="StyledElement.Name"/>
        [ImplementProperty(
            typeof(StyledElement),
            nameof(StyledElement.NameProperty),
            Order = PinOrder.Styled,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<string> _name;
    }

    /// <inheritdoc cref="Visual"/>
    [ProcessNode]
    public abstract partial class VisualNodeBase<T> : StyledElementNodeBase<T>
        where T : Visual, new()
    {
        /// <inheritdoc cref="Visual.IsVisible"/>
        [ImplementProperty(
            typeof(Visual),
            nameof(Visual.IsVisibleProperty),
            Order = PinOrder.Visual,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _isVisible;

        /// <inheritdoc cref="Visual.Effect"/>
        [ImplementProperty(
            typeof(Visual),
            nameof(Visual.EffectProperty),
            Order = PinOrder.Visual,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<IEffect> _effect;

        private Optional<Matrix> _renderTransform;

        /// <param name="renderTransform"><inheritdoc cref="Visual.RenderTransformProperty" path="/summary/node()"/></param>
        [Fragment(Order = PinOrder.Visual)]
        public void SetRenderTransform(
            [Pin(Visibility = Model.PinVisibility.Optional)] Optional<Matrix> renderTransform
        )
        {
            if (_renderTransform == renderTransform)
                return;

            _renderTransform = renderTransform;

            if (renderTransform.HasValue)
            {
                renderTransform.Value.Decompose(
                    out Vector3 scale,
                    out Matrix rotation,
                    out Vector3 translation
                );
                rotation.Decompose(out float yaw, out float pitch, out float roll);

                var builder = TransformOperations.CreateBuilder(3);

                builder.AppendScale(scale.X, scale.Y);
                var angleDegrees = MathUtil.RadiansToDegrees(roll);
                builder.AppendRotate(angleDegrees);
                builder.AppendTranslate(translation.X, translation.Y);

                _output.SetValue(Visual.RenderTransformProperty, builder.Build());
            }
            else
            {
                _output.ClearValue(Visual.RenderTransformProperty);
            }
        }
    }

    /// <inheritdoc cref="Layoutable"/>
    [ProcessNode]
    public abstract partial class LayoutableNodeBase<T> : VisualNodeBase<T>
        where T : Layoutable, new()
    {
        /// <inheritdoc cref="Layoutable.HorizontalAlignment"/>
        [ImplementProperty(
            typeof(Layoutable),
            nameof(Layoutable.HorizontalAlignmentProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<HorizontalAlignment> _horizontalAlignment;

        /// <inheritdoc cref="Layoutable.VerticalAlignment"/>
        [ImplementProperty(
            typeof(Layoutable),
            nameof(Layoutable.VerticalAlignmentProperty),
            Order = PinOrder.Layoutable,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<VerticalAlignment> _verticalAlignment;
    }

    /// <inheritdoc cref="Interactive"/>
    [ProcessNode]
    public abstract class InteractiveNodeBase<T> : LayoutableNodeBase<T>
        where T : Interactive, new() { }

    /// <inheritdoc cref="InputElement"/>
    [ProcessNode]
    public abstract partial class InputElementNodeBase<T> : InteractiveNodeBase<T>
        where T : InputElement, new()
    {
        /// <inheritdoc cref="InputElement.Focusable"/>
        [ImplementProperty(
            typeof(InputElement),
            nameof(InputElement.FocusableProperty),
            Order = PinOrder.InputElement,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _focusable;

        /// <inheritdoc cref="InputElement.IsEnabled"/>
        [ImplementProperty(
            typeof(InputElement),
            nameof(InputElement.IsEnabledProperty),
            Order = PinOrder.InputElement,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<bool> _isEnabled;
    }

    /// <inheritdoc cref="Control"/>
    [ProcessNode]
    public abstract partial class ControlNodeBase<T> : InputElementNodeBase<T>
        where T : Control, new()
    {
        /// <inheritdoc cref="Control.FocusAdorner"/>
        [ImplementProperty(
            typeof(Control),
            nameof(Control.FocusAdornerProperty),
            PinVisibility = Model.PinVisibility.Optional,
            Order = PinOrder.Control
        )]
        private Optional<ITemplate<Control>> _focusAdorner;

        /// <inheritdoc cref="Control.ContextMenu"/>
        [ImplementProperty(
            typeof(Control),
            nameof(Control.ContextMenuProperty),
            PinVisibility = Model.PinVisibility.Optional,
            Order = PinOrder.Control
        )]
        private Optional<ContextMenu> _contextMenu;

        /// <inheritdoc cref="Control.ContextFlyout"/>
        [ImplementProperty(
            typeof(Control),
            nameof(Control.ContextFlyoutProperty),
            PinVisibility = Model.PinVisibility.Optional,
            Order = PinOrder.Control
        )]
        private Optional<FlyoutBase> _contextFlyout;
    }

    [ProcessNode(Name = "Control")]
    public class ControlNode : ControlNodeBase<Control> { }
}
