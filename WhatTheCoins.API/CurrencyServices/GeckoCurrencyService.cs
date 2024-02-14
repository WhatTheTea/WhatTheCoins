using System.Collections.Immutable;
using System.Text.Json;

namespace WhatTheCoins.API.CurrencyServices;

public sealed class GeckoCurrencyService(HttpClient httpClient) : ICurrencyService
{
    private readonly ICandlesFactory _candlesFactory = new CandlesFactories.GeckoCandlesFactory(httpClient);
    private readonly ICurrencyFactory _currencyFactory = new CurrencyFactories.GeckoCurrencyFactory(httpClient);
    private const string SearchRequestURL = "https://api.coingecko.com/api/v3/search?query={0}";
    public async Task<IImmutableList<Candle>> GetCandles(string id)
    {
        var candles = await _candlesFactory.MakeCandles(id);
        return candles;
    }

    public async Task<Currency> GetByIdAsync(string id)
    {
        var currency = await _currencyFactory.MakeCurrency(id);
        return currency;
    }

    public async Task<string?> SearchAsync(string query)
    {
        var request = await httpClient.GetAsync(string.Format(SearchRequestURL, query));
        var rawJSON = await request.Content.ReadAsStringAsync();
        var dto = JsonDocument.Parse(rawJSON).Deserialize<DTO.CoinGecko.Search.DTO>();
        return dto?.Coins[0].Id;
    }

    public async Task<IImmutableList<Currency>> GetTop10Async()
    {
        throw new NotImplementedException();
    }
}