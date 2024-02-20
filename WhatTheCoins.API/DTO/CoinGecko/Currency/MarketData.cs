using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinGecko.Currency;

internal record MarketData(
    [property: JsonPropertyName("current_price")]
    Dictionary<string, double> CurrentPrice,
    [property: JsonPropertyName("total_volume")]
    Dictionary<string, double> TotalVolume,
    [property: JsonPropertyName("price_change_percentage_24h")]
    double? PriceChange24h
);