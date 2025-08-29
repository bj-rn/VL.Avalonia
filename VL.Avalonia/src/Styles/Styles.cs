using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Primitives.PopupPositioning;
using Avalonia.Controls.Templates;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Media.Transformation;
using Avalonia.Styling;
using Stride.Core.Mathematics;
using System.Globalization;
using VL.Avalonia.Extensions;
using VL.Core.Import;
using VL.Lib.Animation;
using AvaloniaMatrix = Avalonia.Matrix;
using AvaloniaPoint = Avalonia.Point;
using Matrix = Stride.Core.Mathematics.Matrix;

namespace VL.Avalonia.Styles
{
    #region Animatable

    [ProcessNode(Name = "SetClock")]
    public class SetClock : StyleSetter<IClock>
    {
        public SetClock() : base("Clock") { }
    }


    [ProcessNode(Name = "SetTransitions")]
    public class SetTransitions : StyleSetterTransitions
    {
        public SetTransitions() : base("Transitions")
        {
        }
    }

    #endregion

    #region StyledElement

    [ProcessNode(Name = "SetTheme")]
    public class SetTheme : StyleSetter<ControlTheme>
    {
        public SetTheme() : base("Theme") { }
    }

    #endregion

    #region Visual

    [ProcessNode(Name = "SetClipToBounds")]
    public class SetClipToBounds : StyleSetter<bool>
    {
        public SetClipToBounds() : base("ClipToBounds") { }
    }

    [ProcessNode(Name = "SetClip")]
    public class SetClip : StyleSetter<Geometry>
    {
        public SetClip() : base("Clip") { }
    }

    [ProcessNode(Name = "SetIsVisible")]
    public class SetIsVisible : StyleSetter<bool>
    {
        public SetIsVisible() : base("IsVisible") { }
    }

