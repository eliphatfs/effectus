using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ControlzEx.Theming;

namespace Effectus
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var theme = RuntimeThemeGenerator.Current.GenerateRuntimeTheme("Light", Color.FromRgb(0xd6, 0xcf, 0xe2));
            ThemeManager.Current.AddTheme(theme);
            ThemeManager.Current.ChangeTheme(Current, theme);
            base.OnStartup(e);
        }
    }
}
