using System.Collections.Immutable;
using System.Text.Json;
using WhatTheCoins.API.DTO.CoinGecko;

namespace WhatTheCoins.API;

public class GeckoCurrencyService(HttpClient httpClient) : ICurrencyService
{
    private const string CurrencyDataRequest = "https://api.coingecko.com/api/v3/coins/{0}";

    private static readonly IReadOnlyCollection<string> MarketPlaceURLs = new HashSet<string>
    {
        "https://www.coingecko.com/en/coins/{0}",
        "https://coincap.io/assets/{0}"
    };

    private async Task<CoinGeckoDTO> GetGeckoDTOById(string id)
    {
        var response = await httpClient.GetAsync(string.Format(CurrencyDataRequest, id));
        var rawJson = await response.Content.ReadAsStringAsync();
        var dto = JsonDocument.Parse(rawJson).Deserialize<CoinGeckoDTO>();
        return dto ?? throw new InvalidOperationException("JSON deserialized into null");
    }

    private static IImmutableList<string> BuildMarketPlaces(string id)
    {
        var marketPlaces = MarketPlaceURLs.Select(s => string.Format(s, id))
                                                            .ToImmutableArray();
        return marketPlaces;
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

    public async Task<Currency> GetByCodeAsync(string code)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Currency>> GetTop10Async()
    {
        throw new NotImplementedException();
    }

    public async Task<Currency> GetExchangeRateAsync(Currency with)
    {
        throw new NotImplementedException();
    }
}