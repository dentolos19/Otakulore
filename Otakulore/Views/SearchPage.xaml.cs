using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Otakulore.Models;
using Otakulore.ViewModels;

namespace Otakulore.Views;

public partial class SearchPage
{

    private readonly BackgroundWorker _animeSearcher;
    private readonly BackgroundWorker _mangaSearcher;

    private SearchViewModel ViewModel => (SearchViewModel)DataContext;

    public SearchPage(string query)
    {
        _animeSearcher = new BackgroundWorker();
        _mangaSearcher = new BackgroundWorker();
        _animeSearcher.DoWork += async (_, _) =>
        {
            try
            {
                var animeResults = await App.Jikan.SearchAnime(query);
                Dispatcher.Invoke(() =>
                {
                    foreach (var animeResult in animeResults.Results)
                        AnimeSearchList.Items.Add(MediaItemModel.Create(animeResult));
                    ViewModel.HasAnimeSearched = true;
                });
            }
            catch
            {
                // do nothing
            }
        };
        _animeSearcher.DoWork += async (_, _) =>
        {
            try
            {
                var mangaResults = await App.Jikan.SearchManga(query);
                Dispatcher.Invoke(() =>
                {
                    foreach (var mangaResult in mangaResults.Results)
                        MangaSearchList.Items.Add(MediaItemModel.Create(mangaResult));
                    ViewModel.HasMangaSearched = true;
                });
            }
            catch
            {
                // do nothing
            }
        };
        InitializeComponent();
    }

    private void OnTabChanged(object sender, SelectionChangedEventArgs args)
    {
        if (AnimeTab.IsSelected && !ViewModel.HasAnimeSearched)
            _animeSearcher.RunWorkerAsync();
        if (MangaTab.IsSelected && !ViewModel.HasMangaSearched)
            _mangaSearcher.RunWorkerAsync();
    }

    private void OnOpenAnime(object sender, MouseButtonEventArgs args)
    {
        if (AnimeSearchList.SelectedItem is MediaItemModel item)
            NavigationService.Navigate(new DetailsPage(item.Type, item.Id));
    }

    private void OnOpenManga(object sender, MouseButtonEventArgs args)
    {
        if (AnimeSearchList.SelectedItem is MediaItemModel item)
            NavigationService.Navigate(new DetailsPage(item.Type, item.Id));
    }

}