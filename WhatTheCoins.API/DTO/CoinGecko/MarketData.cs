using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinGecko;

internal record MarketData(
    [property: JsonPropertyName("current_price")] CurrentPrice CurrentPrice,
    [property: JsonPropertyName("total_volume")] TotalVolume TotalVolume,
    [property: JsonPropertyName("price_change_24h")] double? PriceChange24h
);