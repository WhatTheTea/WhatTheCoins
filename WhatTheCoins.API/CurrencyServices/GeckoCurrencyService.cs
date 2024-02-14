using System.Collections.Immutable;
using System.Text.Json;
using WhatTheCoins.API.CurrencyFactories;
using WhatTheCoins.API.DTO.CoinGecko;
using WhatTheCoins.API.OHCLFactories;

namespace WhatTheCoins.API;

public class GeckoCurrencyService(HttpClient httpClient) : ICurrencyService
{
    private const string OHCLDataRequest = "https://api.coingecko.com/api/v3/coins/{0}/ohlc?vs_currency={1}&days={2}";
    private ICandlesFactory _candlesFactory = new GeckoCandlesFactory();
    private ICurrencyFactory _currencyFactory = new GeckoCurrencyFactory(httpClient);
    private async Task<ImmutableArray<ImmutableArray<double>>> GetOHCLData(string id)
    {
        var response = await httpClient.GetAsync(string.Format(OHCLDataRequest, id, "usd", 7));
        var rawJson = await response.Content.ReadAsStringAsync();
        var dto = JsonDocument.Parse(rawJson).Deserialize<ImmutableArray<ImmutableArray<double>>>();
        return dto;
    }

    public async Task<IImmutableList<Candle>> GetOHCL(string id)
    {
        var rawOHCL = await GetOHCLData(id);
        var candles = rawOHCL.Select(arr => 
            new Candle(
                DateTimeOffset.FromUnixTimeMilliseconds((long)arr[0]).DateTime, 
                arr[1], arr[2], arr[3], arr[4])
        ).ToImmutableArray();
        return candles;
    }

    public async Task<Currency> GetByIdAsync(string id)
    {
        var currency = await _currencyFactory.MakeCurrency(id);
        return currency;
    }

    public Task<Currency?> Search(string query)
    {
        throw new NotImplementedException();
    }

    public async Task<IImmutableList<Currency>> GetTop10Async()
    {
        throw new NotImplementedException();
    }
}