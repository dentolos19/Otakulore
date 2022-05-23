using System.Collections.ObjectModel;
using AniListNet;
using AniListNet.Objects;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Pages;

namespace Otakulore.Models;

public partial class HomeViewModel : ObservableObject
{

    [ObservableProperty] private ObservableCollection<MediaItemModel> _trendingItems = new();
    [ObservableProperty] private ObservableCollection<MediaItemModel> _favoriteItems = new();
    [ObservableProperty] private ObservableCollection<MediaItemModel> _popularItems = new();

    public HomeViewModel()
    {
        Task.Run(async () =>
        {
            var results = await App.Client.SearchMediaAsync(new AniFilter { Sort = MediaSort.Trending });
            foreach (var item in results.Data)
                TrendingItems.Add(new MediaItemModel(item));
        });
        Task.Run(async () =>
        {
            var results = await App.Client.SearchMediaAsync(new AniFilter { Sort = MediaSort.Favorites });
            foreach (var item in results.Data)
                FavoriteItems.Add(new MediaItemModel(item));
        });
        Task.Run(async () =>
        {
            var results = await App.Client.SearchMediaAsync(new AniFilter { Sort = MediaSort.Popularity });
            foreach (var item in results.Data)
                PopularItems.Add(new MediaItemModel(item));
        });
    }

    [ICommand]
    private async Task Search(string query)
    {
        await Shell.Current.GoToAsync(nameof(SearchPage), new Dictionary<string, object>
        {
            { "query", query }
        });
    }

}