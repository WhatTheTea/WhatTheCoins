using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinGecko.Search;

public record Coin(
    [property: JsonPropertyName("id")] string? Id
);