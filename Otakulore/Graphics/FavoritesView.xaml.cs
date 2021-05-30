using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Otakulore.Core.Kitsu;
using Otakulore.Models;

namespace Otakulore.Graphics
{

    public partial class FavoritesView
    {

        public FavoritesView()
        {
            InitializeComponent();
            UpdateFavorites();
            FilterFavorites(null, null);
        }

        public void UpdateFavorites()
        {
            var list = new List<ShelfItemModel>();
            foreach (var id in App.Settings.FavoritesList)
            {
                ThreadPool.QueueUserWorkItem(async _ =>
                {
                    var data = await KitsuApi.GetAnimeAsync(id);
                    list.Add(new ShelfItemModel
                    {
                        ImageUrl = data.Attributes.PosterImage.OriginalImageUrl,
                        Title = data.Attributes.CanonicalTitle,
                        Data = data
                    });
                });
            }
            FavoritesList.ItemsSource = list;
            ((CollectionView)CollectionViewSource.GetDefaultView(FavoritesList.ItemsSource)).Filter = FilterFavoriteItem;
        }

        private bool FilterFavoriteItem(object item)
        {
            var inputQuery = FilterInput.Text;
            if (string.IsNullOrEmpty(inputQuery))
                return true;
            if (item is ShelfItemModel model)
                return model.Title.Contains(inputQuery, StringComparison.OrdinalIgnoreCase);
            return true;
        }

        private void FilterFavorites(object sender, TextChangedEventArgs args)
        {
            CollectionViewSource.GetDefaultView(FavoritesList.ItemsSource).Refresh();
        }

        private void ShowDetails(object sender, MouseButtonEventArgs args)
        {
            if (FavoritesList.SelectedItem is not ShelfItemModel model)
                return;
            App.DetailsViewPage.ShowDetails(model.Data);
            App.NavigateSinglePage(App.DetailsViewPage);
        }

    }

}