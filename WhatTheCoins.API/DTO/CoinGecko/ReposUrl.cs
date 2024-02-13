using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinGecko;

public record ReposUrl(
    [property: JsonPropertyName("github")] IReadOnlyList<string> Github,
    [property: JsonPropertyName("bitbucket")] IReadOnlyList<object> Bitbucket
);