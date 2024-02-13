namespace WhatTheCoins.API;

public interface ICurrencyService
{
    Currency GetById(string id);
    Currency GetByCode(string code);
    IEnumerable<Currency> GetTop10();
    Currency GetExchangeRate(Currency with);
}