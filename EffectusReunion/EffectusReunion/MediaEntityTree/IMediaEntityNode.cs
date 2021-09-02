using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.UI.Xaml;
using EffectusReunion.MediaTransportSystem;
using Microsoft.Graphics.Canvas;

namespace EffectusReunion.MediaEntityTree
{
    /// <summary>
    /// Base class for all descriptors and logical nodes.
    /// Produces audio and video entity nodes.
    /// </summary>
    public interface IMediaEntityNode
    {
        public void Render(TransportControl transport, CanvasDrawingSession canvas, AudioFrameInputNode audio);
    }
}
