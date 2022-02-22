using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Common.Collections;
using CommunityToolkit.WinUI;
using CommunityToolkit.WinUI.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core.AniList;
using Otakulore.Core.Helpers;
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
            TypeDropdown.Items.Add(new ComboBoxItem { Content = status.ToEnumDescription(true), Tag = status });
        foreach (var status in (MediaEntryStatus[])Enum.GetValues(typeof(MediaEntryStatus)))
            StatusDropdown.Items.Add(new ComboBoxItem { Content = status.ToEnumDescription(true), Tag = status });
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (_isAlreadyNavigated || args.Parameter is not int id)
            return;
        _isAlreadyNavigated = true;
        _id = id;
        var incrementalCollection = new IncrementalLoadingCollection<Source, MediaEntryItemModel>(new Source(_id), 500);
        incrementalCollection.OnStartLoading += () => ProgressIndicator.IsActive = true;
        incrementalCollection.OnEndLoading += () => ProgressIndicator.IsActive = false;
        var collection = new AdvancedCollectionView(incrementalCollection, true);
        collection.Filter += filterItem =>
        {
            if (filterItem is not MediaEntryItemModel entryItem)
                return false;
            var query = SearchBox.Text;
            var result = entryItem.Entry.Media.Title.Preferred.Contains(query, StringComparison.OrdinalIgnoreCase);
            if (TypeDropdown.SelectedItem is ComboBoxItem { Tag: MediaType type })
                result = result && entryItem.Entry.Media.Type == type;
            if (StatusDropdown.SelectedItem is ComboBoxItem { Tag: MediaEntryStatus status })
                result = result && entryItem.Entry.Status == status;
            return result;
        };
        List.ItemsSource = collection;
        TypeDropdown.SelectedIndex = 0;
        StatusDropdown.SelectedIndex = 0;
    }

    private void OnSearchChanged(object sender, TextChangedEventArgs args)
    {
        if (List.ItemsSource is AdvancedCollectionView collection)
            collection.RefreshFilter();
    }

    private void OnTypeChanged(object sender, SelectionChangedEventArgs args)
    {
        if (List.ItemsSource is AdvancedCollectionView collection)
            collection.RefreshFilter();
    }

    private void OnStatusChanged(object sender, SelectionChangedEventArgs args)
    {
        if (List.ItemsSource is AdvancedCollectionView collection)
            collection.RefreshFilter();
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaEntryItemModel item)
            App.NavigateFrame(typeof(DetailsPage), item.Entry.MediaId);
    }

    public class Source : IIncrementalSource<MediaEntryItemModel>
    {

        private readonly int _id;

        public Source(int id)
        {
            _id = id;
        }

        public async Task<IEnumerable<MediaEntryItemModel>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = new())
        {
            return (await App.Client.GetUserEntries(_id, new AniPaginationOptions(pageIndex + 1, pageSize))).Data.Select(entry => new MediaEntryItemModel(entry));
        }

    }

}