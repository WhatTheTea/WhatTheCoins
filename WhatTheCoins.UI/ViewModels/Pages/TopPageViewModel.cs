using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using WhatTheCoins.API;
using WhatTheCoins.UI.ViewModels.Controls;

namespace WhatTheCoins.UI.ViewModels.Pages;

public class TopPageViewModel : ReactiveObject, IRoutableViewModel
{
    private readonly ICurrencyService _currencyService;


    public TopPageViewModel(ICurrencyService currencyService)
    {
        _currencyService = currencyService;

        LoadTopCurrencies = ReactiveCommand.CreateFromTask(async _ => await GetTopCurrenciesAsync());
        LoadTopCurrencies.ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(currencies => TopCurrencies = currencies);
        LoadTopCurrencies.Execute().Subscribe();
    }

    [Reactive] public IEnumerable<CurrencyViewModel> TopCurrencies { get; private set; }

    public ReactiveCommand<Unit, IEnumerable<CurrencyViewModel>> LoadTopCurrencies { get; }
// TODO
    private async Task<IEnumerable<CurrencyViewModel>> GetTopCurrenciesAsync()
    {
        var apiResponse = await _currencyService.GetTop10Async();
        return await Task.WhenAll(apiResponse.Select(GetCurrencyViewModel));
    }
    private async Task<CurrencyViewModel> GetCurrencyViewModel(Currency currency)
    {
        var candles = await _currencyService.GetCandles(currency.Id);
        return new CurrencyInfoViewModel(currency, candles, HostScreen);
    }
// END 
    public string? UrlPathSegment => "top";
    public IScreen HostScreen { get; }
}