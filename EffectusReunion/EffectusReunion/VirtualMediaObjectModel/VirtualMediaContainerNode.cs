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
    using IBasicVirtualNode = IVirtualMediaNode<FrameworkElement, IAudioInputNode>;
    public class VirtualMediaContainerNode : VirtualMediaNode<Canvas, AudioSubmixNode>
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
        protected override Canvas CreateVisualNode()
        {
            var canvas = new Canvas();
            foreach (var child in Children)
                canvas.Children.Add(child.VisualNode);
            return canvas;
        }
        public virtual void AppendChild(IBasicVirtualNode node)
        {
            _children.Add(node);
            VisualNode.Children.Add(node.VisualNode);
        }
    }
}
