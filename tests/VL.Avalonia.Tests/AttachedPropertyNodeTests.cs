using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using VL.Avalonia.Controls;
using VL.Core;

namespace VL.Avalonia.Tests;

public class AttachedPropertyNodeTests
{
    public static TheoryData<string> Nodes()
    {
        var data = new TheoryData<string>();
        foreach (var type in typeof(AttachedPropertyBase).Assembly.GetTypes()
            .Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(AttachedPropertyBase)))
            .OrderBy(t => t.Name))
        {
            data.Add(type.FullName!);
        }
        return data;
    }

    private static AttachedPropertyBase Create(string typeName) =>
        (AttachedPropertyBase)Activator.CreateInstance(typeof(AttachedPropertyBase).Assembly.GetType(typeName)!)!;

    // Calls the node's value setter (e.g. SetRow) with a sample value for every pin.
    private static void SetValue(AttachedPropertyBase node)
    {
        var setter = node.GetType()
            .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Single(m => m.Name.StartsWith("Set") && m.Name != nameof(AttachedPropertyBase.SetInput));

        var args = setter.GetParameters()
            .Select(p => Activator.CreateInstance(p.ParameterType, Sample(p.ParameterType.GetGenericArguments()[0])))
            .ToArray();

        setter.Invoke(node, args);
    }

    private static object Sample(Type type) =>
        type == typeof(int) ? 1
        : type == typeof(float) ? 1f
        : type == typeof(bool) ? true
        : type.IsEnum ? Enum.GetValues(type).GetValue(1)!
        : "Sibling";

    private static AvaloniaProperty[] SetAttachedProperties(Control control) =>
        AvaloniaPropertyRegistry.Instance.GetRegisteredAttached(control.GetType())
            .Where(control.IsSet)
            .ToArray();

    [AvaloniaTheory]
    [MemberData(nameof(Nodes))]
    public void Changing_the_input_moves_the_attached_value_to_the_new_control(string typeName)
    {
        var node = Create(typeName);
        var first = new Border();
        var second = new Border();
        SetValue(node);

        node.SetInput(first);
        var attached = SetAttachedProperties(first);
        Assert.NotEmpty(attached);

        node.SetInput(second);

        Assert.Empty(SetAttachedProperties(first));
        Assert.Equal(attached, SetAttachedProperties(second));
        Assert.Same(second, node.Output);
    }

    [AvaloniaTheory]
    [MemberData(nameof(Nodes))]
    public void Disconnecting_the_input_clears_the_attached_value(string typeName)
    {
        var node = Create(typeName);
        var control = new Border();
        SetValue(node);
        node.SetInput(control);

        node.SetInput(default);

        Assert.Empty(SetAttachedProperties(control));
        Assert.Null(node.Output);
    }

    [AvaloniaTheory]
    [MemberData(nameof(Nodes))]
    public void Optional_holding_null_is_treated_as_no_input(string typeName)
    {
        var node = Create(typeName);

        // Without a value the node clears; with a value it sets. Neither may touch a null control.
        node.SetInput(new Optional<Control>(null!));
        SetValue(node);

        Assert.Null(node.Output);
    }

    [AvaloniaFact]
    public void Chained_nodes_without_input_do_not_throw()
    {
        var row = new GridRowProperty();
        var column = new GridColumnProperty();
        column.SetColumn(2);

        // VL converts the null Output into an Optional holding null.
        column.SetInput(row.Output!);

        Assert.Null(column.Output);
    }

    [AvaloniaFact]
    public void Setting_the_same_input_again_keeps_the_value()
    {
        var node = new GridRowProperty();
        var control = new Border();
        node.SetRow(3);
        node.SetInput(control);

        node.SetInput(control);

        Assert.Equal(3, Grid.GetRow(control));
    }

    [AvaloniaFact]
    public void ToolTip_moves_all_its_attached_values_to_the_new_control()
    {
        var node = new ToolTipWrapper<Control>(null!);
        var first = new Border();
        var second = new Border();
        node.SetPlacement(PlacementMode.Top);
        node.SetHorizontalOffset(5f);
        node.SetVerticalOffset(6f);
        node.SetShowDelay(7);
        node.SetBetweenShowDelay(8);
        node.SetShowDisabled(true);
        node.SetInput(first);

        node.SetInput(second);

        Assert.Empty(SetAttachedProperties(first));
        Assert.NotNull(ToolTip.GetTip(second));
        Assert.Equal(PlacementMode.Top, ToolTip.GetPlacement(second));
        Assert.Equal(5, ToolTip.GetHorizontalOffset(second));
        Assert.Equal(6, ToolTip.GetVerticalOffset(second));
        Assert.Equal(7, ToolTip.GetShowDelay(second));
        Assert.Equal(8, ToolTip.GetBetweenShowDelay(second));
        Assert.True(ToolTip.GetShowOnDisabled(second));
    }
}
