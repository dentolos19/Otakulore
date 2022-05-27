using System.Collections.ObjectModel;
using AniListNet.Models;
using AniListNet.Objects;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Pages;

namespace Otakulore.Models;

public partial class HomeViewModel : ObservableObject
{

    [ObservableProperty] private string? _query;
    [ObservableProperty] private ObservableCollection<MediaItemModel> _trendingItems = new();
    [ObservableProperty] private ObservableCollection<MediaItemModel> _favoriteItems = new();
    [ObservableProperty] private ObservableCollection<MediaItemModel> _popularItems = new();

    public HomeViewModel()
    {
        Task.Run(async () =>
        {
            var results = await App.Client.SearchMediaAsync(new MediaFilter { Sort = MediaSort.Trending });
            foreach (var item in results.Data)
                TrendingItems.Add(new MediaItemModel(item));
        });
        Task.Run(async () =>
        {
            var results = await App.Client.SearchMediaAsync(new MediaFilter { Sort = MediaSort.Favorites });
            foreach (var item in results.Data)
                FavoriteItems.Add(new MediaItemModel(item));
        });
        Task.Run(async () =>
        {
            var results = await App.Client.SearchMediaAsync(new MediaFilter { Sort = MediaSort.Popularity });
            foreach (var item in results.Data)
                PopularItems.Add(new MediaItemModel(item));
        });
    }

    [ICommand]
    private async Task Search()
    {
        if (!string.IsNullOrEmpty(Query))
            await Shell.Current.GoToAsync(nameof(SearchPage), new Dictionary<string, object>
            {
                { "query", Query }
            });
    }

}