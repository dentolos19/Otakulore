using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Content;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore.Models;

[SingletonService]
public partial class AppShellModel : ObservableObject
{

    private readonly DataService _data = MauiHelper.GetService<DataService>();

    [ObservableProperty] private string _appVersion;
    [ObservableProperty] private string _rateRemaining;

    public AppShellModel()
    {
        _data.Client.RateChanged += (_, args) => RateRemaining = args.RateRemaining.ToString();
        AppVersion = Utilities.GetVersionString();
    }

}