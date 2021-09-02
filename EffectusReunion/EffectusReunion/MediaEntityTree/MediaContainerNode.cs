using EffectusReunion.MediaTransportSystem;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Windows.Media.Audio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EffectusReunion.MediaEntityTree
{
    public class MediaContainerNode : IMediaEntityNode
    {
        private readonly List<IMediaEntityNode> _children = new();
        public IList<IMediaEntityNode> Children => _children;
        public Matrix3x2 Transform = Matrix3x2.Identity;
        public void Render(TransportControl transport, CanvasDrawingSession canvas, AudioFrameInputNode audio)
        {
            var oldTransform = canvas.Transform;
            canvas.Transform *= Transform;
            foreach (IMediaEntityNode child in Children)
            {
                child.Render(transport, canvas, audio);
            }
            canvas.Transform = oldTransform;
        }
    }
}
