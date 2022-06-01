using System;
using System.Linq;
using AniListNet;
using AniListNet.Models;
using AniListNet.Objects;
using CommunityToolkit.WinUI;
using Humanizer;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Models;
using Otakulore.Views.Dialogs;

namespace Otakulore.Views.Pages;

public sealed partial class SearchPage
{

    private SearchMediaFilter? _filter;

    public SearchPage()
    {
        InitializeComponent();
        foreach (var sort in (MediaSort[])Enum.GetValues(typeof(MediaSort)))
            SearchSortSelection.Items.Add(new ComboBoxItem { Content = sort.Humanize(), Tag = sort });
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.NavigationMode != NavigationMode.New || args.Parameter is not SearchMediaFilter filter)
            return;
        _filter = filter;
        SearchInputBox.Text = _filter.Query;
        SearchSortSelection.SelectedItem = SearchSortSelection.Items.FirstOrDefault(item => (MediaSort)((ComboBoxItem)item).Tag == _filter.Sort) ?? SearchSortSelection.Items.First();
        SearchCommand.Execute(null);
    }

    private void OnSearch(XamlUICommand sender, ExecuteRequestedEventArgs args)
    {
        _filter ??= new SearchMediaFilter();
        _filter.Query = SearchInputBox.Text;
        if (SearchSortSelection.SelectedItem is ComboBoxItem { Tag: MediaSort sort })
            _filter.Sort = sort;
        var source = new IncrementalSource<MediaItemModel>(async (index, size) => (await App.Client.SearchMediaAsync(_filter, new AniPaginationOptions(index + 1, size))).Data.Select(media => new MediaItemModel(media)));
        var collection = new IncrementalLoadingCollection<IncrementalSource<MediaItemModel>, MediaItemModel>(source, 100);
        collection.OnStartLoading += () => SearchResultIndicator.IsActive = true;
        collection.OnEndLoading += () => SearchResultIndicator.IsActive = false;
        SearchResultList.ItemsSource = collection;
    }

    private async void OnFilter(object sender, RoutedEventArgs args)
    {
        var dialog = new FilterMediaDialog(_filter);
        await App.AttachDialog(dialog);
        if (dialog.Result == null)
            return;
        _filter = dialog.Result;
        SearchCommand.Execute(null);
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel { Data: Media media })
            Frame.Navigate(typeof(DetailsPage), media.Id);
    }

}