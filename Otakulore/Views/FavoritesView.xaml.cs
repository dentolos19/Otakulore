using Otakulore.Core;
using Otakulore.Core.Services.Common;
using Otakulore.Core.Services.Kitsu;
using Otakulore.Models;
using Otakulore.ViewModels;
using System;
using System.ComponentModel;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Otakulore.Views
{

    public sealed partial class FavoritesView
    {

        private readonly BackgroundWorker _contentLoader;

        public FavoritesView()
        {
            InitializeComponent();
            _contentLoader = new BackgroundWorker();
            _contentLoader.DoWork += LoadContent;
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            _contentLoader.RunWorkerAsync();
        }

        private async void LoadContent(object sender, DoWorkEventArgs args)
        {
            foreach (var details in App.Settings.FavoriteList)
            {
                if (details.MediaType == CommonMediaType.Anime)
                {
                    var reloadedDetails = ServiceUtilities.CastCommonMediaDetails(await KitsuApi.GetAnimeAsync(details.KitsuId.ToString()));
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => ContentList.Items.Add(ContentItemModel.CreateModel(reloadedDetails)));
                }
                else
                {
                    var reloadedDetails = ServiceUtilities.CastCommonMediaDetails(await KitsuApi.GetMangaAsync(details.KitsuId.ToString()));
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => ContentList.Items.Add(ContentItemModel.CreateModel(reloadedDetails)));
                }
            }
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => ((LoadingViewModel)DataContext).IsLoading = false);
        }

        private void ShowDetails(object sender, ItemClickEventArgs args)
        {
            if (args.ClickedItem is ContentItemModel model)
                Frame.Navigate(typeof(DetailsView), model.Data);
        }

    }

}