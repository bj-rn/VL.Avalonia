using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.CodeAnalysis;

// TEMPLATE
/*
    protected Optional<IBrush> _background;
    /// <inheritdoc cref="_background"/>
    [Fragment(Order = -10)]
    public void SetBackground([Pin(Visibility = Model.PinVisibility.Optional)] Optional<IBrush> background)
    {
        if (_background != background)
        {
            if (background.HasValue)
            {
                _output.SetValue(Border.BackgroundProperty, (cast)background.Value);
            }
            else
            {
                _output.ClearValue(Border.BackgroundProperty);
            }

            _background = background;
        }
    }
*/

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
                attr.NamedArguments.Where(x => x.Key == "Order")
                    .FirstOrDefault()
                    .Value.Value?.ToString()
                ?? "0"
            );
            var pinVisibility = PinVisibilities[
                attr.NamedArguments.Where(x => x.Key == "PinVisibility")
                    .FirstOrDefault()
                    .Value.Value?.ToString()
                ?? "0"
            ];

            var xmlDoc = fieldSymbol.GetDocumentationCommentXml();
            var paramDoc = "";

            if (!string.IsNullOrEmpty(xmlDoc))
            {
                var doc = new XmlDocument();
                doc.LoadXml(xmlDoc);

                var param = doc.SelectSingleNode("//param");

                if (param != null)
                {
                    paramDoc = $"/// {param.OuterXml}";
                    paramDoc = Regex.Replace(paramDoc, @"[\r\n]+", " ");
                }
            }

            var fieldType = fieldSymbol.Type as INamedTypeSymbol;
            var typeArg =
                fieldType
                    ?.TypeArguments.FirstOrDefault()
                    ?.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat) ?? "object";

            var propertyPath = attr.ConstructorArguments.ElementAt(0).Value?.ToString();

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

            var template =
                $@"
    {paramDoc}
    [Fragment(Order = {order})]
    public void {methodName}([Pin(Visibility = {pinVisibility})] Optional<{typeArg}> {paramBase})
    {{
        if ({fieldName} != {paramBase})
        {{
            if ({paramBase}.HasValue)
            {{
                _output.SetValue({propertyPath}, {typeCast} {paramBase}.Value);
            }}
            else 
            {{
                _output.ClearValue({propertyPath});
            }}

            {fieldName} = {paramBase};
        }}
    }}
";

            return template;
        }
    }
}
