using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinGecko.Currency;

internal record DTO(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("localization")]
    Localization Localization,
    [property: JsonPropertyName("description")]
    Description Description,
    [property: JsonPropertyName("links")] Links Links,
    [property: JsonPropertyName("image")] Image Image,
    [property: JsonPropertyName("market_cap_rank")]
    int MarketCapRank,
    [property: JsonPropertyName("market_data")]
    MarketData MarketData
) : CurrencyDTO
{
    internal override API.Currency ToCurrency()
    {
        return new API.Currency(Id, Symbol, MarketData.TotalVolume["usd"],
            MarketData.PriceChange24h ?? 0, MarketData.CurrentPrice.ToImmutableDictionary(),
            BuildMarketPlaces(Id));
    }
}