using System.Reactive;
using VL.Avalonia.Helpers;
using VL.Lib.Reactive;

namespace VL.Avalonia.Tests;

public class ChannelCommandTests
{
    private static (IChannel<T> Channel, List<T> Received) Recorded<T>()
    {
        var channel = ChannelHelpers.CreateChannelOfType<T>();
        var received = new List<T>();
        channel.Subscribe(received.Add);
        return (channel, received);
    }

    [Fact]
    public void Binding_pushes_the_parameter_to_the_channel()
    {
        var (channel, received) = Recorded<int>();
        var binding = new ChannelCommandBinding<int>();
        binding.SetCommandChannel(channel);

        binding.Execute(7);

        Assert.Equal(new[] { 7 }, received);
    }

    [Fact]
    public void Unit_binding_pushes_to_the_channel()
    {
        var (channel, received) = Recorded<Unit>();
        var binding = new ChannelCommandBindingUnit();
        binding.SetCommandChannel(channel);

        binding.Execute(null);

        Assert.Single(received);
    }

    [Fact]
    public void Binding_without_a_channel_does_nothing()
    {
        var binding = new ChannelCommandBindingUnit();

        binding.Execute(null);

        Assert.True(binding.CanExecute(null));
    }

    [Fact]
    public void Disposing_the_binding_leaves_the_channel_alive()
    {
        var (channel, received) = Recorded<Unit>();
        var binding = new ChannelCommandBindingUnit();
        binding.SetCommandChannel(channel);

        binding.Dispose();
        channel.OnNext(Unit.Default);

        Assert.Single(received);
    }

    [Fact]
    public void Disposed_binding_no_longer_pushes()
    {
        var (channel, received) = Recorded<Unit>();
        var binding = new ChannelCommandBindingUnit();
        binding.SetCommandChannel(channel);

        binding.Dispose();
        binding.Execute(null);

        Assert.Empty(received);
    }

    [Fact]
    public void Typed_channel_command_can_execute_and_pushes_the_parameter()
    {
        var (channel, received) = Recorded<int>();
        var command = new TChannelCommand<int> { CommandChannel = channel };

        Assert.True(command.CanExecute(3));
        command.Execute(3);

        Assert.Equal(new[] { 3 }, received);
    }
}
