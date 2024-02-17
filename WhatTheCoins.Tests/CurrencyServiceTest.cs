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
        var apiProvider = ExpectedData.GetExpectedProvider(ExpectedData.Top10ParamToReturn);
        var service = new CurrencyService(apiProvider);

        var top10 = await service.GetTop10Async();
        var top10Ids = top10.Select(currency => currency.Id);

        top10Ids.Should().ContainInOrder(ExpectedData.ExpectedCoins);
    }
}