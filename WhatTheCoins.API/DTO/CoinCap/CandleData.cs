using System.Globalization;
using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinCap;

internal record CandleData(
    [property: JsonPropertyName("open")] string Open,
    [property: JsonPropertyName("high")] string High,
    [property: JsonPropertyName("low")] string Low,
    [property: JsonPropertyName("close")] string Close,
    [property: JsonPropertyName("volume")] string Volume,
    [property: JsonPropertyName("period")] long Period
) : ICandleDTO
{
    public Candle ToCandle()
    {
        return new Candle(DateTimeOffset.FromUnixTimeMilliseconds(Period).DateTime,
            double.Parse(Open, CultureInfo.InvariantCulture),
            double.Parse(High, CultureInfo.InvariantCulture),
            double.Parse(Low, CultureInfo.InvariantCulture),
            double.Parse(Close, CultureInfo.InvariantCulture)
        );
    }
}