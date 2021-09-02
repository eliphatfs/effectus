using EffectusReunion.MediaTransportSystem;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EffectusReunion.MediaEntityTree
{
    public class MediaContainerNode : IMediaEntityNode
    {
        private readonly List<IMediaEntityNode> _children = new();
        public IReadOnlyList<IMediaEntityNode> Children => _children;

        public void Render(TransportControl transport, CanvasDrawingSession canvas, AudioFrameInputNode audio)
        {
            throw new NotImplementedException();
        }
    }
}
