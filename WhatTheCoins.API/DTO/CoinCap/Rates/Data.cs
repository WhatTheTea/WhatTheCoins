using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinCap.Rates;

internal record Data(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("currencySymbol")] string CurrencySymbol,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("rateUsd")] string RateUsd
);