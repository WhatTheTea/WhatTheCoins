using System.Reactive.Disposables;
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
            this.OneWayBind(ViewModel,
                    vm => vm.ComplexName,
                    v => v.CurrencyId.Text)
                .DisposeWith(disposable);
            this.BindCommand(ViewModel,
                    vm => vm.GoBack,
                    v => v.BackButton)
                .DisposeWith(disposable);
            this.OneWayBind(ViewModel,
                    vm => vm.Price,
                    v => v.CurrencyPrice.Text)
                .DisposeWith(disposable);
            this.OneWayBind(ViewModel,
                    vm => vm.PriceChange,
                    v => v.CurrencyPriceChange.Text)
                .DisposeWith(disposable);
            this.OneWayBind(ViewModel,
                    vm => vm.Volume,
                    v => v.CurrencyVolume.Text)
                .DisposeWith(disposable);
        });
    }
}