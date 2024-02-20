using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using WhatTheCoins.API;

namespace WhatTheCoins.UI.ViewModels;

public class TopPageViewModel : ReactiveObject
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

    private async Task<IEnumerable<CurrencyViewModel>> GetTopCurrenciesAsync()
    {
        var apiResponse = await _currencyService.GetTop10Async();
        return apiResponse.Select(currency => new CurrencyViewModel(currency));
    }
}