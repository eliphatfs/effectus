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
        public FrameworkElement VisualNode
        {
            get; protected set;
        }
        public IAudioNode AudioNode
        {
            get; protected set;
        }
        public virtual void Initialize(AudioGraph graph)
        {
            VisualNode = CreateVisualNode();
            AudioNode = CreateAudioNode(graph);
        }
        protected abstract FrameworkElement CreateVisualNode();
        protected abstract IAudioNode CreateAudioNode(AudioGraph graph);
        public virtual void Update(VirtualTransportControl transport)
        {
        }
    }
}
