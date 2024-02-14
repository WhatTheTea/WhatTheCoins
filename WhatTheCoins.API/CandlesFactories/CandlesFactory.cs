using System.Collections.Immutable;

namespace WhatTheCoins.API.OHCLFactories;

public class CandlesFactory : ICandlesFactory
{
    public ICandlesFactory SetReferenceCurrency(string currency)
    {
        throw new NotImplementedException();
    }

    public ICandlesFactory SetDays(int days)
    {
        throw new NotImplementedException();
    }

    public Task<IImmutableList<Candle>> MakeCandles(string currencyId)
    {
        throw new NotImplementedException();
    }
}