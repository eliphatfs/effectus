using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace EffectusReunion.RenderingLayer
{
    public class TextRenderer : Renderer
    {
        public string Text;
        public override void Render(CanvasDrawingSession session)
        {
            base.Render(session);
            session.DrawText(Text, new Vector2(500, 300), Windows.UI.Color.FromArgb(255, 30, 30, 30));
        }
    }
}
