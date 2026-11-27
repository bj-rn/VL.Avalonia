using System.Collections;
using Avalonia.Collections;

namespace VL.Avalonia.Helpers
{
    /// <summary>
    /// The list handed to an items property. Avalonia looks items up through <see cref="IList"/>
    /// with any object (a tree view asks every expanded items list for the clicked item), and
    /// <see cref="AvaloniaList{T}"/> casts that object to <typeparamref name="T"/>, which throws
    /// for an item of another type. Here such an item is not found, like in a <see cref="List{T}"/>.
    /// </summary>
    internal sealed class ItemsList<T> : AvaloniaList<T>, IList
    {
        bool IList.Contains(object? value) => IsCompatible(value) && Contains((T)value!);

        int IList.IndexOf(object? value) => IsCompatible(value) ? IndexOf((T)value!) : -1;

        void IList.Remove(object? value)
        {
            if (IsCompatible(value))
                Remove((T)value!);
        }

        private static bool IsCompatible(object? value) => value is T || (value is null && default(T) is null);
    }
}
