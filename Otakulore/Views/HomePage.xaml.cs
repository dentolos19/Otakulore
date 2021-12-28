using Otakulore.Models;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Otakulore.Views;

public partial class HomePage
{

    private readonly BackgroundWorker _topAnimeLoader;
    private readonly BackgroundWorker _topMangaLoader;

    public HomePage()
    {
        _topAnimeLoader = new BackgroundWorker();
        _topMangaLoader = new BackgroundWorker();
        _topAnimeLoader.DoWork += async (_, _) =>
        {
            var topAnime = await App.Jikan.GetAnimeTop();
            Dispatcher.Invoke(() =>
            {
                foreach (var anime in topAnime.Top)
                    TopAnimeList.Items.Add(MediaItemModel.Create(anime));
            });
        };
        _topAnimeLoader.DoWork += async (_, _) =>
        {
            var topManga = await App.Jikan.GetMangaTop();
            Dispatcher.Invoke(() =>
            {
                foreach (var manga in topManga.Top)
                    TopMangaList.Items.Add(MediaItemModel.Create(manga));
            });
        };
        InitializeComponent();

    }

    private void OnTabChanged(object sender, SelectionChangedEventArgs args)
    {
        if (TopAnimeTab.IsSelected && !(TopAnimeList.Items.Count > 0))
            _topAnimeLoader.RunWorkerAsync();
        if (TopMangaTab.IsSelected && !(TopMangaList.Items.Count > 0))
            _topMangaLoader.RunWorkerAsync();
    }

    private void OnOpenAnime(object sender, MouseButtonEventArgs args)
    {
        if (TopAnimeList.SelectedItem is MediaItemModel item)
            NavigationService.Navigate(new DetailsPage(item.Type, item.Id));
    }

    private void OnOpenManga(object sender, MouseButtonEventArgs args)
    {
        if (TopMangaList.SelectedItem is MediaItemModel item)
            NavigationService.Navigate(new DetailsPage(item.Type, item.Id));
    }

}