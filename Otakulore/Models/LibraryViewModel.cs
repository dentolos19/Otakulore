using System.Collections.ObjectModel;
using AniListNet;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Services;

namespace Otakulore.Models;

public partial class LibraryViewModel : ObservableObject
{

    private readonly DataService _data = MauiHelper.GetService<DataService>();

    private int? _userId;
    private int _currentPageIndex;
    private bool _hasNextPage = true;

    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private ObservableCollection<MediaEntryItemModel> _items = new();

    public async Task CheckAuthenticationStatus()
    {
        if (_data.Client.IsAuthenticated)
        {
            if (_userId is not null)
                return;
            var user = await _data.Client.GetAuthenticatedUserAsync();
            _userId = user.Id;
            await AccumulateCommand.ExecuteAsync(null);
        }
        else
        {
            _userId = null;
            _currentPageIndex = 0;
            _hasNextPage = true;
            await Toast.Make("You need to login into AniList via the settings to access this feature.").Show();
        }
    }

    [ICommand]
    private async Task Accumulate()
    {
        if (!_userId.HasValue)
            return;
        if (IsLoading || !_hasNextPage)
            return;
        IsLoading = true;
        var results = await _data.Client.GetUserEntriesAsync(_userId.Value, new AniPaginationOptions(++_currentPageIndex));
        if (results.Data is not { Length: > 0 })
        {
            _hasNextPage = false;
            IsLoading = false;
            return;
        }
        _hasNextPage = results.HasNextPage;
        foreach (var item in results.Data)
            Items.Add(new MediaEntryItemModel(item));
        IsLoading = false;
    }

}