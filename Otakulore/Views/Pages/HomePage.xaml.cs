using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Core.AniList;
using Otakulore.Models;

namespace Otakulore.Views.Pages;

public sealed partial class HomePage
{

    private bool _isAlreadyNavigated;

    public HomePage()
    {
        InitializeComponent();
        NavigationCacheMode = NavigationCacheMode.Enabled;
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        if (_isAlreadyNavigated)
            return;
        _isAlreadyNavigated = true;
        var season = Utilities.GetSeasonFromDate(DateTime.Today);
        var popularMedia = await App.Client.SearchMedia(null, MediaSort.Popularity, new AniPaginationOptions(1, 20));
        var trendingMedia = await App.Client.SearchMedia(null, MediaSort.Trending);
        var favoriteMedia = await App.Client.SearchMedia(null, MediaSort.Favorites);
        var seasonalMedia = await App.Client.GetMediaBySeason(season);
        LoadingIndicator.IsLoading = false;
        foreach (var media in popularMedia.Data)
            PopularList.Items.Add(new MediaItemModel(media));
        foreach (var media in trendingMedia.Data)
            TrendingList.Items.Add(new MediaItemModel(media));
        foreach (var media in favoriteMedia.Data)
            FavoriteList.Items.Add(new MediaItemModel(media));
        foreach (var media in seasonalMedia.Data)
            SeasonalList.Items.Add(new MediaItemModel(media));
    }

    private void OnBannerTapped(object sender, TappedRoutedEventArgs args)
    {
        if (PopularList.SelectedItem is MediaItemModel item)
            App.NavigateFrame(typeof(DetailsPage), item.Media);
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel item)
            App.NavigateFrame(typeof(DetailsPage), item.Media);
    }

}