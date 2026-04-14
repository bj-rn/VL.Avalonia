using Avalonia.Controls;

namespace VL.Avalonia.Controls
{
    public static class MenuExtensions
    {
        /// <summary>
        /// Opens menu
        /// </summary>
        /// <param name="input">MenuBase</param>
        public static void Open(this MenuBase input) => input?.Open();

        /// <summary>
        /// Closes menu
        /// </summary>
        /// <param name="input">MenuBase</param>
        public static void Close(MenuBase? input) => input?.Close();

        /// <summary>
        /// Gets if menu is open
        /// </summary>
        /// <param name="input">MenuBase</param>
        public static bool IsOpen(MenuBase? input) => input?.IsOpen ?? false;
    }
}
