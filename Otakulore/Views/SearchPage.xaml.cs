using Otakulore.Core;
using Otakulore.Models;
using Otakulore.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using JikanDotNet;

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
                ViewModel.HasNoSearchResult = false;
                ViewModel.SearchItems.Clear();
            });
            if (type == MediaType.Anime)
            {
                AnimeSearchResult? searchResults = null;
                try
                {
                    searchResults = await App.JikanService.SearchAnime(query);
                }
                catch
                {
                    Dispatcher.Invoke(() => ViewModel.HasNoSearchResult = true);
                }
                Dispatcher.Invoke(() =>
                {
                    if (searchResults != null && searchResults.Results.Count > 0)
                    {
                        foreach (var searchResult in searchResults.Results)
                        {
                            var searchItem = new SearchItemModel
                            {
                                Type = MediaType.Anime,
                                Id = searchResult.MalId,
                                ImageUrl = searchResult.ImageURL,
                                Title = searchResult.Title,
                                Description = searchResult.Description,
                                Year = searchResult.StartDate.HasValue ? searchResult.StartDate.Value.Year.ToString() : "????",
                                Contents = searchResult.Episodes.HasValue ? $"{searchResult.Episodes.Value} episode(s)" : "No episodes",
                                Status = searchResult.Airing ? "Airing" : "Finished",
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
            else if (type == MediaType.Manga)
            {
                MangaSearchResult? searchResults = null;
                try
                {
                    searchResults = await App.JikanService.SearchManga(query);
                }
                catch
                {
                    Dispatcher.Invoke(() => ViewModel.HasNoSearchResult = true);
                }
                Dispatcher.Invoke(() =>
                {
                    if (searchResults != null && searchResults.Results.Count > 0)
                    {
                        foreach (var searchResult in searchResults.Results)
                        {
                            var searchItem = new SearchItemModel
                            {
                                Type = MediaType.Manga,
                                Id = searchResult.MalId,
                                ImageUrl = searchResult.ImageURL,
                                Title = searchResult.Title,
                                Description = searchResult.Description,
                                Year = searchResult.StartDate.HasValue ? searchResult.StartDate.Value.Year.ToString() : "????",
                                Contents = !searchResult.Chapters.HasValue ? "No chapters" : searchResult.Publishing ? "Progressing chapters" : $"{searchResult.Chapters} chapter(s)",
                                Status = searchResult.Publishing ? "Publishing" : "Finished",
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

    private void RequestSearch(int typeIndex, string query)
    {
        _searchWorker.CancelAsync();
        _searchWorker.RunWorkerAsync(new KeyValuePair<MediaType, string>((MediaType)typeIndex, query));
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.ExtraData is not string query)
            return;
        SearchInput.Text = query;
        CategorySelection.SelectedIndex = 0;
    }

    protected override void OnNavigatedFrom(NavigationEventArgs args)
    {
        _searchWorker.CancelAsync();
    }

    private void OnSearchRequest(object sender, KeyEventArgs args)
    {
        if (args.Key == Key.Enter)
            RequestSearch(CategorySelection.SelectedIndex, SearchInput.Text);
    }

    private void OnCategoryChange(object sender, SelectionChangedEventArgs args)
    {
        RequestSearch(CategorySelection.SelectedIndex, SearchInput.Text);
    }

    private void OnOpenItem(object sender, MouseButtonEventArgs args)
    {
        if (SearchList.SelectedItem is SearchItemModel item)
            Frame.Navigate(typeof(DetailsPage), new KeyValuePair<MediaType, long>(item.Type, item.Id));
    }

}