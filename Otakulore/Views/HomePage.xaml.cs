using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.AniList;
using Otakulore.Core;
using Otakulore.Models;

namespace Otakulore.Views;

public sealed partial class HomePage
{

    public HomePage()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        var date = DateTime.Today;
        var day = date.DayOfYear - Convert.ToInt32(DateTime.IsLeapYear(date.Year) && date.DayOfYear > 59);
        var season = day switch
        {
            < 80 or >= 355 => MediaSeason.Winter,
            >= 80 and < 172 => MediaSeason.Spring,
            >= 172 and < 266 => MediaSeason.Summer,
            _ => MediaSeason.Fall
        };
        var trendingMedia = await App.Client.Query.GetTrendingMedia();
        var seasonalMedia = await App.Client.Query.GetSeasonalMedia(season, date.Year);
        foreach (var entry in trendingMedia.Page.TrendingContent)
            TopList.Items.Add(new MediaItemModel(entry.Media));
        foreach (var entry in seasonalMedia.Page.Content)
           SeasonalList.Items.Add(new MediaItemModel(entry));
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), item.Data);
    }

}