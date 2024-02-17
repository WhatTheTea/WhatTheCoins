using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinCap;

internal record RatesData(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("rateUsd")]
    string RateUsd
);