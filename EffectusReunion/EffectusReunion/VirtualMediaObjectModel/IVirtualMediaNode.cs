using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.UI.Xaml;

namespace EffectusReunion.VirtualMediaObjectModel
{
    public interface IVirtualMediaNode<out VisualT, out AudioT>
        where VisualT : FrameworkElement
        where AudioT : IAudioInputNode
    {
        public VisualT VisualNode
        {
            get;
        }
        public AudioT AudioNode
        {
            get;
        }
    }
}
