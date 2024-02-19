using System.Net.Http;
using System.Reactive.Linq;
using ReactiveUI;
using Splat;
using WhatTheCoins.API;
using WhatTheCoins.API.ApiProviders;

namespace WhatTheCoins.UI.ViewModels;

public class AppViewModel : ReactiveObject
{
    private string _searchTerm;

    public string SearchTerm
    {
        get => _searchTerm;
        set => this.RaiseAndSetIfChanged(ref _searchTerm, value);
    }

    private readonly ObservableAsPropertyHelper<IEnumerable<CurrencyViewModel>> _searchResults;
    public IEnumerable<CurrencyViewModel> SearchResults => _searchResults.Value;

    private readonly ObservableAsPropertyHelper<bool> _isAvailable;
    public bool IsAvailable => _isAvailable.Value;

    private readonly ICurrencyService _currencyService =
        new CurrencyService(Locator.Current.GetService<CoinCapApiProvider>()!);

    public AppViewModel()
    {
        _searchResults = this.WhenAnyValue(x => x.SearchTerm)
            .Throttle(TimeSpan.FromMilliseconds(800))
            .Select(term => term?.Trim())
            .DistinctUntilChanged()
            .Where(term => !string.IsNullOrWhiteSpace(term))
            .SelectMany(SearchCurrencies)
            .ObserveOn(RxApp.MainThreadScheduler)
            .ToProperty(this, x => x.SearchResults);
    }

    private async Task<IEnumerable<CurrencyViewModel>> SearchCurrencies(string term, CancellationToken token)
    {
        var apiResponse = await _currencyService.SearchAsync(term);
        return apiResponse.Select(c => new CurrencyViewModel(c));
    }
}