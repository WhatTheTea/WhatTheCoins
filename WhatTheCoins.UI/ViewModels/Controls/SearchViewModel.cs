using System.Reactive.Linq;
using LiveChartsCore.Kernel;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using WhatTheCoins.API;
using WhatTheCoins.API.ApiProviders;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace WhatTheCoins.UI.ViewModels.Controls;

public class SearchViewModel : ReactiveObject, IRoutableViewModel
{
    private readonly ICurrencyService _currencyService;
    private readonly ObservableAsPropertyHelper<bool> _isAvailable;
    private readonly ObservableAsPropertyHelper<IEnumerable<CurrencyViewModel>> _searchResults;

    public SearchViewModel(ICurrencyService currencyService, ISnackbarService snackbarService, IScreen? hostScreen = null)
    {
        _currencyService = currencyService;
        HostScreen = hostScreen;
        _searchResults = this.WhenAnyValue(x => x.SearchTerm)
            .Throttle(TimeSpan.FromMilliseconds(800))
            .Select(term => term?.Trim())
            .DistinctUntilChanged()
            .Where(term => !string.IsNullOrWhiteSpace(term))
            .SelectMany(SearchCurrenciesAsync)
            .ObserveOn(RxApp.MainThreadScheduler)
            .ToProperty(this, x => x.SearchResults);
        _searchResults.ThrownExceptions.Subscribe(e => snackbarService.Show("Error",
            e.Message,
            ControlAppearance.Danger,
            null,
            TimeSpan.FromSeconds(5)));
    }

    [Reactive] public string SearchTerm { get; set; }

    public IEnumerable<CurrencyViewModel> SearchResults => _searchResults.Value;
    public bool IsAvailable => _isAvailable.Value;

    public string? UrlPathSegment => "search";
    public IScreen HostScreen { get; }

    private async Task<IEnumerable<CurrencyViewModel>> SearchCurrenciesAsync(string term, CancellationToken token)
    {
        var currencies = await _currencyService.With<CoinCapApiProvider>().SearchAsync(term);
        return await Task.WhenAll(currencies.Select(async c => await GetCurrencyViewModel(c)));
    }

    private async Task<CurrencyViewModel> GetCurrencyViewModel(Currency currency)
    {
        var candles = await _currencyService.With<CoinGeckoApiProvider>().GetCandles(currency.Id);
        return new CurrencyItemViewModel(currency, candles, HostScreen);
    }
}