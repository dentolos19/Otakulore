using Otakulore.Core;
using Otakulore.Models;
using Otakulore.ViewModels;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using ModernWpf.Controls;

namespace Otakulore.Views;

public partial class SearchPage
{

    private readonly BackgroundWorker _searchWorker;

    private SearchViewModel ViewModel => (SearchViewModel)DataContext;

    public SearchPage()
    {
        _searchWorker = new BackgroundWorker { WorkerSupportsCancellation = true };
        _searchWorker.DoWork += async (_, args) =>
        {
            if (args.Argument is not ObjectData data)
                return;
            Dispatcher.Invoke(() =>
            {
                ViewModel.HasSearchFinished = false;
                ViewModel.SearchResults.Clear();
            });
            try
            {
                if (data.MediaType == MediaType.Anime)
                {
                    var searchResults = await App.Jikan.SearchAnime(data.Query);
                    Dispatcher.Invoke(() =>
                    {
                        if (searchResults != null && searchResults.Results.Count > 0)
                            foreach (var searchResult in searchResults.Results)
                                ViewModel.SearchResults.Add(MediaItemModel.Create(searchResult));
                    });
                }
                else if (data.MediaType == MediaType.Manga)
                {
                    var searchResults = await App.Jikan.SearchManga(data.Query);
                    Dispatcher.Invoke(() =>
                    {
                        if (searchResults != null && searchResults.Results.Count > 0)
                            foreach (var searchResult in searchResults.Results)
                                ViewModel.SearchResults.Add(MediaItemModel.Create(searchResult));
                    });
                }
            }
            catch
            {
                // do nothing
            }
            Dispatcher.Invoke(() => ViewModel.HasSearchFinished = true);
        };
        InitializeComponent();
    }

    private void Search(int typeIndex, string query)
    {
        _searchWorker.CancelAsync();
        _searchWorker.RunWorkerAsync(new ObjectData { MediaType = (MediaType)typeIndex, Query = query });
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.ExtraData is not ObjectData data)
            return;
        SearchInput.Text = data.Query;
        TypeSelection.SelectedIndex = 0;
    }

    protected override void OnNavigatedFrom(NavigationEventArgs args)
    {
        _searchWorker.CancelAsync();
    }

    private void OnSearch(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        Search(TypeSelection.SelectedIndex, SearchInput.Text);
    }

    private void OnTypeChanged(object sender, SelectionChangedEventArgs args)
    {
        Search(TypeSelection.SelectedIndex, SearchInput.Text);
    }

    private void OnOpenMedia(object sender, MouseButtonEventArgs args)
    {
        if (ResultList.SelectedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), new ObjectData { MediaType = item.Type, Id = item.Id });
    }

}