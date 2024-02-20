﻿using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using WhatTheCoins.API;
using WhatTheCoins.API.ApiProviders;
using WhatTheCoins.UI.ViewModels;
using WhatTheCoins.UI.ViewModels.Controls;
using WhatTheCoins.UI.ViewModels.Pages;
using WhatTheCoins.UI.Views;
using WhatTheCoins.UI.Views.Controls;
using WhatTheCoins.UI.Views.Pages;

namespace WhatTheCoins.UI;

public static class IoCExtensions
{
    public static IServiceCollection AddWhatTheCoinsApi(this IServiceCollection services) =>
        services.AddHttpClient()
            .AddSingleton<ICurrencyService, CurrencyService>()
            .AddSingleton<IApiProvider, CoinCapApiProvider>();


    public static IServiceCollection AddWhatTheCoinUiViewModels(this IServiceCollection services) =>
        services.AddSingleton<MainWindowViewModel>()
            .AddSingleton<TopPageViewModel>()
            .AddSingleton<SearchPageViewModel>();

    public static IServiceCollection AddWhatTheCoinsViews(this IServiceCollection services) =>
        services
            .AddTransient<IViewFor<CurrencyViewModel>, CurrencyView>()
            .AddSingleton<IViewFor<SearchPageViewModel>, SearchPageView>()
            .AddSingleton<IViewFor<SearchViewModel>, SearchView>();
}