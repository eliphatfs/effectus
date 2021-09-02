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
using Windows.Storage.Pickers;

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

        private async void Render_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".mp4");
            var ag = await AudioGraph.CreateAsync(new(Windows.Media.Render.AudioRenderCategory.Media));
            var af = await ag.Graph.CreateFileInputNodeAsync(await picker.PickSingleFileAsync());
            // var media = MediaSource.CreateFromStorageFile(await picker.PickSingleFileAsync());
            await System.Threading.Tasks.Task.CompletedTask;
        }

        private void Renderer_OnProgress(float obj)
        {
            _ = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                progress.Value = obj;
            });
        }
    }
}
