using System.Reactive;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Controls;

[ProcessNode(Name = "AttachedFlyout")]
public partial class AttachedFlyoutWrapper<T>
    where T : Control
{
    private T? _input;

    [Fragment(Order = -10)]
    public void SetInput(T? input)
    {
        if (_input != input)
        {
            _input = input;

            UpdateSetters();
        }
    }

    public T? Output => _input;

    private Optional<FlyoutBase> _flyout;

    [Fragment(Order = -5)]
    public void SetFlyout(Optional<FlyoutBase> flyout)
    {
        if (_flyout != flyout)
        {
            _flyout = flyout;

            UpdateSetters();
        }
    }

    private IChannel<Unit>? _showAttachedFlyoutChannel;

    public void SetShowAttachedFlyoutChannel(IChannel<Unit>? showAttachedFlyoutChannel)
    {
        if (_showAttachedFlyoutChannel != showAttachedFlyoutChannel)
        {
            _showAttachedFlyoutChannel = showAttachedFlyoutChannel;

            if (_showAttachedFlyoutChannel != null)
            {
                _showAttachedFlyoutChannel.Subscribe(_ =>
                {
                    if (_input != null)
                    {
                        FlyoutBase.ShowAttachedFlyout(_input);
                    }
                });
            }
        }
    }

    internal void UpdateSetters()
    {
        if (_input != null)
        {
            if (_flyout.HasValue)
            {
                FlyoutBase.SetAttachedFlyout(_input, _flyout.Value);
            }
            else
            {
                _input.ClearValue(FlyoutBase.AttachedFlyoutProperty);
            }
        }
    }
}
