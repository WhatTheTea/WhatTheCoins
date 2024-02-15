using System.Collections.Immutable;

namespace WhatTheCoins.API;

public class CurrencyService(IApiProvider apiProvider) : ICurrencyService
{
    private IApiProvider ApiProvider
    {
        get => apiProvider;
        set => ChangeApiProvider(value);
    }
    public void ChangeApiProvider(IApiProvider provider)
    {
        apiProvider = provider;
    }

    public Task<IImmutableList<Candle>> GetCandles(string id, int days = 7, string referenceCurrency = "usd") =>
        ApiProvider.GetCandles(id, days, referenceCurrency);
    public Task<Currency> GetByIdAsync(string id) => ApiProvider.GetByIdAsync(id);
    public Task<string?> SearchAsync(string query) => ApiProvider.SearchAsync(query);
    public Task<IImmutableList<Currency>> GetTop10Async() => ApiProvider.GetTop10Async();
}