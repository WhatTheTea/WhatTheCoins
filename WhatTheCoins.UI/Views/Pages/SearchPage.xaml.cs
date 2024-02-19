using System.Reactive.Disposables;
using System.Windows.Controls;
using ReactiveUI;
using Splat;
using WhatTheCoins.UI.ViewModels;

namespace WhatTheCoins.UI.Views.Pages;

public partial class SearchPage
{
    public SearchPage()
    {
        InitializeComponent();
        ViewModel = Locator.Current.GetService<SearchPageViewModel>();
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