using Avalonia;
using Avalonia.Controls.Embedding;
using Avalonia.Rendering.Composition;
using Avalonia.Skia;
using Avalonia.Styling;
using System;
using VL.Core;
using VL.Core.Import;
using VL.Lib.IO.Notifications;
using VL.Model;
using Application = Avalonia.Application;
using Point = Avalonia.Point;
using RectangleF = Stride.Core.Mathematics.RectangleF;
using Vector2 = Stride.Core.Mathematics.Vector2;

namespace VL.Skia.Avalonia
{

    [ProcessNode(HasStateOutput = true, Name = "AvaloniaLayer", FragmentSelection = FragmentSelection.Explicit)]
    public sealed class AvaloniaLayer : ILayer, IDisposable
    {
        private readonly GammaTopLevelImpl topLevelImpl;
        private readonly EmbeddableControlRoot controlRoot;

        [Fragment]
        public AvaloniaLayer(
            [Pin(Visibility = PinVisibility.Optional)]
            Action<Application>? onSetupApplication = null
            )
        {
            AvaloniaInitializer.Init();

            var locator = AvaloniaLocator.Current;
            topLevelImpl = new GammaTopLevelImpl(locator.GetRequiredService<Compositor>());
            controlRoot = new EmbeddableControlRoot(topLevelImpl);

            onSetupApplication?.Invoke(AvaloniaInitializer.Instance);
        }


        private Optional<object> _content;

        [Fragment(Order = -10)]
        public void SetContent(Optional<object> content)
        {
            if (_content != content)
            {
                _content = content;

                if (_content.HasValue)
                {
                    controlRoot.SetValue(EmbeddableControlRoot.ContentProperty, content.Value);
                }
                else
                {
                    controlRoot.ClearValue(EmbeddableControlRoot.ContentProperty);
                }
            }
        }

        private ThemeVariant _requestedThemeVariant = ThemeVariant.Default;
        /// <summary>
        /// Requested Theme Variant
        /// </summary>
        [Fragment]
        public ThemeVariant RequestedThemeVariant
        {
            private get => _requestedThemeVariant;
            set
            {
                if (_requestedThemeVariant != value)
                {
                    _requestedThemeVariant = value;
                    controlRoot.RequestedThemeVariant = value;
                }
            }
        }

        public void Dispose()
        {
            controlRoot.StopRendering();
            controlRoot.Dispose();
        }

        public RectangleF? Bounds => default;
        public bool Notify(INotification notification, CallerInfo caller)
        {
            return topLevelImpl.Notify(notification, caller);
        }

        public bool SendNotification(INotification notification, Func<NotificationWithPosition, Vector2> getPosition)
        {

            return topLevelImpl.SendNotification(notification, getPosition);
        }

        public void Render(CallerInfo caller)
        {
            if (!controlRoot.IsInitialized)
            {
                topLevelImpl.ClientSize = caller.ViewportBounds.ToAvaloniaRect().Size;
                controlRoot.Prepare();
                controlRoot.StartRendering();
            }
            topLevelImpl.Render(caller);

            // TODO: this is a hack to trigger the render loop
            GammaRenderTimer.Instance.TriggerTick(TimeSpan.FromMilliseconds(16));
        }
    }
}
