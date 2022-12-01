using System.Collections.ObjectModel;
using AniListNet.Objects;
using AniListNet.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Helpers;
using Otakulore.Pages;
using Otakulore.Services;

namespace Otakulore.Models;

[SingletonService]
public partial class HomePageModel : ObservableObject
{

    [ObservableProperty] private string _query;
    [ObservableProperty] private bool _isTrendingLoading = true;
    [ObservableProperty] private bool _isFavoriteLoading = true;
    [ObservableProperty] private bool _isPopularLoading = true;
    [ObservableProperty] private ObservableCollection<MediaItemModel> _trendingItems = new();
    [ObservableProperty] private ObservableCollection<MediaItemModel> _favoriteItems = new();
    [ObservableProperty] private ObservableCollection<MediaItemModel> _popularItems = new();

    public HomePageModel()
    {
        var data = MauiHelper.GetService<DataService>();
        Task.Run(async () =>
        {
            var results = await data.Client.SearchMediaAsync(new SearchMediaFilter { Sort = MediaSort.Trending });
            foreach (var item in results.Data)
                TrendingItems.Add(new MediaItemModel(item));
            IsTrendingLoading = false;
        });
        Task.Run(async () =>
        {
            var results = await data.Client.SearchMediaAsync(new SearchMediaFilter { Sort = MediaSort.Favorites });
            foreach (var item in results.Data)
                FavoriteItems.Add(new MediaItemModel(item));
            IsFavoriteLoading = false;
        });
        Task.Run(async () =>
        {
            var results = await data.Client.SearchMediaAsync(new SearchMediaFilter { Sort = MediaSort.Popularity });
            foreach (var item in results.Data)
                PopularItems.Add(new MediaItemModel(item));
            IsPopularLoading = false;
        });
    }

    [RelayCommand]
    private Task Search()
    {
        return MauiHelper.Navigate(
            typeof(SearchPage),
            new Dictionary<string, object>
            {
                { "filter", new SearchMediaFilter { Query = Query } }
            }
        );
    }

    [RelayCommand]
    private Task SeeMoreTrending()
    {
        return MauiHelper.Navigate(
            typeof(SearchPage),
            new Dictionary<string, object>
            {
                { "filter", new SearchMediaFilter { Sort = MediaSort.Trending } }
            }
        );
    }

    [RelayCommand]
    private Task SeeMoreFavorite()
    {
        return MauiHelper.Navigate(
            typeof(SearchPage),
            new Dictionary<string, object>
            {
                { "filter", new SearchMediaFilter { Sort = MediaSort.Favorites } }
            }
        );
    }

    [RelayCommand]
    private Task SeeMorePopular()
    {
        return MauiHelper.Navigate(
            typeof(SearchPage),
            new Dictionary<string, object>
            {
                { "filter", new SearchMediaFilter { Sort = MediaSort.Popularity } }
            }
        );
    }

}