using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinGecko.Search;

internal record DTO(
    [property: JsonPropertyName("coins")] IImmutableList<Coin> Coins
);