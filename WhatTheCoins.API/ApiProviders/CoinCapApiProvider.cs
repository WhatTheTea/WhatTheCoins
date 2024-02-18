using System.Collections.Immutable;
using System.Globalization;
using WhatTheCoins.API.DTO.CoinCap;

namespace WhatTheCoins.API.ApiProviders;

public class CoinCapApiProvider(IHttpClientFactory httpClientFactory) : ApiProviderBase(httpClientFactory)
{
    private const string CurrencyDataRequestURL = "https://api.coincap.io/v2/assets/{0}";
    private const string AssetsDataRequestURL = "https://api.coincap.io/v2/assets";
    private const string ExchangeRatesRequestURL = "https://api.coincap.io/v2/rates";
    private const string CandlesDataRequestURL = 
        "https://api.coincap.io/v2/candles?exchange=poloniex&interval=d{1}&baseId={0}&quoteId={2}";

    public override async Task<Currency> GetByIdAsync(string id)
    {
        var dto = await GetDTO<DTO<CurrencyData>>(string.Format(CurrencyDataRequestURL, id));
        var currency = dto.Data.ToCurrency();
        currency = currency with { SymbolToPrice = await GetExchangeRatesFor(currency) };
        return currency;
    }

    private async Task<ImmutableDictionary<string, double>> GetExchangeRatesFor(Currency currency)
    {
        var dto = await GetDTO<DTO<IEnumerable<RatesData>>>(ExchangeRatesRequestURL);
        var priceUSD = currency.SymbolToPrice["usd"];
        return GenerateRatesFrom(dto.Data, priceUSD).AddRange(currency.SymbolToPrice);
    }

    private static ImmutableDictionary<string, double> GenerateRatesFrom(IEnumerable<RatesData> data, double toUsd) => 
        data.Where(d => d.Symbol != "USD")
            .Select(d =>
                {
                    var exchangeRate = toUsd / double.Parse(d.RateUsd, CultureInfo.InvariantCulture);
                    return new KeyValuePair<string,double>(d.Symbol.ToLower(), exchangeRate);
                }
            ).ToImmutableDictionary();

    public override async Task<IImmutableList<string>> SearchAsync(string query)
    {
        var dto = await GetDTO<DTO<IEnumerable<CurrencyData>>>(AssetsDataRequestURL);
        return dto.Data.Where(d => d.Symbol.Contains(query, StringComparison.InvariantCultureIgnoreCase)
                                   || d.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase) ||
                                   d.Id.Contains(query, StringComparison.InvariantCultureIgnoreCase))
            .Select(x => x.Id).ToImmutableArray();
    }

    public override async Task<IImmutableList<string>> GetTop10Async()
    {
        var dto = await GetDTO<DTO<IEnumerable<CurrencyData>>>(AssetsDataRequestURL+"?limit=10");
        return dto.Data.Take(10).Select(d => d.Id).ToImmutableArray();
    }

    public override async Task<IImmutableList<Candle>> GetCandles(string id, int days = 7, string referenceCurrency = "usd")
    {
        var dto = await GetDTO<DTO<IEnumerable<CandleData>>>(string.Format(CandlesDataRequestURL,
            id,
            days,
            referenceCurrency));
        return dto.Data.Select(data => data.ToCandle()).ToImmutableArray();
    }
}