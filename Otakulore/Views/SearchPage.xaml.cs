using Otakulore.Core;
using Otakulore.Models;
using Otakulore.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Otakulore.Views;

public partial class SearchPage
{

    private readonly BackgroundWorker _searchWorker;

    private SearchViewModel ViewModel => (SearchViewModel)DataContext;

    public SearchPage()
    {
        _searchWorker = new BackgroundWorker();
        _searchWorker.WorkerSupportsCancellation = true;
        _searchWorker.DoWork += async (_, args) =>
        {
            if (args.Argument is not KeyValuePair<MediaType, string>(var type, var query))
                return;
            Dispatcher.Invoke(() =>
            {
                ViewModel.HasSearchFinished = false;
                ViewModel.SearchItems.Clear();
            });
            if (type == MediaType.Anime)
            {
                try
                {
                    var searchResults = await App.Jikan.SearchAnime(query);
                    Dispatcher.Invoke(() =>
                    {
                        if (!(searchResults.Results.Count > 0))
                            return;
                        foreach (var searchResult in searchResults.Results)
                            ViewModel.SearchItems.Add(MediaItemModel.Create(searchResult));
                    });
                }
                catch
                {
                    // do nothing
                }
            }
            else if (type == MediaType.Manga)
            {
                try
                {
                    var searchResults = await App.Jikan.SearchManga(query);
                    Dispatcher.Invoke(() =>
                    {
                        if (!(searchResults.Results.Count > 0))
                            return;
                        foreach (var searchResult in searchResults.Results)
                            ViewModel.SearchItems.Add(MediaItemModel.Create(searchResult));
                    });
                }
                catch
                {
                    // do nothing
                }
            }
            Dispatcher.Invoke(() => ViewModel.HasSearchFinished = true);
        };
        InitializeComponent();
    }

    private void Search(int typeIndex, string query)
    {
        _searchWorker.CancelAsync();
        _searchWorker.RunWorkerAsync(new KeyValuePair<MediaType, string>((MediaType)typeIndex, query));
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.ExtraData is not string query)
            return;
        SearchInput.Text = query;
        TypeSelection.SelectedIndex = 0;
    }

    protected override void OnNavigatedFrom(NavigationEventArgs args)
    {
        _searchWorker.CancelAsync();
    }

    private void OnSearch(object sender, KeyEventArgs args)
    {
        if (args.Key == Key.Enter)
            Search(TypeSelection.SelectedIndex, SearchInput.Text);
    }

    private void OnTypeChange(object sender, SelectionChangedEventArgs args)
    {
        Search(TypeSelection.SelectedIndex, SearchInput.Text);
    }

    private void OnOpenMedia(object sender, MouseButtonEventArgs args)
    {
        if (SearchList.SelectedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), new KeyValuePair<MediaType, long>(item.Type, item.Id));
    }

}