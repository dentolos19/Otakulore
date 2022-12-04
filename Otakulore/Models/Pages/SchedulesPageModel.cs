using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore.Models;

[SingletonService]
public partial class SchedulesPageModel : BasePageModel
{

    private readonly ExternalService _externalService = MauiHelper.GetService<ExternalService>();

    [ObservableProperty] private ObservableCollection<MediaScheduleItemModel> _items = new();

    public override async void Initialize(object? args = null)
    {
        var result = await _externalService.AniClient.GetMediaSchedulesAsync();
        foreach (var item in result.Data)
            Items.Add(MediaScheduleItemModel.Map(item));
    }

}