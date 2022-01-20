using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
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
        SearchResultList.Items.Clear();
        switch (type)
        {
            case MediaType.Anime:
            {
                var result = await App.Jikan.SearchAnime(query);
                foreach (var entry in result.Results)
                    SearchResultList.Items.Add(MediaItemModel.Create(entry));
                break;
            }
            case MediaType.Manga:
            {
                var result = await App.Jikan.SearchManga(query);
                foreach (var entry in result.Results)
                    SearchResultList.Items.Add(MediaItemModel.Create(entry));
                break;
            }
        }
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
            Frame.Navigate(typeof(DetailsPage), new PageParameter { MediaType = item.Type, Id = item.Id });
    }

}