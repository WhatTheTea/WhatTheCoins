using WhatTheCoins.API;
using WhatTheCoins.API.ApiProviders;

namespace WhatTheCoins.Tests.ApiProviderTests;
[TestFixture(typeof(CoinGeckoApiProvider))]
public class CoinCapApiProviderTest<TApiProvider> : ApiProviderTest<TApiProvider> where TApiProvider : IApiProvider
{
    public override void SetUpFixture()
    {
        // throw new NotImplementedException();
    }
}