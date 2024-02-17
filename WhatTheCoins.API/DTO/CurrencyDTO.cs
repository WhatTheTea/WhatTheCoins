using System.Collections.Immutable;
using System.Globalization;

namespace WhatTheCoins.API.DTO;

internal abstract record CurrencyDTO
{
    private static readonly ImmutableArray<string> MarketPlacesURL =
    [
        "https://www.coingecko.com/en/coins/{0}", "https://coincap.io/assets/{0}"
    ];

    internal abstract Currency ToCurrency();

    protected static ImmutableArray<string> BuildMarketPlaces(string id)
    {
        return MarketPlacesURL.Select(s => string.Format(s, id)).ToImmutableArray();
    }

    protected static double ConvertToDouble(string num)
    {
        return double.Parse(num, CultureInfo.InvariantCulture);
    }
}