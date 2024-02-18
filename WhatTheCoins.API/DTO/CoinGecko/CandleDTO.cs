using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace WhatTheCoins.API.DTO.CoinGecko;

internal class CandleDTO : Collection<long>, ICandleDTO
{
    public Candle ToCandle() => new(
            ConvertUnixToDateTime(this[0]),
            this[1],
            this[2],
            this[3],
            this[4]
            );
    
    private static DateTime ConvertUnixToDateTime(long ms)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(ms).DateTime;
    }
}