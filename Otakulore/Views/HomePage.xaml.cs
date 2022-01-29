using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Core.AniList;
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
        var season = Utilities.GetSeasonFromDate(date);
        var trendingMedia = await App.Client.GetTrendingMedia();
        var seasonalMedia = await App.Client.GetSeasonalMedia(season, date.Year);
        for (var index = 0; index < 5; index++)
            if (trendingMedia[index].Media.Type == MediaType.Anime)
                BannerView.Items.Add(new MediaItemModel(trendingMedia[index].Media));
        foreach (var entry in trendingMedia)
            TrendingList.Items.Add(new MediaItemModel(entry.Media));
        foreach (var entry in seasonalMedia)
            SeasonalList.Items.Add(new MediaItemModel(entry));
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel item)
            Frame.Navigate(typeof(DetailsPage), item.Data);
    }

}