using System.Reactive.Disposables;
using System.Windows.Controls;
using ReactiveUI;

namespace WhatTheCoins.UI.Views.Controls;

public partial class CurrencyInfoView
{
    public CurrencyInfoView()
    {
        InitializeComponent();

        this.WhenActivated(disposable =>
        {
            this.OneWayBind(ViewModel, 
                    vm => vm.Candlesticks, 
                    view => view.CandlesChart.Series)
                .DisposeWith(disposable);
            this.OneWayBind(ViewModel, 
                    vm => vm.XAxes, 
                    view => view.CandlesChart.XAxes)
                .DisposeWith(disposable);
        });
    }
}