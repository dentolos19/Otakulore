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
using Otakulore.Core;
using Otakulore.Core.AniList;
using Otakulore.Models;

namespace Otakulore.Views;

public sealed partial class ProfileListPage
{

    private int _id;

    public AdvancedCollectionView? Items { get; private set; }

    public ProfileListPage()
    {
        InitializeComponent();
        StatusDropdown.Items.Add(new ComboBoxItem { Content = "All" });
        foreach (var status in (MediaEntryStatus[])Enum.GetValues(typeof(MediaEntryStatus)))
            StatusDropdown.Items.Add(new ComboBoxItem { Content = status.GetEnumDescription(true), Tag = status });
        StatusDropdown.SelectedIndex = 0;
    }

    private void UpdateCollection(Source source)
    {
        var incrementalCollection = new IncrementalLoadingCollection<Source, MediaEntryItemModel>(source);
        incrementalCollection.OnStartLoading += () => ProgressIndicator.IsActive = true;
        incrementalCollection.OnEndLoading += () => ProgressIndicator.IsActive = false;
        var collection = new AdvancedCollectionView(incrementalCollection, true);
        collection.Filter += filterItem =>
        {
            if (filterItem is not MediaEntryItemModel entryItem)
                return false;
            var query = SearchBox.Text;
            var result = entryItem.Entry.Media.Title.Preferred.Contains(query, StringComparison.OrdinalIgnoreCase);
            if (((ComboBoxItem)StatusDropdown.SelectedItem).Tag is not MediaEntryStatus status)
                return result;
            return entryItem.Entry.Status == status && result;
        };
        Items = collection;
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.NavigationMode != NavigationMode.New || args.Parameter is not int id)
            return;
        _id = id;
        UpdateCollection(new Source(_id));
    }

    private void OnSearchChanged(object sender, TextChangedEventArgs args)
    {
        Items?.RefreshFilter();
    }

    private void OnStatusChanged(object sender, SelectionChangedEventArgs args)
    {
        Items?.RefreshFilter();
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaEntryItemModel item)
            App.NavigateContent(typeof(DetailsPage), item.Entry.Media);
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
            return (await App.Client.GetUserEntries(_id, new PageOptions(pageIndex, pageSize))).Data.Select(entry => new MediaEntryItemModel(entry));
        }

    }

}