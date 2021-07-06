using System;
using System.ComponentModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Otakulore.Core.Services.Kitsu;
using Otakulore.ViewModels;

namespace Otakulore.Views
{

    public sealed partial class DetailsOverviewView
    {

        private readonly BackgroundWorker _genreLoader;

        private string _id;

        public DetailsOverviewView()
        {
            InitializeComponent();
            _genreLoader = new BackgroundWorker();
            _genreLoader.DoWork += LoadGenres;
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            if (!(args.Parameter is KitsuData<KitsuAnimeAttributes> data))
                return;
            _id = data.Id;
            DataContext = DetailsOverviewViewModel.CreateViewModel(data);
            if (App.Settings.FavoriteList.Contains(_id))
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
            var genres = await KitsuApi.GetAnimeGenresAsync(_id);
            if (genres != null && genres.Length > 0)
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
                if (!App.Settings.FavoriteList.Contains(_id))
                    App.Settings.FavoriteList.Add(_id);
                FavoriteButton.IsChecked = true;
                ToolTipService.SetToolTip(FavoriteButton, new ToolTip { Content = "Remove from Favorites" });
            }
            else
            {
                if (App.Settings.FavoriteList.Contains(_id))
                    App.Settings.FavoriteList.Remove(_id);
                FavoriteButton.IsChecked = false;
                ToolTipService.SetToolTip(FavoriteButton, new ToolTip { Content = "Add to Favorites" });
            }
        }

    }

}