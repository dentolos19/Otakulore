using System.Collections.ObjectModel;
using AniListNet;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Services;

namespace Otakulore.Models;

public partial class SettingsViewModel : ObservableObject
{

    private readonly SettingsService _settings;

    [ObservableProperty] private int _themeIndex;
    [ObservableProperty] private string _credits;
    [ObservableProperty] private string _appVersion;
    [ObservableProperty] private string _rateRemaining = "Unknown";
    [ObservableProperty] private ObservableCollection<ProviderItemModel> _providers = new();

    public SettingsViewModel(AniClient client, ResourceService resourceService, SettingsService settingsService, VariableService variableService)
    {
        client.RateChanged += (_, args) => RateRemaining = args.RateRemaining.ToString();
            _settings = settingsService;
        foreach (var provider in variableService.Providers)
            Providers.Add(new ProviderItemModel(provider));
        ThemeIndex = _settings.ThemeIndex;
        Credits = resourceService.Credits;
        #if ANDROID
        AppVersion = $"{VersionTracking.CurrentVersion} ({VersionTracking.CurrentBuild})";
        #else
        var version = VersionTracking.CurrentVersion;
        var buildVersion = version.Split('.')[3];
        #if DEBUG
        buildVersion = "Debug";
        #endif
        AppVersion = $"{version.Remove(version.LastIndexOf("."))} ({buildVersion})";
        #endif
    }
    
    [ICommand]
    private async Task Update()
    {
        if (_settings.ThemeIndex != ThemeIndex)
        {
            _settings.ThemeIndex = ThemeIndex;
            await Toast.Make("Restart this app to take effect.").Show();
        }
    }

}