using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Otakulore.Core.Kitsu;
using Otakulore.Models;

namespace Otakulore.Views
{

    public partial class FavoritesView
    {

        private readonly BackgroundWorker _worker;

        public FavoritesView()
        {
            InitializeComponent();
            _worker = new BackgroundWorker();
            _worker.DoWork += LoadFavorites;
            _worker.RunWorkerAsync();
        }

        private async void LoadFavorites(object? sender, DoWorkEventArgs args)
        {
            foreach (var id in App.UserPreferences.FavoritesList)
            {
                var data = await KitsuApi.GetAnimeAsync(id);
                await Dispatcher.BeginInvoke(() => FavoritesList.Items.Add(new ShelfItemModel
                {
                    ImageUrl = data.Attributes.PosterImage.OriginalImageUrl,
                    Title = data.Attributes.CanonicalTitle,
                    Data = data
                }));
            }
        }

        private void RefreshFavorites(object sender, RoutedEventArgs args)
        {
            FavoritesList.Items.Clear();
            _worker.RunWorkerAsync();
        }

        private void ShowDetails(object sender, MouseButtonEventArgs args)
        {
            if (FavoritesList.SelectedItem is not ShelfItemModel model)
                return;
            App.NavigateSinglePage(new DetailsView(model.Data));
        }

    }

}