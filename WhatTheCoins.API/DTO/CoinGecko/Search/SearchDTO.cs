using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinGecko.Search;

public record SearchDTO(
    [property: JsonPropertyName("coins")] IImmutableList<Coin> Coins
);