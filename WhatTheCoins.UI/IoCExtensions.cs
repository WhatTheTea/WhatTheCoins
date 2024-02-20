using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using WhatTheCoins.API;
using WhatTheCoins.API.ApiProviders;
using WhatTheCoins.UI.ViewModels;
using WhatTheCoins.UI.ViewModels.Controls;
using WhatTheCoins.UI.ViewModels.Pages;
using WhatTheCoins.UI.Views.Controls;
using WhatTheCoins.UI.Views.Pages;

namespace WhatTheCoins.UI;

public static class IoCExtensions
{
    public static IServiceCollection AddWhatTheCoinsApi(this IServiceCollection services)
    {
        return services.AddHttpClient()
            .AddSingleton<ICurrencyService, CurrencyService>()
            .AddSingleton<CoinCapApiProvider>()
            .AddSingleton<CoinGeckoApiProvider>()
            .AddSingleton<IApiProvider,
                CoinCapApiProvider>(factory => Locator.Current.GetService<CoinCapApiProvider>());
    }


    public static IServiceCollection AddWhatTheCoinUiViewModels(this IServiceCollection services)
    {
        return services.AddSingleton<MainWindowViewModel>()
            .AddSingleton<TopPageViewModel>()
            .AddSingleton<SearchPageViewModel>();
    }

    public static IServiceCollection AddWhatTheCoinsViews(this IServiceCollection services)
    {
        return services
            .AddTransient<IViewFor<CurrencyItemViewModel>, CurrencyItemView>()
            .AddTransient<IViewFor<CurrencyInfoViewModel>, CurrencyInfoView>()
            .AddSingleton<IViewFor<SearchPageViewModel>, SearchPageView>()
            .AddSingleton<IViewFor<SearchViewModel>, SearchView>()
            .AddSingleton<IViewFor<TopPageViewModel>, TopPage>()
            .AddSingleton<IViewFor<TopViewModel>, TopView>();
    }
}