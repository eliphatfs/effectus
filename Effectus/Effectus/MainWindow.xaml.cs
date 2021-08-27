using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Effectus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        internal readonly Storyboard storyboard = new();
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            storyboard.SlipBehavior = SlipBehavior.Slip;
            MediaTimeline video1 = new(new Uri("Metal_Wind_Chimes_at_Sunset.mp4", UriKind.Relative));
            video1.BeginTime = TimeSpan.FromSeconds(3);
            MediaTimeline video2 = new(new Uri("Metal_Wind_Chimes_at_Sunset.mp4", UriKind.Relative));
            video2.BeginTime = TimeSpan.FromSeconds(3);
            video1.CurrentTimeInvalidated += Storyboard_CurrentTimeInvalidated;
            video2.CurrentTimeInvalidated += Storyboard_CurrentTimeInvalidated;
            Storyboard.SetTarget(video1, testVideo1);
            Storyboard.SetTarget(video2, testVideo2);
            storyboard.Children.Add(video1);
            storyboard.Children.Add(video2);
            storyboard.Begin(this, true);
        }

        private void Storyboard_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            videoTimesLabel.Content = $"{testVideo1.Position.TotalMilliseconds:0.0}\n{testVideo2.Position.TotalMilliseconds:0.0}";
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RandomJump_Click(object sender, RoutedEventArgs e)
        {
            storyboard.SeekAlignedToLastTick(
                this, TimeSpan.FromSeconds(new Random().NextDouble() * 15),
                TimeSeekOrigin.BeginTime
            );
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            storyboard.Resume(this);
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            storyboard.Pause(this);
            Storyboard_CurrentTimeInvalidated(sender, e);
        }

        private void Step_Click(object sender, RoutedEventArgs e)
        {
            storyboard.SeekAlignedToLastTick(
                this, storyboard.GetCurrentTime(this).Value + TimeSpan.FromMilliseconds(10),
                TimeSeekOrigin.BeginTime
            );
            Storyboard_CurrentTimeInvalidated(sender, e);
        }
    }
}
