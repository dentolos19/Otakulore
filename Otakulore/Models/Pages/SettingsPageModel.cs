using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Content;
using Otakulore.Helpers;
using Otakulore.Pages;
using Otakulore.Services;

namespace Otakulore.Models;

[SingletonService]
public partial class SettingsPageModel : ObservableObject
{

    private readonly DataService _data = MauiHelper.GetService<DataService>();
    private readonly SettingsService _settings = MauiHelper.GetService<SettingsService>();

    [ObservableProperty] private string _avatarUrl;
    [ObservableProperty] private string _username;
    [ObservableProperty] private bool _isLoggedIn;
    [ObservableProperty] private int _themeIndex;
    [ObservableProperty] private string _credits;
    [ObservableProperty] private string _appVersion;
    [ObservableProperty] private string _rateRemaining = "Unknown";
    [ObservableProperty] private ObservableCollection<ProviderItemModel> _providers = new();

    public SettingsPageModel()
    {
        var resources = MauiHelper.GetService<ResourceService>();
        var variables = MauiHelper.GetService<VariableService>();
        _data.Client.RateChanged += (_, args) => RateRemaining = args.RateRemaining.ToString();
        foreach (var provider in variables.Providers)
            Providers.Add(new ProviderItemModel(provider));
        ThemeIndex = _settings.ThemeIndex;
        Credits = resources.Credits;
        AppVersion = Utilities.GetVersionString();
    }

    public async Task CheckAuthenticationStatus()
    {
        if (_data.Client.IsAuthenticated)
        {
            if (_isLoggedIn)
                return;
            var user = await _data.Client.GetAuthenticatedUserAsync();
            AvatarUrl = user.Avatar.LargeImageUrl.ToString();
            Username = user.Name;
            IsLoggedIn = true;
        }
        else
        {
            AvatarUrl = "anilist.png";
            Username = "AniList";
            IsLoggedIn = false;
        }
    }

    [RelayCommand]
    private async Task Update()
    {
        if (_settings.ThemeIndex != ThemeIndex)
        {
            _settings.ThemeIndex = ThemeIndex;
            await Toast.Make("Restart this app to take effect.").Show();
        }
    }

    [RelayCommand]
    private async Task Login()
    {
        if (IsLoggedIn)
        {
            _settings.AccessToken = null;
            _data.ResetService();
            await CheckAuthenticationStatus();
        }
        else
        {
            await MauiHelper.Navigate(typeof(LoginPage));
        }
    }

}