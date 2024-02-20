using Splat;
using WhatTheCoins.API;

namespace WhatTheCoins.UI;

public static class CurrencyServiceExtensions
{
    public static ICurrencyService With<TProvider>(this ICurrencyService service)
        where TProvider : IApiProvider
    {
        var provider = Locator.Current.GetService<TProvider>();
        service.ChangeApiProvider(provider);
        return service;
    }
}