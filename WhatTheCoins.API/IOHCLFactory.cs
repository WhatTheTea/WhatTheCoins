using System.Collections.Immutable;

namespace WhatTheCoins.API;

public interface IOHCLFactory
{
    public IOHCLFactory SetReferenceCurrency(string currency);
    public IOHCLFactory SetDays(int days);
    public Task<IImmutableList<OHCL>> MakeOHCLs(string currencyId);
}