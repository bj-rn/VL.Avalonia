using System.Reflection;
using Avalonia.Animation.Easings;
using VL.Avalonia.Animation.Easings;
using VL.Core.Import;

namespace VL.Avalonia.Tests;

public class EasingsTests
{
    public static TheoryData<Type> Wrappers()
    {
        var data = new TheoryData<Type>();
        foreach (
            var type in typeof(EasingWrapperBase<>)
                .Assembly.GetTypes()
                .Where(t => !t.IsAbstract && EasingType(t) is not null)
                .OrderBy(t => t.Name)
        )
            data.Add(type);
        return data;
    }

    private static Type? EasingType(Type wrapper)
    {
        for (var t = wrapper.BaseType; t is not null; t = t.BaseType)
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(EasingWrapperBase<>))
                return t.GetGenericArguments()[0];
        return null;
    }

    [Fact]
    public void All_easing_wrappers_are_found()
    {
        Assert.Equal(31, Wrappers().Count);
    }

    [Theory]
    [MemberData(nameof(Wrappers))]
    public void Node_outputs_the_easing_it_is_named_after(Type wrapper)
    {
        var name = wrapper.GetCustomAttribute<ProcessNodeAttribute>()!.Name;
        var easing = EasingType(wrapper)!;

        Assert.Equal(name, easing.Name);
        Assert.Equal(wrapper.Name, easing.Name + "Wrapper");

        var output = wrapper.GetProperty("Output")!.GetValue(Activator.CreateInstance(wrapper));
        Assert.IsType(easing, output);
    }

    [Theory]
    [InlineData(typeof(QuadraticEaseOutWrapper), 0.25, 0.4375)]
    [InlineData(typeof(QuadraticEaseInWrapper), 0.25, 0.0625)]
    [InlineData(typeof(QuadraticEaseInOutWrapper), 0.25, 0.125)]
    public void Easing_curve_differs_by_kind(Type wrapper, double progress, double expected)
    {
        var easing = (Easing)wrapper.GetProperty("Output")!.GetValue(Activator.CreateInstance(wrapper))!;

        Assert.Equal(expected, easing.Ease(progress), 6);
    }
}
