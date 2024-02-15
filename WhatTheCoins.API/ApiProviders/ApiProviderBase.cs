using System.Collections.Immutable;
using System.Text.Json;

namespace WhatTheCoins.API.ApiProviders;

public abstract class ApiProviderBase(HttpClient httpClient) : IApiProvider
{
    public abstract Task<Currency> GetByIdAsync(string id);
    public abstract Task<string?> SearchAsync(string query);
    public abstract Task<IImmutableList<string>> GetTop10Async();
    public abstract Task<IImmutableList<Candle>> GetCandles(string id, int days = 7, string referenceCurrency = "usd");

    protected async Task<T?> GetDTO<T>(string requestURL)
    {
        var request = await httpClient.GetAsync(string.Format(requestURL));
        var rawJSON = await request.Content.ReadAsStringAsync();
        var dto = JsonDocument.Parse(rawJSON).Deserialize<T>();
        return dto;
    }
}