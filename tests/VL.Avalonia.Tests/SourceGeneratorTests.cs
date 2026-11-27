using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using VL.Avalonia.CodeGen;

namespace VL.Avalonia.Tests;

public class SourceGeneratorTests
{
    private const string Usings = """
        using Avalonia.Controls;
        using Avalonia.Media;
        using VL.Avalonia.Attributes;
        using VL.Core;
        using VL.Core.Import;
        using VL.Lib.Collections;
        using VL.Model;

        """;

    private const string Field = """
        [ImplementProperty(typeof(Panel), nameof(Panel.BackgroundProperty))]
        private Optional<IBrush> _background;
        """;

    private static GeneratorRunResult Run(string source)
    {
        var compilation = CSharpCompilation.Create(
            "GeneratorTest",
            new[] { CSharpSyntaxTree.ParseText(Usings + source) },
            References.Value,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );

        return CSharpGeneratorDriver
            .Create(new AvaloniaSourceGenerator())
            .RunGenerators(compilation)
            .GetRunResult()
            .Results
            .Single();
    }

    private static readonly Lazy<MetadataReference[]> References = new(() =>
    {
        // Make sure the assemblies the test source refers to are loaded.
        _ = new[]
        {
            typeof(global::Avalonia.Controls.Panel).Assembly,
            typeof(VL.Avalonia.Attributes.ImplementProperty).Assembly,
            typeof(VL.Core.Import.ProcessNodeAttribute).Assembly,
            typeof(VL.Lib.Collections.Spread<int>).Assembly,
        };
        return AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
            .Select(a => (MetadataReference)MetadataReference.CreateFromFile(a.Location))
            .ToArray();
    });

    private static void AssertGenerated(GeneratorRunResult result, int expectedSources)
    {
        Assert.Null(result.Exception);
        Assert.Empty(result.Diagnostics);
        Assert.Equal(expectedSources, result.GeneratedSources.Length);
    }

    [Fact]
    public void Single_node_generates_one_source()
    {
        var result = Run($$"""
            namespace Gen;
            [ProcessNode] public partial class Node { {{Field}} }
            """);

        AssertGenerated(result, 1);
        Assert.Contains("SetBackground", result.GeneratedSources[0].SourceText.ToString());
    }

    [Fact]
    public void Generic_and_non_generic_node_with_the_same_name_both_generate()
    {
        var result = Run($$"""
            namespace Gen;
            [ProcessNode] public partial class Node { {{Field}} }
            [ProcessNode] public partial class Node<T> { {{Field}} }
            """);

        AssertGenerated(result, 2);
    }

    [Fact]
    public void Nodes_with_the_same_name_in_different_namespaces_both_generate()
    {
        var result = Run($$"""
            namespace Gen.A { [ProcessNode] public partial class Node { {{Field}} } }
            namespace Gen.B { [ProcessNode] public partial class Node { {{Field}} } }
            """);

        AssertGenerated(result, 2);
    }

    [Fact]
    public void Partial_node_split_over_two_declarations_generates_both_parts()
    {
        var result = Run($$"""
            namespace Gen;
            [ProcessNode] public partial class Node { {{Field}} }
            [ProcessNode] public partial class Node
            {
                [ImplementProperty(typeof(Panel), nameof(Panel.BackgroundProperty))]
                private Optional<IBrush> _other;
            }
            """);

        AssertGenerated(result, 2);
    }
}
