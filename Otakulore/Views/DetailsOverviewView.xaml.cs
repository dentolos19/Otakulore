using Otakulore.Core.Services.Common;
using Otakulore.Core.Services.Kitsu;
using Otakulore.ViewModels;
using System;
using System.ComponentModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Otakulore.Views
{

    public sealed partial class DetailsOverviewView
    {

        private readonly BackgroundWorker _genreLoader;

        private CommonMediaDetails _details;

        public DetailsOverviewView()
        {
            InitializeComponent();
            _genreLoader = new BackgroundWorker();
            _genreLoader.DoWork += LoadGenres;
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            if (!(args.Parameter is CommonMediaDetails details))
                return;
            _details = details;
            DataContext = DetailsOverviewViewModel.CreateViewModel(details);
            if (App.Settings.FavoriteList.Contains(_details))
            {
                FavoriteButton.IsChecked = true;
                ToolTipService.SetToolTip(FavoriteButton, new ToolTip { Content = "Remove from Favorites" });
            }
            else
            {
                FavoriteButton.IsChecked = false;
                ToolTipService.SetToolTip(FavoriteButton, new ToolTip { Content = "Add to Favorites" });
            }
            _genreLoader.RunWorkerAsync();
        }

        private async void LoadGenres(object sender, DoWorkEventArgs args)
        {
            KitsuData<KitsuGenreAttributes>[] genres = null;
            if (_details.MediaType == CommonMediaType.Anime)
                genres = await KitsuApi.GetAnimeGenresAsync(_details.KitsuId.ToString());
            else
                genres = await KitsuApi.GetMangaGenresAsync(_details.KitsuId.ToString());
            if (!(genres != null && genres.Length > 0))
                return;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                foreach (var genre in genres)
                    GenreList.Children.Add(new TextBlock
                    {
                        Text = genre.Attributes.Name
                    });
            });
        }

        private void UpdateFavorites(object sender, RoutedEventArgs args)
        {
            if (FavoriteButton.IsChecked == true)
            {
                if (!App.Settings.FavoriteList.Contains(_details))
                    App.Settings.FavoriteList.Add(_details);
                FavoriteButton.IsChecked = true;
                ToolTipService.SetToolTip(FavoriteButton, new ToolTip { Content = "Remove from Favorites" });
            }
            else
            {
                if (App.Settings.FavoriteList.Contains(_details))
                    App.Settings.FavoriteList.Remove(_details);
                FavoriteButton.IsChecked = false;
                ToolTipService.SetToolTip(FavoriteButton, new ToolTip { Content = "Add to Favorites" });
            }
        }

    }

}