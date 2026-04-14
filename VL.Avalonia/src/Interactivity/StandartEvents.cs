using Avalonia.Controls;
using VL.Core.Import;

[assembly: ImportType(typeof(ContainerPreparedEventArgs), Category = "Avalonia.Controls")]

namespace VL.Avalonia.Interactivity
{
    [ProcessNode(Name = "ContainerPreparedEventListener")]
    public class ItemsControlContainerPreparedEventListener
        : StandardEventListenerBase<ItemsControl, ContainerPreparedEventArgs>
    {
        private void OnEvent(object? sender, ContainerPreparedEventArgs e)
        {
            if (sender is ItemsControl control)
            {
                InvokeHandler(control, e);
            }
        }

        protected override void Subscribe(ItemsControl source)
        {
            source.ContainerPrepared += OnEvent;
        }

        protected override void Unsubscribe(ItemsControl source)
        {
            source.ContainerPrepared -= OnEvent;
        }
    }
}
