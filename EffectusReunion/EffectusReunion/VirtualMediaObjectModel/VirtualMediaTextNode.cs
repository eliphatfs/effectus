using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.Media.Audio;
using EffectusReunion.RenderingLayer;

namespace EffectusReunion.VirtualMediaObjectModel
{
    public class VirtualMediaTextNode : VirtualMediaNode<TextRenderer, AudioSubmixNode>
    {
        protected override AudioSubmixNode CreateAudioNode(AudioGraph graph)
        {
            return graph.CreateSubmixNode();
        }

        protected override TextRenderer CreateVisualNode()
        {
            return new ();
        }
    }
}
