using System.Collections.Immutable;

namespace WhatTheCoins.API;

public interface ICandlesFactory
{
    public ICandlesFactory SetReferenceCurrency(string currency);
    public ICandlesFactory SetDays(int days);
    public Task<IImmutableList<Candle>> MakeCandles(string currencyId);
}