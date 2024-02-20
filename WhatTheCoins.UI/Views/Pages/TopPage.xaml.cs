using System.Reactive.Disposables;
using ReactiveUI;
using Splat;
using WhatTheCoins.UI.ViewModels.Pages;

namespace WhatTheCoins.UI.Views.Pages;

public partial class TopPage
{
    public TopPage()
    {
        InitializeComponent();
        ViewModel = Locator.Current.GetService<TopPageViewModel>()!;

        this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel,
                        vm => vm.Router,
                        page => page.ViewHost.Router)
                    .DisposeWith(disposable);
            }
        );
    }
}