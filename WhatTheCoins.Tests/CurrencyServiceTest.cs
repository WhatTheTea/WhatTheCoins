using WhatTheCoins.API;

namespace WhatTheCoins.Tests;

[TestFixture]
[TestOf(typeof(CurrencyService))]
public class CurrencyServiceTest
{

    [Test]
    public async Task SearchIdeal()
    {
        var apiProvider = ExpectedData.CreateIdealProvider();
        var service = new CurrencyService(apiProvider);

        var search = await service.SearchAsync("btc");

        search.Should().BeEquivalentTo(ExpectedData.ExpectedCurrency);
    }
}