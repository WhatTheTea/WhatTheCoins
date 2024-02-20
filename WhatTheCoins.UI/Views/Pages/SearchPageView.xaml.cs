using System.Reactive.Disposables;
using System.Windows.Controls;
using ReactiveUI;
using Splat;
using WhatTheCoins.UI.ViewModels.Pages;

namespace WhatTheCoins.UI.Views.Pages;

public partial class SearchPageView
{
    public SearchPageView()
    {
        InitializeComponent();
        ViewModel ??= Locator.Current.GetService<SearchPageViewModel>();
        this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel,
                        x => x.Router,
                        x => x.ViewHost.Router)
                    .DisposeWith(disposable);
            }
            );
    }
}