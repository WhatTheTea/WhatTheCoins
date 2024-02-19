﻿using System.Reactive.Disposables;
using System.Windows.Controls;
using ReactiveUI;

namespace WhatTheCoins.UI.Views;

public partial class CurrencyView
{
    public CurrencyView()
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
                    viewModel => viewModel.OpenPage,
                    view => view.CurrencyMarket)
                .DisposeWith(disposable);
        });
    }
}