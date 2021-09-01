using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EffectusReunion.VirtualMediaObjectModel;
using Windows.Storage;
using Windows.Media;
using Windows.Media.MediaProperties;
using Windows.Media.Core;
using Windows.Media.Transcoding;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.DirectX.Direct3D11;
using Microsoft.Graphics.Canvas;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.DirectX;

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
        public VirtualTransportControl TransportControl = new VirtualTransportControl();
        public int frame;
        public RenderTargetBitmap RenderTargetBitmap;
        public async Task RenderToFile(IStorageFile file)
        {
            VideoEncodingProperties sourceProp = VideoEncodingProperties.CreateUncompressed(MediaEncodingSubtypes.Bgra8, 1280, 720);
            sourceProp.FrameRate.Numerator = 60;
            sourceProp.FrameRate.Denominator = 1;
            RenderTargetBitmap = new();
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
            IBuffer buffer = null;
            RootNode.VisualNode.Dispatcher.RunAsync(
                Windows.UI.Core.CoreDispatcherPriority.High,
                async () =>
                {
                    RootNode.Update(TransportControl);
                    await RenderTargetBitmap.RenderAsync(RootNode.VisualNode);
                    buffer = await RenderTargetBitmap.GetPixelsAsync();
                }
            ).AsTask().Wait();
            using (var cbp = CanvasBitmap.CreateFromBytes(CanvasDevice.GetSharedDevice(), buffer, 1280, 720, DirectXPixelFormat.B8G8R8A8UIntNormalized))
                args.Request.Sample = MediaStreamSample.CreateFromDirect3D11Surface(cbp, TransportControl.Time);
        }
    }
}
