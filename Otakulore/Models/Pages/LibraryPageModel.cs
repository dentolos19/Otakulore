using System.Collections.ObjectModel;
using AniListNet;
using AniListNet.Objects;
using AniListNet.Parameters;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Humanizer;
using Otakulore.Core.Attributes;
using Otakulore.Services;

namespace Otakulore.Models;

[AsSingletonService]
public partial class LibraryPageModel : ObservableObject
{

    private readonly DataService _data = MauiHelper.GetService<DataService>();

    private int? _userId;
    private int _currentPageIndex;
    private bool _hasNextPage = true;

    [ObservableProperty] private MediaType _type = MediaType.Anime;
    [ObservableProperty] private ObservableCollection<MediaType> _types = new();
    [ObservableProperty] private MediaEntryStatus _status = MediaEntryStatus.Current;
    [ObservableProperty] private ObservableCollection<MediaEntryStatus> _statuses = new();
    [ObservableProperty] private BaseItemModel<MediaEntrySort> _sort;
    [ObservableProperty] private ObservableCollection<BaseItemModel<MediaEntrySort>> _sorts = new();
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private ObservableCollection<MediaItemModel> _items = new();

    public LibraryPageModel()
    {
        foreach (var @enum in (MediaType[])Enum.GetValues(typeof(MediaType)))
            Types.Add(@enum);
        foreach (var @enum in (MediaEntryStatus[])Enum.GetValues(typeof(MediaEntryStatus)))
            Statuses.Add(@enum);
        foreach (var @enum in (MediaEntrySort[])Enum.GetValues(typeof(MediaEntrySort)))
            Sorts.Add(new BaseItemModel<MediaEntrySort>(@enum.Humanize(LetterCasing.Title), @enum));
        Sort = Sorts.Where(item => item.Data == MediaEntrySort.LastUpdated).FirstOrDefault(Sorts.First());
    }

    public async Task CheckAuthenticationStatus()
    {
        if (_data.Client.IsAuthenticated)
        {
            if (_userId is not null)
                return;
            var user = await _data.Client.GetAuthenticatedUserAsync();
            _userId = user.Id;
        }
        else
        {
            _userId = null;
        }
        await RefreshCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    private async Task Refresh()
    {
        Items.Clear();
        if (!_userId.HasValue)
        {
            await Toast.Make("You need to login into AniList via the settings to access this feature.").Show();
            return;
        }
        _currentPageIndex = 0;
        _hasNextPage = true;
        await Accumulate();
    }

    [RelayCommand]
    private async Task Accumulate()
    {
        if (!_userId.HasValue)
            return;
        if (IsLoading || !_hasNextPage)
            return;
        IsLoading = true;
        var results = await _data.Client.GetUserEntriesAsync(_userId.Value, new MediaEntryFilter
        {
            Type = Type,
            Status = Status,
            Sort = Sort.Data
        }, new AniPaginationOptions(++_currentPageIndex));
        if (results.Data is not { Length: > 0 })
        {
            _hasNextPage = false;
            IsLoading = false;
            return;
        }
        _hasNextPage = results.HasNextPage;
        foreach (var item in results.Data)
            Items.Add(new MediaItemModel(item.Media));
        IsLoading = false;
    }

}