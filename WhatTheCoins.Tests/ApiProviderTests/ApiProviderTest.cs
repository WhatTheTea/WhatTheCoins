using WhatTheCoins.API;

namespace WhatTheCoins.Tests.ApiProviderTests;

[TestFixture]
public abstract class ApiProviderTest<TApiProvider> where TApiProvider : IApiProvider
{
    [SetUp]
    public void SetUp()
    {
        HttpClient = null;
    }

    [OneTimeSetUp]
    public abstract void SetUpFixture();

    private static IApiProvider MakeApiProvider(HttpClient httpClient)
    {
        return (TApiProvider)Activator.CreateInstance(typeof(TApiProvider), httpClient)!;
    }

    protected string GetIdResponse = "";
    protected string GetOHCLResponse = "";
    protected string SearchResponse = "";
    protected string Top10Response = "";

    protected HttpClient? HttpClient { get; set; }

    [Test]
    public virtual async Task GetByIdIdeal()
    {
        HttpClient ??= new HttpClientMockBuilder()
            .AddMessage(HttpClientMockBuilder.Any, GetIdResponse)
            .Build();
        var provider = MakeApiProvider(HttpClient);

        var data = await provider.GetByIdAsync("bitcoin");

        data.Should().BeEquivalentTo(ExpectedData.ExpectedCurrency);
    }

    [Test]
    public virtual async Task GetCandlesIdeal()
    {
        HttpClient ??= new HttpClientMockBuilder()
            .AddMessage(HttpClientMockBuilder.Any, GetOHCLResponse)
            .Build();
        var provider = MakeApiProvider(HttpClient);

        var data = await provider.GetCandles("bitcoin", 7, "usd");

        data.Should().BeEquivalentTo(ExpectedData.ExpectedCandles);
    }

    [Test]
    public virtual async Task SearchByCode()
    {
        HttpClient ??= new HttpClientMockBuilder()
            .AddMessage(HttpClientMockBuilder.Any, SearchResponse)
            .Build();
        var provider = MakeApiProvider(HttpClient);

        var data = await provider.SearchAsync("btc");

        data.Should().BeEquivalentTo(ExpectedData.ExpectedSearchResultBTC);
    }

    [Test]
    public virtual async Task Top10()
    {
        HttpClient ??= new HttpClientMockBuilder()
            .AddMessage(HttpClientMockBuilder.Any, Top10Response)
            .Build();
        var provider = MakeApiProvider(HttpClient);

        var data = await provider.GetTop10Async();

        data.Should().ContainInOrder(ExpectedData.ExpectedCoins);
    }
}