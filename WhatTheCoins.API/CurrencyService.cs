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

    public Task<IImmutableList<Candle>> GetCandles(string id, int days = 7, string referenceCurrency = "usd")
    {
        return ApiProvider.GetCandles(id, days, referenceCurrency);
    }

    public Task<Currency> GetByIdAsync(string id)
    {
        return ApiProvider.GetByIdAsync(id);
    }

    public async Task<IImmutableList<Currency>> SearchAsync(string query)
    {
        var ids = await ApiProvider.SearchAsync(query);
        var tasks = ids.Take(4).Select(async id => await ApiProvider.GetByIdAsync(id));
        var currencies = await Task.WhenAll(tasks);
        return currencies.ToImmutableArray();
    }

    public async Task<IImmutableList<Currency>> GetTop10Async()
    {
        var ids = await ApiProvider.GetTop10Async();
        var currencies = await Task.WhenAll(ids.Select(async id => await GetByIdAsync(id)));
        return currencies.ToImmutableArray();
    }
}