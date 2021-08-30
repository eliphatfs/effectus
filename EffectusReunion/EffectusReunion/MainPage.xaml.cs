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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EffectusReunion
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MediaTimelineController controller;
        public MainPage()
        {
            this.InitializeComponent();
            CompositionTarget.Rendering += CompositionTarget_Rendered;
        }

        private void CompositionTarget_Rendered(object sender, object e)
        {
            timeLabels.Text = $"{video1.MediaPlayer.PlaybackSession.Position.TotalMilliseconds}\n{video2.MediaPlayer.PlaybackSession.Position.TotalMilliseconds}";
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            if (controller == null)
            {
                controller = new MediaTimelineController();
                video1.SetMediaPlayer(new Windows.Media.Playback.MediaPlayer());
                video2.SetMediaPlayer(new Windows.Media.Playback.MediaPlayer());
                video1.MediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Metal_Wind_Chimes_at_Sunset.mp4"));
                video2.MediaPlayer.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Metal_Wind_Chimes_at_Sunset.mp4"));
                video1.MediaPlayer.CommandManager.IsEnabled = false;
                video2.MediaPlayer.CommandManager.IsEnabled = false;
                video1.MediaPlayer.TimelineController = controller;
                video2.MediaPlayer.TimelineController = controller;
                controller.Start();
            }
            else
                controller.Resume();
        }

        private void RandomJump_Click(object sender, RoutedEventArgs e)
        {
            controller.Position = TimeSpan.FromSeconds(new Random().NextDouble() * 15);
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            controller.Pause();
        }

        private void Step_Click(object sender, RoutedEventArgs e)
        {
            var old = controller.Position;
            controller.Position = TimeSpan.Zero;
            controller.Position = old + TimeSpan.FromMilliseconds(10);
        }
    }
}
