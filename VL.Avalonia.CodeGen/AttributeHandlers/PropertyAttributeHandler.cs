using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.CodeAnalysis;

namespace VL.Avalonia.CodeGen.AttributeHandlers
{
    public sealed class PropertyAttributeHandler : IAttributeHandler
    {
        private static readonly Dictionary<string, string> PinVisibilities = new Dictionary<
            string,
            string
        >()
        {
            { "0", "Model.PinVisibility.Visible" },
            { "1", "Model.PinVisibility.Optional" },
            { "2", "Model.PinVisibility.Hidden" },
        };

        public bool CanHandle(AttributeData attr) =>
            attr.AttributeClass?.Name.StartsWith("ImplementProperty") == true;

        public string? GenerateMethod(
            AttributeData attr,
            IFieldSymbol fieldSymbol,
            string fieldName
        )
        {
            var tValue = attr
                .AttributeClass?.TypeArguments.ElementAtOrDefault(0)
                ?.ToDisplayString();
            var tProperty = attr
                .AttributeClass?.TypeArguments.ElementAtOrDefault(1)
                ?.ToDisplayString();

            var typeCast = tProperty == null ? "" : $"({tProperty})";

            var order = int.Parse(
                attr.NamedArguments.FirstOrDefault(x => x.Key == "Order").Value.Value?.ToString()
                ?? "0"
            );

            var pinVisibility = PinVisibilities[
                attr.NamedArguments.FirstOrDefault(x => x.Key == "PinVisibility")
                    .Value.Value?.ToString()
                ?? "0"
            ];

            // 1. Resolve names first so we can use paramBase in the XML documentation
            var paramBase = fieldName.TrimStart('_');
            var paramName =
                paramBase.Length > 0
                    ? char.ToLower(paramBase[0]) + paramBase.Substring(1)
                    : fieldName;
            var methodName =
                "Set"
                + (
                    paramBase.Length > 0
                        ? char.ToUpper(paramBase[0]) + paramBase.Substring(1)
                        : fieldName
                );

            // 2. Parse and translate XML documentation
            var xmlDoc = fieldSymbol.GetDocumentationCommentXml();
            var paramDoc = "";

            if (!string.IsNullOrEmpty(xmlDoc))
            {
                var doc = new XmlDocument();
                doc.LoadXml(xmlDoc);

                var memberNode = doc.SelectSingleNode("/member");
                if (memberNode != null)
                {
                    // Check if they manually provided a <param> tag
                    var existingParam = memberNode.SelectSingleNode("param");

                    if (existingParam != null)
                    {
                        paramDoc = $"/// {existingParam.OuterXml}";
                    }
                    else
                    {
                        // Otherwise, grab the inner content (like <inheritdoc> or <summary>)
                        var innerXml = memberNode.InnerXml.Trim();

                        // If it is wrapped in a <summary>, strip the summary tags so it fits inside <param>
                        var summaryNode = memberNode.SelectSingleNode("summary");
                        if (summaryNode != null)
                        {
                            innerXml = summaryNode.InnerXml.Trim();
                        }

                        paramDoc = $"/// <param name=\"{paramBase}\">{innerXml}</param>";
                    }

                    // Collapse into a single line
                    paramDoc = Regex.Replace(paramDoc, @"[\r\n]+", " ");
                }
            }

            // 3. Get generic type argument for Optional<T>
            var fieldType = fieldSymbol.Type as INamedTypeSymbol;
            var typeArg =
                fieldType
                    ?.TypeArguments.FirstOrDefault()
                    ?.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat) ?? "object";

            // 4. Resolve PropertyPath based on which constructor the user used
            string propertyPath = "";
            if (attr.ConstructorArguments.Length == 1)
            {
                // Old behavior: [ImplementProperty("Control.NameProperty")]
                propertyPath = attr.ConstructorArguments[0].Value?.ToString() ?? "";
            }
            else if (attr.ConstructorArguments.Length == 2)
            {
                // New behavior: [ImplementProperty(typeof(Control), nameof(Control.NameProperty))]
                var ownerTypeSymbol = attr.ConstructorArguments[0].Value as INamedTypeSymbol;
                var propertyValueName = attr.ConstructorArguments[1].Value?.ToString();

                // Use FullyQualifiedFormat to ensure it emits global::Namespace.Type
                var typeString = ownerTypeSymbol?.ToDisplayString(
                    SymbolDisplayFormat.FullyQualifiedFormat
                );
                propertyPath = $"{typeString}.{propertyValueName}";
            }

            // 5. Generate the final template using the early-return refactor
            var template =
                $@"
    {paramDoc}
    [Fragment(Order = {order})]
    public void {methodName}([Pin(Visibility = {pinVisibility})] Optional<{typeArg}> {paramBase})
    {{
        if ({fieldName} == {paramBase})
            return;

        {fieldName} = {paramBase};

        if ({paramBase}.HasValue)
        {{
            _output.SetValue({propertyPath}, {typeCast} {paramBase}.Value);
        }}
        else 
        {{
            _output.ClearValue({propertyPath});
        }}
    }}
";

            return template;
        }
    }
}
