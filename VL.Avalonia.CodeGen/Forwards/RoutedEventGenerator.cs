using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace VL.Avalonia.CodeGen.Forwards
{
    public static class RoutedEventGenerator
    {
        const string TARGET_FILENAME = "RoutedEvents.g.cs";

        public static void Generate(SourceProductionContext spc, Compilation compilation)
        {
            var routedEventGenericType = compilation.GetTypeByMetadataName(
                "Avalonia.Interactivity.RoutedEvent`1"
            );
            var routedEventType = compilation.GetTypeByMetadataName(
                "Avalonia.Interactivity.RoutedEvent"
            );
            var routedEventArgsType = compilation.GetTypeByMetadataName(
                "Avalonia.Interactivity.RoutedEventArgs"
            );
            var interactiveType = compilation.GetTypeByMetadataName(
                "Avalonia.Interactivity.Interactive"
            );
            var obsoleteAttributeType = compilation.GetTypeByMetadataName(
                "System.ObsoleteAttribute"
            );

            if (
                routedEventGenericType == null
                || routedEventType == null
                || routedEventArgsType == null
                || interactiveType == null
            )
                return;

            var results = new List<(IFieldSymbol Field, INamedTypeSymbol ArgsType)>();

            foreach (var reference in compilation.SourceModule.ReferencedAssemblySymbols)
            {
                if (!reference.Name.StartsWith("Avalonia"))
                    continue;

                VisitNamespace(
                    reference.GlobalNamespace,
                    routedEventGenericType,
                    routedEventType,
                    routedEventArgsType,
                    obsoleteAttributeType,
                    results
                );
            }

            if (results.Count == 0)
                return;

            var sb = new StringBuilder();

            var knownNamespaces = new HashSet<string>
            {
                "System",
                "Avalonia.Interactivity",
                "VL.Core.Import",
                "VL.Avalonia.Interactivity",
            };

            var namespacesToImport = new SortedSet<string>();
            var validResults =
                new List<(
                    IFieldSymbol Field,
                    INamedTypeSymbol ArgsType,
                    INamedTypeSymbol GenericSender
                )>();

            foreach (var item in results)
            {
                var senderType = item.Field.ContainingType;
                INamedTypeSymbol genericSenderType = null;

                if (senderType.IsStatic)
                {
                    genericSenderType = interactiveType;
                }
                else if (
                    SymbolEqualityComparer.Default.Equals(senderType, interactiveType)
                    || InheritsFrom(senderType, interactiveType)
                )
                {
                    genericSenderType = senderType;
                }
                else
                {
                    continue;
                }

                validResults.Add((item.Field, item.ArgsType, genericSenderType));

                AddNamespace(item.Field.ContainingType.ContainingNamespace);
                AddNamespace(item.ArgsType.ContainingNamespace);
            }

            if (validResults.Count == 0)
                return;

            void AddNamespace(INamespaceSymbol ns)
            {
                if (ns == null || ns.IsGlobalNamespace)
                    return;
                var nsName = ns.ToDisplayString();
                if (!knownNamespaces.Contains(nsName))
                {
                    namespacesToImport.Add(nsName);
                }
            }

            sb.AppendLine("using System;");
            sb.AppendLine("using Avalonia.Interactivity;");
            sb.AppendLine("using VL.Core.Import;");
            sb.AppendLine("using VL.Avalonia.Interactivity;");

            foreach (var ns in namespacesToImport)
            {
                sb.AppendLine($"using {ns};");
            }

            sb.AppendLine();
            sb.AppendLine("namespace VL.Avalonia.Interactivity");
            sb.AppendLine("{");

            var typeFormat = SymbolDisplayFormat.MinimallyQualifiedFormat;

            foreach (
                var (field, argsType, genericSenderType) in validResults
                    .OrderBy(x => x.Field.ContainingType.Name)
                    .ThenBy(x => x.Field.Name)
            )
            {
                var senderType = field.ContainingType;
                var rawEventName = field.Name;

                string shortEventName = rawEventName;
                if (shortEventName.EndsWith("Event"))
                    shortEventName = shortEventName.Substring(0, shortEventName.Length - 5);

                var className = $"{senderType.Name}{shortEventName}RoutedEventListener";
                var nodeName = $"{shortEventName}RoutedEventListener";
                // csharpier-ignore
                sb.AppendLine($@"    [ProcessNode(Name = ""{nodeName}"")]");
                // csharpier-ignore
                sb.AppendLine($@"    public class {className} : RoutedEventListener<{genericSenderType.ToDisplayString(typeFormat)}, {argsType.ToDisplayString(typeFormat)}>");
                // csharpier-ignore
                sb.AppendLine($@"    {{");
                // csharpier-ignore
                sb.AppendLine($@"        public {className}()");
                // csharpier-ignore
                sb.AppendLine($@"        {{");
                // csharpier-ignore
                sb.AppendLine($@"            Event = {senderType.ToDisplayString(typeFormat)}.{field.Name};");
                // csharpier-ignore
                sb.AppendLine($@"        }}");
                // csharpier-ignore
                sb.AppendLine();
                // csharpier-ignore
                sb.AppendLine($@"        public static readonly RoutedEvent<{argsType.ToDisplayString(typeFormat)}> {shortEventName}Event = {senderType.ToDisplayString(typeFormat)}.{field.Name};");
                // csharpier-ignore
                sb.AppendLine($@"    }}");
                // csharpier-ignore
                sb.AppendLine();
            }

            sb.AppendLine("}");

            spc.AddSource(TARGET_FILENAME, SourceText.From(sb.ToString(), Encoding.UTF8));
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

        private static void VisitNamespace(
            INamespaceSymbol namespaceSymbol,
            INamedTypeSymbol routedEventGenericType,
            INamedTypeSymbol routedEventType,
            INamedTypeSymbol routedEventArgsType,
            INamedTypeSymbol obsoleteAttributeType,
            List<(IFieldSymbol Field, INamedTypeSymbol ArgsType)> results
        )
        {
            foreach (var member in namespaceSymbol.GetMembers())
            {
                if (member is INamespaceSymbol nestedNs)
                {
                    VisitNamespace(
                        nestedNs,
                        routedEventGenericType,
                        routedEventType,
                        routedEventArgsType,
                        obsoleteAttributeType,
                        results
                    );
                }
                else if (member is INamedTypeSymbol typeSymbol)
                {
                    if (typeSymbol.DeclaredAccessibility != Accessibility.Public)
                        continue;

                    foreach (var field in typeSymbol.GetMembers().OfType<IFieldSymbol>())
                    {
                        if (!field.IsStatic || field.DeclaredAccessibility != Accessibility.Public)
                            continue;

                        // Skip obsolete fields
                        if (
                            obsoleteAttributeType != null
                            && field
                                .GetAttributes()
                                .Any(ad =>
                                    SymbolEqualityComparer.Default.Equals(
                                        ad.AttributeClass,
                                        obsoleteAttributeType
                                    )
                                )
                        )
                        {
                            continue;
                        }

                        // Check 1: Is it RoutedEvent<T>?
                        if (
                            field.Type is INamedTypeSymbol fieldType
                            && SymbolEqualityComparer.Default.Equals(
                                fieldType.OriginalDefinition,
                                routedEventGenericType
                            )
                        )
                        {
                            var argsType = fieldType.TypeArguments[0] as INamedTypeSymbol;
                            results.Add((field, argsType));
                        }
                    }
                }
            }
        }
    }
}
