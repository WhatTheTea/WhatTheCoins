using System.Collections.Immutable;
using System.Text.Json;
using WhatTheCoins.API.CandlesFactories;
using WhatTheCoins.API.CurrencyFactories;

namespace WhatTheCoins.API.CurrencyServices;

public sealed class GeckoCurrencyService : CurrencyServiceBase
{
    private readonly HttpClient _httpClient;

    public GeckoCurrencyService(HttpClient httpClient) : base(httpClient,
        new GeckoCurrencyFactory(httpClient),
        new GeckoCandlesFactory(httpClient))
    {
        _httpClient = httpClient;
        SearchRequestURL = "https://api.coingecko.com/api/v3/search?query={0}";
    }

    public override async Task<IImmutableList<Candle>> GetCandles(string id)
    {
        var candles = await CandlesFactory.MakeCandles(id);
        return candles;
    }

    public override async Task<Currency> GetByIdAsync(string id)
    {
        var currency = await CurrencyFactory.MakeCurrency(id);
        return currency;
    }

    public override async Task<string?> SearchAsync(string query)
    {
        var dto = await GetDTO<DTO.CoinGecko.Search.DTO>(string.Format(SearchRequestURL, query));
        return dto?.Coins[0].Id;
    }
    public override async Task<IImmutableList<Currency>> GetTop10Async()
    {
        throw new NotImplementedException();
    }
}