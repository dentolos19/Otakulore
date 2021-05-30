using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
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
        }

        public void UpdateFavorites()
        {
            FavoritesList.Items.Clear();
            foreach (var id in App.Settings.FavoritesList)
            {
                ThreadPool.QueueUserWorkItem(async _ =>
                {
                    var data = await KitsuApi.GetAnimeAsync(id);
                    await Dispatcher.BeginInvoke(() => FavoritesList.Items.Add(new ShelfItemModel
                    {
                        ImageUrl = data.Attributes.PosterImage.OriginalImageUrl,
                        Title = data.Attributes.CanonicalTitle,
                        Data = data
                    }));
                });
            }

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