using System.Net.Http;
using System.Reflection;
using System.Windows;
using ReactiveUI;
using Splat;
using WhatTheCoins.API;
using WhatTheCoins.API.ApiProviders;

namespace WhatTheCoins.UI;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public App()
    {
        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
        Locator.CurrentMutable.RegisterConstant(new HttpClient(), typeof(HttpClient));
        Locator.CurrentMutable.RegisterConstant(
            new CoinGeckoApiProvider(Locator.Current.GetService<HttpClient>()!));
        Locator.CurrentMutable.RegisterConstant(new CoinCapApiProvider(Locator.Current.GetService<HttpClient>()!));
    }
}