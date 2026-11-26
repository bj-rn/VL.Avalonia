using Avalonia.Controls;
using Avalonia.Labs.Panels;
using AvaAlignContent = Avalonia.Labs.Panels.AlignContent;
using AvaAlignItems = Avalonia.Labs.Panels.AlignItems;
using AvaFlexBasisKind = Avalonia.Labs.Panels.FlexBasisKind;
using AvaFlexDirection = Avalonia.Labs.Panels.FlexDirection;
using AvaFlexWrap = Avalonia.Labs.Panels.FlexWrap;
using AvaJustifyContent = Avalonia.Labs.Panels.JustifyContent;

namespace VL.Avalonia.Labs.Panels
{
    /// <summary>
    /// Extension methods for <see cref="FlexPanel"/>.
    /// </summary>
    public static class FlexPanelExtensions
    {
        /// <inheritdoc cref="FlexPanel.Direction"/>
        public static AvaFlexDirection Direction(this FlexPanel flexPanel) =>
            flexPanel != null ? flexPanel.Direction : AvaFlexDirection.Row;

        /// <inheritdoc cref="FlexPanel.Direction"/>
        public static void SetDirection(this FlexPanel flexPanel, AvaFlexDirection direction)
        {
            if (flexPanel is not null)
                flexPanel.Direction = direction;
        }

        /// <inheritdoc cref="FlexPanel.JustifyContent"/>
        public static AvaJustifyContent JustifyContent(this FlexPanel flexPanel) =>
            flexPanel != null ? flexPanel.JustifyContent : AvaJustifyContent.FlexStart;

        /// <inheritdoc cref="FlexPanel.JustifyContent"/>
        public static void SetJustifyContent(this FlexPanel flexPanel, AvaJustifyContent justifyContent)
        {
            if (flexPanel is not null)
                flexPanel.JustifyContent = justifyContent;
        }

        /// <inheritdoc cref="FlexPanel.AlignItems"/>
        public static AvaAlignItems AlignItems(this FlexPanel flexPanel) =>
            flexPanel != null ? flexPanel.AlignItems : AvaAlignItems.Stretch;

        /// <inheritdoc cref="FlexPanel.AlignItems"/>
        public static void SetAlignItems(this FlexPanel flexPanel, AvaAlignItems alignItems)
        {
            if (flexPanel is not null)
                flexPanel.AlignItems = alignItems;
        }

        /// <inheritdoc cref="FlexPanel.AlignContent"/>
        public static AvaAlignContent AlignContent(this FlexPanel flexPanel) =>
            flexPanel != null ? flexPanel.AlignContent : AvaAlignContent.Stretch;

        /// <inheritdoc cref="FlexPanel.AlignContent"/>
        public static void SetAlignContent(this FlexPanel flexPanel, AvaAlignContent alignContent)
        {
            if (flexPanel is not null)
                flexPanel.AlignContent = alignContent;
        }

        /// <inheritdoc cref="FlexPanel.Wrap"/>
        public static AvaFlexWrap Wrap(this FlexPanel flexPanel) =>
            flexPanel != null ? flexPanel.Wrap : AvaFlexWrap.NoWrap;

        /// <inheritdoc cref="FlexPanel.Wrap"/>
        public static void SetWrap(this FlexPanel flexPanel, AvaFlexWrap wrap)
        {
            if (flexPanel is not null)
                flexPanel.Wrap = wrap;
        }

        /// <inheritdoc cref="FlexPanel.ColumnSpacing"/>
        public static float ColumnSpacing(this FlexPanel flexPanel) =>
            flexPanel != null ? (float)flexPanel.ColumnSpacing : 0f;

        /// <inheritdoc cref="FlexPanel.ColumnSpacing"/>
        public static void SetColumnSpacing(this FlexPanel flexPanel, float columnSpacing)
        {
            if (flexPanel is not null)
                flexPanel.ColumnSpacing = columnSpacing;
        }

        /// <inheritdoc cref="FlexPanel.RowSpacing"/>
        public static float RowSpacing(this FlexPanel flexPanel) =>
            flexPanel != null ? (float)flexPanel.RowSpacing : 0f;

        /// <inheritdoc cref="FlexPanel.RowSpacing"/>
        public static void SetRowSpacing(this FlexPanel flexPanel, float rowSpacing)
        {
            if (flexPanel is not null)
                flexPanel.RowSpacing = rowSpacing;
        }

        // AttachedProperties:

        /// <inheritdoc cref="Flex.GetAlignSelf"/>
        public static AvaAlignItems? AlignSelf(this Control control) =>
            control != null ? Flex.GetAlignSelf(control) : null;

        /// <inheritdoc cref="Flex.SetAlignSelf"/>
        public static void SetAlignSelf(this Control control, AvaAlignItems alignSelf)
        {
            if (control is not null)
                Flex.SetAlignSelf(control, alignSelf);
        }

        /// <inheritdoc cref="Flex.GetOrder"/>
        public static int Order(this Control control) =>
            control != null ? Flex.GetOrder(control) : 0;

        /// <inheritdoc cref="Flex.SetOrder"/>
        public static void SetOrder(this Control control, int order)
        {
            if (control is not null)
                Flex.SetOrder(control, order);
        }

        /// <summary>Gets the flex basis value of the child. Relative values are percentages (0-100).</summary>
        public static float BasisValue(this Control control) =>
            control != null ? (float)Flex.GetBasis(control).Value : 0f;

        /// <summary>Gets how the flex basis value of the child is interpreted.</summary>
        public static AvaFlexBasisKind BasisKind(this Control control) =>
            control != null ? Flex.GetBasis(control).Kind : AvaFlexBasisKind.Auto;

        /// <summary>Sets the flex basis of the child. Relative values are percentages (0-100). Negative or NaN values are treated as 0.</summary>
        public static void SetBasis(this Control control, float basis, AvaFlexBasisKind kind)
        {
            if (control is not null)
                Flex.SetBasis(control, FlexValueHelper.ToBasis(basis, kind));
        }

        /// <inheritdoc cref="Flex.GetShrink"/>
        public static float Shrink(this Control control) =>
            control != null ? (float)Flex.GetShrink(control) : (float)FlexValueHelper.DefaultShrink;

        /// <inheritdoc cref="Flex.SetShrink"/>
        public static void SetShrink(this Control control, float shrink)
        {
            if (control is not null)
                Flex.SetShrink(control, FlexValueHelper.ToFactor(shrink, FlexValueHelper.DefaultShrink));
        }

        /// <inheritdoc cref="Flex.GetGrow"/>
        public static float Grow(this Control control) =>
            control != null ? (float)Flex.GetGrow(control) : (float)FlexValueHelper.DefaultGrow;

        /// <inheritdoc cref="Flex.SetGrow"/>
        public static void SetGrow(this Control control, float grow)
        {
            if (control is not null)
                Flex.SetGrow(control, FlexValueHelper.ToFactor(grow, FlexValueHelper.DefaultGrow));
        }
    }
}