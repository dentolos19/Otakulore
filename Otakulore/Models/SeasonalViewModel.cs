using System.Collections.ObjectModel;
using AniListNet;
using AniListNet.Objects;
using AniListNet.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Services;

namespace Otakulore.Models;

public partial class SeasonalViewModel : ObservableObject
{

    private readonly DataService _data;

    private SearchMediaFilter? _accumulationFilter;
    private int _currentPageIndex;
    private bool _hasNextPage = true;

    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private ObservableCollection<MediaItemModel> _items = new();

    public MediaSeason Season
    {
        get => _accumulationFilter?.Season ?? MediaSeason.Winter;
        set => _accumulationFilter = new SearchMediaFilter { Season = value };
    }

    public SeasonalViewModel()
    {
        _data = MauiHelper.GetService<DataService>();
    }

    [ICommand]
    private async Task Accumulate()
    {
        if (IsLoading || !_hasNextPage)
            return;
        IsLoading = true;
        var results = await _data.Client.SearchMediaAsync(_accumulationFilter, new AniPaginationOptions(++_currentPageIndex));
        if (results.Data is not { Length: > 0 })
        {
            _hasNextPage = false;
            IsLoading = false;
            return;
        }
        _hasNextPage = results.HasNextPage;
        foreach (var item in results.Data)
            Items.Add(new MediaItemModel(item));
        IsLoading = false;
    }

}