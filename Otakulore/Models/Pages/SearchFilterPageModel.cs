using System.Collections.ObjectModel;
using AniListNet.Objects;
using AniListNet.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Pages;
using Otakulore.Utilities.Attributes;

namespace Otakulore.Models;

[TransientService]
public partial class SearchFilterPageModel : BasePageModel
{

    private SearchMediaFilter _filter = new();

    [ObservableProperty] private MediaSort _selectedSort = MediaSort.Relevance;
    [ObservableProperty] private MediaType _selectedType;
    [ObservableProperty] private bool _typeEnabled;
    [ObservableProperty] private bool _onList;

    [ObservableProperty] private ObservableCollection<MediaSort> _sortItems = new();
    [ObservableProperty] private ObservableCollection<MediaType> _typeItems = new();

    public SearchFilterPageModel()
    {
        foreach (var @enum in (MediaSort[])Enum.GetValues(typeof(MediaSort)))
            SortItems.Add(@enum);
        foreach (var @enum in (MediaType[])Enum.GetValues(typeof(MediaType)))
            TypeItems.Add(@enum);
    }

    protected override void Initialize(object? args = null)
    {
        if (args is not SearchMediaFilter filter)
            return;
        _filter = filter;
        SelectedSort = _filter.Sort;
        OnList = _filter.OnList.GetValueOrDefault();
        if (_filter.Type.HasValue)
        {
            SelectedType = _filter.Type.Value;
            TypeEnabled = true;
        }
    }

    [RelayCommand]
    private async Task Filter()
    {
        _filter.Sort = SelectedSort;
        _filter.Type = TypeEnabled ? SelectedType : null;
        _filter.OnList = OnList;
        await MauiHelper.NavigateBack(false);
        await MauiHelper.NavigateBack(false);
        await MauiHelper.Navigate(typeof(SearchPage), _filter);
    }

    [RelayCommand]
    private async Task Reset()
    {
        await MauiHelper.NavigateBack(false);
        await MauiHelper.NavigateBack(false);
        await MauiHelper.Navigate(typeof(SearchPage), _filter.Query);
    }

    [RelayCommand]
    private Task Cancel()
    {
        return MauiHelper.NavigateBack();
    }

}