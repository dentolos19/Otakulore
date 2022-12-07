using System.Collections.ObjectModel;
using AniListNet;
using AniListNet.Objects;
using AniListNet.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Humanizer;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore.Models;

[SingletonService]
public partial class ProfilePageModel : BasePageModel
{

    private int? _id;

    [ObservableProperty] private string _avatarUrl;
    [ObservableProperty] private string _username;

    [ObservableProperty] private MediaType _selectedType = MediaType.Anime;
    [ObservableProperty] private MediaEntryStatus _selectedStatus = MediaEntryStatus.Current;
    [ObservableProperty] private MediaEntrySort _selectedSort = MediaEntrySort.LastUpdated;

    [ObservableProperty] private ObservableCollection<MediaType> _types = new();
    [ObservableProperty] private ObservableCollection<MediaEntryStatus> _statuses = new();
    [ObservableProperty] private ObservableCollection<MediaEntrySort> _sorts = new();
    [ObservableProperty] private AccumulableCollection<MediaItemModel> _items = new();

    protected override void Initialize(object? args = null)
    {
        foreach (var @enum in (MediaType[])Enum.GetValues(typeof(MediaType)))
            Types.Add(@enum);
        foreach (var @enum in (MediaEntryStatus[])Enum.GetValues(typeof(MediaEntryStatus)))
            Statuses.Add(@enum);
        foreach (var @enum in (MediaEntrySort[])Enum.GetValues(typeof(MediaEntrySort)))
            Sorts.Add(@enum);
    }

    public override async void OnNavigatedTo()
    {
        await UpdateAuthenticationStatus();
        if (!DataService.Instance.Client.IsAuthenticated)
        {
            await ParentPage.DisplayAlert("Profile", "You need an AniList account to use this page.", "Close");
            return;
        }
        RefreshItemsCommand.Execute(null);
    }

    private async Task UpdateAuthenticationStatus()
    {
        if (DataService.Instance.Client.IsAuthenticated)
        {
            if (_id is not null)
                return;
            var user = await DataService.Instance.Client.GetAuthenticatedUserAsync();
            _id = user.Id;
            AvatarUrl = user.Avatar.LargeImageUrl.ToString();
            Username = user.Name;
        }
        else
        {
            _id = null;
            AvatarUrl = "anilist.png";
            Username = "AniList";
        }
    }

    [RelayCommand]
    private Task RefreshItems()
    {
        if (_id is null)
            return Task.CompletedTask;
        Items = new AccumulableCollection<MediaItemModel>();
        Items.AccumulationFunc += async index =>
        {
            var result = await DataService.Instance.Client.GetUserEntriesAsync(
                _id.Value,
                new MediaEntryFilter
                {
                    Type = SelectedType,
                    Status = SelectedStatus,
                    Sort = SelectedSort
                },
                new AniPaginationOptions(index)
            );
            return (
                result.HasNextPage,
                result.Data.Select(MediaItemModel.Map).ToList()
            );
        };
        Items.AccumulateCommand.Execute(null);
        return Task.CompletedTask;
    }

}