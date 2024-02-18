using System.Collections.ObjectModel;
using System.Net.Http;
using WhatTheCoins.API;
using WhatTheCoins.API.ApiProviders;

namespace WhatTheCoins.UI.ViewModels;

public class Top10ViewModel
{
    public ObservableCollection<string> Top10Currencies;
    public static readonly ICurrencyService service = new API.CurrencyService(new CoinGeckoApiProvider(new HttpClient()));

    public async Task LoadTop10Currencies()
    {
        var k = ;
    }
}