﻿using System.Diagnostics;
using System.Reactive;
using ReactiveUI;
using WhatTheCoins.API;

namespace WhatTheCoins.UI.ViewModels;

public class CurrencyViewModel : ReactiveObject
{
    private readonly Currency _currency;

    public Uri MarketUrl => new(_currency.MarketPlaces[0]);
    public string Id => _currency.Id;
    public double Price => _currency.SymbolToPrice["usd"];
    
    public ReactiveCommand<Unit, Unit> OpenPage { get; }
    
    public CurrencyViewModel(Currency currency)
    {
        _currency = currency;
        OpenPage = ReactiveCommand.Create(
            () =>
            {
                Process.Start(
                    new ProcessStartInfo(this.MarketUrl.ToString())
                    {
                        UseShellExecute = true
                    });
            });
    }
    
}