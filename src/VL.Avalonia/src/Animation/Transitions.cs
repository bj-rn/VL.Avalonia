using Avalonia.Animation;
using VL.Core.Import;

namespace VL.Avalonia.Animation.Transitions
{
    [ProcessNode(Name = "BoolTransition", HasStateOutput = true)]
    public class BoolTransitionWrapper : TransitionBaseWrapper<BoolTransition> { }

    [ProcessNode(Name = "BoxShadowsTransition", HasStateOutput = true)]
    public class BoxShadowsTransitionWrapper : TransitionBaseWrapper<BoxShadowsTransition> { }

    [ProcessNode(Name = "BrushTransition", HasStateOutput = true)]
    public class BrushTransitionWrapper : TransitionBaseWrapper<BrushTransition> { }

    [ProcessNode(Name = "ColorTransition", HasStateOutput = true)]
    public class ColorTransitionWrapper : TransitionBaseWrapper<ColorTransition> { }

    [ProcessNode(Name = "CornerRadiusTransition", HasStateOutput = true)]
    public class CornerRadiusTransitionWrapper : TransitionBaseWrapper<CornerRadiusTransition> { }

    [ProcessNode(Name = "DoubleTransition", HasStateOutput = true)]
    public class DoubleTransitionWrapper : TransitionBaseWrapper<DoubleTransition> { }

    [ProcessNode(Name = "FloatTransition", HasStateOutput = true)]
    public class FloatTransitionWrapper : TransitionBaseWrapper<FloatTransition> { }

    [ProcessNode(Name = "IntegerTransition", HasStateOutput = true)]
    public class IntegerTransitionWrapper : TransitionBaseWrapper<IntegerTransition> { }

    [ProcessNode(Name = "PointTransition", HasStateOutput = true)]
    public class PointTransitionWrapper : TransitionBaseWrapper<PointTransition> { }

    [ProcessNode(Name = "RelativePointTransition", HasStateOutput = true)]
    public class RelativePointTransitionWrapper : TransitionBaseWrapper<RelativePointTransition> { }



    [ProcessNode(Name = "SizeTransition", HasStateOutput = true)]
    public class SizeTransitionWrapper : TransitionBaseWrapper<SizeTransition> { }

    [ProcessNode(Name = "ThicknessTransition", HasStateOutput = true)]
    public class ThicknessTransitionWrapper : TransitionBaseWrapper<ThicknessTransition> { }

    [ProcessNode(Name = "TransformOperationsTransition", HasStateOutput = true)]
    public class TransformOperationsTransitionWrapper : TransitionBaseWrapper<TransformOperationsTransition> { }

    [ProcessNode(Name = "VectorTransition", HasStateOutput = true)]
    public class VectorTransitionWrapper : TransitionBaseWrapper<VectorTransition> { }



}
