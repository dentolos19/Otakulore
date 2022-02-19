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
using Otakulore.Views.Pages;

namespace Otakulore.Views.Panels;

public sealed partial class ProfileListPanel
{

    private bool _isAlreadyNavigated;
    private int _id;
    private AnimeSource? _animeSource;
    private MangaSource? _mangaSource;

    public ProfileListPanel()
    {
        InitializeComponent();
        StatusDropdown.Items.Add(new ComboBoxItem { Content = "All" });
        foreach (var status in (MediaType[])Enum.GetValues(typeof(MediaType)))
            TypeDropdown.Items.Add(new ComboBoxItem { Content = status.ToEnumDescription(true), Tag = status });
        foreach (var status in (MediaEntryStatus[])Enum.GetValues(typeof(MediaEntryStatus)))
            StatusDropdown.Items.Add(new ComboBoxItem { Content = status.ToEnumDescription(true), Tag = status });
    }

    private void UpdateCollection<T>(T source) where T : IIncrementalSource<MediaEntryItemModel>
    {
        var incrementalCollection = new IncrementalLoadingCollection<T, MediaEntryItemModel>(source, 500);
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
        List.ItemsSource = collection;
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (_isAlreadyNavigated)
            return;
        _isAlreadyNavigated = true;
        if (args.Parameter is not int id)
            return;
        _id = id;
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
        if (TypeDropdown.SelectedItem is not ComboBoxItem { Tag: MediaType type })
            return;
        switch (type)
        {
            case MediaType.Anime:
                _animeSource ??= new AnimeSource(_id);
                UpdateCollection(_animeSource);
                break;
            case MediaType.Manga:
                _mangaSource ??= new MangaSource(_id);
                UpdateCollection(_mangaSource);
                break;
        }
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

    public class AnimeSource : IIncrementalSource<MediaEntryItemModel>
    {

        private readonly int _id;

        public AnimeSource(int id)
        {
            _id = id;
        }

        public async Task<IEnumerable<MediaEntryItemModel>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = new())
        {
            return (await App.Client.GetUserEntries(_id, MediaType.Anime, new AniPaginationOptions(pageIndex + 1, pageSize))).Data.Select(entry => new MediaEntryItemModel(entry));
        }

    }

    public class MangaSource : IIncrementalSource<MediaEntryItemModel>
    {

        private readonly int _id;

        public MangaSource(int id)
        {
            _id = id;
        }

        public async Task<IEnumerable<MediaEntryItemModel>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = new())
        {
            return (await App.Client.GetUserEntries(_id, MediaType.Manga, new AniPaginationOptions(pageIndex + 1, pageSize))).Data.Select(entry => new MediaEntryItemModel(entry));
        }

    }

}