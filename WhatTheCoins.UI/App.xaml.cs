using System.ComponentModel;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using Microsoft.Extensions.Hosting;
using Splat.Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WhatTheCoins.API;
using WhatTheCoins.API.ApiProviders;
using WhatTheCoins.UI.ViewModels;
using WhatTheCoins.UI.Views;
using WhatTheCoins.UI.Views.Controls;
using WhatTheCoins.UI.Views.Pages;

namespace WhatTheCoins.UI;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public App()
    {
        InitIoC();
    }
    public IServiceProvider Container { get; private set; }

    private void InitIoC()
    {
        var host = Host
            .CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.UseMicrosoftDependencyResolver();
                var resolver = Locator.CurrentMutable;
                resolver.InitializeSplat();
                resolver.InitializeReactiveUI();
                ConfigureServices(services);
            })
            .ConfigureLogging(loggingBuilder =>
            {})
            .UseEnvironment(Environments.Development)
            .Build();
        Container = host.Services;
        Container.UseMicrosoftDependencyResolver();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient()
            .AddSingleton<ICurrencyService, CurrencyService>()
            .AddSingleton<IApiProvider, CoinCapApiProvider>();

        services.AddSingleton<AppViewModel>()
            .AddTransient<SearchPageViewModel>();

        services.AddSingleton<IViewFor<AppViewModel>, MainWindow>()
            .AddTransient<IViewFor<SearchPageViewModel>, SearchPage>()
            .AddTransient<IViewFor<CurrencyViewModel>, CurrencyView>();
    }
}