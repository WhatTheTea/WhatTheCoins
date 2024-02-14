using System.Collections.Immutable;

namespace WhatTheCoins.API;

public interface ICurrencyService
{
    Task<Currency> GetByIdAsync(string id);
    Task<Currency?> Search(string query);
    Task<IImmutableList<Currency>> GetTop10Async();
    Task<IImmutableList<OHCL>> GetOHCL(string id);
}