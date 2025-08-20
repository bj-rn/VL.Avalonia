using Avalonia.UpDock.Controls;
using VL.Avalonia.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.UpDock.Controls
{
    [ProcessNode(Name = "ResizeDockPanel")]
    public class ResizeDockPanelWrapper : DockPanelWrapperBase<ResizeDockPanel>
    {

    }

    [ProcessNode(Name = "DockSpacePanel")]
    public class DockSpacePanelWrapper : ResizeDockPanelWrapper
    {

    }

    [ProcessNode(Name = "SplitPanel")]
    public class SplitPanelWrapper : PanelWrapperBase<SplitPanel>
    {
        protected Optional<string> _fractions;
        /// <param name="fractions">Comma separated integers: "1, 1, 2"</param>
        public void SetFractions(Optional<string> fractions)
        {
            if (_fractions != fractions)
            {
                if (fractions.HasValue)
                {
                    var parsedFractions = SplitFractions.Parse(fractions.Value);

                    _output.Fractions = parsedFractions;
                }
                else
                {
                    _output.Fractions = SplitFractions.Default;
                }

                _fractions = fractions;
            }
        }
    }

    [ProcessNode(Name = "ClosableTabItem")]
    public partial class ClosableTabItemWrapper : TabItemWrapperBase<ClosableTabItem>
    {

    }

    [ProcessNode(Name = "RearrangeTabControl")]
    public partial class RearrangeTabControlWrapper : TabControlWrapperBase<RearrangeTabControl, object>
    {

    }
}
