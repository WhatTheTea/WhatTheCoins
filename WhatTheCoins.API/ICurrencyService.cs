namespace WhatTheCoins.API;

public interface ICurrencyService
{
    Task<Currency> GetByIdAsync(string id);
    Task<Currency> GetByCodeAsync(string code);
    Task<IEnumerable<Currency>> GetTop10Async();
    Task<Currency> GetExchangeRateAsync(Currency with);
}