using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinGecko.Currency;

internal record Image(
    [property: JsonPropertyName("thumb")] string Thumb,
    [property: JsonPropertyName("small")] string Small,
    [property: JsonPropertyName("large")] string Large
);