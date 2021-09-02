using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Microsoft.Graphics.Canvas;

namespace EffectusReunion.RenderingLayer
{
    public class Renderer
    {
        public Renderer Parent;
        public Matrix4x4 LocalTransform;
        public Matrix4x4 InheritedTransform => Parent?.GlobalTransform ?? Matrix4x4.Identity;
        public Matrix4x4 GlobalTransform => InheritedTransform * LocalTransform;
        public virtual void Render(CanvasDrawingSession session)
        {
        }
    }
}
