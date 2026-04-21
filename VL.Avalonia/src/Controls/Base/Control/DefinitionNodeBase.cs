using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Core;
using VL.Core.Import;
using VL.Model;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Base wrapper for <see cref="DefinitionBase"/>
    /// </summary>
    [ProcessNode]
    public abstract partial class DefinitionNodeBase<T> : AvaloniaObjectNodeBase<T>
        where T : DefinitionBase, new()
    {
        /// <summary>Sets shared size group property. Marks column / row definition as belonging to a group "Foo" or "Bar".</summary>
        [ImplementProperty(
            typeof(DefinitionBase),
            nameof(DefinitionBase.SharedSizeGroupProperty),
            Order = PinOrder.Control,
            PinVisibility = PinVisibility.Optional
        )]
        private Optional<string> _sharedSizeGroup;
    }
}
