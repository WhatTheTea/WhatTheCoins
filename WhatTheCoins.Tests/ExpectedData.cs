using System.Collections.Immutable;
using Moq;
using WhatTheCoins.API;

namespace WhatTheCoins.Tests;

public static class ExpectedData
{
    public const string ExpectedSearchResultBTC = "bitcoin";

    public static readonly Currency ExpectedCurrency = new(
        "bitcoin",
        "btc",
        PriceChange: -2.72424,
        Volume: 33970260124,
        SymbolToPrice: new Dictionary<string, double>
        {
            { "usd", 49418 }
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

    public static readonly string[] ExpectedCoins =
    [
        "bitcoin", "ethereum", "tether", "binancecoin", "solana", "ripple", "usd-coin", "staked-ether", "cardano",
        "avalanche-2"
    ];

    public static IApiProvider ExpectedProvider
    {
        get
        {
            var idealProvider = new Mock<IApiProvider>();
            idealProvider.Setup(provider => provider.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(ExpectedCurrency);
            idealProvider.Setup(provider => provider.SearchAsync(It.IsAny<string>()))
                .ReturnsAsync([ExpectedCurrency.Id]);
            idealProvider.Setup(provider => provider.GetCandles(It.IsAny<string>(),
                    It.IsAny<int>(),
                    It.IsAny<string>()))
                .ReturnsAsync(ExpectedCandles);
            idealProvider.Setup(provider => provider.GetTopAsync()).ReturnsAsync(ExpectedCoins.ToImmutableArray());
            return idealProvider.Object;
        }
    }

    public static Dictionary<string, string> Top10ParamToReturn => ExpectedCoins.ToDictionary(s => s);

    // TODO: Make ApiProviderMockBuilder analogous to HttpClientMockBuilder
    public static IApiProvider GetExpectedProvider(Dictionary<string, string> paramToReturn)
    {
        var idealProvider = new Mock<IApiProvider>();
        idealProvider.Setup(provider => provider.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync((string arg) =>
                new Currency(paramToReturn[arg],
                    "",
                    default,
                    default,
                    new Dictionary<string, double>().ToImmutableDictionary(),
                    new ImmutableArray<string>()));
        idealProvider.Setup(provider => provider.SearchAsync(It.IsAny<string>())).ReturnsAsync([ExpectedCurrency.Id]);
        idealProvider.Setup(provider => provider.GetCandles(It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<string>()))
            .ReturnsAsync(ExpectedCandles);
        idealProvider.Setup(provider => provider.GetTopAsync()).ReturnsAsync(ExpectedCoins.ToImmutableArray());
        return idealProvider.Object;
    }
}