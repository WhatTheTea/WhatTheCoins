using System.Reactive.Linq;
using ReactiveUI;
using Splat;
using WhatTheCoins.API;
using WhatTheCoins.API.ApiProviders;

namespace WhatTheCoins.UI.ViewModels;

public class SearchPageViewModel : ReactiveObject
{
    private readonly ObservableAsPropertyHelper<bool> _isAvailable;
    private readonly ICurrencyService _currencyService;
    private readonly ObservableAsPropertyHelper<IEnumerable<CurrencyViewModel>> _searchResults;
    private string _searchTerm;
    public string SearchTerm
    {
        
        get => _searchTerm;
        set => this.RaiseAndSetIfChanged(ref _searchTerm, value);
    }
    public IEnumerable<CurrencyViewModel> SearchResults => _searchResults.Value;
    public bool IsAvailable => _isAvailable.Value;
    

    public SearchPageViewModel(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
        _searchResults = this.WhenAnyValue(x => x.SearchTerm)
            .Throttle(TimeSpan.FromMilliseconds(800))
            .Select(term => term?.Trim())
            .DistinctUntilChanged()
            .Where(term => !string.IsNullOrWhiteSpace(term))
            .SelectMany(SearchCurrenciesAsync)
            .ObserveOn(RxApp.MainThreadScheduler)
            .ToProperty(this, x => x.SearchResults);
    }

    private async Task<IEnumerable<CurrencyViewModel>> SearchCurrenciesAsync(string term, CancellationToken token)
    {
        var apiResponse = await _currencyService.SearchAsync(term);
        return apiResponse.Select(c => new CurrencyViewModel(c));
    }
}