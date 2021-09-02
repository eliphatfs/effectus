using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EffectusReunion.VirtualMediaObjectModel;
using Windows.Storage;
using Windows.Media.MediaProperties;
using Windows.Media.Core;
using Windows.Media.Transcoding;
using Windows.UI;
using Microsoft.Graphics.Canvas;
using Windows.Media.Audio;
using EffectusReunion.RenderingLayer;

namespace EffectusReunion.VirtualTransport
{
    public class VirtualTransportVideoRender
    {
        public VirtualMediaContainerNode RootNode
        {
            get;
            protected set;
        }
        public VirtualTransportVideoRender(VirtualMediaContainerNode rootNode)
        {
            RootNode = rootNode;
        }
        public event Action<float> OnProgress;
        public VirtualTransportControl TransportControl = new();
        public int frame;
        public async Task RenderToFile(IStorageFile file)
        {
            VideoEncodingProperties sourceProp = VideoEncodingProperties.CreateUncompressed(MediaEncodingSubtypes.Bgra8, 1280, 720);
            sourceProp.FrameRate.Numerator = 60;
            sourceProp.FrameRate.Denominator = 1;
            sourceProp.Bitrate = 60 * 1280 * 720 * 4;
            MediaStreamSource mediaStreamSource = new(new VideoStreamDescriptor(sourceProp));
            mediaStreamSource.SampleRequested += MediaStreamSource_SampleRequested;
            mediaStreamSource.Duration = TimeSpan.FromSeconds(50);
            MediaEncodingProfile mediaEncodingProfile = MediaEncodingProfile.CreateMp4(VideoEncodingQuality.HD720p);
            var prepareOp = await new MediaTranscoder().PrepareMediaStreamSourceTranscodeAsync(
                mediaStreamSource,
                await file.OpenAsync(FileAccessMode.ReadWrite),
                mediaEncodingProfile
            );
            var transCodeOp = prepareOp.TranscodeAsync();
            transCodeOp.Progress += (asyncInfo, percent) => OnProgress?.Invoke((float)percent);
        }

        private void MediaStreamSource_SampleRequested(MediaStreamSource sender, MediaStreamSourceSampleRequestedEventArgs args)
        {
            frame++;
            TransportControl.Time = TimeSpan.FromSeconds(frame / 60.0);
            if (TransportControl.Time > TimeSpan.FromSeconds(50))
                return;
            RootNode.Update(TransportControl);
            using CanvasRenderTarget renderer = new(CanvasDevice.GetSharedDevice(), 1280, 720, 96);
            using CanvasDrawingSession session = renderer.CreateDrawingSession();
            session.Clear(Color.FromArgb(255, 251, 251, 254));
            RenderAll(session, RootNode);
            args.Request.Sample = MediaStreamSample.CreateFromDirect3D11Surface(renderer, TransportControl.Time);
        }

        private void RenderAll(CanvasDrawingSession s, IVirtualMediaNode<Renderer, IAudioInputNode> root)
        {
            if (root is VirtualMediaContainerNode c)
            {
                foreach (var child in c.Children)
                    RenderAll(s, child);
            }
            root.VisualNode.Render(s);
        }
    }
}
