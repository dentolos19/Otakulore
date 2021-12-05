using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using JikanDotNet;
using Otakulore.Core;
using Otakulore.Models;
using Otakulore.Services;
using Otakulore.ViewModels;

namespace Otakulore.Views;

public partial class DetailsPage
{

    private readonly BackgroundWorker _detailsLoader;
    private readonly BackgroundWorker _sourceLoader;

    private Anime? _anime;
    private Manga? _manga;

    private DetailsViewModel ViewModel => (DetailsViewModel)DataContext;

    public DetailsPage()
    {
        _detailsLoader = new BackgroundWorker();
        _sourceLoader = new BackgroundWorker { WorkerSupportsCancellation = true };
        _detailsLoader.DoWork += async (_, args) =>
        {
            if (args.Argument is not KeyValuePair<MediaType, long>(var type, var id))
                return;
            var viewModel = new DetailsViewModel { Type = type, Id = id };
            if (type == MediaType.Anime)
            {
                try
                {
                    _anime = await App.Jikan.GetAnime(id);

                    viewModel.ImageUrl = _anime.ImageURL;
                    viewModel.Title = _anime.Title;
                    viewModel.Subtitle = _anime.Premiered;
                    viewModel.Synopsis = _anime.Synopsis;
                    viewModel.Background = _anime.Background;
                    viewModel.Format = _anime.Type;
                    viewModel.Status = _anime.Status;
                    viewModel.Contents = _anime.Episodes.HasValue ? _anime.Episodes.Value.ToString() : "Unknown";
                    viewModel.IsFavorite = App.Settings.Favorites.FirstOrDefault(item => item.Type == type && item.Id == id) is not null;

                    viewModel.Titles.Add(_anime.Title);
                    viewModel.Titles.Add(_anime.TitleEnglish);
                    viewModel.Titles.Add(_anime.TitleJapanese);
                    foreach (var title in _anime.TitleSynonyms)
                        viewModel.Titles.Add(title);
                    foreach (var provider in App.AnimeProviders)
                        viewModel.Providers.Add(ProviderItemModel.Create(provider));
                }
                catch
                {
                    // TODO: notify user of the exception
                }
            }
            else if (type == MediaType.Manga)
            {
                try
                {
                    _manga = await App.Jikan.GetManga(id);

                    viewModel.ImageUrl = _manga.ImageURL;
                    viewModel.Title = _manga.Title;
                    viewModel.Subtitle = _manga.Published.From.HasValue ? _manga.Published.From.Value.Year.ToString() : "????";
                    viewModel.Synopsis = _manga.Synopsis;
                    viewModel.Background = _manga.Background;
                    viewModel.Format = _manga.Type;
                    viewModel.Status = _manga.Status;
                    viewModel.Contents = _manga.Chapters.HasValue ? _manga.Chapters.Value.ToString() : "Unknown";
                    viewModel.IsFavorite = App.Settings.Favorites.FirstOrDefault(item => item.Type == type && item.Id == id) is not null;

                    viewModel.Titles.Add(_manga.Title);
                    viewModel.Titles.Add(_manga.TitleEnglish);
                    viewModel.Titles.Add(_manga.TitleJapanese);
                    foreach (var title in _manga.TitleSynonyms)
                        viewModel.Titles.Add(title);
                    foreach (var provider in App.MangaProviders)
                        viewModel.Providers.Add(ProviderItemModel.Create(provider));
                }
                catch
                {
                    // TODO: notify user of the exception
                }
            }
            Dispatcher.Invoke(() => DataContext = viewModel);
        };
        _sourceLoader.DoWork += (_, args) =>
        {
            if (args.Argument is not KeyValuePair<string, IProvider>(var query, var provider))
                return;
            Dispatcher.Invoke(() =>
            {
                ViewModel.HasSourcesLoaded = false;
                ViewModel.Sources.Clear();
            });
            if (provider is IAnimeProvider animeProvider)
            {
                try
                {
                    var animeList = animeProvider.SearchAnime(query);
                    Dispatcher.Invoke(() =>
                    {
                        foreach (var anime in animeList)
                            ViewModel.Sources.Add(new SourceItemModel
                            {
                                Type = MediaType.Anime,
                                ImageUrl = anime.ImageUrl,
                                Title = anime.Title,
                                Info = anime
                            });
                    });
                }
                catch
                {
                    // TODO: notify user of the exception
                }
            }
            else if (provider is IMangaProvider mangaProvider)
            {
                try
                {
                    var mangaList = mangaProvider.SearchManga(query);
                    Dispatcher.Invoke(() =>
                    {
                        foreach (var manga in mangaList)
                            ViewModel.Sources.Add(new SourceItemModel
                            {
                                Type = MediaType.Manga,
                                ImageUrl = manga.ImageUrl,
                                Title = manga.Title
                            });
                    });
                }
                catch
                {
                    // TODO: notify user of the exception
                }
            }
            Dispatcher.Invoke(() => ViewModel.HasSourcesLoaded = true);
        };
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.ExtraData is not KeyValuePair<MediaType, long> data)
            return;
        _detailsLoader.RunWorkerAsync(data);
    }

    private void OnFavorite(object sender, RoutedEventArgs args)
    {
        if (ViewModel.IsFavorite)
        {
            var item = App.Settings.Favorites.FirstOrDefault(item => item.Type == ViewModel.Type && item.Id == ViewModel.Id);
            if (item is not null)
                return;
            switch (ViewModel.Type)
            {
                case MediaType.Anime:
                    if (_anime is not null)
                        App.Settings.Favorites.Add(MediaItemModel.Create(_anime));
                    break;
                case MediaType.Manga:
                    if (_manga is not null)
                        App.Settings.Favorites.Add(MediaItemModel.Create(_manga));
                    break;
            }
        }
        else
        {
            var item = App.Settings.Favorites.FirstOrDefault(item => item.Type == ViewModel.Type && item.Id == ViewModel.Id);
            if (item is not null)
                App.Settings.Favorites.Remove(item);
        }
    }

    private void OnTabChanged(object sender, SelectionChangedEventArgs args)
    {
        if (!SourceTab.IsSelected)
            return;
        if (TitleSelection.SelectedIndex < 0)
            TitleSelection.SelectedIndex = 0;
        if (ProviderSelection.SelectedIndex < 0)
            ProviderSelection.SelectedIndex = 0;
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        if (TitleSelection.SelectedItem is not string title)
            return;
        if (ProviderSelection.SelectedItem is not ProviderItemModel providerItem)
            return;
        _sourceLoader.CancelAsync();
        _sourceLoader.RunWorkerAsync(new KeyValuePair<string, IProvider>(title, providerItem.Provider));
    }

    private void OnOpenSource(object sender, MouseButtonEventArgs args)
    {
        // TODO: open source
    }

}