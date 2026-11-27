using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using VL.Avalonia.Data;
using VL.Lib.Reactive;

namespace VL.Avalonia.Tests;

public class BindingNameNodeTests
{
    private readonly IChannel<string> _channel = ChannelHelpers.CreateChannelOfType<string>();

    private BindingNameNode<AvaloniaObject, string> Node(AvaloniaObject input, string? propertyName)
    {
        var node = new BindingNameNode<AvaloniaObject, string>();
        node.SetChannel(_channel);
        node.SetInput(input);
        node.SetProperty(propertyName);
        return node;
    }

    [AvaloniaFact]
    public void Binds_the_named_property()
    {
        var textBox = new TextBox();
        _channel.Value = "a";

        using var node = Node(textBox, "Text");
        _channel.Value = "b";

        Assert.Equal("b", textBox.Text);
    }

    [AvaloniaFact]
    public void Name_set_before_input_binds_once_input_arrives()
    {
        var textBox = new TextBox();
        using var node = new BindingNameNode<AvaloniaObject, string>();
        node.SetChannel(_channel);
        node.SetProperty("Text");

        node.SetInput(textBox);
        _channel.Value = "a";

        Assert.Equal("a", textBox.Text);
    }

    [AvaloniaFact]
    public void Switching_to_another_property_moves_the_binding()
    {
        var textBox = new TextBox();
        using var node = Node(textBox, "Text");

        node.SetProperty("Watermark");
        _channel.Value = "a";

        Assert.Equal("a", textBox.Watermark);
        Assert.NotEqual("a", textBox.Text);
    }

    [AvaloniaTheory]
    [InlineData("NonExistent")]
    [InlineData("")]
    [InlineData(null)]
    public void Unresolvable_name_detaches_the_previous_binding(string? propertyName)
    {
        var textBox = new TextBox();
        using var node = Node(textBox, "Text");

        node.SetProperty(propertyName);
        _channel.Value = "a";

        Assert.NotEqual("a", textBox.Text);
    }

    [AvaloniaFact]
    public void Input_without_the_property_does_not_get_the_previous_property_bound()
    {
        var border = new Border();
        using var node = Node(new TextBox(), "Text");

        node.SetInput(border);
        _channel.Value = "a";

        Assert.False(border.IsSet(TextBox.TextProperty));
    }

    [AvaloniaFact]
    public void Returning_to_an_input_with_the_property_rebinds()
    {
        var textBox = new TextBox();
        using var node = Node(textBox, "Text");

        node.SetInput(new Border());
        node.SetInput(textBox);
        _channel.Value = "a";

        Assert.Equal("a", textBox.Text);
    }
}
