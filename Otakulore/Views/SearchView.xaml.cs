using System;
using System.ComponentModel;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Otakulore.Core.Kitsu;
using Otakulore.Models;
using Otakulore.ViewModels;

namespace Otakulore.Views
{

    public sealed partial class SearchView
    {

        private readonly BackgroundWorker _searchWorker;
        
        public SearchView()
        {
            InitializeComponent();
            _searchWorker = new BackgroundWorker();
            _searchWorker.DoWork += SearchWork;
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            if (!(args.Parameter is string query))
                return;
            QueryText.Text = query;
            _searchWorker.RunWorkerAsync(query);
        }

        private async void SearchWork(object sender, DoWorkEventArgs args)
        {
            if (!(args.Argument is string query))
                return;
            var results = await KitsuApi.SearchAnimeAsync(query);
            if (results != null && results.Length > 0)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    foreach (var data in results)
                        ContentList.Items.Add(ContentItemModel.CreateModel(data));
                    ((LoadingViewModel)DataContext).IsLoading = false;
                });
            }
            else
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    if (Frame.CanGoBack)
                        Frame.GoBack();
                    await new MessageDialog("No results were found matching your query.").ShowAsync();
                });
            }
        }

        private void ShowDetails(object sender, ItemClickEventArgs args)
        {
            if (args.ClickedItem is ContentItemModel model)
                Frame.Navigate(typeof(DetailsView), model.Data);
        }

    }

}