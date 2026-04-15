using Avalonia.Controls.Primitives;
using VL.Avalonia.Attributes;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

/// <summary>
/// Base class for controls that display a value within a range.
/// </summary>
[ProcessNode]
public abstract partial class RangeBaseWrapper<T> : ControlNodeBase<T>
    where T : RangeBase, new()
{
    #region Core Properties

    protected ChannelTwoWayBinding<float, double> _valueBinding;

    protected RangeBaseWrapper()
    {
        _valueBinding = new ChannelTwoWayBinding<float, double>(
            _output,
            RangeBase.ValueProperty,
            (x) => (double)x,
            (x) => (float)x
        );
    }

    /// <param name="valueChannel">
    /// Gets or sets the current value
    /// </param>
    [Fragment(Order = PinOrder.Main)]
    public void SetValueChannel(IChannel<float> valueChannel) =>
        _valueBinding.SetChannel(valueChannel);

    /// <param name="minimum">
    /// Sets the minimum possible value.
    /// </param>
    [ImplementProperty("RangeBase.MinimumProperty")]
    protected Optional<float> _mininum;

    /// <param name="maximum">
    /// Sets the maximum possible value.
    /// </param>
    [ImplementProperty("RangeBase.MaximumProperty")]
    protected Optional<float> _maximum;

    #endregion

    #region Additional Properties

    /// <param name="smallChange">
    /// Sets the small increment value added or subtracted from
    /// </param>
    [ImplementProperty(
        "RangeBase.SmallChangeProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<float> _smallChange;

    /// <param name="smallChange">
    /// Sets the large increment value added or subtracted from
    /// </param>
    [ImplementProperty(
        "RangeBase.LargeChangeProperty",
        PinVisibility = Model.PinVisibility.Optional
    )]
    protected Optional<float> _largeChange;

    #endregion
}

