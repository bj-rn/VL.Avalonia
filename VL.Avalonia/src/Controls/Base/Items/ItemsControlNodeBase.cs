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
    /// Base wrapper for <see cref="ItemsControl">ItemsControl</see>
    /// </summary>
    [ProcessNode(FragmentSelection = FragmentSelection.Explicit)]
    public abstract partial class ItemsControlNodeBase<TControl, TValue>
        : ControlWrapperBase<TControl>,
            IDisposable
        where TControl : ItemsControl, new()
    {
        private IChannel<IReadOnlyList<TValue>>? _itemsSource;
        private IDisposable? _itemSourceBinding;

        private Spread<TValue>? _items;

        [Fragment]
        public ItemsControlNodeBase() { }

        /// <summary>
        /// Binds observable items source.
        /// </summary>
        /// <param name="itemsSource">Observable items source</param>
        public virtual void SetItemsSource(IChannel<IReadOnlyList<TValue>> itemsSource)
        {
            if (!ReferenceEquals(_itemsSource, itemsSource))
            {
                _itemSourceBinding?.Dispose();

                if (itemsSource != null)
                {
                    _itemSourceBinding = _output.Bind(
                        ItemsControl.ItemsSourceProperty,
                        itemsSource.StartWith(itemsSource.Value)
                    );
                }
                else
                {
                    _output.ClearValue(ItemsControl.ItemsSourceProperty);
                }

                _itemsSource = itemsSource;
                _items = null;
            }
        }

        /// <summary>
        /// Sets items
        /// </summary>
        /// <param name="items">Spread of items</param>
        public virtual void SetItems(Spread<TValue> items)
        {
            if (!ReferenceEquals(_items, items))
            {
                _itemSourceBinding?.Dispose();

                if (items != null)
                {
                    _output.SetValue(ItemsControl.ItemsSourceProperty, items);
                }
                else
                {
                    _output.ClearValue(ItemsControl.ItemsSourceProperty);
                }

                _items = items;
                _itemsSource = null;
            }
        }

        [ImplementProperty(
            "ItemsControl.ItemTemplateProperty",
            PinVisibility = Model.PinVisibility.Optional
        )]
        protected Optional<IDataTemplate> _itemTemplate;

        [ImplementProperty(
            "ItemsControl.ItemsPanelProperty",
            PinVisibility = Model.PinVisibility.Optional
        )]
        protected Optional<ITemplate<Panel>> _itemsPanel;

        [ImplementProperty(
            "ItemsControl.ItemContainerThemeProperty",
            PinVisibility = Model.PinVisibility.Optional
        )]
        protected Optional<ControlTheme> _itemContainerTheme;

        public virtual void Dispose()
        {
            _itemSourceBinding?.Dispose();
        }
    }
}
