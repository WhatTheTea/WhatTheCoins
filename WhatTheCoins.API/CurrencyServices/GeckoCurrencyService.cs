using System.Collections.Immutable;
using System.Text.Json;
using WhatTheCoins.API.CurrencyFactories;
using WhatTheCoins.API.DTO.CoinGecko;
using WhatTheCoins.API.CandlesFactories;

namespace WhatTheCoins.API;

public class GeckoCurrencyService(HttpClient httpClient) : ICurrencyService
{
    private readonly ICandlesFactory _candlesFactory = new GeckoCandlesFactory(httpClient);
    private readonly ICurrencyFactory _currencyFactory = new GeckoCurrencyFactory(httpClient);
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

    public Task<string?> SearchAsync(string query)
    {
        throw new NotImplementedException();
    }

    public async Task<IImmutableList<Currency>> GetTop10Async()
    {
        throw new NotImplementedException();
    }
}