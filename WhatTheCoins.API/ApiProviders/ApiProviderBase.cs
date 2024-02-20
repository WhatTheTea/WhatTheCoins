using System.Collections.Immutable;
using System.Net;
using System.Text.Json;

namespace WhatTheCoins.API.ApiProviders;

public abstract class ApiProviderBase(HttpClient httpClient) : IApiProvider
{
    public abstract Task<Currency> GetByIdAsync(string id);
    public abstract Task<IImmutableList<string>> SearchAsync(string query);
    public abstract Task<IImmutableList<string>> GetTopAsync();
    public abstract Task<IImmutableList<Candle>> GetCandles(string id, int days = 7, string referenceCurrency = "usd");

    protected async Task<T> GetDTO<T>(string requestURL)
    {
        var request = await httpClient.GetAsync(requestURL);
        if (request.StatusCode != HttpStatusCode.OK) throw new Exception("Request " + requestURL + " is not ok: "+await request.Content.ReadAsStringAsync()); 
        var rawJSON = await request.Content.ReadAsStringAsync();
        var dto = JsonDocument.Parse(rawJSON).Deserialize<T>();
        return dto;
    }
}