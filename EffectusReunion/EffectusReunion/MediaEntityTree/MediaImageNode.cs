using EffectusReunion.MediaTransportSystem;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.Audio;

namespace EffectusReunion.MediaEntityTree
{
    public class MediaImageNode : IMediaEntityNode
    {
        public CanvasBitmap Image;
        public Rect LocalRenderRect
        {
            get; set;
        }
        public void Render(TransportControl transport, CanvasDrawingSession canvas, AudioFrameInputNode audio)
        {
            canvas.DrawImage(Image, LocalRenderRect);
        }
    }
}
