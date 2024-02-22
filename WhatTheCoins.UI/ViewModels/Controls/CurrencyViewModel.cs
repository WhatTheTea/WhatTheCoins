using ReactiveUI;
using WhatTheCoins.API;

namespace WhatTheCoins.UI.ViewModels.Controls;

public class CurrencyViewModel(Currency currency, IEnumerable<Candle> candles) : ReactiveObject
{
    public IEnumerable<string> MarketUrls => currency.MarketPlaces;
    public string Id => currency.Id;
    public string Price => $"Price: ${currency.SymbolToPrice["usd"]:N}";
    public string Volume => $"Volume: ${currency.Volume:N0}";
    public string PriceChange => $"Change: {currency.PriceChange:F2}";
    public Dictionary<string, double> Exchanges => currency.SymbolToPrice.ToDictionary();
    public string Symbol => currency.Symbol;
    public string ComplexName => $"{currency.Id} [{currency.Symbol}]";
}