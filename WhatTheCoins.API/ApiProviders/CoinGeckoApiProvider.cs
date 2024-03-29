﻿using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Web;
using WhatTheCoins.API.DTO.CoinGecko;

namespace WhatTheCoins.API.ApiProviders;

public class CoinGeckoApiProvider(HttpClient httpClient) : ApiProviderBase(httpClient)
{
    private const string CurrencyDataRequestURL = "https://api.coingecko.com/api/v3/coins/{0}";

    private const string CandlesDataRequestURL =
        "https://api.coingecko.com/api/v3/coins/{0}/ohlc?vs_currency={2}&days={1}";

    private const string SearchRequestURL = "https://api.coingecko.com/api/v3/search?query={0}";

    private const string TopRequestURL =
        "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=5&page=1&sparkline=false&price_change_percentage=24h&locale=en";

    public override async Task<Currency> GetByIdAsync(string id)
    {
        // TODO: Own exceptions
        var dto = await GetDTO<DTO.CoinGecko.Currency.DTO>(string.Format(CurrencyDataRequestURL, id));
        return dto.ToCurrency();
    }

    public override async Task<IImmutableList<string>> SearchAsync(string query)
    {
        var dto = await GetDTO<DTO.CoinGecko.Search.DTO>(string.Format(SearchRequestURL, query));
        return dto.Coins.Select(coin => coin.Id).ToImmutableArray();
    }

    public override async Task<IImmutableList<string>> GetTopAsync()
    {
        var rawCurrencies = await GetDTO<ImmutableArray<DTO.CoinGecko.Currency.DTO>>(TopRequestURL);
        var currencies = rawCurrencies.Select(dto => dto.Id).ToImmutableArray();
        return currencies;
    }

    public override async Task<IImmutableList<Candle>> GetCandles(string id, int days = 7,
        string referenceCurrency = "usd")
    {
        var rawCandles = await GetDTO<Collection<CandleDTO>>(
            string.Format(CandlesDataRequestURL, id, HttpUtility.UrlEncode(days.ToString()), referenceCurrency));

        // Unpack data to Candles
        return rawCandles.Select(candle => candle.ToCandle()).ToImmutableArray();
    }
}