using System.Diagnostics;
using System.Reactive;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using ReactiveUI;
using WhatTheCoins.API;

namespace WhatTheCoins.UI.ViewModels.Controls;

public class CurrencyViewModel(Currency currency, IEnumerable<Candle> candles) : ReactiveObject
{
    public Uri[] MarketUrls => currency.MarketPlaces.Select(s => new Uri(s)).ToArray();
    public string Id => currency.Id;
    public double Price => currency.SymbolToPrice["usd"];
    
    
}