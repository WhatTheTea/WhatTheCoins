namespace WhatTheCoins.API;

internal interface ICurrencyFactory
{
    public Task<Currency> MakeCurrency(string id);
}