using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Core;
using Otakulore.Services;

namespace Otakulore.Models;

public partial class AppShellViewModel : ObservableObject
{

    private readonly DataService _data = MauiHelper.GetService<DataService>();

    [ObservableProperty] private string _appVersion;
    [ObservableProperty] private string _rateRemaining;

    public AppShellViewModel()
    {
        _data.Client.RateChanged += (_, args) => RateRemaining = args.RateRemaining.ToString();
        AppVersion = Utilities.GetVersionString();
    }

}