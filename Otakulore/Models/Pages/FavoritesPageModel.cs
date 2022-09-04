using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Core.Attributes;
using Otakulore.Services;

namespace Otakulore.Models;

[AsSingletonService]
public partial class FavoritesPageModel : ObservableObject
{

    private static readonly DataService _data = MauiHelper.GetService<DataService>();

    private int? _id;

    [ObservableProperty] private bool _isAnimeLoading;
    [ObservableProperty] private bool _isMangaLoading;
    [ObservableProperty] private ObservableCollection<MediaItemModel> _animeItems = new();
    [ObservableProperty] private ObservableCollection<MediaItemModel> _mangaItems = new();

    public async Task CheckAuthenticationStatus()
    {
        if (_data.Client.IsAuthenticated)
        {
            if (_id is not null)
                return;
            var user = await _data.Client.GetAuthenticatedUserAsync();
            _id = user.Id;
        }
        else
        {
            _id = null;
        }
        await RefreshCommand.ExecuteAsync(null);
    }

    [RelayCommand]
    private async Task Refresh()
    {
        await Task.Run(async () =>
        {
            AnimeItems.Clear();
            if (!_id.HasValue)
            {
                await Toast.Make("You need to login into AniList via the settings to access this feature.").Show();
                return;
            }
            IsAnimeLoading = true;
            var results = await _data.Client.GetUserAnimeFavoritesAsync(_id.Value);
            foreach (var item in results.Data)
                AnimeItems.Add(new MediaItemModel(item));
            IsAnimeLoading = false;
        });
        await Task.Run(async () =>
        {
            MangaItems.Clear();
            if (!_id.HasValue)
            {
                await Toast.Make("You need to login into AniList via the settings to access this feature.").Show();
                return;
            }
            IsMangaLoading = true;
            var results = await _data.Client.GetUserMangaFavoritesAsync(_id.Value);
            foreach (var item in results.Data)
                MangaItems.Add(new MediaItemModel(item));
            IsMangaLoading = false;
        });
    }

    [RelayCommand]
    private async Task SeeMoreAnime()
    {
        await Toast.Make("This feature is not implemented yet!").Show();
    }

    [RelayCommand]
    private async Task SeeMoreManga()
    {
        await Toast.Make("This feature is not implemented yet!").Show();
    }

}