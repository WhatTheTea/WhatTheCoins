using System.Collections.Immutable;

namespace WhatTheCoins.API.ApiProviders;

public class CoinGeckoApiProvider(HttpClient httpClient) : ApiProviderBase(httpClient)
{
    private const string CurrencyDataRequestURL = "https://api.coingecko.com/api/v3/coins/{0}";
    private const string CandlesDataRequestURL = "https://api.coingecko.com/api/v3/coins/{0}/ohlc?vs_currency={1}&days={2}";
    private const string SearchRequestURL = "https://api.coingecko.com/api/v3/search?query={0}";
    private const string Top10RequestURL = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=10&page=1&sparkline=false&price_change_percentage=24h&locale=en";

    public override async Task<Currency> GetByIdAsync(string id)
    {
        // TODO: Own exceptions
        var dto = await GetDTO<DTO.CoinGecko.Currency.DTO>(string.Format(CurrencyDataRequestURL, id)) ??
                  throw new Exception("id not found on CoinGecko");
        return dto.ToCurrency();
    }
    public override async Task<string?> SearchAsync(string query)
    {
        var dto = await GetDTO<DTO.CoinGecko.Search.DTO>(string.Format(SearchRequestURL, query));
        return dto?.Coins[0].Id;
    }
    public override async Task<IImmutableList<string>> GetTop10Async()
    {
        var rawCurrencies = await GetDTO<ImmutableArray<DTO.CoinGecko.Currency.DTO>>(Top10RequestURL);
        var currencies = rawCurrencies.Select(dto => dto.Id).ToImmutableArray();
        return currencies;
    }
    public override async Task<IImmutableList<Candle>> GetCandles(string id, int days = 7,
        string referenceCurrency = "usd")
    {
        var rawCandles = await GetDTO<ImmutableArray<ImmutableArray<double>>>(
            string.Format(CandlesDataRequestURL,
                id,
                days,
                referenceCurrency));
        // Unpack data to Candles
        return rawCandles.Select(arr =>
            new Candle(ConvertUnixToDateTime((long)arr[0]),
                arr[1], arr[2], arr[3], arr[4])
        ).ToImmutableArray();
    }
    private static DateTime ConvertUnixToDateTime(long ms)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(ms).DateTime;
    }
}