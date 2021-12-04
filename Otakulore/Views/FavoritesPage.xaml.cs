using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Navigation;
using Otakulore.Core;
using Otakulore.Models;
using Otakulore.ViewModels;

namespace Otakulore.Views;

public partial class FavoritesPage
{

    private readonly BackgroundWorker _favoriteWorker;

    private FavoriteViewModel ViewModel => (FavoriteViewModel)DataContext;

    public FavoritesPage()
    {
        _favoriteWorker = new BackgroundWorker();
        _favoriteWorker.DoWork += async delegate
        {
            foreach (var animeId in App.Settings.AnimeFavorites)
            {
                var anime = await App.Jikan.GetAnime(animeId);
                Dispatcher.Invoke(() => ViewModel.AnimeFavoriteList.Add(MediaItemModel.Create(anime)));
            }
            Dispatcher.Invoke(() => ViewModel.HasFinishedLoadingAnime = true);
            foreach (var mangaId in App.Settings.MangaFavorites)
            {
                var manga = await App.Jikan.GetManga(mangaId);
                Dispatcher.Invoke(() => ViewModel.MangaFavoriteList.Add(MediaItemModel.Create(manga)));
            }
            Dispatcher.Invoke(() => ViewModel.HasFinishedLoadingManga = true);
        };
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        _favoriteWorker.RunWorkerAsync();
    }

    private void OnOpenAnimeMedia(object sender, MouseButtonEventArgs args)
    {
        if (AnimeFavoriteList.SelectedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), new KeyValuePair<MediaType, long>(item.Type, item.Id));
    }

    private void OnOpenMangaMedia(object sender, MouseButtonEventArgs args)
    {
        if (MangaFavoriteList.SelectedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), new KeyValuePair<MediaType, long>(item.Type, item.Id));
    }

}