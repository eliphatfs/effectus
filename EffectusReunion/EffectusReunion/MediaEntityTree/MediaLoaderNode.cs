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
using Windows.Media;

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
            agr.Graph.Stop();
            node.audioSubgraph = agr.Graph;
            var finr = await node.audioSubgraph.CreateFileInputNodeAsync(file);
            if (finr.Status != AudioFileNodeCreationStatus.Success)
                throw new MediaEntityException(finr.Status.ToString(), finr.ExtendedError);
            node.audioLoader = finr.FileInputNode;
            node.audioReader = node.audioSubgraph.CreateFrameOutputNode();
            node.audioLoader.AddOutgoingConnection(node.audioReader);
            node.videoLoader = new();
            node.videoLoader.Source = MediaSource.CreateFromStorageFile(file);
            node.videoLoader.IsVideoFrameServerEnabled = true;
            node.audioSubgraph.QuantumStarted += node.AudioSubgraph_QuantumStarted;
            node.videoLoader.VideoFrameAvailable += node.VideoLoader_VideoFrameAvailable;
            node.videoLoader.PlaybackSession.PositionChanged += node.PlaybackSession_PositionChanged;
            node.videoLoader.IsMuted = true;
            return node;
        }

        protected TimeSpan videoTimeline = TimeSpan.MaxValue;
        protected TimeSpan alignedVideoTimeline = TimeSpan.MaxValue;
        protected TaskCompletionSource<bool> videoSeekDispatcher = new();
        protected TaskCompletionSource<bool> videoDispatcher = new();
        protected CanvasRenderTarget videoSink = null;

        private void PlaybackSession_PositionChanged(MediaPlaybackSession sender, object args)
        {
            alignedVideoTimeline = sender.Position;
            videoSeekDispatcher.TrySetResult(true);
        }
        private void VideoLoader_VideoFrameAvailable(MediaPlayer sender, object args)
        {
            if (videoSink != null)
            {
                sender.CopyFrameToVideoSurface(
                    videoSink,
                    new(0, 0, videoSink.SizeInPixels.Width, videoSink.SizeInPixels.Height)
                );
                videoDispatcher.TrySetResult(true);
            }
        }

        protected TaskCompletionSource<bool> audioDispatcher = new();
        protected TimeSpan audioTimeline = TimeSpan.MaxValue;
        protected TimeSpan requestedTimeline;
        protected AudioFrameInputNode audioSink;
        private void AudioSubgraph_QuantumStarted(AudioGraph sender, object args)
        {
            if (!audioDispatcher.Task.IsCompleted)
            {
                var frame = audioReader.GetFrame();
                audioSink.AddFrame(frame);
                audioTimeline += frame.Duration.Value;
                if (audioTimeline >= requestedTimeline + TimeSpan.FromMilliseconds(120))
                {
                    audioDispatcher.SetResult(true);
                    audioSubgraph.Stop();
                }
            }

        }

        public void Render(TransportControl transport, CanvasDrawingSession canvas, AudioFrameInputNode audio)
        {
            requestedTimeline = transport.Time;
            audioSink = audio;
            if (videoSink == null
                || videoSink.SizeInPixels.Width != (int)(0.5 + LocalRenderRect.Width)
                || videoSink.SizeInPixels.Height != (int)(0.5 + LocalRenderRect.Height)
            )
            {
                if (videoSink != null)
                    videoSink.Dispose();
                videoSink = new(CanvasDevice.GetSharedDevice(), (float)LocalRenderRect.Width, (float)LocalRenderRect.Height, 96);
            }
            if (!transport.IsPlaying)
            {
                audioTimeline = TimeSpan.MaxValue;
                if (videoTimeline != transport.Time)
                {
                    videoLoader.PlaybackSession.Position = transport.Time;
                    videoTimeline = transport.Time;
                }
                canvas.DrawImage(videoSink, LocalRenderRect);
            }
            else
            {
                if (audioTimeline == TimeSpan.MaxValue)
                {
                    audioLoader.Seek(audioTimeline = requestedTimeline);
                    audioReader.GetFrame();
                }
                audioDispatcher = new();
                audioSubgraph.Start();
                if (alignedVideoTimeline >= transport.Time || alignedVideoTimeline < transport.Time - TimeSpan.FromMilliseconds(100))
                {
                    videoSeekDispatcher = new();
                    videoLoader.PlaybackSession.Position = transport.Time;
                    videoSeekDispatcher.Task.Wait();
                    videoDispatcher = new();
                }
                while (alignedVideoTimeline < transport.Time)
                {
                    videoSeekDispatcher = new();
                    videoLoader.StepForwardOneFrame();
                    videoSeekDispatcher.Task.Wait();
                    videoDispatcher = new();
                }
                videoDispatcher.Task.Wait();
                canvas.DrawImage(videoSink, LocalRenderRect);
                audioDispatcher.Task.Wait();
            }
        }
    }
}
