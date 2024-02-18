using WhatTheCoins.API;

namespace WhatTheCoins.Tests.ApiProviderTests;

[TestFixture]
public abstract class ApiProviderTest<TApiProvider> where TApiProvider : IApiProvider
{
    [SetUp]
    public void SetUp()
    {
        HttpClientFactory = null;
    }

    [OneTimeSetUp]
    public abstract void SetUpFixture();

    private static IApiProvider MakeApiProvider(IHttpClientFactory httpClientFactory)
    {
        return (TApiProvider)Activator.CreateInstance(typeof(TApiProvider), httpClientFactory)!;
    }

    protected string GetIdResponse = "";
    protected string GetCandlesResponse = "";
    protected string SearchResponse = "";
    protected string Top10Response = "";

    protected IHttpClientFactory? HttpClientFactory { get; set; }

    [Test]
    public virtual async Task GetByIdIdeal()
    {
        HttpClientFactory ??= new HttpClientFactoryMockBuilder()
            .AddMessage(HttpClientFactoryMockBuilder.Any, GetIdResponse)
            .Build();
        var provider = MakeApiProvider(HttpClientFactory);

        var data = await provider.GetByIdAsync("bitcoin");

        data.Should().BeEquivalentTo(ExpectedData.ExpectedCurrency);
    }

    [Test]
    public virtual async Task GetCandlesIdeal()
    {
        HttpClientFactory ??= new HttpClientFactoryMockBuilder()
            .AddMessage(HttpClientFactoryMockBuilder.Any, GetCandlesResponse)
            .Build();
        var provider = MakeApiProvider(HttpClientFactory);

        var data = await provider.GetCandles("bitcoin", 7, "usd");

        data.Should().BeEquivalentTo(ExpectedData.ExpectedCandles);
    }

    [Test]
    public virtual async Task SearchByCode()
    {
        HttpClientFactory ??= new HttpClientFactoryMockBuilder()
            .AddMessage(HttpClientFactoryMockBuilder.Any, SearchResponse)
            .Build();
        var provider = MakeApiProvider(HttpClientFactory);

        var data = await provider.SearchAsync("btc");

        data.Should().Contain(ExpectedData.ExpectedSearchResultBTC);
    }

    [Test]
    public virtual async Task Top10()
    {
        HttpClientFactory ??= new HttpClientFactoryMockBuilder()
            .AddMessage(HttpClientFactoryMockBuilder.Any, Top10Response)
            .Build();
        var provider = MakeApiProvider(HttpClientFactory);

        var data = await provider.GetTop10Async();

        data.Should().ContainInOrder(ExpectedData.ExpectedCoins);
    }
}