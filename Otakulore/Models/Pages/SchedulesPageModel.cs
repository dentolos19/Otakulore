using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore.Models;

[SingletonService]
public partial class SchedulesPageModel : BasePageModel
{

    private readonly ExternalService _externalService = MauiHelper.GetService<ExternalService>();

    [ObservableProperty] private ObservableCollection<MediaItemModel> _items = new();

    protected override void Initialize(object? args = null)
    {
        RefreshItemsCommand.Execute(null);
    }

    [RelayCommand]
    private async Task RefreshItems()
    {
        Items.Clear();
        var result = await _externalService.AniClient.GetMediaSchedulesAsync();
        foreach (var item in result.Data)
            Items.Add(MediaItemModel.Map(item));
    }

}