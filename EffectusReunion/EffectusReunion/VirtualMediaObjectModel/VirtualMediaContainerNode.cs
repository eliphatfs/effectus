using EffectusReunion.RenderingLayer;
using EffectusReunion.VirtualTransport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EffectusReunion.VirtualMediaObjectModel
{
    using IBasicVirtualNode = IVirtualMediaNode<Renderer, IAudioInputNode>;
    public class VirtualMediaContainerNode : VirtualMediaNode<Renderer, AudioSubmixNode>
    {
        private readonly List<IBasicVirtualNode> _children = new();
        public IReadOnlyList<IBasicVirtualNode> Children => _children;
        protected override AudioSubmixNode CreateAudioNode(AudioGraph graph)
        {
            var mix = graph.CreateSubmixNode();
            foreach (var child in Children)
                child.AudioNode.AddOutgoingConnection(mix);
            return mix;
        }
        protected override Renderer CreateVisualNode()
        {
            Renderer renderer = new();
            foreach (var child in Children)
                child.VisualNode.Parent = renderer;
            return renderer;
        }
        public virtual void AppendChild(IBasicVirtualNode node)
        {
            _children.Add(node);
            // TODO: Update Logic Organization
        }
        public override void Update(VirtualTransportControl transport)
        {
            foreach (var child in Children)
                child.Update(transport);
        }
    }
}
