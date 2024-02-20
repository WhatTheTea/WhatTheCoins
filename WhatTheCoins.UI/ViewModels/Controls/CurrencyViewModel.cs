using ReactiveUI;
using WhatTheCoins.API;

namespace WhatTheCoins.UI.ViewModels.Controls;

public class CurrencyViewModel(Currency currency, IEnumerable<Candle> candles) : ReactiveObject
{
    public Uri[] MarketUrls => currency.MarketPlaces.Select(s => new Uri(s)).ToArray();
    public string Id => currency.Id;
    public string Price => $"Price: ${currency.SymbolToPrice["usd"]:N}";
    public string Volume => $"Volume: ${currency.Volume:N0}";
    public string PriceChange => $"Change: {currency.PriceChange:F2}";
    public string Symbol => currency.Symbol;
    public string ComplexName => $"{currency.Id} [{currency.Symbol}]";
}