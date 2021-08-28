using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace EffectusReunion
{
    public sealed partial class ScalableSymbolIcon : ContentControl
    {
        public ScalableSymbolIcon()
        {
            DefaultStyleKey = typeof(ScalableSymbolIcon);
        }

        public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(
            "Symbol", typeof(Symbol), typeof(ScalableSymbolIcon),
            new PropertyMetadata(Symbol.Page)
        );

        public Symbol Symbol
        {
            get => (Symbol)GetValue(SymbolProperty);
            set => SetValue(SymbolProperty, value);
        }
    }
}
