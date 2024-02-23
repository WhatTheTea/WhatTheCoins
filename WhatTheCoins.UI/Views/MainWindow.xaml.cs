using System.Windows;
using ReactiveUI;
using WhatTheCoins.UI.ViewModels;
using WhatTheCoins.UI.Views.Pages;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using ReactiveUI.Wpf;
using Splat;

namespace WhatTheCoins.UI.Views;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private ISnackbarService _snackbarService = Locator.Current.GetService<ISnackbarService>();
    public MainWindow() 
    {
        InitializeComponent();
        _snackbarService.SetSnackbarPresenter(this.SnackbarPresenter);
        Loaded += (_, _) =>
        {
            SystemThemeWatcher.Watch(this, WindowBackdropType.Auto);
            MainMenuNavigationView.Navigate(typeof(MainPage));
        };
    }

}