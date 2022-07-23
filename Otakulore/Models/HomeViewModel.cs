using System.Collections.ObjectModel;
using AniListNet;
using AniListNet.Objects;
using AniListNet.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Pages;

namespace Otakulore.Models;

public partial class HomeViewModel : ObservableObject
{

    [ObservableProperty] private string _query;
    [ObservableProperty] private bool _isTrendingLoading = true;
    [ObservableProperty] private bool _isFavoriteLoading = true;
    [ObservableProperty] private bool _isPopularLoading = true;
    [ObservableProperty] private ObservableCollection<MediaItemModel> _trendingItems = new();
    [ObservableProperty] private ObservableCollection<MediaItemModel> _favoriteItems = new();
    [ObservableProperty] private ObservableCollection<MediaItemModel> _popularItems = new();

    public HomeViewModel(AniClient client)
    {
        Task.Run(async () =>
        {
            var results = await client.SearchMediaAsync(new SearchMediaFilter { Sort = MediaSort.Trending });
            foreach (var item in results.Data)
                TrendingItems.Add(new MediaItemModel(item));
            IsTrendingLoading = false;
        });
        Task.Run(async () =>
        {
            var results = await client.SearchMediaAsync(new SearchMediaFilter { Sort = MediaSort.Favorites });
            foreach (var item in results.Data)
                FavoriteItems.Add(new MediaItemModel(item));
            IsFavoriteLoading = false;
        });
        Task.Run(async () =>
        {
            var results = await client.SearchMediaAsync(new SearchMediaFilter { Sort = MediaSort.Popularity });
            foreach (var item in results.Data)
                PopularItems.Add(new MediaItemModel(item));
            IsPopularLoading = false;
        });
    }

    [ICommand]
    private Task Search(string query)
    {
        return MauiHelper.NavigateTo(
            typeof(SearchPage),
            new Dictionary<string, object>
            {
                { "query", query }
            }
        );
    }

    [ICommand]
    private Task SeeMoreTrending()
    {
        return Task.CompletedTask; // TODO
    }

    [ICommand]
    private Task SeeMoreFavorite()
    {
        return Task.CompletedTask; // TODO
    }

    [ICommand]
    private Task SeeMorePopular()
    {
        return Task.CompletedTask; // TODO
    }

}