using System.Collections.Immutable;

namespace WhatTheCoins.API.ApiProviders;

public class CoinCapApiProvider(HttpClient httpClient) : ApiProviderBase(httpClient)
{
    public override Task<Currency> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public override Task<string?> SearchAsync(string query)
    {
        throw new NotImplementedException();
    }

    public override Task<IImmutableList<string>> GetTop10Async()
    {
        throw new NotImplementedException();
    }

    public override Task<IImmutableList<Candle>> GetCandles(string id, int days = 7, string referenceCurrency = "usd")
    {
        throw new NotImplementedException();
    }
}