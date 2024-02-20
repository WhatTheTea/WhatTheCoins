using System.Collections.Immutable;

namespace WhatTheCoins.API;

public interface ICurrencyService
{
    Task<Currency> GetByIdAsync(string id);
    Task<IImmutableList<Currency>> SearchAsync(string query);
    Task<IImmutableList<Currency>> GetTopAsync();
    Task<IImmutableList<Candle>> GetCandles(string id, int days = 7, string referenceCurrency = "usd");

    void ChangeApiProvider(IApiProvider provider);
}