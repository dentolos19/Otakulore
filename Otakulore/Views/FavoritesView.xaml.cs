using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Otakulore.Core.Kitsu;
using Otakulore.Models;
using Otakulore.ViewModels;

namespace Otakulore.Views
{
    
    public sealed partial class FavoritesView
    {

        private readonly BackgroundWorker _contentWorker;

        public FavoritesView()
        {
            InitializeComponent();
            _contentWorker = new BackgroundWorker();
            _contentWorker.DoWork += ContentWork;
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            var package = new DataPackage { RequestedOperation = DataPackageOperation.Copy };
            package.SetText(ApplicationData.Current.RoamingFolder.Path);
            Clipboard.SetContent(package);
            _contentWorker.RunWorkerAsync();
        }

        private async void ContentWork(object sender, DoWorkEventArgs args)
        {
            var favorites = new List<KitsuData>();
            foreach (var favoriteId in App.Settings.FavoriteList)
                favorites.Add(await KitsuApi.GetAnimeAsync(favoriteId));
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                ((LoadingViewModel)DataContext).IsLoading = false;
                foreach (var data in favorites)
                    ContentList.Items.Add(ContentItemModel.CreateModel(data));
            });
        }

        private void ShowDetails(object sender, ItemClickEventArgs args)
        {
            if (args.ClickedItem is ContentItemModel model)
                Frame.Navigate(typeof(DetailsView), model.Data);
        }

    }

}