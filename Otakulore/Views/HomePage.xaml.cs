using System;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Core.AniList;
using Otakulore.Models;

namespace Otakulore.Views;

public sealed partial class HomePage
{

    public ObservableCollection<MediaItemModel> BannerItems { get; } = new();
    public ObservableCollection<MediaItemModel> TrendingItems { get; } = new();
    public ObservableCollection<MediaItemModel> SeasonalItems { get; } = new();

    public HomePage()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.NavigationMode != NavigationMode.New)
            return;
        var season = Utilities.GetSeasonFromDate(DateTime.Today);
        var popularMedia = await App.Client.SearchMedia(null, MediaSort.Popularity);
        var trendingMedia = await App.Client.SearchMedia(null, MediaSort.Trending);
        var seasonalMedia = await App.Client.GetMediaBySeason(season);
        LoadingIndicator.IsLoading = false;
        foreach (var media in popularMedia.Data)
            BannerItems.Add(new MediaItemModel(media));
        foreach (var media in trendingMedia.Data)
            TrendingItems.Add(new MediaItemModel(media));
        foreach (var media in seasonalMedia.Data)
            SeasonalItems.Add(new MediaItemModel(media));
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), item.Media);
    }

}