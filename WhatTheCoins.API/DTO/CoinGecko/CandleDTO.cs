using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace WhatTheCoins.API.DTO.CoinGecko;

internal class CandleDTO : Collection<long>, ICandleDTO
{
    public Candle ToCandle() =>
        new(DateTimeOffset.FromUnixTimeMilliseconds(this[0]).DateTime,
            this[1],
            this[2],
            this[3],
            this[4]
        );
}