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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Effectus
{
    /// <summary>
    /// Interaction logic for SimpleSubWindow.xaml
    /// </summary>
    public partial class SimpleSubWindow
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
            "Title", typeof(string),
            typeof(SimpleSubWindow)
        );
        public string Title {
            set => SetValue(TitleProperty, value);
            get => (string)GetValue(TitleProperty);
        }
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(
            "Icon", typeof(MahApps.Metro.IconPacks.PackIconBootstrapIconsKind),
            typeof(SimpleSubWindow)
        );
        public MahApps.Metro.IconPacks.PackIconBootstrapIconsKind Icon {
            get => (MahApps.Metro.IconPacks.PackIconBootstrapIconsKind)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        public static readonly DependencyProperty WindowColorProperty =
            DependencyProperty.Register(
            "WindowColor", typeof(Brush),
            typeof(SimpleSubWindow),
            new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0xe6, 0xe7, 0xed)))
        );
        public Brush WindowColor {
            set => SetValue(WindowColorProperty, value);
            get => (Brush)GetValue(WindowColorProperty);
        }
        public SimpleSubWindow()
        {
            // Colors: inactive e6e7ed ede8ef active 705697 title d6cfe2
            InitializeComponent();
        }
    }
}
