using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using ReactiveUI;
using WhatTheCoins.API;

namespace WhatTheCoins.UI.ViewModels.Controls;

public class CurrencyInfoViewModel : CurrencyViewModel, IRoutableViewModel
{
    public CurrencyInfoViewModel(Currency currency, IEnumerable<Candle> candles, IScreen screen) : base(currency, candles)
    {
        HostScreen = screen;
        
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

    public string? UrlPathSegment => "currency";
    public IScreen HostScreen { get; }
    public ISeries[] Candlesticks { get; }
    public Axis[] XAxes { get; }
}