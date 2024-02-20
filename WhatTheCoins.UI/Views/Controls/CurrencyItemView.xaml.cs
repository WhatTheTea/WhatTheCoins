using System.Reactive.Disposables;
using ReactiveUI;

namespace WhatTheCoins.UI.Views.Controls;

public partial class CurrencyItemView
{
    public CurrencyItemView()
    {
        InitializeComponent();
        this.WhenActivated(disposable =>
        {
            this.OneWayBind(ViewModel,
                    viewModel => viewModel.Id,
                    view => view.CurrencyId.Text)
                .DisposeWith(disposable);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.Price,
                    view => view.CurrencyPrice.Text)
                .DisposeWith(disposable);

            this.BindCommand(ViewModel,
                    viewModel => viewModel.OpenCurrencyInfo,
                    view => view.CurrencyMarket)
                .DisposeWith(disposable);
        });
    }
}