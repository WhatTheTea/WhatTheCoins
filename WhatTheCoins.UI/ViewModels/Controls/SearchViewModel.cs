using System.Diagnostics;
using System.Reactive.Linq;
using ReactiveUI;
using Splat;
using WhatTheCoins.API;

namespace WhatTheCoins.UI.ViewModels.Controls;

public class SearchViewModel : ReactiveObject, IRoutableViewModel
{
    private readonly ICurrencyService _currencyService;
    private readonly ObservableAsPropertyHelper<bool> _isAvailable;
    private readonly ObservableAsPropertyHelper<IEnumerable<CurrencyViewModel>> _searchResults;
    private string _searchTerm;


    public SearchViewModel(ICurrencyService currencyService, IScreen? hostScreen = null)
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
    }

    public string SearchTerm
    {
        get => _searchTerm;
        set => this.RaiseAndSetIfChanged(ref _searchTerm, value);
    }

    public IEnumerable<CurrencyViewModel> SearchResults => _searchResults.Value;
    public bool IsAvailable => _isAvailable.Value;

    private async Task<IEnumerable<CurrencyViewModel>> SearchCurrenciesAsync(string term, CancellationToken token)
    {
        var apiResponse = await _currencyService.SearchAsync(term);
        return apiResponse.Select(c => new CurrencyViewModel(c));
    }

    public string? UrlPathSegment => "search";
    public IScreen HostScreen { get; }
}