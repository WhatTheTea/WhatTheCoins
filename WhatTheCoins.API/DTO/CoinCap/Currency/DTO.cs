using System.Collections.Immutable;
using System.Globalization;
using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinCap.Currency;

internal record DTO(
    [property: JsonPropertyName("data")] Data Data,
    [property: JsonPropertyName("timestamp")]
    long Timestamp) : CurrencyDTO
{
    internal override API.Currency ToCurrency() => new(
        Id: Data.Id.ToLower(),
        Symbol: Data.Symbol.ToLower(),
        Volume: ConvertToDouble(Data.VolumeUsd24Hr),
        PriceChange: ConvertToDouble(Data.ChangePercent24Hr),
        SymbolToPrice:new Dictionary<string, double> {
            {"usd", ConvertToDouble(Data.PriceUsd)}
        }.ToImmutableDictionary(), 
        MarketPlaces:BuildMarketPlaces(Data.Id)
    );
}