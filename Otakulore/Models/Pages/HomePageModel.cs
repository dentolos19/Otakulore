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
public partial class HomePageModel : BasePageModel
{

    [ObservableProperty] private ObservableCollection<MediaItemModel> _trendingItems = new();
    [ObservableProperty] private ObservableCollection<MediaItemModel> _favoriteItems = new();
    [ObservableProperty] private ObservableCollection<MediaItemModel> _popularItems = new();

    protected override void Initialize(object? args = null)
    {
        RefreshTrendingItemsCommand.Execute(null);
        RefreshFavoriteItemsCommand.Execute(null);
        RefreshPopularItemsCommand.Execute(null);
    }

    [RelayCommand]
    private Task Search(string query)
    {
        return MauiHelper.Navigate(typeof(SearchPage), query);
    }

    [RelayCommand]
    private async Task RefreshTrendingItems()
    {
        TrendingItems.Clear();
        var result = await DataService.Instance.Client.SearchMediaAsync(new SearchMediaFilter { Sort = MediaSort.Trending });
        foreach (var item in result.Data)
            TrendingItems.Add(MediaItemModel.Map(item));
    }

    [RelayCommand]
    private async Task RefreshFavoriteItems()
    {
        FavoriteItems.Clear();
        var result = await DataService.Instance.Client.SearchMediaAsync(new SearchMediaFilter { Sort = MediaSort.Favorites });
        foreach (var item in result.Data)
            FavoriteItems.Add(MediaItemModel.Map(item));
    }

    [RelayCommand]
    private async Task RefreshPopularItems()
    {
        PopularItems.Clear();
        var result = await DataService.Instance.Client.SearchMediaAsync(new SearchMediaFilter { Sort = MediaSort.Popularity });
        foreach (var item in result.Data)
            PopularItems.Add(MediaItemModel.Map(item));
    }

}