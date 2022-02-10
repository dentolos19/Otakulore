using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Common.Collections;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Core.AniList;
using Otakulore.Models;

namespace Otakulore.Views;

public sealed partial class SearchPage
{

    public SearchPage()
    {
        InitializeComponent();
        foreach (var sort in (MediaSort[])Enum.GetValues(typeof(MediaSort)))
            SearchSortBox.Items.Add(new ComboBoxItem { Content = sort.GetEnumDescription(true), Tag = sort });
    }

    public void Search()
    {
        var query = SearchBox.Text;
        var sort = (MediaSort)((ComboBoxItem)SearchSortBox.SelectedItem).Tag;
        var collection = new IncrementalLoadingCollection<Source, MediaItemModel>(new Source(query, sort));
        collection.OnStartLoading += () => SearchResultIndicator.IsActive = true;
        collection.OnEndLoading += () => SearchResultIndicator.IsActive = false;
        SearchResultList.ItemsSource = collection;
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.NavigationMode != NavigationMode.New || args.Parameter is not string query)
            return;
        SearchBox.Text = query;
        SearchSortBox.SelectedIndex = 0;
        Search();
    }

    private void OnFilterSearch(object sender, RoutedEventArgs args)
    {
        App.ShowNotification("This feature is currently not implemented!");
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), item.Media);
    }

    public class Source : IIncrementalSource<MediaItemModel>
    {

        private readonly string _query;
        private readonly MediaSort _sort;

        public Source(string query, MediaSort sort)
        {
            _query = query;
            _sort = sort;
        }

        public async Task<IEnumerable<MediaItemModel>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = new())
        {
            return (await App.Client.SearchMedia(_query, _sort, new PageOptions(pageIndex, pageSize))).Data.Select(media => new MediaItemModel(media));
        }

    }

}