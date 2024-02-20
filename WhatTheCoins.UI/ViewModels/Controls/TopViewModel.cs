using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using WhatTheCoins.API;
using WhatTheCoins.API.ApiProviders;

namespace WhatTheCoins.UI.ViewModels.Controls;

public class TopViewModel : ReactiveObject, IRoutableViewModel
{
    private readonly ICurrencyService _currencyService;

// END 
    public TopViewModel(ICurrencyService currencyService, IScreen hostScreen)
    {
        _currencyService = currencyService;
        HostScreen = hostScreen;

        LoadTopCurrencies = ReactiveCommand.CreateFromTask(async _ => await GetTopCurrenciesAsync());
        LoadTopCurrencies.ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(currencies => TopCurrencies = currencies);
        LoadTopCurrencies.Execute().Subscribe();
    }

    [Reactive] public IEnumerable<CurrencyViewModel> TopCurrencies { get; private set; }

    public ReactiveCommand<Unit, IEnumerable<CurrencyViewModel>> LoadTopCurrencies { get; }
    public string? UrlPathSegment => "top";
    public IScreen HostScreen { get; }

// TODO
    private async Task<IEnumerable<CurrencyViewModel>> GetTopCurrenciesAsync()
    {
        var apiResponse = await _currencyService.With<CoinCapApiProvider>().GetTopAsync();
        return await Task.WhenAll(apiResponse.Select(GetCurrencyViewModel));
    }

    private async Task<CurrencyViewModel> GetCurrencyViewModel(Currency currency)
    {
        var candles = await _currencyService.With<CoinGeckoApiProvider>().GetCandles(currency.Id);
        return new CurrencyItemViewModel(currency, candles, HostScreen);
    }
}