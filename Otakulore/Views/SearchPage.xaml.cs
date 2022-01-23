using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.AniList;
using Otakulore.Models;

namespace Otakulore.Views;

public sealed partial class SearchPage
{

    public SearchPage()
    {
        InitializeComponent();
    }

    private async void Search()
    {

        var query = SearchInput.Text;
        var type = (MediaType)SearchTypeSelection.SelectedIndex;
        var result = await App.Client.Query.SearchMedia(query, type);
        SearchResultList.Items.Clear();
        foreach (var entry in result.Page.Content)
            SearchResultList.Items.Add(new MediaItemModel(entry));
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not string query)
            return;
        SearchInput.Text = query;
        SearchTypeSelection.SelectedIndex = 0;
    }

    private void OnSearchRequested(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        Search();
    }

    private void OnSearchTypeChanged(object sender, SelectionChangedEventArgs args)
    {
        Search();
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), item.Data);
    }

}