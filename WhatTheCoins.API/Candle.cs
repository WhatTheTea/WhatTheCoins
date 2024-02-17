namespace WhatTheCoins.API;

public sealed record Candle(
    DateTime DateTime,
    double Open,
    double High,
    double Low,
    double Close
);