using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinCap.Currency;

internal record Data(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("rank")] string Rank,
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("supply")] string Supply,
    [property: JsonPropertyName("maxSupply")] string MaxSupply,
    [property: JsonPropertyName("marketCapUsd")] string MarketCapUsd,
    [property: JsonPropertyName("volumeUsd24Hr")] string VolumeUsd24Hr,
    [property: JsonPropertyName("priceUsd")] string PriceUsd,
    [property: JsonPropertyName("changePercent24Hr")] string ChangePercent24Hr
);