using System.Collections.ObjectModel;
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
    [ObservableProperty] private ObservableCollection<ProviderItemModel> _providers = new();

    public SettingsViewModel(ResourceService resourceService, SettingsService settingsService, VariableService variableService)
    {
        _settings = settingsService;
        Credits = resourceService.Credits;
        foreach (var provider in variableService.Providers)
            Providers.Add(new ProviderItemModel(provider));
        ThemeIndex = _settings.ThemeIndex;
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