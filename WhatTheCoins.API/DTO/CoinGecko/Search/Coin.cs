using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinGecko.Search;

internal record Coin(
    [property: JsonPropertyName("id")] string? Id
);