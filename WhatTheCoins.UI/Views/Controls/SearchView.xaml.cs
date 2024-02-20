using System.Reactive.Disposables;
using ReactiveUI;

namespace WhatTheCoins.UI.Views.Controls;

public partial class SearchView
{
    public SearchView()
    {
        InitializeComponent();
        this.WhenActivated(disposableRegistration =>
        {
            this.OneWayBind(ViewModel,
                    viewModel => viewModel.IsAvailable,
                    view => view.SearchResultsListBox.Visibility)
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.SearchResults,
                    view => view.SearchResultsListBox.ItemsSource)
                .DisposeWith(disposableRegistration);

            this.Bind(ViewModel,
                    viewModel => viewModel.SearchTerm,
                    view => view.SearchTextBox.Text)
                .DisposeWith(disposableRegistration);
        });
    }
}