namespace WhatTheCoins.API;

public sealed record OHCL
(
    DateTime DateTime,
    double Open,
    double High,
    double Low,
    double Close
);