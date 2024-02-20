using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReactiveUI;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;
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

    private IServiceProvider Container { get; set; }

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
            .UseEnvironment(Environments.Development)
            .Build();
        Container = host.Services;
        Container.UseMicrosoftDependencyResolver();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient()
            .AddSingleton<ICurrencyService, CurrencyService>()
            .AddSingleton<IApiProvider, CoinCapApiProvider>();

        services.AddSingleton<MainWindowViewModel>()
            .AddTransient<SearchPageViewModel>()
            .AddTransient<TopPageViewModel>();

        services.AddSingleton<IViewFor<MainWindowViewModel>, MainWindow>()
            .AddTransient<IViewFor<SearchPageViewModel>, SearchPage>()
            .AddTransient<IViewFor<CurrencyViewModel>, CurrencyView>()
            .AddTransient<IViewFor<TopPageViewModel>, Top10Page>();
    }
}