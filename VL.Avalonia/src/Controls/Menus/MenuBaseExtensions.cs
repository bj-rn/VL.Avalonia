using Avalonia.Controls;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="MenuBase"/>.
    /// </summary>
    public static class MenuBaseExtensions
    {
        /// <inheritdoc cref="MenuBase.IsOpen"/>
        public static bool IsOpen(this MenuBase menuBase) =>
            menuBase != null ? menuBase.IsOpen : false;

        /// <inheritdoc cref="MenuBase.Open"/>
        public static void Open(this MenuBase menuBase)
        {
            menuBase?.Open();
        }

        /// <inheritdoc cref="MenuBase.Close"/>
        public static void Close(this MenuBase menuBase)
        {
            menuBase?.Close();
        }
    }
}
