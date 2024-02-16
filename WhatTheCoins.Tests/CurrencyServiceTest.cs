using WhatTheCoins.API;

namespace WhatTheCoins.Tests;

[TestFixture]
[TestOf(typeof(CurrencyService))]
public class CurrencyServiceTest
{

    [Test]
    public async Task SearchIdeal()
    {
        var apiProvider = ExpectedData.ExpectedProvider;
        var service = new CurrencyService(apiProvider);

        var search = await service.SearchAsync("btc");

        search.Should().BeEquivalentTo(ExpectedData.ExpectedCurrency);
    }

    [Test]
    public async Task Top10Ideal()
    {
        var apiProvider = ExpectedData.ExpectedProvider;
        var service = new CurrencyService(apiProvider);

        var top10 = await service.GetTop10Async();

        top10.Should().BeEquivalentTo(ExpectedData.ExpectedCurrencies);
    }
}