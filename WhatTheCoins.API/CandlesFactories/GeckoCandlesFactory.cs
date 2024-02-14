using System.Collections.Immutable;
using System.Text.Json;

namespace WhatTheCoins.API.CandlesFactories;

internal class GeckoCandlesFactory(HttpClient httpClient) : ICandlesFactory
{
    private string _referenceCurrency = "usd";
    private int _days = 7;
    private const string CandlesDataRequest = "https://api.coingecko.com/api/v3/coins/{0}/ohlc?vs_currency={1}&days={2}";

    public string ReferenceCurrency
    {
        get => _referenceCurrency;
        set => SetReferenceCurrency(value);
    }

    public int Days
    {
        get => _days;
        set => SetDays(value);
    }

    public ICandlesFactory SetReferenceCurrency(string currency)
    {
        _referenceCurrency = currency;
        return this;
    }

    public ICandlesFactory SetDays(int days)
    {
        _days = days;
        return this;
    }
    private async Task<ImmutableArray<ImmutableArray<double>>> GetCandlesData(string id)
    {
        var response = await httpClient.GetAsync(
            string.Format(CandlesDataRequest, id, ReferenceCurrency, Days));
        var rawJson = await response.Content.ReadAsStringAsync();
        var dto = JsonDocument.Parse(rawJson).Deserialize<ImmutableArray<ImmutableArray<double>>>();
        return dto;
    }

    private static DateTime ConvertUnixToDateTime(long ms) => 
        DateTimeOffset.FromUnixTimeMilliseconds(ms).DateTime;
    public async Task<IImmutableList<Candle>> MakeCandles(string currencyId)
    {
        var rawCandles = await GetCandlesData(currencyId);
        var candles = rawCandles.Select(arr =>
            new Candle(ConvertUnixToDateTime((long)arr[0]),
                arr[1], arr[2], arr[3], arr[4])
        ).ToImmutableArray();
        return candles;
    }

}