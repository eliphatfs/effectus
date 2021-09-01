using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Audio;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EffectusReunion
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
        }

        private void RandomJump_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Step_Click(object sender, RoutedEventArgs e)
        {
        }

        public class TestTextNode : VirtualMediaObjectModel.VirtualMediaTextNode
        {
            public override void Update(VirtualTransport.VirtualTransportControl transport)
            {
                base.Update(transport);
                VisualNode.Text = transport.Time.ToString();
            }
        }

        private async void Render_Click(object sender, RoutedEventArgs e)
        {
            var testNode = new VirtualMediaObjectModel.VirtualMediaContainerNode();
            var ag = await AudioGraph.CreateAsync(new AudioGraphSettings(Windows.Media.Render.AudioRenderCategory.Media));
            var text = new TestTextNode();
            text.Initialize(ag.Graph);
            testNode.AppendChild(text);
            testNode.Initialize(ag.Graph);
            testNode.VisualNode.Width = 1280;
            testNode.VisualNode.Height = 720;
            var renderer = new VirtualTransport.VirtualTransportVideoRender(testNode);
            renderer.OnProgress += Renderer_OnProgress;
            await renderer.RenderToFile(await DownloadsFolder.CreateFileAsync("test.mp4"));
        }

        private void Renderer_OnProgress(float obj)
        {
            _ = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                progress.Minimum = 0;
                progress.Maximum = 1;
                progress.Value = obj;
            });
        }
    }
}
