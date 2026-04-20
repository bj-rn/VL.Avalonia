using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Media;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="SplitView"/>.
    /// </summary>
    public static class SplitViewExtensions
    {
        /// <inheritdoc cref="SplitView.CompactPaneLength"/>
        public static float CompactPaneLength(this SplitView splitView) =>
            splitView != null ? (float)splitView.CompactPaneLength : 48f;

        /// <inheritdoc cref="SplitView.CompactPaneLength"/>
        public static void SetCompactPaneLength(this SplitView splitView, float compactPaneLength)
        {
            if (splitView is not null)
                splitView.CompactPaneLength = compactPaneLength;
        }

        /// <inheritdoc cref="SplitView.DisplayMode"/>
        public static SplitViewDisplayMode DisplayMode(this SplitView splitView) =>
            splitView != null ? splitView.DisplayMode : SplitViewDisplayMode.Overlay;

        /// <inheritdoc cref="SplitView.DisplayMode"/>
        public static void SetDisplayMode(
            this SplitView splitView,
            SplitViewDisplayMode displayMode
        )
        {
            if (splitView is not null)
                splitView.DisplayMode = displayMode;
        }

        /// <inheritdoc cref="SplitView.IsPaneOpen"/>
        public static bool IsPaneOpen(this SplitView splitView) =>
            splitView != null ? splitView.IsPaneOpen : false;

        /// <inheritdoc cref="SplitView.IsPaneOpen"/>
        public static void SetIsPaneOpen(this SplitView splitView, bool isPaneOpen)
        {
            if (splitView is not null)
                splitView.IsPaneOpen = isPaneOpen;
        }

        /// <inheritdoc cref="SplitView.OpenPaneLength"/>
        public static float OpenPaneLength(this SplitView splitView) =>
            splitView != null ? (float)splitView.OpenPaneLength : 320f;

        /// <inheritdoc cref="SplitView.OpenPaneLength"/>
        public static void SetOpenPaneLength(this SplitView splitView, float openPaneLength)
        {
            if (splitView is not null)
                splitView.OpenPaneLength = openPaneLength;
        }

        /// <inheritdoc cref="SplitView.PaneBackground"/>
        public static IBrush? PaneBackground(this SplitView splitView) =>
            splitView != null ? splitView.PaneBackground : null;

        /// <inheritdoc cref="SplitView.PaneBackground"/>
        public static void SetPaneBackground(this SplitView splitView, IBrush? paneBackground)
        {
            if (splitView is not null)
                splitView.PaneBackground = paneBackground;
        }

        /// <inheritdoc cref="SplitView.PanePlacement"/>
        public static SplitViewPanePlacement PanePlacement(this SplitView splitView) =>
            splitView != null ? splitView.PanePlacement : default(SplitViewPanePlacement);

        /// <inheritdoc cref="SplitView.PanePlacement"/>
        public static void SetPanePlacement(
            this SplitView splitView,
            SplitViewPanePlacement panePlacement
        )
        {
            if (splitView is not null)
                splitView.PanePlacement = panePlacement;
        }

        /// <inheritdoc cref="SplitView.Pane"/>
        public static object? Pane(this SplitView splitView) =>
            splitView != null ? splitView.Pane : null;

        /// <inheritdoc cref="SplitView.Pane"/>
        public static void SetPane(this SplitView splitView, object? pane)
        {
            if (splitView is not null)
                splitView.Pane = pane;
        }

        /// <inheritdoc cref="SplitView.PaneTemplate"/>
        public static IDataTemplate? PaneTemplate(this SplitView splitView) =>
            splitView != null ? splitView.PaneTemplate : null;

        /// <inheritdoc cref="SplitView.PaneTemplate"/>
        public static void SetPaneTemplate(this SplitView splitView, IDataTemplate? paneTemplate)
        {
            if (splitView is not null)
                splitView.PaneTemplate = paneTemplate;
        }

        /// <inheritdoc cref="SplitView.UseLightDismissOverlayMode"/>
        public static bool UseLightDismissOverlayMode(this SplitView splitView) =>
            splitView != null ? splitView.UseLightDismissOverlayMode : false;

        /// <inheritdoc cref="SplitView.UseLightDismissOverlayMode"/>
        public static void SetUseLightDismissOverlayMode(
            this SplitView splitView,
            bool useLightDismissOverlayMode
        )
        {
            if (splitView is not null)
                splitView.UseLightDismissOverlayMode = useLightDismissOverlayMode;
        }

        /// <inheritdoc cref="SplitView.TemplateSettings"/>
        public static SplitViewTemplateSettings? TemplateSettings(this SplitView splitView) =>
            splitView != null ? splitView.TemplateSettings : null;
    }
}
