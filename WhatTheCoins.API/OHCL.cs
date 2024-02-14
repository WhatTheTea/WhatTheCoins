namespace WhatTheCoins.API;

public record OHCL
(
    DateTime DateTime,
    double Open,
    double High,
    double Low,
    double Close
);