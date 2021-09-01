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
    public abstract class VirtualMediaNode<VisualT, AudioT> : IVirtualMediaNode<VisualT, AudioT>
        where VisualT: FrameworkElement
        where AudioT : IAudioInputNode
    {
        public VisualT VisualNode
        {
            get; protected set;
        }
        public AudioT AudioNode
        {
            get; protected set;
        }
        public virtual void Initialize(AudioGraph graph)
        {
            VisualNode = CreateVisualNode();
            AudioNode = CreateAudioNode(graph);
        }
        protected abstract VisualT CreateVisualNode();
        protected abstract AudioT CreateAudioNode(AudioGraph graph);
        public virtual void Update(VirtualTransportControl transport)
        {
        }
    }
}
