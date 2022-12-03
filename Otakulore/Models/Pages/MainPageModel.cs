using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore.Models;

[SingletonService]
public partial class MainPageModel : ObservableObject
{

    private readonly ExternalService _externalService = MauiHelper.GetService<ExternalService>();

    [ObservableProperty] private int _rateRemaining;

    public MainPageModel()
    {
        _externalService.AniClient.RateChanged += (_, args) => RateRemaining = args.RateRemaining;
    }

}