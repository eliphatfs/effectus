using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace EffectusReunion
{
    /// <summary>
    /// Interaction logic for SimpleSubWindow.xaml
    /// </summary>
    public partial class SimpleSubWindow : ContentControl
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
            "Title", typeof(string),
            typeof(SimpleSubWindow),
            new PropertyMetadata("")
        );
        public string Title
        {
            set => SetValue(TitleProperty, value);
            get => (string)GetValue(TitleProperty);
        }
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(
            "Icon", typeof(Symbol),
            typeof(SimpleSubWindow),
            new PropertyMetadata(Symbol.Page)
        );
        public Symbol Icon
        {
            get => (Symbol)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        public static readonly DependencyProperty WindowColorProperty =
            DependencyProperty.Register(
            "WindowColor", typeof(Brush),
            typeof(SimpleSubWindow),
            new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0xff, 0xe6, 0xe7, 0xed)))
        );
        public Brush WindowColor
        {
            set => SetValue(WindowColorProperty, value);
            get => (Brush)GetValue(WindowColorProperty);
        }
        public SimpleSubWindow() : base()
        {
            DefaultStyleKey = typeof(SimpleSubWindow);
            // Colors: inactive e6e7ed ede8ef active 705697 title d6cfe2
        }
    }
}
