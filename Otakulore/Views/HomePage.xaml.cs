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
        var popularMedia = await App.Client.GetMediaByTrend(MediaTrendSort.Popularity);
        var trendingMedia = await App.Client.GetMediaByTrend();
        var seasonalMedia = await App.Client.GetMediaBySeason(season);
        LoadingIndicator.IsLoading = false;
        foreach (var mediaTrend in popularMedia.Data)
            BannerItems.Add(new MediaItemModel(mediaTrend.Media));
        foreach (var mediaTrend in trendingMedia.Data)
            TrendingItems.Add(new MediaItemModel(mediaTrend.Media));
        foreach (var media in seasonalMedia.Data)
            SeasonalItems.Add(new MediaItemModel(media));
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), item.Media);
    }

}