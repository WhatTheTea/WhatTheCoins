using System.Reactive.Disposables;
using System.Windows.Controls;
using ReactiveUI;

namespace WhatTheCoins.UI.Views.Controls;

public partial class TopView
{
    public TopView()
    {
        InitializeComponent();

        this.WhenActivated(d =>
        {
            this.OneWayBind(ViewModel,
                    vm => vm.TopCurrencies,
                    v => v.TopListBox.ItemsSource)
                .DisposeWith(d);
        });
    }
}