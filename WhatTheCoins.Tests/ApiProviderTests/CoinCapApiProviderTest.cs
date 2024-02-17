using NUnit.Framework.Internal;
using WhatTheCoins.API;
using WhatTheCoins.API.ApiProviders;

namespace WhatTheCoins.Tests.ApiProviderTests;
[TestFixture(typeof(CoinCapApiProvider))]
public class CoinCapApiProviderTest<TApiProvider> : ApiProviderTest<TApiProvider> where TApiProvider : IApiProvider
{
    public override void SetUpFixture()
    {
        GetIdResponse = "{\"data\":{\"id\":\"bitcoin\",\"rank\":\"1\",\"symbol\":\"BTC\",\"name\":\"Bitcoin\",\"supply\":\"17193925.0000000000000000\",\"maxSupply\":\"21000000.0000000000000000\",\"marketCapUsd\":\"119179791817.6740161068269075\",\"volumeUsd24Hr\":\"33970260124\",\"priceUsd\":\"49418\",\"changePercent24Hr\":\"-2.72424\",\"vwap24Hr\":\"7175.0663247679233209\"},\"timestamp\":1533581098863}"; 
        GetOHCLResponse = "";
        SearchResponse = "";
        Top10Response = "";
    }

    private const string GetExchangesResponse = "{\"data\":[{\"id\":\"united-states-dollar\",\"symbol\":\"USD\",\"currencySymbol\":\"$\",\"type\":\"fiat\",\"rateUsd\":\"1.0000000000000000\"}],\"timestamp\":1536347807471}";
    [Test]
    public override async Task GetByIdIdeal()
    {
        base.HttpClient = new HttpClientMockBuilder()
            .AddMessage("https://api.coincap.io/v2/assets/bitcoin", GetIdResponse)
            .AddMessage("https://api.coincap.io/v2/rates", GetExchangesResponse)
            .Build();
        await base.GetByIdIdeal();
    }
}