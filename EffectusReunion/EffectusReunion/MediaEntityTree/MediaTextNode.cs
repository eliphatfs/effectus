using EffectusReunion.MediaTransportSystem;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.Audio;
using Windows.UI;

namespace EffectusReunion.MediaEntityTree
{
    public class MediaTextNode : IMediaEntityNode
    {
        public string Text = "";
        public Color Foreground = Colors.WhiteSmoke;
        public CanvasTextFormat TextFormat = new();
        public Vector2 LocalPosition;
        public void Render(TransportControl transport, CanvasDrawingSession canvas, AudioFrameInputNode audio)
        {
            canvas.DrawText(Text, LocalPosition, Foreground, TextFormat);
        }
    }
}
