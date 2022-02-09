using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Core.AniList;
using Otakulore.Models;

namespace Otakulore.Views;

public sealed partial class ProfileListPage
{

    public ProfileListPage()
    {
        InitializeComponent();
        StatusDropdown.Items.Add(new ComboBoxItem { Content = "All" });
        foreach (var status in (MediaEntryStatus[])Enum.GetValues(typeof(MediaEntryStatus)))
            StatusDropdown.Items.Add(new ComboBoxItem { Content = status.GetEnumDescription(true), Tag = status });
        StatusDropdown.SelectedIndex = 0;
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.NavigationMode == NavigationMode.New && args.Parameter is int id)
            MediaEntryList.ItemsSource = new IncrementalCollection(id);
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaEntryItemModel item)
            App.NavigateContent(typeof(DetailsPage), item.Entry.Media);
    }

    public class IncrementalCollection : ObservableCollection<MediaEntryItemModel>, ISupportIncrementalLoading
    {

        private readonly int _id;

        private int _index;

        public bool HasMoreItems { get; private set; } = true;

        public IncrementalCollection(int id)
        {
            _id = id;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return AsyncInfo.Run(async _ =>
            {
                var page = await App.Client.GetUserEntries(_id, ++_index);
                foreach (var entry in page.Data)
                    Add(new MediaEntryItemModel(entry));
                HasMoreItems = page.HasNextPage;
                return new LoadMoreItemsResult((uint)(page.Data.Count + 1));
            });
        }

    }

}