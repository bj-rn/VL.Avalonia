using System.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using VL.Core;
using StyledElement = Avalonia.StyledElement;

namespace VL.Avalonia.Helpers
{
    /// <summary>
    /// Shared logic for detaching a <see cref="Control"/> from its current visual parent
    /// so it can be re-parented elsewhere, together with a persistent warning message
    /// that automatically disposes itself after a short delay.
    /// </summary>
    public static class ReparentingHelper
    {
        private static readonly TimeSpan MessageLifetime = TimeSpan.FromSeconds(9);

        /// <summary>
        /// Detaches <paramref name="control"/> from its current parent, if any, and raises
        /// a persistent warning message that disposes itself after <see cref="MessageLifetime"/>.
        /// </summary>
        public static void DetachFromParent(NodeContext nodeContext, Control control)
        {
            var parent = control.Parent;
            if (TryDetach(control))
                EnqueueReparentWarning(nodeContext, control, parent!);
        }

        /// <summary>
        /// Detaches <paramref name="control"/> from its current parent without raising a warning.
        /// Returns false if it had no parent or the parent can't give it up.
        /// </summary>
        internal static bool TryDetach(Control control)
        {
            switch (control.Parent)
            {
                case Panel panel:
                    panel.Children.Remove(control);
                    return true;
                case ContentControl contentControl when ReferenceEquals(contentControl.Content, control):
                    contentControl.Content = null;
                    return true;
                case ContentPresenter contentPresenter when ReferenceEquals(contentPresenter.Content, control):
                    contentPresenter.Content = null;
                    return true;
                case Decorator decorator when ReferenceEquals(decorator.Child, control):
                    decorator.Child = null;
                    return true;
                case Viewbox viewbox when ReferenceEquals(viewbox.Child, control):
                    viewbox.Child = null;
                    return true;
                case ItemsControl itemsControl:
                    return TryRemoveItem(itemsControl, control);
                default:
                    return false;
            }
        }

        private static bool TryRemoveItem(ItemsControl itemsControl, Control control)
        {
            if (itemsControl.ItemsSource is null)
                return itemsControl.Items.Remove(control);

            // Items is read-only while ItemsSource is in use; the item has to leave the source.
            if (itemsControl.ItemsSource is IList { IsReadOnly: false, IsFixedSize: false } source
                && source.Contains(control))
            {
                source.Remove(control);
                return true;
            }

            return false;
        }

        private static void EnqueueReparentWarning(NodeContext nodeContext, Control control, StyledElement previousParent)
        {
            var message = nodeContext.AddPersistentMessage(
                Lang.MessageSeverity.Warning,
                $"Control '{control}' was removed from its previous parent '{previousParent}' to be re-parented."
            );

            _ = DisposeAfterDelayAsync(message);
        }

        private static async Task DisposeAfterDelayAsync(IDisposable message)
        {
            await Task.Delay(MessageLifetime).ConfigureAwait(false);
            message.Dispose();
        }
    }
}
