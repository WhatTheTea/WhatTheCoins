using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinGecko.Currency;

internal record DTO : CurrencyDTO
{
    internal DTO(string id, string symbol, string name, Localization localization, Description description, Links links, Image image, int marketCapRank, MarketData marketData)
    {
        Id = id;
        Symbol = symbol;
        Name = name;
        Localization = localization;
        Description = description;
        Links = links;
        Image = image;
        MarketCapRank = marketCapRank;
        MarketData = marketData;
    }

    [property: JsonPropertyName("id")] string Id{ get; init; }
    [property: JsonPropertyName("symbol")] string Symbol{ get; init; }
    [property: JsonPropertyName("name")] string Name{ get; init; }
    [property: JsonPropertyName("localization")]
    Localization Localization{ get; init; }
    [property: JsonPropertyName("description")]
    Description Description{ get; init; }
    [property: JsonPropertyName("links")] Links Links{ get; init; }
    [property: JsonPropertyName("image")] Image Image{ get; init; }
    [property: JsonPropertyName("market_cap_rank")]
    int MarketCapRank{ get; init; }
    [property: JsonPropertyName("market_data")]
    private MarketData MarketData { get; init; }

    internal override API.Currency ToCurrency() => 
    new API.Currency(Id, Symbol, MarketData.TotalVolume["usd"],
                MarketData.PriceChange24h ?? 0, MarketData.CurrentPrice.ToImmutableDictionary(), 
                BuildMarketPlaces(Id));
}