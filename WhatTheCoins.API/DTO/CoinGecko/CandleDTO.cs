using System.Collections.ObjectModel;

namespace WhatTheCoins.API.DTO.CoinGecko;

internal class CandleDTO : Collection<double>, ICandleDTO
{
    public Candle ToCandle()
    {
        return new Candle(DateTimeOffset.FromUnixTimeMilliseconds((long)this[0]).DateTime,
            this[1],
            this[2],
            this[3],
            this[4]
        );
    }
}