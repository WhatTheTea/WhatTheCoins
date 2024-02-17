using System.Collections.Immutable;
using System.Globalization;
using WhatTheCoins.API.DTO.CoinCap;

namespace WhatTheCoins.API.ApiProviders;

public class CoinCapApiProvider(HttpClient httpClient) : ApiProviderBase(httpClient)
{
    private const string CurrencyDataRequestURL = "https://api.coincap.io/v2/assets/{0}";
    private const string AssetsDataRequestURL = "https://api.coincap.io/v2/assets";
    private const string ExchangeRatesRequestURL = "https://api.coincap.io/v2/rates";

    private const string CandlesDataRequestURL =
        "https://api.coincap.io/v2/candles?exchange=poloniex&interval={hours}&baseId={base}&quoteId={quote}";

    public override async Task<Currency> GetByIdAsync(string id)
    {
        var dto = await GetDTO<DTO<CurrencyData>>(string.Format(CurrencyDataRequestURL, id));
        var currency = dto.Data.ToCurrency();
        currency = currency with { SymbolToPrice = await GetExchangeRatesFor(currency) };
        return currency;
    }

    private async Task<IImmutableDictionary<string, double>> GetExchangeRatesFor(Currency currency)
    {
        var dto = await GetDTO<DTO<IEnumerable<RatesData>>>(ExchangeRatesRequestURL);
        var priceUSD = currency.SymbolToPrice["usd"];
        var symbolToPrice = new Dictionary<string, double>(currency.SymbolToPrice);
        foreach (var d in dto.Data)
        {
            if (d.Symbol == "USD") continue;
            var exchangeRate = priceUSD / double.Parse(d.RateUsd, CultureInfo.InvariantCulture);
            symbolToPrice.Add(d.Symbol.ToLower(), exchangeRate);
        }

        return symbolToPrice.ToImmutableDictionary();
    }

    public override Task<string?> SearchAsync(string query)
    {
        // var dto = await GetDTO<DTO<IEnumerable<CurrencyData>>>()
        throw new NotImplementedException();
    }

    public override Task<IImmutableList<string>> GetTop10Async()
    {
        throw new NotImplementedException();
    }

    public override Task<IImmutableList<Candle>> GetCandles(string id, int days = 7, string referenceCurrency = "usd")
    {
        throw new NotImplementedException();
    }
}