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
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Otakulore.Views
{

    public sealed partial class SearchView
    {

        private readonly BackgroundWorker _contentLoader;

        public SearchView()
        {
            InitializeComponent();
            _contentLoader = new BackgroundWorker();
            _contentLoader.DoWork += LoadContent;
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            if (!(args.Parameter is string query))
                return;
            QueryText.Text = query;
            _contentLoader.RunWorkerAsync(query);
        }

        private async void LoadContent(object sender, DoWorkEventArgs args)
        {
            if (!(args.Argument is string query))
                return;
            var results = new List<CommonMediaDetails>();
            results.AddRange((await KitsuApi.SearchAnimeAsync(query)).Select(ServiceUtilities.CastCommonMediaDetails));
            results.AddRange((await KitsuApi.SearchMangaAsync(query)).Select(ServiceUtilities.CastCommonMediaDetails));
            if (results.Count > 0)
            {
                var models = results.Select(ContentItemModel.CreateModel).ToArray();
                foreach (var model in models)
                    model.LevenshteinDistance = query.ComputeLevenshteinDistance(model.Title);
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    foreach (var model in models.OrderBy(item => item.LevenshteinDistance))
                        ContentList.Items.Add(model);
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