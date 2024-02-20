using System.Reactive;
using ReactiveUI;
using WhatTheCoins.API;

namespace WhatTheCoins.UI.ViewModels.Controls;

public class CurrencyItemViewModel : CurrencyViewModel
{
    public ReactiveCommand<Unit, IRoutableViewModel> OpenCurrencyInfo;

    public CurrencyItemViewModel(Currency currency, IEnumerable<Candle> candles, IScreen screen) : base(currency,
        candles)
    {
        OpenCurrencyInfo =
            ReactiveCommand.CreateFromObservable(() =>
                screen.Router.Navigate.Execute(new CurrencyInfoViewModel(currency, candles, screen)));
    }
}