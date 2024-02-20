using Splat;
using WhatTheCoins.API;
using WhatTheCoins.API.ApiProviders;

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