using System.Collections.Immutable;

namespace WhatTheCoins.API;

public interface IApiProvider
{
    Task<Currency> GetByIdAsync(string id);
    Task<string?> SearchAsync(string query);
    Task<IImmutableList<Currency>> GetTop10Async();
    Task<IImmutableList<Candle>> GetCandles(string id, int days, string referenceCurrency);
}