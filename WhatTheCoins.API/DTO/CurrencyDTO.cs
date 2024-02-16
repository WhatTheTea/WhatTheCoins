using System.Collections.Immutable;

namespace WhatTheCoins.API.DTO;

internal abstract record CurrencyDTO
{
    internal abstract Currency ToCurrency();

    private static readonly ImmutableArray<string> MarketPlacesURL =
    [
        "https://www.coingecko.com/en/coins/{0}", "https://coincap.io/assets/{0}"
    ];

    protected static ImmutableArray<string> BuildMarketPlaces(string id)
    {
        return MarketPlacesURL.Select(s => string.Format(s, id)).ToImmutableArray();
    }
}