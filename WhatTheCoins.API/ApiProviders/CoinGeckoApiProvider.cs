using System.Collections.Immutable;

namespace WhatTheCoins.API.ApiProviders;

public class CoinGeckoApiProvider(HttpClient httpClient) : ApiProviderBase(httpClient)
{
    private const string CurrencyDataRequest = "https://api.coingecko.com/api/v3/coins/{0}";
    private const string CandlesDataRequest =
        "https://api.coingecko.com/api/v3/coins/{0}/ohlc?vs_currency={1}&days={2}";
    private const string SearchRequestURL = "https://api.coingecko.com/api/v3/search?query={0}";
    private static readonly ImmutableArray<string> MarketPlacesURL = [
        "https://www.coingecko.com/en/coins/{0}",
        "https://coincap.io/assets/{0}"
    ];
    public override async Task<Currency> GetByIdAsync(string id)
    {   // TODO: Own exceptions
        var dto = await GetDTO<DTO.CoinGecko.Currency.DTO>(string.Format(CurrencyDataRequest, id)) ?? throw new Exception("id not found on CoinGecko");
        var marketPlaces = BuildMarketPlaces(dto.Id);
        var currency = new Currency(dto.Id, dto.Symbol,
            dto.MarketData.TotalVolume["usd"],
            dto.MarketData.PriceChange24h ?? 0,
            dto.MarketData.CurrentPrice.ToImmutableDictionary(),
            marketPlaces
        );
        return currency;
    }
    private static ImmutableArray<string> BuildMarketPlaces(string id)
    {
        var marketPlaces = MarketPlacesURL.Select(s => string.Format(s, id))
            .ToImmutableArray();
        return marketPlaces;
    }

    public override async Task<string?> SearchAsync(string query)
    {
        var dto = await GetDTO<DTO.CoinGecko.Search.DTO>(string.Format(SearchRequestURL, query));
        return dto?.Coins[0].Id;
    }

    public override Task<IImmutableList<string>> GetTop10Async()
    {
        throw new NotImplementedException();
    }
    public override async Task<IImmutableList<Candle>> GetCandles(string id, int days = 7, string referenceCurrency = "usd")
    {
        var rawCandles = await GetDTO<ImmutableArray<ImmutableArray<double>>>(
            string.Format(CandlesDataRequest,
            id,
            days,
            referenceCurrency));
        // Unpack data to Candles
        return rawCandles.Select(arr =>
            new Candle(ConvertUnixToDateTime((long)arr[0]),
                arr[1], arr[2], arr[3], arr[4])
        ).ToImmutableArray();
    }
    private static DateTime ConvertUnixToDateTime(long ms) => 
        DateTimeOffset.FromUnixTimeMilliseconds(ms).DateTime;
}