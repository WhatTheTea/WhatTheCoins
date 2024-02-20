using System.Reactive.Disposables;
using ReactiveUI;
using Splat;
using WhatTheCoins.UI.ViewModels;

namespace WhatTheCoins.UI.Views.Pages;

public partial class Top10Page
{
    public Top10Page()
    {
        InitializeComponent();
        ViewModel = Locator.Current.GetService<TopPageViewModel>()!;

        this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel,
                        vm => vm.TopCurrencies,
                        page => page.TopListBox.ItemsSource)
                    .DisposeWith(disposable);
            }
        );
    }
}