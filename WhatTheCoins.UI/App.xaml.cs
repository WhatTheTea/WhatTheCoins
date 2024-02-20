using Microsoft.Extensions.Hosting;
using ReactiveUI;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

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

                services.AddWhatTheCoinsApi()
                    .AddWhatTheCoinUiViewModels()
                    .AddWhatTheCoinsViews()
                    ;
            })
            .UseEnvironment(Environments.Development)
            .Build();
        Container = host.Services;
        Container.UseMicrosoftDependencyResolver();
    }
}