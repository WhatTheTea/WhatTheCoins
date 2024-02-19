using System.Reactive.Disposables;
using System.Windows.Interop;
using ReactiveUI;
using Splat;
using WhatTheCoins.UI.ViewModels;
using WhatTheCoins.UI.Views.Pages;
using Wpf.Ui.Controls;

namespace WhatTheCoins.UI.Views;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += (_, _) =>
        {
            Wpf.Ui.Appearance.SystemThemeWatcher.Watch(this, WindowBackdropType.Auto);
            MainMenuNavigationView.Navigate(typeof(MainPage));
        };
    }
}