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
    public partial class SimpleSubWindow : Grid
    {
        public string Title { set; get; }
        public MahApps.Metro.IconPacks.PackIconBootstrapIconsKind Icon { get; set; }
        public Color WindowColor { set; get; } = Color.FromRgb(0xed, 0xe8, 0xef);
        public SimpleSubWindow()
        {
            // Colors: inactive e6e7ed ede8ef active 705697 title d6cfe2
            InitializeComponent();
        }
    }
}
