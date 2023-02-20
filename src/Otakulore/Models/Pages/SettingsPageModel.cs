using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Pages;
using Otakulore.Services;
using Otakulore.Utilities.Attributes;
using Otakulore.Utilities.Enumerations;

namespace Otakulore.Models;

[SingletonService]
public partial class SettingsPageModel : BasePageModel
{
    [ObservableProperty] private string _avatarUrl;

    [ObservableProperty] private string _credits;
    [ObservableProperty] private bool _isLoggedIn;
    [ObservableProperty] private string _loginButtonText;
    [ObservableProperty] private ObservableCollection<ProviderItemModel> _providerItems = new();

    [ObservableProperty] private Theme _selectedTheme;

    [ObservableProperty] private ObservableCollection<Theme> _themeItems = new();
    [ObservableProperty] private string _username;

    public SettingsPageModel()
    {
        foreach (var item in (Theme[])Enum.GetValues(typeof(Theme)))
            ThemeItems.Add(item);
        foreach (var item in ContentService.Instance.Providers)
            ProviderItems.Add(ProviderItemModel.Map(item));
    }

    public override async void OnNavigatedTo()
    {
        await UpdateAuthenticationStatus();
    }

    public override async void OnNavigatedFrom()
    {
        await UpdateAuthenticationStatus();
    }

    protected override async void Initialize(object? args = null)
    {
        Credits = await MauiHelper.ReadTextAsset("Credits.txt");
        SelectedTheme = SettingsService.Instance.AppTheme;
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
    private async Task Update()
    {
        if (SettingsService.Instance.AppTheme != SelectedTheme)
        {
            App.UpdateTheme(SelectedTheme);
            SettingsService.Instance.AppTheme = SelectedTheme;
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