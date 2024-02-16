﻿using WhatTheCoins.API;

namespace WhatTheCoins.Tests.ApiProviderTests;

[TestFixture]
public abstract class ApiProviderTest<TApiProvider> where TApiProvider : IApiProvider
{
    protected static IApiProvider MakeApiProvider(HttpClient httpClient) =>
        (TApiProvider)Activator.CreateInstance(typeof(TApiProvider), httpClient)!;

    protected static string GetIdResponse = "";
    protected static string GetOHCLResponse = "";
    protected static string SearchResponse = "";
    protected static string Top10Response = "";
    [Test]
    public virtual async Task GetByIdIdeal()
    {
        var httpClient = HttpClientMock.MockHttpClient(GetIdResponse);
        var provider = MakeApiProvider(httpClient);
        
        var data = await provider.GetByIdAsync("bitcoin");
        
        data.Should().BeEquivalentTo(ExpectedData.ExpectedCurrency);
    }
    [Test]
    public virtual async Task GetCandlesIdeal()
    {
        var httpClient = HttpClientMock.MockHttpClient(GetOHCLResponse);
        var provider = MakeApiProvider(httpClient);
        
        var data = await provider.GetCandles("bitcoin", 7, "usd");

        data.Should().BeEquivalentTo(ExpectedData.ExpectedCandles);
    }
    [Test]
    public virtual async Task SearchByCode()
    {
        var httpClient = HttpClientMock.MockHttpClient(SearchResponse);
        var provider = MakeApiProvider(httpClient);
        
        var data = await provider.SearchAsync("btc");

        data.Should().BeEquivalentTo(ExpectedData.ExpectedSearchResultBTC);
    }
    [Test]
    public virtual async Task Top10()
    {
        var httpClient = HttpClientMock.MockHttpClient(Top10Response);
        var provider = MakeApiProvider(httpClient);

        var data = await provider.GetTop10Async();

        data.Should().BeEquivalentTo(ExpectedData.CreateExpectedCurrencies());
    }

    [OneTimeSetUp]
    public abstract void SetUpFixture();
}