using System;
using System.Linq;
using AniListNet;
using AniListNet.Objects;
using CommunityToolkit.WinUI;
using CommunityToolkit.WinUI.UI;
using Humanizer;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Models;
using Otakulore.Views.Pages;

namespace Otakulore.Views.Panels;

public sealed partial class ProfileListPanel
{

    private bool _isAlreadyNavigated;
    private int _id;

    public ProfileListPanel()
    {
        InitializeComponent();
        TypeDropdown.Items.Add(new ComboBoxItem { Content = "All" });
        StatusDropdown.Items.Add(new ComboBoxItem { Content = "All" });
        foreach (var status in (MediaType[])Enum.GetValues(typeof(MediaType)))
            TypeDropdown.Items.Add(new ComboBoxItem { Content = status.Humanize(), Tag = status });
        foreach (var status in (MediaEntryStatus[])Enum.GetValues(typeof(MediaEntryStatus)))
            StatusDropdown.Items.Add(new ComboBoxItem { Content = status.Humanize(), Tag = status });
    }

    public void RefreshCollection()
    {
        var incrementalSource = new IncrementalSource<MediaItemModel>(async (index, size) => (await App.Client.GetUserEntriesAsync(_id, new AniPaginationOptions(index + 1, size))).Data.Select(entry => new MediaItemModel(entry)));
        var incrementalCollection = new IncrementalLoadingCollection<IncrementalSource<MediaItemModel>, MediaItemModel>(incrementalSource, 500);
        incrementalCollection.OnStartLoading += () => EntryListIndicator.IsActive = true;
        incrementalCollection.OnEndLoading += () => EntryListIndicator.IsActive = false;
        var collection = new AdvancedCollectionView(incrementalCollection, true);
        collection.Filter += filterItem =>
        {
            if (filterItem is not MediaItemModel { Data: MediaEntry entry })
                return false;
            var query = SearchBox.Text;
            var result = entry.Media.Title.PreferredTitle.Contains(query, StringComparison.OrdinalIgnoreCase);
            if (TypeDropdown.SelectedItem is ComboBoxItem { Tag: MediaType type })
                result = result && entry.Media.Type == type;
            if (StatusDropdown.SelectedItem is ComboBoxItem { Tag: MediaEntryStatus status })
                result = result && entry.Status == status;
            return result;
        };
        EntryList.ItemsSource = collection;
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (_isAlreadyNavigated || args.Parameter is not User user)
            return;
        _isAlreadyNavigated = true;
        _id = user.Id;
        RefreshCollection();
        TypeDropdown.SelectedIndex = 0;
        StatusDropdown.SelectedIndex = 0;
    }

    private void OnSearchChanged(object sender, TextChangedEventArgs args)
    {
        if (EntryList.ItemsSource is AdvancedCollectionView collection)
            collection.RefreshFilter();
    }

    private void OnTypeChanged(object sender, SelectionChangedEventArgs args)
    {
        if (EntryList.ItemsSource is AdvancedCollectionView collection)
            collection.RefreshFilter();
    }

    private void OnStatusChanged(object sender, SelectionChangedEventArgs args)
    {
        if (EntryList.ItemsSource is AdvancedCollectionView collection)
            collection.RefreshFilter();
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel { Data: MediaEntry entry })
            App.NavigateFrame(typeof(DetailsPage), entry.Media.Id);
    }

}