using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore.Models;

[SingletonService]
public partial class MainPageModel : BasePageModel
{

    [ObservableProperty] private int _rateRemaining;

    public MainPageModel()
    {
        DataService.Instance.Client.RateChanged += (_, args) => RateRemaining = args.RateRemaining;
    }

}