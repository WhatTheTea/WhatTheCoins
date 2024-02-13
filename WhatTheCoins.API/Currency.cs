namespace WhatTheCoins.API;

public record Currency(
    string Id,
    string Symbol,
    double Volume,
    double PriceChange,
    List<Seller> Sellers
    );