using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Helpers;
using Otakulore.Pages;
using Otakulore.Services;

namespace Otakulore.Models;

[SingletonService]
public partial class SettingsPageModel : BasePageModel
{

    [ObservableProperty] private string _avatarUrl;
    [ObservableProperty] private string _username;
    [ObservableProperty] private string _loginButtonText;
    [ObservableProperty] private bool _isLoggedIn;

    [ObservableProperty] private ObservableCollection<ProviderItemModel> _providers = new();

    public override async void OnNavigatedTo() => await UpdateAuthenticationStatus();
    public override async void OnNavigatedFrom() => await UpdateAuthenticationStatus();

    protected override void Initialize(object? args = null)
    {
        foreach (var item in ContentService.Instance.Providers)
            Providers.Add(ProviderItemModel.Map(item));
    }

    private async Task UpdateAuthenticationStatus()
    {
        if (DataService.Instance.Client.IsAuthenticated)
        {
            if (_isLoggedIn)
                return;
            var user = await DataService.Instance.Client.GetAuthenticatedUserAsync();
            AvatarUrl = user.Avatar.LargeImageUrl.ToString();
            Username = user.Name;
            IsLoggedIn = true;
            LoginButtonText = "Logout";
        }
        else
        {
            AvatarUrl = "anilist.png";
            Username = "AniList";
            IsLoggedIn = false;
            LoginButtonText = "Login";
        }
    }

    [RelayCommand]
    private async Task Login()
    {
        if (IsLoggedIn)
        {
            SettingsService.Instance.AccessToken = null;
            DataService.Instance.ResetClient();
            await UpdateAuthenticationStatus();
        }
        else
        {
            await MauiHelper.Navigate(typeof(LoginPage));
        }
    }

}