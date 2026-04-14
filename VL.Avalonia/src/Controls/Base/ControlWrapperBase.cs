using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Transformation;
using Stride.Core.Mathematics;
using VL.Avalonia.Attributes;
using VL.Avalonia.Controls.Base.Primitives;
using VL.Avalonia.Helpers;
using VL.Avalonia.Styles;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base class for all controls, seems more flexible then using CodeGen for each prop.
    /// Inherits: <c>AnimatableBaseWrapper</c>
    /// Implements: <c>Output</c>, <c>Style</c>, <c>Classes</c>, <c>Name</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [ProcessNode]
    public abstract partial class ControlWrapperBase<T> : AnimatableWrapperBase<T>
        where T : Control, new()
    {
        protected Optional<IAvaloniaStyle> _style;

        [Fragment(Order = PinOrder.Style)]
        /// <param name="style">
        /// Style Setters
        /// </param>
        public void SetStyle(Optional<IAvaloniaStyle> style)
        {
            if (_style != style)
            {
                if (style.HasValue)
                {
                    _output.TryUpdateStyles(style.Value);
                }
                else
                {
                    _output.Styles.Clear();
                }

                _style = style;
            }
        }

        #region StyledElement Properties

        protected Optional<string> _classes;

        /// <param name="classes">
        /// Collection of CSS-like class names for styling purposes
        /// </param>
        [Fragment(Order = PinOrder.Style)]
        public void SetClasses(
            [Pin(Visibility = Model.PinVisibility.Optional)] Optional<string> classes
        )
        {
            if (_classes != classes)
            {
                _classes = classes;
                _output.TryUpdateClasses(classes.Value);
            }
        }

        /// <param traget="name">
        /// Sets name of control
        /// </param>
        [ImplementProperty(
            "Control.NameProperty",
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        protected Optional<string> _name;

        #endregion

        #region Focus Context Properties

        /// <param name="focusAdorner">
        /// Template for a visual adorner shown when the control is focused.
        /// </param>
        [ImplementProperty(
            "Control.FocusAdornerProperty",
            PinVisibility = Model.PinVisibility.Optional
        )]
        protected Optional<ITemplate<Control>> _focusAdorner;

        /// <param name="contextMenu">
        /// The context menu shown for this control (ContextMenu).
        /// </param>
        [ImplementProperty(
            "Control.ContextMenuProperty",
            PinVisibility = Model.PinVisibility.Optional
        )]
        protected Optional<ContextMenu> _contextMenu;

        /// <param name="contextFlyout">
        /// The context flyout shown for this control (FlyoutBase).
        /// </param>
        [ImplementProperty(
            "Control.ContextFlyoutProperty",
            PinVisibility = Model.PinVisibility.Optional
        )]
        protected Optional<FlyoutBase> _contextFlyout;

        #endregion

        #region Layoutable Properties

        /// <param name="horizontalAlignment">
        /// How the control is positioned horizontally within its parent
        /// </param>
        [ImplementProperty(
            "Control.HorizontalAlignmentProperty",
            Order = 100,
            PinVisibility = Model.PinVisibility.Optional
        )]
        protected Optional<HorizontalAlignment> _horizontalAlignment;

        /// <param name="verticalAlignment">
        /// How the control is positioned vertically within its parent
        /// </param>
        [ImplementProperty(
            "Control.VerticalAlignmentProperty",
            Order = 100,
            PinVisibility = Model.PinVisibility.Optional
        )]
        protected Optional<VerticalAlignment> _verticalAlignment;

        #endregion

        #region Visual Properties

        /// <param name="isVisible">
        /// Whether the control is visible in the user interface
        /// </param>
        [ImplementProperty(
            "Control.IsVisibleProperty",
            PinVisibility = Model.PinVisibility.Optional
        )]
        protected Optional<bool> _isVisible;

        /// <param name="effect">
        /// Sets effect of the control
        /// </param>
        [ImplementProperty("Control.EffectProperty", PinVisibility = Model.PinVisibility.Optional)]
        protected Optional<IEffect> _effect;

        /// <param name="renderTransform">
        /// The transform applied to the control's rendering (scaling, rotation, translation, skew)
        /// </param>
        protected Optional<Matrix> _renderTransform;

        public void SetRenderTransform(
            [Pin(Visibility = Model.PinVisibility.Optional)] Optional<Matrix> renderTransform
        )
        {
            if (_renderTransform != renderTransform)
            {
                if (renderTransform.HasValue)
                {
                    // WORKAROUND: https://github.com/AvaloniaUI/Avalonia/discussions/13960
                    // Gets RenderTransform work with Transition unoptimized manner

                    // var transform = new MatrixTransform(renderTransform.Value.ToAvaloniaMatrix());
                    // _output.SetValue(Control.RenderTransformProperty, transform);

                    renderTransform.Value.Decompose(
                        out Vector3 scale,
                        out Matrix rotation,
                        out Vector3 translation
                    );
                    rotation.Decompose(out float yaw, out float pitch, out float roll);

                    var builder = TransformOperations.CreateBuilder(3);

                    builder.AppendScale(scale.X, scale.Y);
                    builder.AppendRotate(roll);
                    builder.AppendTranslate(translation.X, translation.Y);

                    var transform = builder.Build();

                    _output.SetValue(Control.RenderTransformProperty, transform);
                }
                else
                {
                    _output.ClearValue(Control.RenderTransformProperty);
                }

                _renderTransform = renderTransform;
            }
        }

        /// <param name="zIndex">
        /// The z-order (layering) of the control relative to its siblings
        /// </param>
        [ImplementProperty(
            "Control.ZIndexProperty",
            Order = 900,
            PinVisibility = Model.PinVisibility.Optional
        )]
        protected Optional<int> _zIndex;

        #endregion

        #region InputElement Properties

        /// <param name="focusable">
        /// Whether the control can receive keyboard focus
        /// </param>
        [ImplementProperty(
            "Control.FocusableProperty",
            PinVisibility = Model.PinVisibility.Optional
        )]
        protected Optional<bool> _focusable;

        /// <param name="isEnabled">
        /// Whether the control responds to user interaction
        /// </param>
        [ImplementProperty(
            "Control.IsEnabledProperty",
            Order = 1000,
            PinVisibility = Model.PinVisibility.Optional
        )]
        protected Optional<bool> _isEnabled;
        #endregion
    }
}
