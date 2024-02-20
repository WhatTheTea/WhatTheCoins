using System.Reactive;
using ReactiveUI;
using WhatTheCoins.API;
using WhatTheCoins.UI.ViewModels.Controls;

namespace WhatTheCoins.UI.ViewModels.Pages;

public class SearchPageViewModel : ReactiveObject, IScreen
{
    public SearchPageViewModel(ICurrencyService currencyService)
    {
        Router = new RoutingState();
        GoNext = ReactiveCommand.CreateFromObservable((IRoutableViewModel model) => Router.Navigate.Execute(model));
        GoBack = ReactiveCommand.CreateFromObservable(() => Router.NavigateBack.Execute(Unit.Default));

        GoNext.Execute(new SearchViewModel(currencyService, this)).Subscribe();
    }

    public ReactiveCommand<IRoutableViewModel, IRoutableViewModel> GoNext { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoBack { get; }
    public RoutingState Router { get; }
}