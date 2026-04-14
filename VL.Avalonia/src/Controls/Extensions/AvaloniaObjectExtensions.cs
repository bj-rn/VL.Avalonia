using Avalonia;
using Avalonia.Data;

namespace VL.Avalonia
{
    /// <summary>
    /// Extension methods for <see cref="AvaloniaObject"/>.
    /// </summary>
    public static class AvaloniaObjectExtensions
    {
        /// <inheritdoc cref="AvaloniaObject.CheckAccess"/>
        public static bool CheckAccess(this AvaloniaObject avaloniaObject)
        {
            return avaloniaObject?.CheckAccess() ?? false;
        }

        /// <inheritdoc cref="AvaloniaObject.VerifyAccess"/>
        public static void VerifyAccess(this AvaloniaObject avaloniaObject)
        {
            avaloniaObject?.VerifyAccess();
        }

        /// <inheritdoc cref="AvaloniaObject.ClearValue(AvaloniaProperty)"/>
        public static void ClearValue(this AvaloniaObject avaloniaObject, AvaloniaProperty property)
        {
            avaloniaObject?.ClearValue(property);
        }

        /// <inheritdoc cref="AvaloniaObject.ClearValue{T}(AvaloniaProperty{T})"/>
        public static void ClearValue<T>(
            this AvaloniaObject avaloniaObject,
            AvaloniaProperty<T> property
        )
        {
            avaloniaObject?.ClearValue(property);
        }

        /// <inheritdoc cref="AvaloniaObject.GetValue(AvaloniaProperty)"/>
        public static object? GetValue(
            this AvaloniaObject avaloniaObject,
            AvaloniaProperty property
        )
        {
            return avaloniaObject?.GetValue(property);
        }

        /// <inheritdoc cref="AvaloniaObject.GetValue{T}(StyledProperty{T})"/>
        public static T? GetValue<T>(this AvaloniaObject avaloniaObject, StyledProperty<T> property)
        {
            if (avaloniaObject is not null)
            {
                return avaloniaObject.GetValue(property);
            }
            return default;
        }

        /// <inheritdoc cref="AvaloniaObject.GetValue{T}(DirectPropertyBase{T})"/>
        public static T? GetValue<T>(
            this AvaloniaObject avaloniaObject,
            DirectPropertyBase<T> property
        )
        {
            if (avaloniaObject is not null)
            {
                return avaloniaObject.GetValue(property);
            }
            return default;
        }

        /// <inheritdoc cref="AvaloniaObject.IsAnimating(AvaloniaProperty)"/>
        public static bool IsAnimating(
            this AvaloniaObject avaloniaObject,
            AvaloniaProperty property
        )
        {
            return avaloniaObject?.IsAnimating(property) ?? false;
        }

        /// <inheritdoc cref="AvaloniaObject.IsSet(AvaloniaProperty)"/>
        public static bool IsSet(this AvaloniaObject avaloniaObject, AvaloniaProperty property)
        {
            return avaloniaObject?.IsSet(property) ?? false;
        }

        /// <inheritdoc cref="AvaloniaObject.SetValue(AvaloniaProperty, object?, BindingPriority)"/>
        public static IDisposable? SetValue(
            this AvaloniaObject avaloniaObject,
            AvaloniaProperty property,
            object? value,
            BindingPriority priority = BindingPriority.LocalValue
        )
        {
            return avaloniaObject?.SetValue(property, value, priority);
        }

        /// <inheritdoc cref="AvaloniaObject.SetValue{T}(StyledProperty{T}, T, BindingPriority)"/>
        public static IDisposable? SetValue<T>(
            this AvaloniaObject avaloniaObject,
            StyledProperty<T> property,
            T value,
            BindingPriority priority = BindingPriority.LocalValue
        )
        {
            return avaloniaObject?.SetValue(property, value, priority);
        }

        /// <inheritdoc cref="AvaloniaObject.SetValue{T}(DirectPropertyBase{T}, T)"/>
        public static void SetValue<T>(
            this AvaloniaObject avaloniaObject,
            DirectPropertyBase<T> property,
            T value
        )
        {
            avaloniaObject?.SetValue(property, value);
        }

        /// <inheritdoc cref="AvaloniaObject.SetCurrentValue(AvaloniaProperty, object?)"/>
        public static void SetCurrentValue(
            this AvaloniaObject avaloniaObject,
            AvaloniaProperty property,
            object? value
        )
        {
            avaloniaObject?.SetCurrentValue(property, value);
        }

        /// <inheritdoc cref="AvaloniaObject.SetCurrentValue{T}(StyledProperty{T}, T)"/>
        public static void SetCurrentValue<T>(
            this AvaloniaObject avaloniaObject,
            StyledProperty<T> property,
            T value
        )
        {
            avaloniaObject?.SetCurrentValue(property, value);
        }

        /// <inheritdoc cref="AvaloniaObject.Bind(AvaloniaProperty, IBinding)"/>
        public static IDisposable? Bind(
            this AvaloniaObject avaloniaObject,
            AvaloniaProperty property,
            IBinding binding
        )
        {
            return avaloniaObject?.Bind(property, binding);
        }

        /// <inheritdoc cref="AvaloniaObject.Bind(AvaloniaProperty, IObservable{object?}, BindingPriority)"/>
        public static IDisposable? Bind(
            this AvaloniaObject avaloniaObject,
            AvaloniaProperty property,
            IObservable<object?> source,
            BindingPriority priority = BindingPriority.LocalValue
        )
        {
            return avaloniaObject?.Bind(property, source, priority);
        }

        /// <inheritdoc cref="AvaloniaObject.Bind{T}(StyledProperty{T}, IObservable{T}, BindingPriority)"/>
        public static IDisposable? Bind<T>(
            this AvaloniaObject avaloniaObject,
            StyledProperty<T> property,
            IObservable<T> source,
            BindingPriority priority = BindingPriority.LocalValue
        )
        {
            return avaloniaObject?.Bind(property, source, priority);
        }

        /// <inheritdoc cref="AvaloniaObject.Bind{T}(DirectPropertyBase{T}, IObservable{T})"/>
        public static IDisposable? Bind<T>(
            this AvaloniaObject avaloniaObject,
            DirectPropertyBase<T> property,
            IObservable<T> source
        )
        {
            return avaloniaObject?.Bind(property, source);
        }

        /// <inheritdoc cref="AvaloniaObject.CoerceValue(AvaloniaProperty)"/>
        public static void CoerceValue(
            this AvaloniaObject avaloniaObject,
            AvaloniaProperty property
        )
        {
            avaloniaObject?.CoerceValue(property);
        }
    }
}
