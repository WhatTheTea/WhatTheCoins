using System.Reactive.Disposables;
using ReactiveUI;
using WhatTheCoins.UI.ViewModels;

namespace WhatTheCoins.UI.Views;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        ViewModel = new AppViewModel();

        Loaded += (_, _) => Wpf.Ui.Appearance.SystemThemeWatcher.Watch(this);
    }
}