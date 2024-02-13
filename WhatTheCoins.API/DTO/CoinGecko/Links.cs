using System.Text.Json.Serialization;

namespace WhatTheCoins.API.DTO.CoinGecko;

public record Links(
    [property: JsonPropertyName("homepage")] IReadOnlyList<string> Homepage,
    [property: JsonPropertyName("whitepaper")] string Whitepaper,
    [property: JsonPropertyName("blockchain_site")] IReadOnlyList<string> BlockchainSite,
    [property: JsonPropertyName("official_forum_url")] IReadOnlyList<string> OfficialForumUrl,
    [property: JsonPropertyName("twitter_screen_name")] string TwitterScreenName,
    [property: JsonPropertyName("facebook_username")] string FacebookUsername,
    [property: JsonPropertyName("subreddit_url")] string SubredditUrl,
    [property: JsonPropertyName("repos_url")] ReposUrl ReposUrl
);