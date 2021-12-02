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
            if (args.Argument is not KeyValuePair<int, string> pair)
                return;
            Dispatcher.Invoke(() =>
            {
                ViewModel.HasNoSearchResult = false;
                ViewModel.SearchItems.Clear();
            });
            if (pair.Key == 1)
            {
                var searchResults = await App.JikanService.SearchManga(pair.Value);
                Dispatcher.Invoke(() =>
                {
                    if (searchResults.Results.Count > 0)
                    {
                        foreach (var searchResult in searchResults.Results)
                        {
                            var searchItem = new SearchItemModel
                            {
                                Type = SearchType.Manga,
                                ImageUrl = searchResult.ImageURL,
                                Title = searchResult.Title,
                                Year = searchResult.StartDate.HasValue ? searchResult.StartDate.Value.Year.ToString() : "????",
                                Contents = !searchResult.Chapters.HasValue ? "No chapters" : searchResult.Publishing ? "Progressing chapters" : $"{searchResult.Chapters} chapter(s)",
                                Status = searchResult.Publishing ? "Publishing" : "Finished",
                                Description = searchResult.Description,
                                Score = searchResult.Score.HasValue ? searchResult.Score.Value / 2 : -1
                            };
                            ViewModel.SearchItems.Add(searchItem);
                        }
                    }
                    else
                    {
                        ViewModel.HasNoSearchResult = true;
                    }
                });
            }
            else
            {
                var searchResults = await App.JikanService.SearchAnime(pair.Value);
                Dispatcher.Invoke(() =>
                {
                    if (searchResults.Results.Count > 0)
                    {
                        foreach (var searchResult in searchResults.Results)
                        {
                            var searchItem = new SearchItemModel
                            {
                                Type = SearchType.Anime,
                                ImageUrl = searchResult.ImageURL,
                                Title = searchResult.Title,
                                Year = searchResult.StartDate.HasValue ? searchResult.StartDate.Value.Year.ToString() : "????",
                                Contents = searchResult.Episodes.HasValue ? $"{searchResult.Episodes.Value} episode(s)" : "No episodes",
                                Status = searchResult.Airing ? "Airing" : "Finished",
                                Description = searchResult.Description,
                                Score = searchResult.Score.HasValue ? searchResult.Score.Value / 2 : -1
                            };
                            ViewModel.SearchItems.Add(searchItem);
                        }
                    }
                    else
                    {
                        ViewModel.HasNoSearchResult = true;
                    }
                });
            }
        };
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.ExtraData is not string query)
            return;
        SearchInput.Text = query;
        CategorySelection.SelectedIndex = 0;
    }

    private void OnSearchRequest(object sender, KeyEventArgs args)
    {
        if (args.Key != Key.Enter)
            return;
        _searchWorker.CancelAsync();
        _searchWorker.RunWorkerAsync(new KeyValuePair<int, string>(
            CategorySelection.SelectedIndex,
            SearchInput.Text));
    }

    private void OnCategoryChange(object sender, SelectionChangedEventArgs args)
    {
        _searchWorker.CancelAsync();
        _searchWorker.RunWorkerAsync(new KeyValuePair<int, string>(
            CategorySelection.SelectedIndex,
            SearchInput.Text));
    }

}