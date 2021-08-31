using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.UI.Xaml;
using EffectusReunion.VirtualTransport;

namespace EffectusReunion.VirtualMediaObjectModel
{
    /// <summary>
    /// Base class for all descriptors and logical nodes.
    /// Produces audio and video entity nodes.
    /// </summary>
    public abstract class VirtualMediaNode
    {
        public abstract FrameworkElement CreateVisualNode();
        public abstract IAudioNode CreateAudioNode(AudioGraph graph);
        public virtual void Update(VirtualTransportControl transport)
        {
        }
    }
}
