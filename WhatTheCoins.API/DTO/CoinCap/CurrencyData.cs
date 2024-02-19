using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinCap;

internal record CurrencyData(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("rank")] string Rank,
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("supply")] string Supply,
    [property: JsonPropertyName("maxSupply")]
    string MaxSupply,
    [property: JsonPropertyName("marketCapUsd")]
    string MarketCapUsd,
    [property: JsonPropertyName("volumeUsd24Hr")]
    string VolumeUsd24Hr,
    [property: JsonPropertyName("priceUsd")]
    string PriceUsd,
    [property: JsonPropertyName("changePercent24Hr")]
    string ChangePercent24Hr
) : CurrencyDTO
{
    internal override Currency ToCurrency()
    {
        return new Currency(
            Id.ToLower(),
            Symbol.ToLower(),
            ConvertToDouble(VolumeUsd24Hr),
            ConvertToDouble(ChangePercent24Hr),
            new Dictionary<string, double>
            {
                { "usd", ConvertToDouble(PriceUsd) }
            }.ToImmutableDictionary(),
            BuildMarketPlaces(Id)
        );
    }
}