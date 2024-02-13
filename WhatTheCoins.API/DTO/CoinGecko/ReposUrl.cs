using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinGecko;

internal record ReposUrl(
    [property: JsonPropertyName("github")] IReadOnlyList<string> Github,
    [property: JsonPropertyName("bitbucket")] IReadOnlyList<object> Bitbucket
);