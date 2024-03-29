﻿using System.Collections.Immutable;

namespace WhatTheCoins.API;

public interface IApiProvider
{
    Task<Currency> GetByIdAsync(string id);
    Task<IImmutableList<string>> SearchAsync(string query);
    Task<IImmutableList<string>> GetTopAsync();
    Task<IImmutableList<Candle>> GetCandles(string id, int days, string referenceCurrency);
}