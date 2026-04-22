using Avalonia.Controls;
using Avalonia.Styling;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="Flyout"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class FlyoutNodeBase<T> : PopupFlyoutBaseNode<T>
        where T : Flyout, new()
    {
        /// <summary>Sets the content to display in this flyout.</summary>
        [ImplementProperty(
            typeof(Flyout),
            nameof(Flyout.ContentProperty),
            Order = PinOrder.Main,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<object> _content;

        /// <summary>Sets the ControlTheme that is applied to the container element generated for the flyout presenter.</summary>
        [ImplementProperty(
            typeof(Flyout),
            nameof(Flyout.FlyoutPresenterThemeProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<ControlTheme> _flyoutPresenterTheme;
    }
}
