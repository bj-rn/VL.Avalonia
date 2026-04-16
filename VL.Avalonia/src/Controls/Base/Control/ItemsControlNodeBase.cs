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
        : TemplatedControlNodeBase<TControl>,
            IDisposable
        where TControl : ItemsControl, new()
    {
        private IChannel<IReadOnlyList<TValue>>? _itemsSource;
        private IDisposable? _itemSourceBinding;

        private IReadOnlyList<TValue>? _items;

        [Fragment]
        public ItemsControlNodeBase() { }

        /// <summary>
        /// Binds observable items source.
        /// </summary>
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
        /// Sets items on source
        /// </summary>
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

        /// <inheritdoc cref="ItemsControl.ItemTemplate"/>
        [ImplementProperty(
            typeof(ItemsControl),
            nameof(ItemsControl.ItemTemplateProperty),
            Order = PinOrder.DataTemplate,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<IDataTemplate> _itemTemplate;

        /// <inheritdoc cref="ItemsControl.ItemsPanel"/>
        [ImplementProperty(
            typeof(ItemsControl),
            nameof(ItemsControl.ItemsPanelProperty),
            Order = PinOrder.DataTemplate,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<ITemplate<Panel>> _itemsPanel;

        /// <inheritdoc cref="ItemsControl.ItemContainerTheme"/>
        [ImplementProperty(
            typeof(ItemsControl),
            nameof(ItemsControl.ItemContainerThemeProperty),
            Order = PinOrder.DataTemplate,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<ControlTheme> _itemContainerTheme;

        public override void Dispose()
        {
            _itemSourceBinding?.Dispose();
            base.Dispose();
        }
    }
}
