using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VL.Avalonia.Controls;
using VL.Avalonia.Helpers;
using VL.Core.Import;
using VL.Lib.Reactive;

namespace VL.Avalonia.Custom.Controls.Value
{
    /// <summary>
    /// ColorView
    /// <br/><br/><see href="https://docs.avaloniaui.net/docs/reference/controls/colorpicker/">ColorView</see>
    /// </summary>
    [ProcessNode(Name = "ColorView")]
    public partial class ColorViewWrapper<T> : ControlWrapperBase<T> where T : ColorView, new()
    {
        protected ChannelTwoWayBinding<Color, Color> _valueBinding;
        public ColorViewWrapper()
        {
            _valueBinding = new ChannelTwoWayBinding<Color, Color>(_output, ColorView.ColorProperty, (x) => x, (x) => x);
        }

        /// <param name="colorChannel">
        /// Gets or sets the current value
        /// </param>
        [Fragment(Order = 0 /*PinOrder.Main*/)]
        public void SetValueChannel(IChannel<Color> colorChannel) =>
            _valueBinding.SetChannel(colorChannel);
    }
}
