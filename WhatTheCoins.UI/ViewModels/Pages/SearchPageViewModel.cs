using System.Reactive;
using ReactiveUI;
using Splat;
using WhatTheCoins.API;
using WhatTheCoins.UI.ViewModels.Controls;
using Wpf.Ui;

namespace WhatTheCoins.UI.ViewModels.Pages;

public class SearchPageViewModel : ReactiveObject, IScreen
{
    public SearchPageViewModel(ICurrencyService currencyService, ISnackbarService snackbarService)
    {
        Router = new RoutingState();
        GoNext = ReactiveCommand.CreateFromObservable((IRoutableViewModel model) => Router.Navigate.Execute(model));
        GoBack = ReactiveCommand.CreateFromObservable(() => Router.NavigateBack.Execute(Unit.Default));

        GoNext.Execute(new SearchViewModel(currencyService, snackbarService, this)).Subscribe();
    }

    public ReactiveCommand<IRoutableViewModel, IRoutableViewModel> GoNext { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> GoBack { get; }
    public RoutingState Router { get; }
}