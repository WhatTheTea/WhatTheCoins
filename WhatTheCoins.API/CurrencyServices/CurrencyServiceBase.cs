using System.Collections.Immutable;
using System.Text.Json;

namespace WhatTheCoins.API.CurrencyServices;

public abstract class CurrencyServiceBase(
    HttpClient httpClient,
    ICurrencyFactory currencyFactory,
    ICandlesFactory candlesFactory)
    : ICurrencyService
{
    protected readonly ICandlesFactory CandlesFactory = candlesFactory;
    protected readonly ICurrencyFactory CurrencyFactory = currencyFactory;
    protected string SearchRequestURL = "";

    public abstract Task<IImmutableList<Candle>> GetCandles(string id);
    public abstract Task<Currency> GetByIdAsync(string id);
    public abstract Task<string?> SearchAsync(string query);
    public abstract Task<IImmutableList<Currency>> GetTop10Async();

    protected virtual async Task<T?> GetDTO<T>(string requestURL)
    {
        var request = await httpClient.GetAsync(string.Format(requestURL));
        var rawJSON = await request.Content.ReadAsStringAsync();
        var dto = JsonDocument.Parse(rawJSON).Deserialize<T>();
        return dto;
    }
}