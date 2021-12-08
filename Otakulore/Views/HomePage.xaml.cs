using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Navigation;
using Otakulore.Core;
using Otakulore.Models;
using Otakulore.ViewModels;

namespace Otakulore.Views;

public partial class HomePage
{

    private readonly BackgroundWorker _contentLoader;

    private HomeViewModel ViewModel => (HomeViewModel)DataContext;

    public HomePage()
    {
        _contentLoader = new BackgroundWorker();
        _contentLoader.DoWork += async (_, _) =>
        {
            var topAnimes = await App.Jikan.GetAnimeTop();
            // var topMangas = await App.Jikan.GetMangaTop();
            Dispatcher.Invoke(() =>
            {
                foreach (var topAnime in topAnimes.Top)
                    ViewModel.TopAnimes.Add(MediaItemModel.Create(topAnime));
            });
        };
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        _contentLoader.RunWorkerAsync();
    }

    private void OnOpenAnime(object sender, MouseButtonEventArgs args)
    {
        if (TopAnimeList.SelectedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), new ObjectData { MediaType = item.Type, Id = item.Id });
    }

    private void OnOpenManga(object sender, MouseButtonEventArgs args)
    {
        if (TopMangaList.SelectedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), new ObjectData { MediaType = item.Type, Id = item.Id });
    }

}