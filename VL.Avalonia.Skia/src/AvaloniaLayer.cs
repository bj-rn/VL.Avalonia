using Avalonia;
using Avalonia.Controls.Embedding;
using Avalonia.Rendering.Composition;
using Avalonia.Skia;
using Avalonia.Styling;
using VL.Core;
using VL.Core.Import;
using VL.Lib.IO.Notifications;
using VL.Model;
using VL.Skia;
using Application = Avalonia.Application;
using RectangleF = Stride.Core.Mathematics.RectangleF;
using Vector2 = Stride.Core.Mathematics.Vector2;

namespace VL.Avalonia.Skia
{
    [ProcessNode(
        HasStateOutput = true,
        Name = "AvaloniaLayer",
        FragmentSelection = FragmentSelection.Explicit
    )]
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
                if (content.HasValue)
                {
                    controlRoot.SetValue(EmbeddableControlRoot.ContentProperty, content.Value);
                }
                else
                {
                    controlRoot.ClearValue(EmbeddableControlRoot.ContentProperty);
                }

                _content = content;
            }
        }

        private Optional<ThemeVariant> _requestedThemeVariant;

        /// <param name="requestedThemeVariant">Requested Theme Variant</param>
        [Fragment(Order = -10)]
        public void SetRequestedThemeVariant(Optional<ThemeVariant> requestedThemeVariant)
        {
            if (_requestedThemeVariant != requestedThemeVariant)
            {
                if (requestedThemeVariant.HasValue)
                {
                    var theme = requestedThemeVariant.Value;

                    controlRoot.SetValue(
                        EmbeddableControlRoot.RequestedThemeVariantProperty,
                        requestedThemeVariant.Value
                    );
                }
                else
                {
                    controlRoot.ClearValue(EmbeddableControlRoot.RequestedThemeVariantProperty);
                }

                _requestedThemeVariant = requestedThemeVariant;
            }
        }

        private Optional<float> _scalingFactor;

        [Fragment(Order = -10)]
        public void SetScalingFactor(
            [Pin(Visibility = PinVisibility.Optional)] Optional<float> scalingFactor
        )
        {
            if (_scalingFactor != scalingFactor)
            {
                if (scalingFactor.HasValue)
                {
                    if (scalingFactor.Value == 0.0f)
                        throw new ArgumentOutOfRangeException("Scaling factor can't be 0");

                    topLevelImpl.SetScaling(scalingFactor.Value);
                }
                else
                {
                    topLevelImpl.SetScaling(1.0f);
                }

                _scalingFactor = scalingFactor;
            }
        }

        private Optional<bool> _compensateRenderOffset;

        /// <param name="compensateRenderOffset">
        /// When the AvaloniaLayer is rendered as a sub-region of a larger viewport (e.g. embedded
        /// in a VL.ImGui SkiaWidget on Stride), notifications still arrive in outer window-pixel
        /// coordinates, while Avalonia hit-tests in its own layer-local space starting at (0, 0).
        /// Enable this to subtract the captured render origin from incoming Notify positions so
        /// the layer hit-tests where it visually renders. Default true. Disable for setups that
        /// already handle the offset themselves.
        /// </param>
        [Fragment(Order = -10)]
        public void SetCompensateRenderOffset(
            [Pin(Visibility = PinVisibility.Optional)] Optional<bool> compensateRenderOffset
        )
        {
            if (_compensateRenderOffset != compensateRenderOffset)
            {
                topLevelImpl.CompensateRenderOffset =
                    !compensateRenderOffset.HasValue || compensateRenderOffset.Value;
                _compensateRenderOffset = compensateRenderOffset;
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

        public bool SendNotification(
            INotification notification,
            Func<NotificationWithPosition, Vector2> getPosition
        )
        {
            return topLevelImpl.SendNotification(notification, getPosition);
        }

        public void Render(CallerInfo caller)
        {
            if (!controlRoot.IsInitialized)
            {
                topLevelImpl.SetClientSize(caller.ViewportBounds.ToAvaloniaRect().Size);
                controlRoot.Prepare();
                controlRoot.StartRendering();
            }
            topLevelImpl.Render(caller);

            // TODO: this is a hack to trigger the render loop
            GammaRenderTimer.Instance.TriggerTick(TimeSpan.FromMilliseconds(16));
        }
    }
}
