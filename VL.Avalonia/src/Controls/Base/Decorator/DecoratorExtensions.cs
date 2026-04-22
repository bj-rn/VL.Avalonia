using Avalonia;
using Avalonia.Controls;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="Decorator"/>.
    /// </summary>
    public static class DecoratorExtensions
    {
        /// <inheritdoc cref="Decorator.Child"/>
        public static Control? Child(this Decorator decorator) =>
            decorator != null ? decorator.Child : null;

        /// <inheritdoc cref="Decorator.Child"/>
        public static void SetChild(this Decorator decorator, Control? child)
        {
            if (decorator is not null)
                decorator.Child = child;
        }

        /// <inheritdoc cref="Decorator.Padding"/>
        public static Thickness Padding(this Decorator decorator) =>
            decorator != null ? decorator.Padding : new Thickness();

        /// <inheritdoc cref="Decorator.Padding"/>
        public static void SetPadding(this Decorator decorator, Thickness padding)
        {
            if (decorator is not null)
                decorator.Padding = padding;
        }
    }
}
