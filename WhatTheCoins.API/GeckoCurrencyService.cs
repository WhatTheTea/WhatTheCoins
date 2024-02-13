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

    public IEnumerable<Currency> GetTop10()
    {
        throw new NotImplementedException();
    }

    public Currency GetExchangeRate(Currency with)
    {
        throw new NotImplementedException();
    }
}