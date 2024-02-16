using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinCap.Rates;

internal class DTO(
    [property: JsonPropertyName("data")] IReadOnlyList<Data> Data,
    [property: JsonPropertyName("timestamp")]
    long Timestamp
);