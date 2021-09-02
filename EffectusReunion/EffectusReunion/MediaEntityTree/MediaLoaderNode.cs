using EffectusReunion.MediaTransportSystem;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.Foundation;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Media.Render;
using Windows.Storage;

namespace EffectusReunion.MediaEntityTree
{
    public class MediaLoaderNode : IMediaEntityNode
    {
        public Rect LocalRenderRect
        {
            get; set;
        }
        protected MediaPlayer videoLoader;
        protected AudioGraph audioSubgraph;
        protected AudioFileInputNode audioLoader;
        protected AudioFrameOutputNode audioReader;
        public IStorageFile SourceFile
        {
            get; private set;
        }
        protected MediaLoaderNode(IStorageFile file)
        {
            SourceFile = file;
        }
        public static async Task<MediaLoaderNode> CreateFromFileAsync(IStorageFile file)
        {
            MediaLoaderNode node = new(file);
            var agr = await AudioGraph.CreateAsync(new(AudioRenderCategory.Media));
            if (agr.Status != AudioGraphCreationStatus.Success)
                throw new MediaEntityException(agr.Status.ToString(), agr.ExtendedError);
            node.audioSubgraph = agr.Graph;
            var finr = await node.audioSubgraph.CreateFileInputNodeAsync(file);
            if (finr.Status != AudioFileNodeCreationStatus.Success)
                throw new MediaEntityException(finr.Status.ToString(), finr.ExtendedError);
            node.audioLoader = finr.FileInputNode;
            node.audioReader = node.audioSubgraph.CreateFrameOutputNode();
            node.audioLoader.AddOutgoingConnection(node.audioReader);
            node.videoLoader = new();
            node.videoLoader.Source = MediaSource.CreateFromStorageFile(file);
            return node;
        }
        public void Render(TransportControl transport, CanvasDrawingSession canvas, AudioFrameInputNode audio)
        {
            // canvas.DrawImage(null, LocalRenderRect);
        }
    }
}
