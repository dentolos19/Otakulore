using Otakulore.Core;
using Otakulore.Core.Services.Common;
using Otakulore.Core.Services.Kitsu;
using Otakulore.Models;
using Otakulore.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Otakulore.Views
{

    public sealed partial class TrendingView
    {

        private readonly BackgroundWorker _contentLoader;

        public TrendingView()
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
            var results = new List<CommonMediaDetails>();
            results.AddRange((await KitsuApi.GetTrendingAnimeAsync()).Select(ServiceUtilities.CastCommonMediaDetails));
            results.AddRange((await KitsuApi.GetTrendingMangaAsync()).Select(ServiceUtilities.CastCommonMediaDetails));
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                foreach (var details in results.OrderBy(item => item.AverageRating))
                    ContentList.Items.Add(ContentItemModel.CreateModel(details));
                ((LoadingViewModel)DataContext).IsLoading = false;
            });
        }

        private void ShowDetails(object sender, ItemClickEventArgs args)
        {
            if (args.ClickedItem is ContentItemModel model)
                Frame.Navigate(typeof(DetailsView), model.Data);
        }

    }

}