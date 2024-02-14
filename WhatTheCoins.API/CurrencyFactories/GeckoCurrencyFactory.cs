using System.Collections.Immutable;
using System.Text.Json;
using WhatTheCoins.API.DTO.CoinGecko;

namespace WhatTheCoins.API.CurrencyFactories;

public class GeckoCurrencyFactory(HttpClient httpClient) : ICurrencyFactory
{
    private const string CurrencyDataRequest = "https://api.coingecko.com/api/v3/coins/{0}";
    public async Task<Currency> MakeCurrency(string id)
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
    private async Task<CoinGeckoDTO> GetGeckoDTOById(string id)
    {
        var response = await httpClient.GetAsync(string.Format(CurrencyDataRequest, id));
        var rawJson = await response.Content.ReadAsStringAsync();
        var dto = JsonDocument.Parse(rawJson).Deserialize<CoinGeckoDTO>();
        return dto ?? throw new InvalidOperationException("JSON deserialized into null");
    }

    private static ImmutableArray<string> BuildMarketPlaces(string id)
    {
        var marketPlaces = MarketPlacesURL.Select(s => string.Format(s, id))
            .ToImmutableArray();
        return marketPlaces;
    }

    private static readonly IReadOnlyCollection<string> MarketPlacesURL = new HashSet<string>
    {
        "https://www.coingecko.com/en/coins/{0}",
        "https://coincap.io/assets/{0}"
    };
}