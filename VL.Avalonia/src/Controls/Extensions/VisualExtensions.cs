using Avalonia;
using Avalonia.Media;
using Stride.Core.Mathematics;
using VL.Avalonia.Extensions;
using AvaFlowDirection = Avalonia.Media.FlowDirection;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="Visual"/>.
    /// </summary>
    public static partial class VisualExtensions
    {
        /// <inheritdoc cref="Visual.Bounds"/>
        public static RectangleF Bounds(this Visual? visual) =>
            visual?.Bounds.FromRect() ?? RectangleF.Empty;

        /// <inheritdoc cref="Visual.Bounds"/>
        public static RectangleF BoundsDIP(this Visual? visual, float scaling = 0.01f) =>
            visual?.Bounds.FromRectDIP(scaling) ?? RectangleF.Empty;

        /// <inheritdoc cref="Visual.ClipToBounds"/>
        public static bool ClipToBounds(this Visual visual) => visual?.ClipToBounds ?? false;

        /// <inheritdoc cref="Visual.ClipToBounds"/>
        public static void SetClipToBounds(this Visual visual, bool clipToBounds)
        {
            if (visual is not null)
                visual.ClipToBounds = clipToBounds;
        }

        /// <inheritdoc cref="Visual.Clip"/>
        public static Geometry? Clip(this Visual visual) => visual?.Clip;

        /// <inheritdoc cref="Visual.Clip"/>
        public static void SetClip(this Visual visual, Geometry? clip)
        {
            if (visual is not null)
                visual.Clip = clip;
        }

        /// <inheritdoc cref="Visual.IsEffectivelyVisible"/>
        public static bool IsEffectivelyVisible(this Visual visual) =>
            visual?.IsEffectivelyVisible ?? false;

        /// <inheritdoc cref="Visual.IsVisible"/>
        public static bool IsVisible(this Visual visual) => visual?.IsVisible ?? true; // Default is true in Avalonia

        /// <inheritdoc cref="Visual.IsVisible"/>
        public static void SetIsVisible(this Visual visual, bool isVisible)
        {
            if (visual is not null)
                visual.IsVisible = isVisible;
        }

        /// <inheritdoc cref="Visual.Opacity"/>
        public static float Opacity(this Visual visual) =>
            visual != null ? (float)visual.Opacity : 1.0f;

        /// <inheritdoc cref="Visual.Opacity"/>
        public static void SetOpacity(this Visual visual, float opacity)
        {
            if (visual is not null)
                visual.Opacity = opacity;
        }

        /// <inheritdoc cref="Visual.OpacityMask"/>
        public static IBrush? OpacityMask(this Visual visual) => visual?.OpacityMask;

        /// <inheritdoc cref="Visual.OpacityMask"/>
        public static void SetOpacityMask(this Visual visual, IBrush? opacityMask)
        {
            if (visual is not null)
                visual.OpacityMask = opacityMask;
        }

        /// <inheritdoc cref="Visual.Effect"/>
        public static IEffect? Effect(this Visual visual) => visual?.Effect;

        /// <inheritdoc cref="Visual.Effect"/>
        public static void SetEffect(this Visual visual, IEffect? effect)
        {
            if (visual is not null)
                visual.Effect = effect;
        }

        /// <inheritdoc cref="Visual.HasMirrorTransform"/>
        public static bool HasMirrorTransform(this Visual visual) =>
            visual?.HasMirrorTransform ?? false;

        /// <inheritdoc cref="Visual.RenderTransform"/>
        public static ITransform? RenderTransform(this Visual visual) => visual?.RenderTransform;

        /// <inheritdoc cref="Visual.RenderTransform"/>
        public static void SetRenderTransform(this Visual visual, ITransform? renderTransform)
        {
            if (visual is not null)
                visual.RenderTransform = renderTransform;
        }

        /// <inheritdoc cref="Visual.RenderTransformOrigin"/>
        public static RelativePoint RenderTransformOrigin(this Visual visual) =>
            visual?.RenderTransformOrigin ?? RelativePoint.Center;

        /// <inheritdoc cref="Visual.RenderTransformOrigin"/>
        public static void SetRenderTransformOrigin(
            this Visual visual,
            RelativePoint renderTransformOrigin
        )
        {
            if (visual is not null)
                visual.RenderTransformOrigin = renderTransformOrigin;
        }

        /// <inheritdoc cref="Visual.FlowDirection"/>
        public static AvaFlowDirection FlowDirection(this Visual visual) =>
            visual?.FlowDirection ?? AvaFlowDirection.LeftToRight;

        /// <inheritdoc cref="Visual.FlowDirection"/>
        public static void SetFlowDirection(this Visual visual, AvaFlowDirection flowDirection)
        {
            if (visual is not null)
                visual.FlowDirection = flowDirection;
        }

        /// <inheritdoc cref="Visual.ZIndex"/>
        public static int ZIndex(this Visual visual) => visual?.ZIndex ?? 0;

        /// <inheritdoc cref="Visual.ZIndex"/>
        public static void SetZIndex(this Visual visual, int zIndex)
        {
            if (visual is not null)
                visual.ZIndex = zIndex;
        }

        /// <inheritdoc cref="Visual.InvalidateVisual"/>
        public static void InvalidateVisual(this Visual visual)
        {
            visual?.InvalidateVisual();
        }
    }
}
