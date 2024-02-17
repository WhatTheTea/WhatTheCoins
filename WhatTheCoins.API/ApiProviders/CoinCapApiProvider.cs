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

    public override async Task<string?> SearchAsync(string query)
    {
        var dto = await GetDTO<DTO<IEnumerable<CurrencyData>>>(AssetsDataRequestURL);
        return dto.Data.Where(d => d.Symbol.Contains(query, StringComparison.InvariantCultureIgnoreCase)
                                   || d.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase) ||
                                   d.Id.Contains(query, StringComparison.InvariantCultureIgnoreCase))
            .Select(x => x.Id).FirstOrDefault();
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