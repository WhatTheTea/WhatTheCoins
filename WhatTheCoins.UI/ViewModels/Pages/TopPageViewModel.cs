using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using WhatTheCoins.API;
using WhatTheCoins.API.ApiProviders;
using WhatTheCoins.UI.ViewModels.Controls;

namespace WhatTheCoins.UI.ViewModels.Pages;

public class TopPageViewModel : ReactiveObject, IScreen
{
    
public RoutingState Router { get; }
public ReactiveCommand<IRoutableViewModel, IRoutableViewModel> GoNext { get; }
public ReactiveCommand<Unit, IRoutableViewModel> GoBack { get; }

public TopPageViewModel(ICurrencyService currencyService)
{
    Router = new RoutingState();
    GoNext = ReactiveCommand.CreateFromObservable((IRoutableViewModel model) => Router.Navigate.Execute(model));
    GoBack = ReactiveCommand.CreateFromObservable(() => Router.NavigateBack.Execute(Unit.Default));

    GoNext.Execute(new TopViewModel(currencyService, this)).Subscribe();
}
}