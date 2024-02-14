using System.Collections.Immutable;
using System.Text.Json;
using WhatTheCoins.API.DTO.CoinGecko;

namespace WhatTheCoins.API;

public class GeckoCurrencyService(HttpClient httpClient) : ICurrencyService
{
    private const string CurrencyDataRequest = "https://api.coingecko.com/api/v3/coins/{0}";
    private const string OHCLDataRequest = "https://api.coingecko.com/api/v3/coins/{0}/ohlc?vs_currency={1}&days={2}";

    private static readonly IReadOnlyCollection<string> MarketPlacesURL = new HashSet<string>
    {
        "https://www.coingecko.com/en/coins/{0}",
        "https://coincap.io/assets/{0}"
    };
    private static ImmutableArray<string> BuildMarketPlaces(string id)
    {
        var marketPlaces = MarketPlacesURL.Select(s => string.Format(s, id))
            .ToImmutableArray();
        return marketPlaces;
    }

    private async Task<CoinGeckoDTO> GetGeckoDTOById(string id)
    {
        var response = await httpClient.GetAsync(string.Format(CurrencyDataRequest, id));
        var rawJson = await response.Content.ReadAsStringAsync();
        var dto = JsonDocument.Parse(rawJson).Deserialize<CoinGeckoDTO>();
        return dto ?? throw new InvalidOperationException("JSON deserialized into null");
    }


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
        var dto = await GetGeckoDTOById(id);
        var currency = new Currency(dto.Id,
            dto.Symbol,
            dto.MarketData.TotalVolume["usd"],
            dto.MarketData.PriceChange24h ?? 0,
            dto.MarketData.CurrentPrice.ToImmutableDictionary(),
            BuildMarketPlaces(dto.Id)
        );
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