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
    public async Task<Currency?> SearchAsync(string query)
    {
        var foundId = await ApiProvider.SearchAsync(query);
        // TODO: own exceptions
        if (string.IsNullOrWhiteSpace(foundId)) throw new Exception("Currency Id not found"); 
        return await ApiProvider.GetByIdAsync(foundId);
    }

    public async Task<IImmutableList<Currency>> GetTop10Async()
    {
        var ids = await ApiProvider.GetTop10Async();
        var currencies = await Task.WhenAll(ids.Select(async id => await GetByIdAsync(id)));
        return currencies.ToImmutableArray();
    }
}