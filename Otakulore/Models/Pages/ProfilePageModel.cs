using System.Collections.ObjectModel;
using AniListNet;
using AniListNet.Objects;
using AniListNet.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Pages;
using Otakulore.Services;
using Otakulore.Utilities;
using Otakulore.Utilities.Attributes;

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

    [ObservableProperty] private ObservableCollection<MediaType> _typeItems = new();
    [ObservableProperty] private ObservableCollection<MediaEntryStatus> _statusItems = new();
    [ObservableProperty] private ObservableCollection<MediaEntrySort> _sortItems = new();
    [ObservableProperty] private AccumulableCollection<MediaItemModel> _items = new();

    public ProfilePageModel()
    {
        foreach (var item in (MediaType[])Enum.GetValues(typeof(MediaType)))
            TypeItems.Add(item);
        foreach (var item in (MediaEntryStatus[])Enum.GetValues(typeof(MediaEntryStatus)))
            StatusItems.Add(item);
        foreach (var item in (MediaEntrySort[])Enum.GetValues(typeof(MediaEntrySort)))
            SortItems.Add(item);
    }

    public override async void OnNavigatedTo()
    {
        await UpdateAuthenticationStatus();
        RefreshItemsCommand.Execute(null);
        if (DataService.Instance.Client.IsAuthenticated)
            return;
        var wantLogin = await ParentPage.DisplayAlert(
            "Otakulore",
            "You need an AniList account to use this feature.",
            "Login",
            "Close"
        );
        if (wantLogin)
            await MauiHelper.Navigate(typeof(LoginPage));
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
        Items = new AccumulableCollection<MediaItemModel>();
        if (_id is null)
            return Task.CompletedTask;
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