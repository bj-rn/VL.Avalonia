using Avalonia.Animation;
using VL.Avalonia.Controls;
using VL.Core.Import;

namespace VL.Avalonia.Animation.Transitions
{
    [ProcessNode(Name = "BoolTransition", HasStateOutput = true)]
    public class BoolTransitionWrapper : TransitionBaseNodeBase<BoolTransition> { }

    [ProcessNode(Name = "BoxShadowsTransition", HasStateOutput = true)]
    public class BoxShadowsTransitionWrapper : TransitionBaseNodeBase<BoxShadowsTransition> { }

    [ProcessNode(Name = "BrushTransition", HasStateOutput = true)]
    public class BrushTransitionWrapper : TransitionBaseNodeBase<BrushTransition> { }

    [ProcessNode(Name = "ColorTransition", HasStateOutput = true)]
    public class ColorTransitionWrapper : TransitionBaseNodeBase<ColorTransition> { }

    [ProcessNode(Name = "CornerRadiusTransition", HasStateOutput = true)]
    public class CornerRadiusTransitionWrapper : TransitionBaseNodeBase<CornerRadiusTransition> { }

    [ProcessNode(Name = "DoubleTransition", HasStateOutput = true)]
    public class DoubleTransitionWrapper : TransitionBaseNodeBase<DoubleTransition> { }

    [ProcessNode(Name = "FloatTransition", HasStateOutput = true)]
    public class FloatTransitionWrapper : TransitionBaseNodeBase<FloatTransition> { }

    [ProcessNode(Name = "IntegerTransition", HasStateOutput = true)]
    public class IntegerTransitionWrapper : TransitionBaseNodeBase<IntegerTransition> { }

    [ProcessNode(Name = "PointTransition", HasStateOutput = true)]
    public class PointTransitionWrapper : TransitionBaseNodeBase<PointTransition> { }

    [ProcessNode(Name = "RelativePointTransition", HasStateOutput = true)]
    public class RelativePointTransitionWrapper
        : TransitionBaseNodeBase<RelativePointTransition> { }

    [ProcessNode(Name = "SizeTransition", HasStateOutput = true)]
    public class SizeTransitionWrapper : TransitionBaseNodeBase<SizeTransition> { }

    [ProcessNode(Name = "ThicknessTransition", HasStateOutput = true)]
    public class ThicknessTransitionWrapper : TransitionBaseNodeBase<ThicknessTransition> { }

    [ProcessNode(Name = "TransformOperationsTransition", HasStateOutput = true)]
    public class TransformOperationsTransitionWrapper
        : TransitionBaseNodeBase<TransformOperationsTransition> { }

    [ProcessNode(Name = "VectorTransition", HasStateOutput = true)]
    public class VectorTransitionWrapper : TransitionBaseNodeBase<VectorTransition> { }
}
