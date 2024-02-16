using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinCap.Currency;

internal record DTO(
    [property: JsonPropertyName("data")] Data Data,
    [property: JsonPropertyName("timestamp")]
    long Timestamp) : CurrencyDTO
{
    internal override API.Currency ToCurrency()
    {
        throw new NotImplementedException();
    }
}