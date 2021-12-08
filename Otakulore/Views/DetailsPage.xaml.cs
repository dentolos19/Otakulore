using System;
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
            if (args.Argument is not ObjectData data)
                return;
            var viewModel = new DetailsViewModel { Type = (MediaType)data.MediaType, Id = data.Id };
            if (data.MediaType == MediaType.Anime)
            {
                try
                {
                    _anime = await App.Jikan.GetAnime((long)data.Id);

                    viewModel.ImageUrl = _anime.ImageURL;
                    viewModel.Title = _anime.Title;
                    viewModel.Subtitle = _anime.Premiered;
                    viewModel.Synopsis = _anime.Synopsis;
                    viewModel.Background = _anime.Background;
                    viewModel.Format = _anime.Type;
                    viewModel.Status = _anime.Status;
                    viewModel.Contents = _anime.Episodes.HasValue ? _anime.Episodes.Value.ToString() : "Unknown";
                    viewModel.IsFavorite = App.Settings.Favorites.FirstOrDefault(item => item.Type == data.MediaType && item.Id == data.Id) is not null;

                    viewModel.Titles.Add(_anime.Title);
                    viewModel.Titles.Add(_anime.TitleEnglish);
                    viewModel.Titles.Add(_anime.TitleJapanese);
                    foreach (var title in _anime.TitleSynonyms)
                        viewModel.Titles.Add(title);
                    foreach (var provider in App.AnimeProviders)
                        viewModel.Providers.Add(ProviderItemModel.Create(provider));
                }
                catch (Exception exception)
                {
                    await Dispatcher.Invoke(async () => await Utilities.CreateExceptionDialog(exception, "Jikan returned an exception!").ShowAsync());
                }
            }
            else if (data.MediaType == MediaType.Manga)
            {
                try
                {
                    _manga = await App.Jikan.GetManga((long)data.Id);

                    viewModel.ImageUrl = _manga.ImageURL;
                    viewModel.Title = _manga.Title;
                    viewModel.Subtitle = _manga.Published.From.HasValue ? _manga.Published.From.Value.Year.ToString() : "????";
                    viewModel.Synopsis = _manga.Synopsis;
                    viewModel.Background = _manga.Background;
                    viewModel.Format = _manga.Type;
                    viewModel.Status = _manga.Status;
                    viewModel.Contents = _manga.Chapters.HasValue ? _manga.Chapters.Value.ToString() : "Unknown";
                    viewModel.IsFavorite = App.Settings.Favorites.FirstOrDefault(item => item.Type == data.MediaType && item.Id == data.Id) is not null;

                    viewModel.Titles.Add(_manga.Title);
                    viewModel.Titles.Add(_manga.TitleEnglish);
                    viewModel.Titles.Add(_manga.TitleJapanese);
                    foreach (var title in _manga.TitleSynonyms)
                        viewModel.Titles.Add(title);
                    foreach (var provider in App.MangaProviders)
                        viewModel.Providers.Add(ProviderItemModel.Create(provider));
                }
                catch (Exception exception)
                {
                    await Dispatcher.Invoke(async () => await Utilities.CreateExceptionDialog(exception, "Jikan returned an exception!").ShowAsync());
                }
            }
            Dispatcher.Invoke(() => DataContext = viewModel);
        };
        _sourceLoader.DoWork += (_, args) =>
        {
            if (args.Argument is not ObjectData data)
                return;
            Dispatcher.Invoke(() =>
            {
                ViewModel.HasSourcesLoaded = false;
                ViewModel.Sources.Clear();
            });
            if (data.Provider is IAnimeProvider animeProvider)
            {
                try
                {
                    var animeList = animeProvider.SearchAnime(data.Query);
                    Dispatcher.Invoke(() =>
                    {
                        foreach (var anime in animeList)
                            ViewModel.Sources.Add(SourceItemModel.Create(anime, MediaType.Anime));
                    });
                }
                catch (Exception exception)
                {
                    Dispatcher.Invoke(async () => await Utilities.CreateExceptionDialog(exception, "The provider returned an exception!").ShowAsync());
                }
            }
            else if (data.Provider is IMangaProvider mangaProvider)
            {
                try
                {
                    var mangaList = mangaProvider.SearchManga(data.Query);
                    Dispatcher.Invoke(() =>
                    {
                        foreach (var manga in mangaList)
                            ViewModel.Sources.Add(SourceItemModel.Create(manga, MediaType.Manga));
                    });
                }
                catch (Exception exception)
                {
                    Dispatcher.Invoke(async () => await Utilities.CreateExceptionDialog(exception, "The provider returned an exception!").ShowAsync());
                }
            }
            Dispatcher.Invoke(() => ViewModel.HasSourcesLoaded = true);
        };
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.ExtraData is ObjectData data)
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
        if (!SourcesTab.IsSelected)
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
        _sourceLoader.RunWorkerAsync(new ObjectData { Query = title, Provider = providerItem.Provider });
    }

    private void OnOpenSource(object sender, MouseButtonEventArgs args)
    {
        if (SourceList.SelectedItem is not SourceItemModel sourceItem)
            return;
        if (ProviderSelection.SelectedItem is not ProviderItemModel providerItem)
            return;
        /*
        switch (sourceItem.Type)
        {
            case MediaType.Anime:
                Frame.Navigate(typeof(AnimePlayerPage), new ObjectData { Provider = providerItem.Provider, MediaInfo = sourceItem.Info });
                break;
            case MediaType.Manga:
                Frame.Navigate(typeof(MangaReaderPage), new ObjectData { Provider = providerItem.Provider, MediaInfo = sourceItem.Info });
                break;
        }
        */
    }

}