    [ProcessNode(Name = "SetOpacity")]
    public class SetOpacity : StyleSetter<float, double>
    {
        public SetOpacity() : base("Opacity", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetOpacityMask")]
    public class SetOpacityMask : StyleSetter<IBrush>
    {
        public SetOpacityMask() : base("OpacityMask") { }
    }

    [ProcessNode(Name = "SetOpacityMask")]
    public class SetOpacityMaskColor : StyleSetterBrushColor
    {
        public SetOpacityMaskColor() : base("OpacityMask") { }
    }

    [ProcessNode(Name = "SetEffect")]
    public class SetEffect : StyleSetter<IEffect>
    {
        public SetEffect() : base("Effect") { }
    }

    /// <summary>Warning: Not animatable</summary>
    [ProcessNode(Name = "SetRenderTransform (Optimized)")]
    public class SetRenderTransform : StyleSetter<Matrix, AvaloniaMatrix>
    {
        public SetRenderTransform() : base("RenderTransform", (x) => x.ToAvaloniaMatrix()) { }
    }

    [ProcessNode(Name = "SetRenderTransform (Transitionable)")]
    public class SetRenderTransformAnimatable : StyleSetter<Matrix, TransformOperations>
    {
        public SetRenderTransformAnimatable() : base("RenderTransform", (x) =>
        {
            x.Decompose(out Vector3 scale, out Matrix rotation, out Vector3 translation);
            rotation.Decompose(out float yaw, out float pitch, out float roll);

            var builder = TransformOperations.CreateBuilder(3);

            builder.AppendScale(scale.X, scale.Y);
            builder.AppendRotate(roll);
            builder.AppendTranslate(translation.X, translation.Y);

            return builder.Build();
        })
        { }
    }

    [ProcessNode(Name = "SetRenderTransformSRT")]
    public class SetRenderTransformSRT : StyleSetterSRT
    {
        public SetRenderTransformSRT() : base("RenderTransform")
        {
        }
    }


    [ProcessNode(Name = "SetRenderTransformOrigin")]
    public class SetRenderTransformOrigin : StyleSetterBase<RelativePoint>
    {
        public SetRenderTransformOrigin() : base("RenderTransformOrigin") { }
    }

    [ProcessNode(Name = "SetZIndex")]
    public class SetZIndex : StyleSetterBase<int>
    {
        public SetZIndex() : base("ZIndex") { }
    }

    #endregion

    // TODO: PIXELS
    #region Layoutable

    [ProcessNode(Name = "SetWidth")]
    public class SetWidth : StyleSetter<float, double>
    {
        public SetWidth() : base("Width", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetHeight")]
    public class SetHeight : StyleSetter<float, double>
    {
        public SetHeight() : base("Height", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetMinWidth")]
    public class SetMinWidth : StyleSetter<float, double>
    {
        public SetMinWidth() : base("MinWidth", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetMaxWidth")]
    public class SetMaxWidth : StyleSetter<float, double>
    {
        public SetMaxWidth() : base("MaxWidth", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetMinHeight")]
    public class SetMinHeight : StyleSetter<float, double>
    {
        public SetMinHeight() : base("MinHeight", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetMaxHeight")]
    public class SetMaxHeight : StyleSetter<float, double>
    {
        public SetMaxHeight() : base("MaxHeight", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetMargin")]
    public class SetMargin : StyleSetter<Thickness>
    {
        public SetMargin() : base("Margin") { }
    }

    [ProcessNode(Name = "SetMargin")]
    public class SetMarginAll : StyleSetterThicknessAll
    {
        public SetMarginAll() : base("Margin") { }
    }

    [ProcessNode(Name = "SetMargin")]
    public class SetMarginHV : StyleSetterThicknessHV
    {
        public SetMarginHV() : base("Margin") { }
    }

    [ProcessNode(Name = "SetMargin")]
    public class SetMarginV4 : StyleSetterThicknessLTRB
    {
        public SetMarginV4() : base("Margin") { }
    }

    [ProcessNode(Name = "SetHorizontalAlignment")]
    public class SetHorizontalAlignment : StyleSetter<HorizontalAlignment>
    {
        public SetHorizontalAlignment() : base("HorizontalAlignment") { }
    }

    [ProcessNode(Name = "SetVerticalAlignment")]
    public class SetVerticalAlignment : StyleSetter<VerticalAlignment>
    {
        public SetVerticalAlignment() : base("VerticalAlignment") { }
    }

    [ProcessNode(Name = "SetUseLayoutRounding")]
    public class SetUseLayoutRounding : StyleSetter<bool>
    {
        public SetUseLayoutRounding() : base("UseLayoutRounding") { }
    }

    #endregion

    #region InputElement

    [ProcessNode(Name = "SetFocusable")]
    public class SetFocusable : StyleSetter<bool>
    {
        public SetFocusable() : base("Focusable") { }
    }

    [ProcessNode(Name = "SetIsEnabled")]
    public class SetIsEnabled : StyleSetter<bool>
    {
        public SetIsEnabled() : base("IsEnabled") { }
    }


    /// <summary>Warning: not implemented</summary>
    [ProcessNode(Name = "SetCursor")]
    public class SetCursor : StyleSetter<Cursor>
    {
        public SetCursor() : base("Cursor") { }
    }

    [ProcessNode(Name = "SetIsHitTestVisible")]
    public class SetIsHitTestVisible : StyleSetter<bool>
    {
        public SetIsHitTestVisible() : base("IsHitTestVisible") { }
    }

    [ProcessNode(Name = "SetIsTabStop")]
    public class SetIsTabStop : StyleSetter<bool>
    {
        public SetIsTabStop() : base("IsTabStop") { }
    }

    [ProcessNode(Name = "SetTabIndex")]
    public class SetTabIndex : StyleSetter<int>
    {
        public SetTabIndex() : base("TabIndex") { }
    }

    #endregion

    #region Control

    /// <summary>Warning: not tested</summary>
    [ProcessNode(Name = "SetFocusAdorner")]
    public class SetFocusAdorner : StyleSetter<ITemplate<Control>>
    {
        public SetFocusAdorner() : base("FocusAdorner") { }
    }

    /// <summary>Warning: not tested</summary>
    [ProcessNode(Name = "SetTag")]
    public class SetTag : StyleSetter<object>
    {
        public SetTag() : base("Tag") { }
    }

    [ProcessNode(Name = "SetContextMenu")]
    public class SetContextMenu : StyleSetter<ContextMenu>
    {
        public SetContextMenu() : base("ContextMenu") { }
    }

    [ProcessNode(Name = "SetContextFlyout")]
    public class SetContextFlyout : StyleSetter<FlyoutBase>
    {
        public SetContextFlyout() : base("ContextFlyout") { }
    }

    #endregion

    #region TemplateControl

    [ProcessNode(Name = "SetBackground")]
    public class SetBackground : StyleSetter<IBrush>
    {
        public SetBackground() : base("Background") { }
    }

    [ProcessNode(Name = "SetBackground")]
    public class SetBackgroundColor : StyleSetterBrushColor
    {
        public SetBackgroundColor() : base("Background") { }
    }

    [ProcessNode(Name = "SetBackgroundSizing")]
    public class SetBackgroundSizing : StyleSetter<BackgroundSizing>
    {
        public SetBackgroundSizing() : base("BackgroundSizing") { }
    }

    [ProcessNode(Name = "SetBorderBrush")]
    public class SetBorderBrush : StyleSetter<IBrush>
    {
        public SetBorderBrush() : base("BorderBrush") { }
    }

    [ProcessNode(Name = "SetBorderBrush")]
    public class SetBorderBrushColor : StyleSetterBrushColor
    {
        public SetBorderBrushColor() : base("BorderBrush") { }
    }

    [ProcessNode(Name = "SetBorderThickness")]
    public class SetBorderThickness : StyleSetterBase<Thickness>
    {
        public SetBorderThickness() : base("BorderThickness") { }
    }

    [ProcessNode(Name = "SetBorderThickness")]
    public class SetBorderThicknessAll : StyleSetterThicknessAll
    {
        public SetBorderThicknessAll() : base("BorderThickness") { }
    }

    [ProcessNode(Name = "SetBorderThickness")]
    public class SetBorderThicknessHV : StyleSetterThicknessHV
    {
        public SetBorderThicknessHV() : base("BorderThickness") { }
    }

    [ProcessNode(Name = "SetBorderThickness")]
    public class SetBorderThicknessLTRB : StyleSetterThicknessLTRB
    {
        public SetBorderThicknessLTRB() : base("BorderThickness") { }
    }

    [ProcessNode(Name = "SetCornerRadius")]
    public class SetCornerRadius : StyleSetter<CornerRadius>
    {
        public SetCornerRadius() : base("CornerRadius") { }
    }

    [ProcessNode(Name = "SetCornerRadius")]
    public class SetCornerRadiusAll : StyleSetter<float, CornerRadius>
    {
        public SetCornerRadiusAll() : base("CornerRadius", (x) => new CornerRadius(x)) { }
    }

    [ProcessNode(Name = "SetCornerRadius")]
    public class SetCornerRadiusHV : StyleSetterCornerRadiusTB
    {
        public SetCornerRadiusHV() : base("CornerRadius") { }
    }

    [ProcessNode(Name = "SetCornerRadius")]
    public class SetCornerRadiusLTRB : StyleSetterCornerRadiusTLTRBRBL
    {
        public SetCornerRadiusLTRB() : base("CornerRadius") { }
    }

    //[ProcessNode(Name = "SetFontFamily")]
    //public class SetFontFamilyAdv : StyleSetterBase<FontFamily>
    //{
    //    public SetFontFamilyAdv() : base("FontFamily") { }
    //}

    [ProcessNode(Name = "SetFontFamily")]
    public class SetFontFamily : StyleSetter<string, FontFamily>
    {
        public SetFontFamily() : base("FontFamily", (x) => new FontFamily(x)) { }
    }


    [ProcessNode(Name = "SetFontFeatures")]
    public class SetFontFeatures : StyleSetter<FontFeatureCollection>
    {
        public SetFontFeatures() : base("FontFeatures") { }
    }


    [ProcessNode(Name = "SetFontSize")]
    public class SetFontSize : StyleSetter<float, double>
    {
        public SetFontSize() : base("FontSize", (x) => (double)x) { }
    }


    [ProcessNode(Name = "SetFontStyle")]
    public class SetFontStyle : StyleSetter<FontStyle>
    {
        public SetFontStyle() : base("FontStyle") { }
    }

    [ProcessNode(Name = "SetFontWeight")]
    public class SetFontWeight : StyleSetter<FontWeight>
    {
        public SetFontWeight() : base("FontWeight") { }
    }

    [ProcessNode(Name = "SetFontStretch")]
    public class SetFontStretch : StyleSetter<FontStretch>
    {
        public SetFontStretch() : base("FontStretch") { }
    }

    [ProcessNode(Name = "SetForeground")]
    public class SetForeground : StyleSetter<IBrush>
    {
        public SetForeground() : base("Foreground") { }
    }

    [ProcessNode(Name = "SetForeground")]
    public class SetForegroundColor : StyleSetterBrushColor
    {
        public SetForegroundColor() : base("Foreground") { }
    }

    [ProcessNode(Name = "SetPadding")]
    public class SetPadding : StyleSetter<Thickness>
    {
        public SetPadding() : base("Padding") { }
    }

    [ProcessNode(Name = "SetPadding")]
    public class SetPaddingAll : StyleSetterThicknessAll
    {
        public SetPaddingAll() : base("Padding") { }
    }

    [ProcessNode(Name = "SetPadding")]
    public class SetPaddingHV : StyleSetterThicknessHV
    {
        public SetPaddingHV() : base("Padding") { }
    }

    [ProcessNode(Name = "SetPadding")]
    public class SetPaddingLTRB : StyleSetterThicknessLTRB
    {
        public SetPaddingLTRB() : base("Padding") { }
    }

    [ProcessNode(Name = "SetTemplate")]
    public class SetTemplate : StyleSetter<IControlTemplate>
    {
        public SetTemplate() : base("Template") { }
    }


    #endregion

    #region ContentControl

    [ProcessNode(Name = "SetContent")]
    public class SetContent : StyleSetter<object>
    {
        public SetContent() : base("Content") { }
    }

    [ProcessNode(Name = "SetContentTemplate")]
    public class SetContentTemplate : StyleSetter<IDataTemplate>
    {
        public SetContentTemplate() : base("ContentTemplate") { }
    }

    [ProcessNode(Name = "SetHorizontalContentAlignment")]
    public class SetHorizontalContentAlignment : StyleSetter<HorizontalAlignment>
    {
        public SetHorizontalContentAlignment() : base("HorizontalContentAlignment") { }
    }

    [ProcessNode(Name = "SetVerticalContentAlignment")]
    public class SetVerticalContentAlignment : StyleSetter<VerticalAlignment>
    {
        public SetVerticalContentAlignment() : base("VerticalContentAlignment") { }
    }

    #endregion

    // TODO:
    #region ItemsPresenter

    /// <summary>Warning: not tested</summary>
    [ProcessNode(Name = "SetItemsPanel")]
    public class SetItemsPanel : StyleSetter<ITemplate<Panel>>
    {
        public SetItemsPanel() : base("ItemsPanel") { }
    }

    #endregion

    #region ItemsControl

    [ProcessNode(Name = "SetItemContainerTheme")]
    public class SetItemContainerTheme : StyleSetter<ControlTheme>
    {
        public SetItemContainerTheme() : base("ItemContainerTheme") { }
    }

    // DOES NOT WORK
    /*
    [ProcessNode(Name = "SetItemsSource")]
    public class SetItemsSource<T> : StyleSetterBase<Spread<T>>
    {
        public SetItemsSource() : base("ItemsSource", (x) => x.Select(y => (object?)y)) { }
    }
    */
    /// <summary>Warning: would change only on items change</summary>
    [ProcessNode(Name = "SetItemTemplate")]
    public class SetItemTemplate : StyleSetter<IDataTemplate>
    {
        public SetItemTemplate() : base("ItemTemplate") { }
    }

    /*
    [ProcessNode(Name = "SetDisplayMemberBinding")]
    public class SetDisplayMemberBinding : StyleSetterBase<IBinding>
    {
        public SetDisplayMemberBinding() : base("DisplayMemberBinding") { }
    }
    */

    #endregion

    #region SelectingItemsControl

    [ProcessNode(Name = "SetAutoScrollToSelectedItem")]
    public class SetAutoScrollToSelectedItem : StyleSetter<bool>
    {
        public SetAutoScrollToSelectedItem() : base("AutoScrollToSelectedItem") { }
    }

    [ProcessNode(Name = "SetSelectedValue")]
    public class SetSelectedValue : StyleSetter<object>
    {
        public SetSelectedValue() : base("SelectedValue") { }
    }

    [ProcessNode(Name = "SetSelectionMode")]
    public class SetSelectionMode : StyleSetter<SelectionMode>
    {
        public SetSelectionMode() : base("SelectionMode") { }
    }

    [ProcessNode(Name = "SetIsSelected")]
    public class SetIsSelected : StyleSetter<bool>
    {
        public SetIsSelected() : base("IsSelected") { }
    }

    [ProcessNode(Name = "SetIsTextSearchEnabled")]
    public class SetIsTextSearchEnabled : StyleSetter<bool>
    {
        public SetIsTextSearchEnabled() : base("IsTextSearchEnabled") { }
    }

    [ProcessNode(Name = "SetWrapSelection")]
    public class SetWrapSelection : StyleSetter<bool>
    {
        public SetWrapSelection() : base("WrapSelection") { }
    }

    #endregion

    #region HeaderedItemsControl

    [ProcessNode(Name = "SetHeader")]
    public class SetHeader : StyleSetter<object>
    {
        public SetHeader() : base("Header") { }
    }

    [ProcessNode(Name = "SetHeaderTemplate")]
    public class SetHeaderTemplate : StyleSetter<IDataTemplate>
    {
        public SetHeaderTemplate() : base("HeaderTemplate") { }
    }

    #endregion

    #region TextBlock

    [ProcessNode(Name = "SetText")]
    public class SetText : StyleSetter<string>
    {
        public SetText() : base("Text") { }
    }

    [ProcessNode(Name = "SetTextDecorations")]
    public class SetTextDecorations : StyleSetterTextDecorationCollection
    {
        public SetTextDecorations() : base() { }
    }

    #endregion

    #region Popup

    [ProcessNode(Name = "SetWindowManagerAddShadowHint")]
    public class SetWindowManagerAddShadowHint : StyleSetter<bool>
    {
        public SetWindowManagerAddShadowHint() : base("WindowManagerAddShadowHint") { }
    }

    [ProcessNode(Name = "SetChild")]
    public class SetChild : StyleSetter<Control>
    {
        public SetChild() : base("Child") { }
    }

    [ProcessNode(Name = "SetInheritsTransform")]
    public class SetInheritsTransform : StyleSetter<bool>
    {
        public SetInheritsTransform() : base("InheritsTransform") { }
    }

    [ProcessNode(Name = "SetIsOpen")]
    public class SetIsOpen : StyleSetter<bool>
    {
        public SetIsOpen() : base("IsOpen") { }
    }

    [ProcessNode(Name = "SetPlacementAnchor")]
    public class SetPlacementAnchor : StyleSetter<PopupAnchor>
    {
        public SetPlacementAnchor() : base("PlacementAnchor") { }
    }

    [ProcessNode(Name = "SetPlacementConstraintAdjustment")]
    public class SetPlacementConstraintAdjustment : StyleSetter<PopupPositionerConstraintAdjustment>
    {
        public SetPlacementConstraintAdjustment() : base("PlacementConstraintAdjustment") { }
    }

    [ProcessNode(Name = "SetPlacementGravity")]
    public class SetPlacementGravity : StyleSetter<PopupGravity>
    {
        public SetPlacementGravity() : base("PlacementGravity") { }
    }

    [ProcessNode(Name = "SetPlacement")]
    public class SetPlacement : StyleSetter<PlacementMode>
    {
        public SetPlacement() : base("Placement") { }
    }

    [ProcessNode(Name = "SetPlacementRect")]
    public class SetPlacementRect : StyleSetter<RectangleF, Rect>
    {
        public SetPlacementRect() : base("PlacementRect", (x) => x.ToRect()) { }
    }

    [ProcessNode(Name = "SetPlacementTarget")]
    public class SetPlacementTarget : StyleSetter<Control>
    {
        public SetPlacementTarget() : base("PlacementTarget") { }
    }

    /// <summary>Warning: not tested</summary>
    [ProcessNode(Name = "SetCustomPopupPlacementCallback")]
    public class SetCustomPopupPlacementCallback : StyleSetter<CustomPopupPlacementCallback>
    {
        public SetCustomPopupPlacementCallback() : base("CustomPopupPlacementCallback") { }
    }

    [ProcessNode(Name = "SetOverlayDismissEventPassThrough")]
    public class SetOverlayDismissEventPassThrough : StyleSetter<bool>
    {
        public SetOverlayDismissEventPassThrough() : base("OverlayDismissEventPassThrough") { }
    }

    [ProcessNode(Name = "SetOverlayInputPassThroughElement")]
    public class SetOverlayInputPassThroughElement : StyleSetter<IInputElement>
    {
        public SetOverlayInputPassThroughElement() : base("OverlayInputPassThroughElement") { }
    }

    // TODO: PIXELS
    [ProcessNode(Name = "SetHorizontalOffset")]
    public class SetHorizontalOffset : StyleSetter<float, double>
    {
        public SetHorizontalOffset() : base("HorizontalOffset", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetIsLightDismissEnabled")]
    public class SetIsLightDismissEnabled : StyleSetter<bool>
    {
        public SetIsLightDismissEnabled() : base("IsLightDismissEnabled") { }
    }

    // TODO: PIXELS

    [ProcessNode(Name = "SetVerticalOffset")]
    public class SetVerticalOffset : StyleSetter<float, double>
    {
        public SetVerticalOffset() : base("VerticalOffset", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetTopmost")]
    public class SetTopmost : StyleSetter<bool>
    {
        public SetTopmost() : base("Topmost") { }
    }

    [ProcessNode(Name = "SetShouldUseOverlayLayer")]
    public class SetShouldUseOverlayLayer : StyleSetter<bool>
    {
        public SetShouldUseOverlayLayer() : base("ShouldUseOverlayLayer") { }
    }

    #endregion

    #region Image

    [ProcessNode(Name = "SetSource")]
    public class SetSource : StyleSetter<IImage>
    {
        public SetSource() : base("Source") { }
    }

    [ProcessNode(Name = "SetBlendMode")]
    public class SetBlendMode : StyleSetter<BitmapBlendingMode>
    {
        public SetBlendMode() : base("BlendMode") { }
    }

    [ProcessNode(Name = "SetStretch")]
    public class SetStretch : StyleSetter<Stretch>
    {
        public SetStretch() : base("Stretch") { }
    }

    [ProcessNode(Name = "SetStretchDirection")]
    public class SetStretchDirection : StyleSetter<StretchDirection>
    {
        public SetStretchDirection() : base("StretchDirection") { }
    }

    #endregion

    #region PathIcon

    [ProcessNode(Name = "SetData")]
    public class SetData : StyleSetter<string, Geometry>
    {
        public SetData() : base("Data", (data) => Geometry.Parse(data)) { }
    }

    #endregion

    #region MaskedTextBox

    [ProcessNode(Name = "SetAsciiOnly")]
    public class SetAsciiOnly : StyleSetter<bool>
    {
        public SetAsciiOnly() : base("AsciiOnly") { }
    }

    [ProcessNode(Name = "SetCulture")]
    public class SetCulture : StyleSetter<CultureInfo>
    {
        public SetCulture() : base("Culture") { }
    }

    [ProcessNode(Name = "SetHidePromptOnLeave")]
    public class SetHidePromptOnLeave : StyleSetter<bool>
    {
        public SetHidePromptOnLeave() : base("HidePromptOnLeave") { }
    }

    [ProcessNode(Name = "SetMask")]
    public class SetMask : StyleSetter<string>
    {
        public SetMask() : base("Mask") { }
    }

    [ProcessNode(Name = "SetPromptChar")]
    public class SetPromptChar : StyleSetter<char>
    {
        public SetPromptChar() : base("PromptChar") { }
    }

    [ProcessNode(Name = "SetResetOnPrompt")]
    public class SetResetOnPrompt : StyleSetter<bool>
    {
        public SetResetOnPrompt() : base("ResetOnPrompt") { }
    }

    [ProcessNode(Name = "SetResetOnSpace")]
    public class SetResetOnSpace : StyleSetter<bool>
    {
        public SetResetOnSpace() : base("ResetOnSpace") { }
    }

    #endregion

    #region ToggleSwitch

    [ProcessNode(Name = "SetOffContent")]
    public class SetOffContent : StyleSetter<object>
    {
        public SetOffContent() : base("OffContent") { }
    }

    [ProcessNode(Name = "SetOffContentTemplate")]
    public class SetOffContentTemplate : StyleSetter<IDataTemplate>
    {
        public SetOffContentTemplate() : base("OffContentTemplate") { }
    }

    [ProcessNode(Name = "SetOnContent")]
    public class SetOnContent : StyleSetter<object>
    {
        public SetOnContent() : base("OnContent") { }
    }

    [ProcessNode(Name = "SetOnContentTemplate")]
    public class SetOnContentTemplate : StyleSetter<IDataTemplate>
    {
        public SetOnContentTemplate() : base("OnContentTemplate") { }
    }

    [ProcessNode(Name = "SetKnobTransitions")]
    public class SetKnobTransitions : StyleSetterTransitions
    {
        public SetKnobTransitions() : base("KnobTransitions") { }
    }

    #endregion

    #region ComboBox

    [ProcessNode(Name = "SetIsEditable")]
    public class SetIsEditable : StyleSetter<bool>
    {
        public SetIsEditable() : base("IsEditable") { }
    }

    [ProcessNode(Name = "SetMaxDropDownHeight")]
    public class SetMaxDropDownHeight : StyleSetter<float, double>
    {
        public SetMaxDropDownHeight() : base("MaxDropDownHeight", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetPlaceholderText")]
    public class SetPlaceholderText : StyleSetter<string>
    {
        public SetPlaceholderText() : base("PlaceholderText") { }
    }

    [ProcessNode(Name = "SetPlaceholderForeground")]
    public class SetPlaceholderForeground : StyleSetter<IBrush>
    {
        public SetPlaceholderForeground() : base("PlaceholderForeground") { }
    }

    [ProcessNode(Name = "SetPlaceholderForeground")]
    public class SetPlaceholderForegroundColor : StyleSetterBrushColor
    {
        public SetPlaceholderForegroundColor() : base("PlaceholderForeground") { }
    }

    [ProcessNode(Name = "SetSelectionBoxItemTemplate")]
    public class SetSelectionBoxItemTemplate : StyleSetter<IDataTemplate>
    {
        public SetSelectionBoxItemTemplate() : base("SelectionBoxItemTemplate") { }
    }

    #endregion

    #region Border

    [ProcessNode(Name = "SetBoxShadow")]
    public class SetBoxShadow : StyleSetter<BoxShadows>
    {
        public SetBoxShadow() : base("BoxShadow") { }
    }

    #endregion

    #region DatePickerPresenter

    [ProcessNode(Name = "SetDayFormat")]
    public class SetDayFormat : StyleSetter<string>
    {
        public SetDayFormat() : base("DayFormat") { }
    }

    [ProcessNode(Name = "SetDayVisible")]
    public class SetDayVisible : StyleSetter<bool>
    {
        public SetDayVisible() : base("DayVisible") { }
    }

    [ProcessNode(Name = "SetMaxYear")]
    public class SetMaxYear : StyleSetter<DateTimeOffset>
    {
        public SetMaxYear() : base("MaxYear") { }
    }

    [ProcessNode(Name = "SetMinYear")]
    public class SetMinYear : StyleSetter<DateTimeOffset>
    {
        public SetMinYear() : base("MinYear") { }
    }

    [ProcessNode(Name = "SetMonthFormat")]
    public class SetMonthFormat : StyleSetter<string>
    {
        public SetMonthFormat() : base("MonthFormat") { }
    }

    [ProcessNode(Name = "SetMonthVisible")]
    public class SetMonthVisible : StyleSetter<bool>
    {
        public SetMonthVisible() : base("MonthVisible") { }
    }

    [ProcessNode(Name = "SetYearFormat")]
    public class SetYearFormat : StyleSetter<string>
    {
        public SetYearFormat() : base("YearFormat") { }
    }

    [ProcessNode(Name = "SetYearVisible")]
    public class SetYearVisible : StyleSetter<bool>
    {
        public SetYearVisible() : base("YearVisible") { }
    }

    #endregion

    #region TimePicker

    [ProcessNode(Name = "SetMinuteIncrement")]
    public class SetMinuteIncrement : StyleSetter<int>
    {
        public SetMinuteIncrement() : base("MinuteIncrement") { }
    }

    [ProcessNode(Name = "SetSecondIncrement")]
    public class SetSecondIncrement : StyleSetter<int>
    {
        public SetSecondIncrement() : base("SecondIncrement") { }
    }

    [ProcessNode(Name = "SetClockIdentifier")]
    public class SetClockIdentifier : StyleSetter<string>
    {
        public SetClockIdentifier() : base("ClockIdentifier") { }
    }

    [ProcessNode(Name = "SetUseSeconds")]
    public class SetUseSeconds : StyleSetter<bool>
    {
        public SetUseSeconds() : base("UseSeconds") { }
    }

    #endregion

    #region RangeBase

    [ProcessNode(Name = "SetMinimum")]
    public class SetMinimum : StyleSetter<float, double>
    {
        public SetMinimum() : base("Minimum", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetMaximum")]
    public class SetMaximum : StyleSetter<float, double>
    {
        public SetMaximum() : base("Maximum", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetSmallChange")]
    public class SetSmallChange : StyleSetter<float, double>
    {
        public SetSmallChange() : base("SmallChange", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetLargeChange")]
    public class SetLargeChange : StyleSetter<float, double>
    {
        public SetLargeChange() : base("LargeChange", (x) => (double)x) { }
    }

    #endregion

    #region WrapPanel

    [ProcessNode(Name = "SetItemSpacing")]
    public class SetItemSpacing : StyleSetter<float, double>
    {
        public SetItemSpacing() : base("ItemSpacing", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetLineSpacing")]
    public class SetLineSpacing : StyleSetter<float, double>
    {
        public SetLineSpacing() : base("LineSpacing", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetOrientation")]
    public class SetOrientation : StyleSetter<Orientation>
    {
        public SetOrientation() : base("Orientation") { }
    }

    /* NOT IMPLEMENTED ON 11.2.1
    [ProcessNode(Name = "SetItemsAlignment")]
    public class SetItemsAlignment : StyleSetter<WrapPanelItemsAlignment>
    {
        public SetItemsAlignment() : base("ItemsAlignment") { }
    }
    */

    [ProcessNode(Name = "SetItemWidth")]
    public class SetItemWidth : StyleSetter<float, double>
    {
        public SetItemWidth() : base("ItemWidth", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetItemHeight")]
    public class SetItemHeight : StyleSetter<float, double>
    {
        public SetItemHeight() : base("ItemHeight", (x) => (double)x) { }
    }

    #endregion

    #region Flyout

    [ProcessNode(Name = "SetFlyoutPresenterTheme")]
    public class SetFlyoutPresenterTheme : StyleSetter<ControlTheme>
    {
        public SetFlyoutPresenterTheme() : base("FlyoutPresenterTheme") { }
    }

    #endregion

    #region ScrollContentPresenter 

    [ProcessNode(Name = "SetCanHorizontallyScroll")]
    public class SetCanHorizontallyScroll : StyleSetter<bool>
    {
        public SetCanHorizontallyScroll() : base("CanHorizontallyScroll") { }
    }

    [ProcessNode(Name = "SetCanVerticallyScroll")]
    public class SetCanVerticallyScroll : StyleSetter<bool>
    {
        public SetCanVerticallyScroll() : base("CanVerticallyScroll") { }
    }

    [ProcessNode(Name = "SetOffset")]
    public class SetOffset : StyleSetter<Vector2, Vector>
    {
        public SetOffset() : base("Offset", (x) => x.ToVector()) { }
    }

    #endregion

    #region SplitButton

    /*
    [ProcessNode(Name = "SetCommand")]
    public class SetCommand : StyleSetter<ICommand>
    {
        public SetCommand() : base("Command") { }
    }

    [ProcessNode(Name = "SetCommandParameter")]
    public class SetCommandParameter : StyleSetter<object>
    {
        public SetCommandParameter() : base("CommandParameter") { }
    }
    */

    [ProcessNode(Name = "SetFlyout")]
    public class SetFlyout : StyleSetter<FlyoutBase>
    {
        public SetFlyout() : base("Flyout") { }
    }

    [ProcessNode(Name = "SetHotKey")]
    public class SetHotKey : StyleSetter<KeyGesture>
    {
        public SetHotKey() : base("HotKey") { }
    }

    #endregion

    #region Shape

    [ProcessNode(Name = "SetFill")]
    public class SetFill : StyleSetter<IBrush>
    {
        public SetFill() : base("Fill") { }
    }

    [ProcessNode(Name = "SetFill")]
    public class SetFillColor : StyleSetterBrushColor
    {
        public SetFillColor() : base("Fill") { }
    }

    [ProcessNode(Name = "SetStroke")]
    public class SetStroke : StyleSetter<IBrush>
    {
        public SetStroke() : base("Stroke") { }
    }

    [ProcessNode(Name = "SetStroke")]
    public class SetStrokeColor : StyleSetterBrushColor
    {
        public SetStrokeColor() : base("Stroke") { }
    }

    [ProcessNode(Name = "SetStrokeDashArray")]
    public class SetStrokeDashArray : StlyeSetterTicks
    {
        public SetStrokeDashArray() : base("StrokeDashArray") { }
    }

    [ProcessNode(Name = "SetStrokeDashOffset")]
    public class SetStrokeDashOffset : StyleSetter<float, double>
    {
        public SetStrokeDashOffset() : base("StrokeDashOffset", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetStrokeThickness")]
    public class SetStrokeThickness : StyleSetter<float, double>
    {
        public SetStrokeThickness() : base("StrokeThickness", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetStrokeLineCap")]
    public class SetStrokeLineCap : StyleSetter<PenLineCap>
    {
        public SetStrokeLineCap() : base("StrokeLineCap") { }
    }

    [ProcessNode(Name = "SetStrokeJoin")]
    public class SetStrokeJoin : StyleSetter<PenLineJoin>
    {
        public SetStrokeJoin() : base("StrokeJoin") { }
    }

    #endregion

    #region Polygon

    [ProcessNode(Name = "SetPoints")]
    public class SetPoints : StyleSetterPoints
    {
        public SetPoints() : base("Points") { }
    }

    #endregion

    #region Line

    [ProcessNode(Name = "SetStartPoint")]
    public class SetStartPoint : StyleSetter<Vector2, AvaloniaPoint>
    {
        public SetStartPoint() : base("StartPoint", (x) => x.ToPoint()) { }
    }

    [ProcessNode(Name = "SetEndPoint")]
    public class SetEndPoint : StyleSetter<Vector2, AvaloniaPoint>
    {
        public SetEndPoint() : base("EndPoint", (x) => x.ToPoint()) { }
    }

    #endregion

    #region Arc

    [ProcessNode(Name = "SetStartAngle")]
    public class SetStartAngle : StyleSetter<float, double>
    {
        public SetStartAngle() : base("StartAngle", (x) => (double)(x * 360.0f)) { }
    }

    [ProcessNode(Name = "SetSweepAngle")]
    public class SetSweepAngle : StyleSetter<float, double>
    {
        public SetSweepAngle() : base("SweepAngle", (x) => (double)(x * 360.0f)) { }
    }

    #endregion

    #region Rectangle

    [ProcessNode(Name = "SetRadiusX")]
    public class SetRadiusX : StyleSetter<float, double>
    {
        public SetRadiusX() : base("RadiusX", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetRadiusY")]
    public class SetRadiusY : StyleSetter<float, double>
    {
        public SetRadiusY() : base("RadiusY", (x) => (double)x) { }
    }

    #endregion

    #region TextPresenter

    [ProcessNode(Name = "SetShowSelectionHighlight")]
    public class SetShowSelectionHighlight : StyleSetter<bool>
    {
        public SetShowSelectionHighlight() : base("ShowSelectionHighlight") { }
    }

    [ProcessNode(Name = "SetCaretIndex")]
    public class SetCaretIndex : StyleSetter<int>
    {
        public SetCaretIndex() : base("CaretIndex") { }
    }

    [ProcessNode(Name = "SetRevealPassword")]
    public class SetRevealPassword : StyleSetter<bool>
    {
        public SetRevealPassword() : base("RevealPassword") { }
    }

    [ProcessNode(Name = "SetPasswordChar")]
    public class SetPasswordChar : StyleSetter<char>
    {
        public SetPasswordChar() : base("PasswordChar") { }
    }

    [ProcessNode(Name = "SetSelectionBrush")]
    public class SetSelectionBrush : StyleSetter<IBrush>
    {
        public SetSelectionBrush() : base("SelectionBrush") { }
    }

    [ProcessNode(Name = "SetSelectionBrush")]
    public class SetSelectionBrushColor : StyleSetterBrushColor
    {
        public SetSelectionBrushColor() : base("SelectionBrush") { }
    }

    [ProcessNode(Name = "SetSelectionForegroundBrush")]
    public class SetSelectionForegroundBrush : StyleSetter<IBrush>
    {
        public SetSelectionForegroundBrush() : base("SelectionForegroundBrush") { }
    }

    [ProcessNode(Name = "SetSelectionForegroundBrush")]
    public class SetSelectionForegroundBrushColor : StyleSetterBrushColor
    {
        public SetSelectionForegroundBrushColor() : base("SelectionForegroundBrush") { }
    }

    [ProcessNode(Name = "SetCaretBrush")]
    public class SetCaretBrush : StyleSetter<IBrush>
    {
        public SetCaretBrush() : base("CaretBrush") { }
    }

    [ProcessNode(Name = "SetCaretBrush")]
    public class SetCaretBrushColor : StyleSetterBrushColor
    {
        public SetCaretBrushColor() : base("CaretBrush") { }
    }

    [ProcessNode(Name = "SetCaretBlinkInterval")]
    public class SetCaretBlinkInterval : StyleSetter<TimeSpan>
    {
        public SetCaretBlinkInterval() : base("CaretBlinkInterval") { }
    }

    [ProcessNode(Name = "SetSelectionStart")]
    public class SetSelectionStart : StyleSetter<int>
    {
        public SetSelectionStart() : base("SelectionStart") { }
    }

    [ProcessNode(Name = "SetSelectionEnd")]
    public class SetSelectionEnd : StyleSetter<int>
    {
        public SetSelectionEnd() : base("SelectionEnd") { }
    }

    [ProcessNode(Name = "SetPreeditText")]
    public class SetPreeditText : StyleSetter<string>
    {
        public SetPreeditText() : base("PreeditText") { }
    }

    [ProcessNode(Name = "SetPreeditTextCursorPosition")]
    public class SetPreeditTextCursorPosition : StyleSetter<int>
    {
        public SetPreeditTextCursorPosition() : base("PreeditTextCursorPosition") { }
    }

    [ProcessNode(Name = "SetTextAlignment")]
    public class SetTextAlignment : StyleSetter<TextAlignment>
    {
        public SetTextAlignment() : base("TextAlignment") { }
    }

    [ProcessNode(Name = "SetTextWrapping")]
    public class SetTextWrapping : StyleSetter<TextWrapping>
    {
        public SetTextWrapping() : base("TextWrapping") { }
    }

    [ProcessNode(Name = "SetLineHeight")]
    public class SetLineHeight : StyleSetter<float, double>
    {
        public SetLineHeight() : base("LineHeight", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetLetterSpacing")]
    public class SetLetterSpacing : StyleSetter<float, double>
    {
        public SetLetterSpacing() : base("LetterSpacing", (x) => (double)x) { }
    }

    #endregion

    #region HeaderedContentControl 

    [ProcessNode(Name = "SetHorizontalHeaderAlignment")]
    public class SetHorizontalHeaderAlignment : StyleSetter<HorizontalAlignment>
    {
        public SetHorizontalHeaderAlignment() : base("HorizontalHeaderAlignment") { }
    }

    [ProcessNode(Name = "SetVerticalHeaderAlignment")]
    public class SetVerticalHeaderAlignment : StyleSetter<VerticalAlignment>
    {
        public SetVerticalHeaderAlignment() : base("VerticalHeaderAlignment") { }
    }

    #endregion

    #region RadioButton

    [ProcessNode(Name = "SetIsChecked")]
    public class SetIsChecked : StyleSetter<bool>
    {
        public SetIsChecked() : base("IsChecked") { }
    }

    [ProcessNode(Name = "SetGroupStyle")]
    public class SetGroupStyle : StyleSetter<Style>
    {
        public SetGroupStyle() : base("GroupStyle") { }
    }

    [ProcessNode(Name = "SetGroupName")]
    public class SetGroupName : StyleSetter<string>
    {
        public SetGroupName() : base("GroupName") { }
    }

    #endregion

    #region ListBox

    /* NOT IMPLEMENTED ON 11.2.1
    [ProcessNode(Name = "SetTextSearchMode")]
    public class SetTextSearchMode : StyleSetter<TextSearchMode>
    {
        public SetTextSearchMode() : base("TextSearchMode") { }
    }

    [ProcessNode(Name = "SetVirtualizationMode")]
    public class SetVirtualizationMode : StyleSetter<VirtualizationMode>
    {
        public SetVirtualizationMode() : base("VirtualizationMode") { }
    }
    */

    #endregion

    #region NotificationCard

    [ProcessNode(Name = "SetIcon")]
    public class SetIcon : StyleSetter<Control>
    {
        public SetIcon() : base("Icon") { }
    }

    [ProcessNode(Name = "SetTitle")]
    public class SetTitle : StyleSetter<string>
    {
        public SetTitle() : base("Title") { }
    }

    [ProcessNode(Name = "SetMessage")]
    public class SetMessage : StyleSetter<string>
    {
        public SetMessage() : base("Message") { }
    }
    /* NOT IMPLEMENTED ON 11.2.1
    [ProcessNode(Name = "SetSeverity")]
    public class SetSeverity : StyleSetter<NotificationSeverity>
    {
        public SetSeverity() : base("Severity") { }
    }
    */

    [ProcessNode(Name = "SetShowCloseButton")]
    public class SetShowCloseButton : StyleSetter<bool>
    {
        public SetShowCloseButton() : base("ShowCloseButton") { }
    }

    [ProcessNode(Name = "SetIsClosable")]
    public class SetIsClosable : StyleSetter<bool>
    {
        public SetIsClosable() : base("IsClosable") { }
    }

    [ProcessNode(Name = "SetDuration")]
    public class SetDuration : StyleSetter<TimeSpan>
    {
        public SetDuration() : base("Duration") { }
    }

    #endregion

    #region DatePicker
    /* 
    [ProcessNode(Name = "SetSelectedDate")]
    public class SetSelectedDate : StyleSetter<DateTimeOffset?>
    {
        public SetSelectedDate() : base("SelectedDate") { }
    }
    */

    [ProcessNode(Name = "SetSelectedDateFormat")]
    public class SetSelectedDateFormat : StyleSetter<string>
    {
        public SetSelectedDateFormat() : base("SelectedDateFormat") { }
    }

    [ProcessNode(Name = "SetDisplayDate")]
    public class SetDisplayDate : StyleSetter<DateTimeOffset>
    {
        public SetDisplayDate() : base("DisplayDate") { }
    }

    [ProcessNode(Name = "SetDisplayDateStart")]
    public class SetDisplayDateStart : StyleSetter<DateTimeOffset?>
    {
        public SetDisplayDateStart() : base("DisplayDateStart") { }
    }

    [ProcessNode(Name = "SetDisplayDateEnd")]
    public class SetDisplayDateEnd : StyleSetter<DateTimeOffset?>
    {
        public SetDisplayDateEnd() : base("DisplayDateEnd") { }
    }

    [ProcessNode(Name = "SetIsDropDownOpen")]
    public class SetIsDropDownOpen : StyleSetter<bool>
    {
        public SetIsDropDownOpen() : base("IsDropDownOpen") { }
    }

    [ProcessNode(Name = "SetCalendarStyle")]
    public class SetCalendarStyle : StyleSetter<Style>
    {
        public SetCalendarStyle() : base("CalendarStyle") { }
    }

    [ProcessNode(Name = "SetCalendarTemplate")]
    public class SetCalendarTemplate : StyleSetter<ITemplate<Control>>
    {
        public SetCalendarTemplate() : base("CalendarTemplate") { }
    }

    [ProcessNode(Name = "SetWatermark")]
    public class SetWatermark : StyleSetter<string>
    {
        public SetWatermark() : base("Watermark") { }
    }

    [ProcessNode(Name = "SetWatermarkTemplate")]
    public class SetWatermarkTemplate : StyleSetter<IDataTemplate>
    {
        public SetWatermarkTemplate() : base("WatermarkTemplate") { }
    }

    #endregion

    #region Grid

    [ProcessNode(Name = "SetShowGridLines")]
    public class SetShowGridLines : StyleSetter<bool>
    {
        public SetShowGridLines() : base("ShowGridLines") { }
    }

    [ProcessNode(Name = "SetRowSpacing")]
    public class SetRowSpacing : StyleSetter<float, double>
    {
        public SetRowSpacing() : base("RowSpacing", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetColumnSpacing")]
    public class SetColumnSpacing : StyleSetter<float, double>
    {
        public SetColumnSpacing() : base("ColumnSpacing", (x) => (double)x) { }
    }

    #endregion

    #region ContentPresenter

    [ProcessNode(Name = "SetTextTrimming")]
    public class SetTextTrimming : StyleSetter<TextTrimming>
    {
        public SetTextTrimming() : base("TextTrimming") { }
    }

    [ProcessNode(Name = "SetMaxLines")]
    public class SetMaxLines : StyleSetter<int>
    {
        public SetMaxLines() : base("MaxLines") { }
    }

    #endregion

    #region StackPanel

    [ProcessNode(Name = "SetSpacing")]
    public class SetSpacing : StyleSetter<float, double>
    {
        public SetSpacing() : base("Spacing", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetAreHorizontalSnapPointsRegular")]
    public class SetAreHorizontalSnapPointsRegular : StyleSetter<bool>
    {
        public SetAreHorizontalSnapPointsRegular() : base("AreHorizontalSnapPointsRegular") { }
    }

    [ProcessNode(Name = "SetAreVerticalSnapPointsRegular")]
    public class SetAreVerticalSnapPointsRegular : StyleSetter<bool>
    {
        public SetAreVerticalSnapPointsRegular() : base("AreVerticalSnapPointsRegular") { }
    }

    #endregion


    #region TextBox

    [ProcessNode(Name = "SetIsInactiveSelectionHighlightEnabled")]
    public class SetIsInactiveSelectionHighlightEnabled : StyleSetter<bool>
    {
        public SetIsInactiveSelectionHighlightEnabled() : base("IsInactiveSelectionHighlightEnabled") { }
    }

    [ProcessNode(Name = "SetClearSelectionOnLostFocus")]
    public class SetClearSelectionOnLostFocus : StyleSetter<bool>
    {
        public SetClearSelectionOnLostFocus() : base("ClearSelectionOnLostFocus") { }
    }

    [ProcessNode(Name = "SetAcceptsReturn")]
    public class SetAcceptsReturn : StyleSetter<bool>
    {
        public SetAcceptsReturn() : base("AcceptsReturn") { }
    }

    [ProcessNode(Name = "SetAcceptsTab")]
    public class SetAcceptsTab : StyleSetter<bool>
    {
        public SetAcceptsTab() : base("AcceptsTab") { }
    }

    [ProcessNode(Name = "SetIsReadOnly")]
    public class SetIsReadOnly : StyleSetter<bool>
    {
        public SetIsReadOnly() : base("IsReadOnly") { }
    }

    [ProcessNode(Name = "SetMaxLength")]
    public class SetMaxLength : StyleSetter<int>
    {
        public SetMaxLength() : base("MaxLength") { }
    }

    [ProcessNode(Name = "SetMinLines")]
    public class SetMinLines : StyleSetter<int>
    {
        public SetMinLines() : base("MinLines") { }
    }

    [ProcessNode(Name = "SetUseFloatingWatermark")]
    public class SetUseFloatingWatermark : StyleSetter<bool>
    {
        public SetUseFloatingWatermark() : base("UseFloatingWatermark") { }
    }

    [ProcessNode(Name = "SetNewLine")]
    public class SetNewLine : StyleSetter<string>
    {
        public SetNewLine() : base("NewLine") { }
    }

    [ProcessNode(Name = "SetInnerLeftContent")]
    public class SetInnerLeftContent : StyleSetter<object>
    {
        public SetInnerLeftContent() : base("InnerLeftContent") { }
    }

    [ProcessNode(Name = "SetInnerRightContent")]
    public class SetInnerRightContent : StyleSetter<object>
    {
        public SetInnerRightContent() : base("InnerRightContent") { }
    }

    #endregion

    #region RefreshContainer

    [ProcessNode(Name = "SetPullDirection")]
    public class SetPullDirection : StyleSetter<PullDirection>
    {
        public SetPullDirection() : base("PullDirection") { }
    }

    #endregion

    #region Button

    [ProcessNode(Name = "SetClickMode")]
    public class SetClickMode : StyleSetter<ClickMode>
    {
        public SetClickMode() : base("ClickMode") { }
    }

    [ProcessNode(Name = "SetIsDefault")]
    public class SetIsDefault : StyleSetter<bool>
    {
        public SetIsDefault() : base("IsDefault") { }
    }

    [ProcessNode(Name = "SetIsCancel")]
    public class SetIsCancel : StyleSetter<bool>
    {
        public SetIsCancel() : base("IsCancel") { }
    }

    #endregion

    #region HyperlinkButton

    [ProcessNode(Name = "SetIsVisited")]
    public class SetIsVisited : StyleSetter<bool>
    {
        public SetIsVisited() : base("IsVisited") { }
    }

    [ProcessNode(Name = "SetNavigateUri")]
    public class SetNavigateUri : StyleSetter<Uri>
    {
        public SetNavigateUri() : base("NavigateUri") { }
    }

    #endregion

    #region DockPanel

    [ProcessNode(Name = "SetLastChildFill")]
    public class SetLastChildFill : StyleSetter<bool>
    {
        public SetLastChildFill() : base("LastChildFill") { }
    }

    [ProcessNode(Name = "SetHorizontalSpacing")]
    public class SetHorizontalSpacing : StyleSetter<float, double>
    {
        public SetHorizontalSpacing() : base("HorizontalSpacing", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetVerticalSpacing")]
    public class SetVerticalSpacing : StyleSetter<float, double>
    {
        public SetVerticalSpacing() : base("VerticalSpacing", (x) => (double)x) { }
    }

    #endregion

    #region MenuItem

    [ProcessNode(Name = "SetInputGesture")]
    public class SetInputGesture : StyleSetter<KeyGesture>
    {
        public SetInputGesture() : base("InputGesture") { }
    }

    [ProcessNode(Name = "SetIsSubMenuOpen")]
    public class SetIsSubMenuOpen : StyleSetter<bool>
    {
        public SetIsSubMenuOpen() : base("IsSubMenuOpen") { }
    }

    [ProcessNode(Name = "SetStaysOpenOnClick")]
    public class SetStaysOpenOnClick : StyleSetter<bool>
    {
        public SetStaysOpenOnClick() : base("StaysOpenOnClick") { }
    }

    [ProcessNode(Name = "SetToggleType")]
    public class SetToggleType : StyleSetter<MenuItemToggleType>
    {
        public SetToggleType() : base("ToggleType") { }
    }

    #endregion

    #region ProgressBar

    [ProcessNode(Name = "SetIsIndeterminate")]
    public class SetIsIndeterminate : StyleSetter<bool>
    {
        public SetIsIndeterminate() : base("IsIndeterminate") { }
    }

    [ProcessNode(Name = "SetShowProgressText")]
    public class SetShowProgressText : StyleSetter<bool>
    {
        public SetShowProgressText() : base("ShowProgressText") { }
    }

    [ProcessNode(Name = "SetProgressTextFormat")]
    public class SetProgressTextFormat : StyleSetter<string>
    {
        public SetProgressTextFormat() : base("ProgressTextFormat") { }
    }

    #endregion

    #region NumericUpDown

    [ProcessNode(Name = "SetAllowSpin")]
    public class SetAllowSpin : StyleSetter<bool>
    {
        public SetAllowSpin() : base("AllowSpin") { }
    }

    [ProcessNode(Name = "SetButtonSpinnerLocation")]
    public class SetButtonSpinnerLocation : StyleSetter<Location>
    {
        public SetButtonSpinnerLocation() : base("ButtonSpinnerLocation") { }
    }

    [ProcessNode(Name = "SetShowButtonSpinner")]
    public class SetShowButtonSpinner : StyleSetter<bool>
    {
        public SetShowButtonSpinner() : base("ShowButtonSpinner") { }
    }

    [ProcessNode(Name = "SetClipValueToMinMax")]
    public class SetClipValueToMinMax : StyleSetter<bool>
    {
        public SetClipValueToMinMax() : base("ClipValueToMinMax") { }
    }

    [ProcessNode(Name = "SetNumberFormat")]
    public class SetNumberFormat : StyleSetter<NumberFormatInfo>
    {
        public SetNumberFormat() : base("NumberFormat") { }
    }

    [ProcessNode(Name = "SetFormatString")]
    public class SetFormatString : StyleSetter<string>
    {
        public SetFormatString() : base("FormatString") { }
    }

    [ProcessNode(Name = "SetIncrement")]
    public class SetIncrement : StyleSetter<float, decimal>
    {
        public SetIncrement() : base("Increment", (x) => (decimal)x) { }
    }

    [ProcessNode(Name = "SetParsingNumberStyle")]
    public class SetParsingNumberStyle : StyleSetter<NumberStyles>
    {
        public SetParsingNumberStyle() : base("ParsingNumberStyle") { }
    }

    [ProcessNode(Name = "SetTextConverter")]
    public class SetTextConverter : StyleSetter<IValueConverter>
    {
        public SetTextConverter() : base("TextConverter") { }
    }

    [ProcessNode(Name = "SetValue")]
    public class SetValue : StyleSetter<float, decimal>
    {
        public SetValue() : base("Value", (x) => (decimal)x) { }
    }

    #endregion

    #region TabControl

    [ProcessNode(Name = "SetTabStripPlacement")]
    public class SetTabStripPlacement : StyleSetter<Dock>
    {
        public SetTabStripPlacement() : base("TabStripPlacement") { }
    }

    #endregion

    #region Slider

    [ProcessNode(Name = "SetIsDirectionReversed")]
    public class SetIsDirectionReversed : StyleSetter<bool>
    {
        public SetIsDirectionReversed() : base("IsDirectionReversed") { }
    }

    [ProcessNode(Name = "SetIsSnapToTickEnabled")]
    public class SetIsSnapToTickEnabled : StyleSetter<bool>
    {
        public SetIsSnapToTickEnabled() : base("IsSnapToTickEnabled") { }
    }

    [ProcessNode(Name = "SetTickFrequency")]
    public class SetTickFrequency : StyleSetter<float, double>
    {
        public SetTickFrequency() : base("TickFrequency", (x) => (double)x) { }
    }

    [ProcessNode(Name = "SetTickPlacement")]
    public class SetTickPlacement : StyleSetter<TickPlacement>
    {
        public SetTickPlacement() : base("TickPlacement") { }
    }

    [ProcessNode(Name = "SetTicks")]
    public class SetTicks : StlyeSetterTicks
    {
        public SetTicks() : base("Ticks") { }
    }

    #endregion

    #region TickBar

    [ProcessNode(Name = "SetReservedSpace")]
    public class SetReservedSpace : StyleSetter<RectangleF, Rect>
    {
        public SetReservedSpace() : base("ReservedSpace", (x) => x.ToRect()) { }
    }

    #endregion

    #region Spinner

    [ProcessNode(Name = "SetValidSpinDirection")]
    public class SetValidSpinDirection : StyleSetter<ValidSpinDirections>
    {
        public SetValidSpinDirection() : base("ValidSpinDirection") { }
    }

    #endregion

    #region Calendar

    [ProcessNode(Name = "SetFirstDayOfWeek")]
    public class SetFirstDayOfWeek : StyleSetter<DayOfWeek>
    {
        public SetFirstDayOfWeek() : base("FirstDayOfWeek") { }
    }

    [ProcessNode(Name = "SetIsTodayHighlighted")]
    public class SetIsTodayHighlighted : StyleSetter<bool>
    {
        public SetIsTodayHighlighted() : base("IsTodayHighlighted") { }
    }

    [ProcessNode(Name = "SetDisplayMode")]
    public class SetDisplayMode : StyleSetter<CalendarMode>
    {
        public SetDisplayMode() : base("DisplayMode") { }
    }

    [ProcessNode(Name = "SetSelectionMode")]
    public class SetSelectionModeCalendar : StyleSetter<CalendarSelectionMode>
    {
        public SetSelectionModeCalendar() : base("SelectionMode") { }
    }

    [ProcessNode(Name = "SetAllowTapRangeSelection")]
    public class SetAllowTapRangeSelection : StyleSetter<bool>
    {
        public SetAllowTapRangeSelection() : base("AllowTapRangeSelection") { }
    }

    #endregion

    #region CalendarItem

    [ProcessNode(Name = "SetHeaderBackground")]
    public class SetHeaderBackground : StyleSetter<IBrush>
    {
        public SetHeaderBackground() : base("HeaderBackground") { }
    }

    [ProcessNode(Name = "SetHeaderBackground")]
    public class SetHeaderBackgroundColor : StyleSetterBrushColor
    {
        public SetHeaderBackgroundColor() : base("HeaderBackground") { }
    }

    [ProcessNode(Name = "SetDayTitleTemplate")]
    public class SetDayTitleTemplate : StyleSetter<ITemplate<Control>>
    {
        public SetDayTitleTemplate() : base("DayTitleTemplate") { }
    }

    #endregion

    #region TransitioningContentControl

    [ProcessNode(Name = "SetPageTransition")]
    public class SetPageTransition : StyleSetter<IPageTransition>
    {
        public SetPageTransition() : base("PageTransition") { }
    }

    [ProcessNode(Name = "SetIsTransitionReversed")]
    public class SetIsTransitionReversed : StyleSetter<bool>
    {
        public SetIsTransitionReversed() : base("IsTransitionReversed") { }
    }

    #endregion

    #region Label

    [ProcessNode(Name = "SetTarget")]
    public class SetTarget : StyleSetter<IInputElement>
    {
        public SetTarget() : base("Target") { }
    }

    #endregion

    #region Expander

    [ProcessNode(Name = "SetContentTransition")]
    public class SetContentTransition : StyleSetter<IPageTransition>
    {
        public SetContentTransition() : base("ContentTransition") { }
    }

    [ProcessNode(Name = "SetExpandDirection")]
    public class SetExpandDirection : StyleSetter<ExpandDirection>
    {
        public SetExpandDirection() : base("ExpandDirection") { }
    }

    [ProcessNode(Name = "SetIsExpanded")]
    public class SetIsExpanded : StyleSetter<bool>
    {
        public SetIsExpanded() : base("IsExpanded") { }
    }

    #endregion

    #region Inline

    [ProcessNode(Name = "SetBaselineAlignment")]
    public class SetBaselineAlignment : StyleSetter<BaselineAlignment>
    {
        public SetBaselineAlignment() : base("BaselineAlignment") { }
    }

    #endregion

    #region AutoCompleteBox

    [ProcessNode(Name = "SetMinimumPrefixLength")]
    public class SetMinimumPrefixLength : StyleSetter<int>
    {
        public SetMinimumPrefixLength() : base("MinimumPrefixLength") { }
    }

    [ProcessNode(Name = "SetMinimumPopulateDelay")]
    public class SetMinimumPopulateDelay : StyleSetter<TimeSpan>
    {
        public SetMinimumPopulateDelay() : base("MinimumPopulateDelay") { }
    }

    [ProcessNode(Name = "SetItemFilter")]
    public class SetItemFilter : StyleSetter<AutoCompleteFilterPredicate<object>>
    {
        public SetItemFilter() : base("ItemFilter") { }
    }

    [ProcessNode(Name = "SetTextFilter")]
    public class SetTextFilter : StyleSetter<AutoCompleteFilterPredicate<string>>
    {
        public SetTextFilter() : base("TextFilter") { }
    }

    [ProcessNode(Name = "SetItemSelector")]
    public class SetItemSelector : StyleSetter<AutoCompleteSelector<object>>
    {
        public SetItemSelector() : base("ItemSelector") { }
    }

    [ProcessNode(Name = "SetTextSelector")]
    public class SetTextSelector : StyleSetter<AutoCompleteSelector<string>>
    {
        public SetTextSelector() : base("TextSelector") { }
    }

    [ProcessNode(Name = "SetAsyncPopulator")]
    public class SetAsyncPopulator : StyleSetter<Func<string, CancellationToken, Task<IEnumerable<object>>>>
    {
        public SetAsyncPopulator() : base("AsyncPopulator") { }
    }

    [ProcessNode(Name = "SetIsTextCompletionEnabled")]
    public class SetIsTextCompletionEnabled : StyleSetter<bool>
    {
        public SetIsTextCompletionEnabled() : base("IsTextCompletionEnabled") { }
    }

    [ProcessNode(Name = "SetSelectedItem")]
    public class SetSelectedItem : StyleSetter<object>
    {
        public SetSelectedItem() : base("SelectedItem") { }
    }

    [ProcessNode(Name = "SetFilterMode")]
    public class SetFilterMode : StyleSetter<AutoCompleteFilterMode>
    {
        public SetFilterMode() : base("FilterMode") { }
    }

    #endregion

    #region DateTimePickerPanel

    [ProcessNode(Name = "SetPanelType")]
    public class SetPanelType : StyleSetter<DateTimePickerPanelType>
    {
        public SetPanelType() : base("PanelType") { }
    }

    [ProcessNode(Name = "SetItemFormat")]
    public class SetItemFormat : StyleSetter<string>
    {
        public SetItemFormat() : base("ItemFormat") { }
    }

    [ProcessNode(Name = "SetShouldLoop")]
    public class SetShouldLoop : StyleSetter<bool>
    {
        public SetShouldLoop() : base("ShouldLoop") { }
    }

    #endregion


}
