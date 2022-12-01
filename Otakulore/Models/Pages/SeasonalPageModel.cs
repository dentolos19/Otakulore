using System.Collections.ObjectModel;
using AniListNet;
using AniListNet.Objects;
using AniListNet.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore.Models;

[TransientService]
public partial class SeasonalPageModel : ObservableObject
{

    private readonly DataService _data = MauiHelper.GetService<DataService>();

    private SearchMediaFilter? _accumulationFilter;
    private int _currentPageIndex;
    private bool _hasNextPage = true;

    [ObservableProperty] private string _info = "Hello World!";
    [ObservableProperty] private int _year = DateTime.Now.Year;
    [ObservableProperty] private ObservableCollection<int> _years = new();
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private ObservableCollection<MediaItemModel> _items = new();

    public MediaSeason? Season
    {
        get => _accumulationFilter?.Season ?? null;
        set
        {
            Info = value switch
            {
                MediaSeason.Winter => "November to February",
                MediaSeason.Spring => "March to May",
                MediaSeason.Summer => "May to September",
                MediaSeason.Fall => "September to November"
            };
            _accumulationFilter ??= new SearchMediaFilter();
            _accumulationFilter.Season = value;
        }
    }

    public SeasonalPageModel()
    {
        var currentYear = DateTime.Now.Year + 1;
        for (var index = 0; index < 5; index++)
            Years.Add(currentYear--);
    }

    [RelayCommand]
    private async Task Refresh()
    {
        Items.Clear();
        _currentPageIndex = 0;
        _hasNextPage = true;
        _accumulationFilter ??= new SearchMediaFilter();
        _accumulationFilter.SeasonYear = Year;
        await Accumulate();
    }

    [RelayCommand]
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