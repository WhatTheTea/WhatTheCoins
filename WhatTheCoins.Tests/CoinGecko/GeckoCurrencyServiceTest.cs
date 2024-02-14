using WhatTheCoins.API;
using WhatTheCoins.API.CurrencyServices;

namespace WhatTheCoins.Tests;

[TestFixture]
[TestOf(typeof(GeckoCurrencyService))]
public class GeckoCurrencyServiceTest
{
    [Test]
    public async Task GetByIdIdeal()
    {
        var httpClient = HttpClientMock.MockHttpClient(ExpectedData.GeckoApiGetIdResponse);
        var service = new GeckoCurrencyService(httpClient);
        
        var data = await service.GetByIdAsync("bitcoin");
        
        data.Should().BeEquivalentTo(ExpectedData.ExpectedCurrency);
    }

    [Test]
    public async Task GetCandlesIdeal()
    {
        var httpClient = HttpClientMock.MockHttpClient(ExpectedData.GeckoApiGetOHCLResponse);
        var service = new GeckoCurrencyService(httpClient);
        
        var data = await service.GetCandles("bitcoin");

        data.Should().BeEquivalentTo(ExpectedData.ExpectedCandles);
    }

    [Test]
    public async Task SearchByCode()
    {
        var httpClient = HttpClientMock.MockHttpClient(ExpectedData.GeckoApiSearchResponseBTC);
        var service = new GeckoCurrencyService(httpClient);
        
        var data = await service.SearchAsync("btc");

        data.Should().BeEquivalentTo(ExpectedData.ExpectedSearchResultBTC);
    }
}