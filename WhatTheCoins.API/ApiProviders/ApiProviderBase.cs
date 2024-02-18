using System.Collections.Immutable;
using System.Text.Json;

namespace WhatTheCoins.API.ApiProviders;

public abstract class ApiProviderBase(IHttpClientFactory httpClientFactory) : IApiProvider
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    public abstract Task<Currency> GetByIdAsync(string id);
    public abstract Task<IImmutableList<string>> SearchAsync(string query);
    public abstract Task<IImmutableList<string>> GetTop10Async();
    public abstract Task<IImmutableList<Candle>> GetCandles(string id, int days = 7, string referenceCurrency = "usd");

    protected async Task<T> GetDTO<T>(string requestURL)
    {
        var request = await _httpClient.GetAsync(string.Format(requestURL));
        var rawJSON = await request.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(requestURL)) throw new Exception("Request url is empty");
        if (string.IsNullOrEmpty(rawJSON)) throw new Exception("Api returned empty string");
        var dto = JsonDocument.Parse(rawJSON).Deserialize<T>();
        return dto ?? throw new Exception("Id not found");
    }
}