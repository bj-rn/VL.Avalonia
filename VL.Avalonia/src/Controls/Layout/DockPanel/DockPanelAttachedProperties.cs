using Avalonia.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Controls
{
    /// <summary>
    /// The edge (Left, Right, Top, Bottom) to which the child is docked.
    /// Attached Property
    /// </summary>
    [ProcessNode(Name = "Dock (DockPanel)")]
    public partial class DockPanelDock : AttachedPropertyBase
    {
        private Optional<Dock> _dock;

        /// <param name="dock">
        /// (Attached) The edge (Left, Right, Top, Bottom) to which the child is docked.
        /// </param>
        public void SetDock(Optional<Dock> dock)
        {
            if (_dock != dock)
            {
                _dock = dock;

                UpdateSetters();
            }
        }

        protected override void UpdateSetters()
        {
            if (_input.HasNoValue)
            {
                return;
            }

            if (_dock.HasValue)
            {
                DockPanel.SetDock(_input.Value, _dock.Value);
            }
            else
            {
                _input.Value?.ClearValue(DockPanel.DockProperty);
            }
        }
    }
}
