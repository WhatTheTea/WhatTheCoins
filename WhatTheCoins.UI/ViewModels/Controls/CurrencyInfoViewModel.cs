using System.Reactive;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using ReactiveUI;
using WhatTheCoins.API;

namespace WhatTheCoins.UI.ViewModels.Controls;

public class CurrencyInfoViewModel : CurrencyViewModel, IRoutableViewModel
{
    public CurrencyInfoViewModel(Currency currency, IEnumerable<Candle> candles, IScreen screen) : base(currency,
        candles)
    {
        HostScreen = screen;
        GoBack = ReactiveCommand.CreateFromObservable(() => HostScreen.Router.NavigateBack.Execute(Unit.Default));
        Candlesticks =
        [
            new CandlesticksSeries<FinancialPointI>
            {
                Values = candles.Select(x => new FinancialPointI(x.High, x.Open, x.Close, x.Low))
            }
        ];
        XAxes =
        [
            new Axis
            {
                LabelsRotation = 15,
                Labels = candles.Select(x => x.DateTime.ToString("yy-MMM-dd")).ToArray()
            }
        ];
    }

    public ReactiveCommand<Unit, IRoutableViewModel> GoBack { get; }
    public ISeries[] Candlesticks { get; }
    public Axis[] XAxes { get; }
    public string? UrlPathSegment => "currency";
    public IScreen HostScreen { get; }
}