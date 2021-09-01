using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.Media.Audio;

namespace EffectusReunion.VirtualMediaObjectModel
{
    public class VirtualMediaTextNode : VirtualMediaNode<TextBlock, AudioSubmixNode>
    {
        protected override AudioSubmixNode CreateAudioNode(AudioGraph graph)
        {
            return graph.CreateSubmixNode();
        }

        protected override TextBlock CreateVisualNode()
        {
            return new TextBlock();
        }
    }
}
