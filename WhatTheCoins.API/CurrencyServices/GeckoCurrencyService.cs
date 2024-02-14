namespace WhatTheCoins.API;

public class GeckoCurrencyService(HttpClient httpClient) : ICurrencyService
{
    private const string CurrencyDataRequest = "https://api.coingecko.com/api/v3/coins/{0}";
    public Currency GetById(string id)
    {
        throw new NotImplementedException();
    }

    public Currency GetByCode(string code)
    {
        throw new NotImplementedException();
    }

    public async Task<Currency> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<Currency> GetByCodeAsync(string code)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Currency>> GetTop10Async()
    {
        throw new NotImplementedException();
    }

    public async Task<Currency> GetExchangeRateAsync(Currency with)
    {
        throw new NotImplementedException();
    }
}