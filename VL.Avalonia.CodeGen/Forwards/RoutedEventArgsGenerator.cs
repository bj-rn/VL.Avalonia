using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace VL.Avalonia.CodeGen.Forwards
{
    public static class RoutedEventArgsGenerator
    {
        const string TARGET_FILENAME = "RoutedEventArgsForwards.g.cs";
        const string TARGET_TYPE_NAME = "Avalonia.Interactivity.RoutedEventArgs";

        public static void Generate(SourceProductionContext spc, Compilation compilation)
        {
            var routedEventArgsType = compilation.GetTypeByMetadataName(TARGET_TYPE_NAME);
            if (routedEventArgsType == null)
                return;

            var foundTypes = new List<INamedTypeSymbol>();

            foreach (var reference in compilation.SourceModule.ReferencedAssemblySymbols)
            {
                if (!reference.Name.StartsWith("Avalonia"))
                    continue;

                // Helper function to visit all types in the assembly
                VisitNamespace(reference.GlobalNamespace, routedEventArgsType, foundTypes);
            }

            if (foundTypes.Count == 0)
            {
                return;
            }

            foundTypes.Add(routedEventArgsType);

            var namespaces = foundTypes
                .Select(t => t.ContainingNamespace.ToDisplayString())
                .Distinct()
                .OrderBy(n => n);

            var sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using VL.Core.Import;");

            foreach (var ns in namespaces)
            {
                sb.AppendLine($"using {ns};");
            }
            sb.AppendLine();

            var typeFormat = SymbolDisplayFormat.MinimallyQualifiedFormat;

            foreach (var type in foundTypes)
            {
                sb.AppendLine(
                    // csharpier-ignore
                    $@"[assembly: ImportType(typeof({type.ToDisplayString(typeFormat)}), Category = ""{type.ContainingNamespace.ToDisplayString()}"")]"
                );
            }

            spc.AddSource(TARGET_FILENAME, SourceText.From(sb.ToString(), Encoding.UTF8));
        }

        private static void VisitNamespace(
            INamespaceSymbol namespaceSymbol,
            INamedTypeSymbol baseType,
            List<INamedTypeSymbol> foundTypes
        )
        {
            foreach (var member in namespaceSymbol.GetMembers())
            {
                if (member is INamespaceSymbol nestedNs)
                {
                    VisitNamespace(nestedNs, baseType, foundTypes);
                }
                else if (member is INamedTypeSymbol typeSymbol)
                {
                    if (
                        typeSymbol.TypeKind == TypeKind.Class
                        && !typeSymbol.IsAbstract
                        && InheritsFrom(typeSymbol, baseType)
                    )
                    {
                        foundTypes.Add(typeSymbol);
                    }
                }
            }
        }

        private static bool InheritsFrom(INamedTypeSymbol type, INamedTypeSymbol baseType)
        {
            var current = type.BaseType;
            while (current != null)
            {
                if (SymbolEqualityComparer.Default.Equals(current, baseType))
                    return true;
                current = current.BaseType;
            }
            return false;
        }
    }
}
