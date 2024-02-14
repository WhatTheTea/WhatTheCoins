using WhatTheCoins.API;

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
    public async Task GetOHCLIdeal()
    {
        var httpClient = HttpClientMock.MockHttpClient(ExpectedData.GeckoApiGetOHCLResponse);
        var service = new GeckoCurrencyService(httpClient);
        var data = await service.GetOHCL("bitcoin");

        data.Should().BeEquivalentTo(ExpectedData.ExpectedOHCLs);
    }
}