using System.Collections.Immutable;

namespace WhatTheCoins.API.OHCLFactories;

public class OHCLFactory : IOHCLFactory
{
    public IOHCLFactory SetReferenceCurrency(string currency)
    {
        throw new NotImplementedException();
    }

    public IOHCLFactory SetDays(int days)
    {
        throw new NotImplementedException();
    }

    public Task<IImmutableList<OHCL>> MakeOHCLs(string currencyId)
    {
        throw new NotImplementedException();
    }
}