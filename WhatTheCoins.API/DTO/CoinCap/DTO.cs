using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinCap;

internal record DTO<TData>(
    [property: JsonPropertyName("data")] TData Data,
    [property: JsonPropertyName("timestamp")]
    long Timestamp
);