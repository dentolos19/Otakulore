using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Core.AniList;

namespace Otakulore.Models;

public partial class HomeViewModel : ObservableObject
{

    [ObservableProperty] private string _query;
    [ObservableProperty] private ObservableCollection<MediaItemModel> _trendingItems = new();
    [ObservableProperty] private ObservableCollection<MediaItemModel> _favoriteItems = new();
    [ObservableProperty] private ObservableCollection<MediaItemModel> _popularItems = new();

    public HomeViewModel()
    {
        Task.Run(async () =>
        {
            var results = (await App.Client.SearchMedia(new AniFilter { Sort = MediaSort.Trending })).Data;
            foreach (var media in results)
                TrendingItems.Add(new MediaItemModel(media));
        });
        Task.Run(async () =>
        {
            var results = (await App.Client.SearchMedia(new AniFilter { Sort = MediaSort.Favorites })).Data;
            foreach (var media in results)
                FavoriteItems.Add(new MediaItemModel(media));
        });
        Task.Run(async () =>
        {
            var results = (await App.Client.SearchMedia(new AniFilter { Sort = MediaSort.Popularity })).Data;
            foreach (var media in results)
                PopularItems.Add(new MediaItemModel(media));
        });
    }

    [ICommand]
    private async void Search()
    {
        await Shell.Current.GoToAsync("search?query=" + Uri.EscapeDataString(Query));
    }

}