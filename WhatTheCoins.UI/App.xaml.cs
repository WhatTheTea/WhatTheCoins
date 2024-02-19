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
        Locator.CurrentMutable.RegisterConstantAnd(new HttpClient(), typeof(HttpClient))
            .RegisterConstantAnd(() => new CoinGeckoApiProvider(Locator.Current.GetService<HttpClient>() ?? throw new ArgumentNullException()))
            .RegisterConstant(() => new CoinCapApiProvider(Locator.Current.GetService<HttpClient>() ?? throw new ArgumentNullException()));
    }
}