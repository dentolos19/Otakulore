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

    public sealed partial class TrendingView
    {

        private readonly BackgroundWorker _contentWorker;

        public TrendingView()
        {
            InitializeComponent();
            _contentWorker = new BackgroundWorker();
            _contentWorker.DoWork += ContentWork;
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            _contentWorker.RunWorkerAsync();
        }

        private async void ContentWork(object sender, DoWorkEventArgs args)
        {
            var results = await KitsuApi.GetTrendingAnimeAsync();
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                ((LoadingViewModel)DataContext).IsLoading = false;
                foreach (var data in results)
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