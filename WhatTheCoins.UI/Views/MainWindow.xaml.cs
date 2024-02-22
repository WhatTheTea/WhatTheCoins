using System.Windows;
using ReactiveUI;
using WhatTheCoins.UI.Views.Pages;
using Wpf.Ui.Appearance;
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
            SystemThemeWatcher.Watch(this, WindowBackdropType.Auto);
            MainMenuNavigationView.Navigate(typeof(MainPage));
        };
    }
}