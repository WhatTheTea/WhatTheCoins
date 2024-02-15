namespace WhatTheCoins.API;

public interface ICurrencyFactory
{
    public Task<Currency> MakeCurrency(string id);
}