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

        /// <param name="itemsSource">Binds observable items source.</param>
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

        /// <param name="items">Sets items on source.</param>
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

        /// <summary>Sets the data template used to display the items in the control.</summary>
        [ImplementProperty(
            typeof(ItemsControl),
            nameof(ItemsControl.ItemTemplateProperty),
            Order = PinOrder.DataTemplate,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<IDataTemplate> _itemTemplate;

        /// <summary>Sets the panel used to display the items.</summary>
        [ImplementProperty(
            typeof(ItemsControl),
            nameof(ItemsControl.ItemsPanelProperty),
            Order = PinOrder.DataTemplate,
            PinVisibility = Model.PinVisibility.Optional
        )]
        private Optional<ITemplate<Panel>> _itemsPanel;

        /// <summary>Sets the <see cref="ControlTheme"/> that is applied to the container element generated for each item.</summary>
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
