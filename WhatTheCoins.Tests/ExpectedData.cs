using System.Collections.Immutable;
using WhatTheCoins.API;

namespace WhatTheCoins.Tests;

public static class ExpectedData
{
    public static readonly Currency ExpectedCurrency = new(
        Id: "bitcoin",
        Symbol: "btc",
        PriceChange: -2.72424,
        Volume: 33970260124,
        SymbolToPrice: new Dictionary<string, double>
        {
            {"btc",1d},
            {"usd",49418}
            
        }.ToImmutableDictionary(),
        MarketPlaces: new HashSet<string>
        {
            "https://www.coingecko.com/en/coins/bitcoin",
            "https://coincap.io/assets/bitcoin"
        }.ToImmutableArray()
    );

    public static readonly ImmutableArray<Candle> ExpectedCandles =
    [
        new Candle(
            DateTimeOffset.FromUnixTimeMilliseconds(1707350400000).DateTime,
            43606, 44176, 43606, 44165
        ),
        new Candle(
            DateTimeOffset.FromUnixTimeMilliseconds(1707364800000).DateTime,
            44318, 44692, 44318, 44600)
    ];

    public const string ExpectedSearchResultBTC = "bitcoin";
}