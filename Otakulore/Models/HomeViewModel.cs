using System;
using System.Collections.ObjectModel;
using Otakulore.Core;

namespace Otakulore.Models;

public class HomeViewModel : BaseViewModel
{

    public ObservableCollection<MediaItemModel> BannerItems { get; } = new();
    public ObservableCollection<MediaItemModel> TrendingItems { get; } = new();
    public ObservableCollection<MediaItemModel> SeasonalItems { get; } = new();

    public async void Load()
    {
        var date = DateTime.Today;
        var season = Utilities.GetSeasonFromDate(date);
        var trendingMedia = await App.Client.GetTrendingMedia();
        var seasonalMedia = await App.Client.GetSeasonalMedia(season, date.Year);
        for (var index = 0; index < 5; index++)
            BannerItems.Add(new MediaItemModel(trendingMedia[index].Media));
        foreach (var entry in trendingMedia)
            TrendingItems.Add(new MediaItemModel(entry.Media));
        foreach (var entry in seasonalMedia)
            SeasonalItems.Add(new MediaItemModel(entry));
    }

}