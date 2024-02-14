namespace WhatTheCoins.API;

public record Currency(
    string Id,
    string Symbol,
    double Volume,
    double PriceChange,
    IDictionary<string, double> MarketPrices
    );