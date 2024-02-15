using WhatTheCoins.API;

namespace WhatTheCoins.Tests.CoinGecko;

[TestFixture]
[TestOf(typeof(CurrencyService))]
public class CoinGeckoApiProvider
{
    [Test]
    public async Task GetByIdIdeal()
    {
        var httpClient = HttpClientMock.MockHttpClient(HttpResponses.GeckoApiGetIdResponse);
        var service = new CurrencyService(new API.ApiProviders.CoinGeckoApiProvider(httpClient));
        
        var data = await service.GetByIdAsync("bitcoin");
        
        data.Should().BeEquivalentTo(ExpectedData.ExpectedCurrency);
    }

    [Test]
    public async Task GetCandlesIdeal()
    {
        var httpClient = HttpClientMock.MockHttpClient(HttpResponses.GeckoApiGetOHCLResponse);
        var service = new CurrencyService(new API.ApiProviders.CoinGeckoApiProvider(httpClient));
        
        var data = await service.GetCandles("bitcoin");

        data.Should().BeEquivalentTo(ExpectedData.ExpectedCandles);
    }

    [Test]
    public async Task SearchByCode()
    {
        var httpClient = HttpClientMock.MockHttpClient(HttpResponses.GeckoApiSearchResponseBTC);
        var service = new CurrencyService(new API.ApiProviders.CoinGeckoApiProvider(httpClient));
        
        var data = await service.SearchAsync("btc");

        data.Should().BeEquivalentTo(ExpectedData.ExpectedSearchResultBTC);
    }

    [Test]
    public async Task Top10()
    {
        // var httpClient = HttpClientMock.MockHttpClient();
        // var service = new CurrencyService(new API.ApiProviders.CoinGeckoApiProvider(httpClient));
    }
}