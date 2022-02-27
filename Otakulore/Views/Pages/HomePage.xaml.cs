﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
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
        var popularMedia = await App.Client.SearchMedia(new AniFilter { Sort = MediaSort.Popularity }, new AniPaginationOptions(1, 10));
        var trendingMedia = await App.Client.SearchMedia(new AniFilter { Sort = MediaSort.Trending });
        var favoriteMedia = await App.Client.SearchMedia(new AniFilter { Sort = MediaSort.Favorites });
        var seasonalMedia = await App.Client.GetMediaBySeason(App.CurrentSeason);
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
        if (PopularList.SelectedItem is MediaItemModel { Media: Media media })
            App.NavigateFrame(typeof(DetailsPage), media.Id);
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel { Media: Media media })
            App.NavigateFrame(typeof(DetailsPage), media.Id);
    }

    private void OnSeeMorePopular(object sender, RoutedEventArgs args)
    {
        App.NavigateFrame(typeof(SearchPage), new AniFilter { Sort = MediaSort.Popularity });
    }

    private void OnSeeMoreTrending(object sender, RoutedEventArgs args)
    {
        App.NavigateFrame(typeof(SearchPage), new AniFilter { Sort = MediaSort.Trending });
    }

    private void OnSeeMoreFavorites(object sender, RoutedEventArgs args)
    {
        App.NavigateFrame(typeof(SearchPage), new AniFilter { Sort = MediaSort.Favorites });
    }

    private void OnSeeMoreSeasonal(object sender, RoutedEventArgs args)
    {
        App.NavigateFrame(typeof(SchedulesPage));
    }

}