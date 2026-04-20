using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Styling;
using VL.Avalonia.Attributes;
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
        private IChannel<IReadOnlyList<TValue>>? _itemsSource;
        private IDisposable? _itemSourceBinding;
        private ISpread? _items;

        [Fragment]
        public MenuFlyoutNodeBase() { }

        /// <param name="itemsSource">Binds observable items source.</param>
        public virtual void SetItemsSource(IChannel<IReadOnlyList<TValue>> itemsSource)
        {
            if (ReferenceEquals(_itemsSource, itemsSource))
                return;

            _itemSourceBinding?.Dispose();
            _itemsSource = itemsSource;
            _items = null;

            if (_itemsSource != null)
            {
                _itemSourceBinding = _output.Bind(
                    MenuFlyout.ItemsSourceProperty,
                    itemsSource.StartWith(itemsSource.Value)
                );
            }
            else
            {
                _output.ClearValue(MenuFlyout.ItemsSourceProperty);
            }
        }

        /// <param name="items">Sets items on source.</param>
        public virtual void SetItems(Spread<TValue?> items)
        {
            if (ReferenceEquals(_items, items))
                return;

            _itemSourceBinding?.Dispose();
            _items = items;
            _itemsSource = null;

            if (_items != null)
            {
                _output.SetValue(MenuFlyout.ItemsSourceProperty, items);
            }
            else
            {
                _output.ClearValue(MenuFlyout.ItemsSourceProperty);
            }
        }

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
            _itemsSource = null;
            _itemSourceBinding?.Dispose();
            _items = null;

            base.Dispose();
        }
    }
}
