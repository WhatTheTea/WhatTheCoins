using System.Collections.Immutable;
using WhatTheCoins.API.ApiProviders;

namespace WhatTheCoins.API;

public sealed record Currency(
    string Id,
    string Symbol,
    double Volume,
    double PriceChange,
    IImmutableDictionary<string, double> SymbolToPrice,
    IImmutableList<string> MarketPlaces
);