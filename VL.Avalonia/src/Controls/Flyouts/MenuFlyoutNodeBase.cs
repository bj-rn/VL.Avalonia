using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Styling;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="MenuFlyout"/>
    /// </summary>
    [ProcessNode(FragmentSelection = FragmentSelection.Explicit)]
    public abstract partial class MenuFlyoutNodeBase<TControl, TValue>
        : PopupFlyoutBaseNode<TControl>,
            IDisposable
        where TControl : MenuFlyout, new()
    {
        private readonly ItemsSourceSync<TValue> _itemsSync;

        [Fragment]
        public MenuFlyoutNodeBase()
        {
            _itemsSync = new ItemsSourceSync<TValue>(_output, MenuFlyout.ItemsSourceProperty);
        }

        /// <param name="itemsSource">Binds observable items source.</param>
        public virtual void SetItemsSource(IChannel<IReadOnlyList<TValue>> itemsSource) =>
            _itemsSync.SetChannel(itemsSource);

        /// <param name="items">Sets items on source.</param>
        public virtual void SetItems(Spread<TValue?> items) => _itemsSync.SetItems(items!);

        /// <summary>Sets the template used for the items</summary>
        [ImplementProperty(
            typeof(MenuFlyout),
            nameof(MenuFlyout.ItemTemplateProperty),
            Order = PinOrder.DataTemplate,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<IDataTemplate> _itemTemplate;

        /// <summary>Sets the <see cref="ControlTheme"/> that is applied to the container element generated for each item.</summary>
        [ImplementProperty(
            typeof(MenuFlyout),
            nameof(MenuFlyout.ItemContainerThemeProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<ControlTheme> _itemContainerTheme;

        /// <summary>Sets the <see cref="ControlTheme"/> that is applied to the container element generated for the flyout presenter.</summary>
        [ImplementProperty(
            typeof(MenuFlyout),
            nameof(MenuFlyout.FlyoutPresenterThemeProperty),
            Order = PinOrder.Style,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<ControlTheme> _flyoutPresenterTheme;

        public override void Dispose()
        {
            _itemsSync.Dispose();

            base.Dispose();
        }
    }
}
