using Avalonia.Controls;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// Extension methods for <see cref="RadioButton"/>.
    /// </summary>
    public static class RadioButtonExtensions
    {
        /// <inheritdoc cref="RadioButton.GroupName"/>
        public static string? GroupName(this RadioButton radioButton) =>
            radioButton != null ? radioButton.GroupName : null;

        /// <inheritdoc cref="RadioButton.GroupName"/>
        public static void SetGroupName(this RadioButton radioButton, string? groupName)
        {
            if (radioButton is not null)
                radioButton.GroupName = groupName;
        }
    }
}
