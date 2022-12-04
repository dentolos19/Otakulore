using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore.Models;

[TransientService]
public partial class SearchPageModel : BasePageModel
{

    private ExternalService _externalService = MauiHelper.GetService<ExternalService>();

    [ObservableProperty] private ObservableCollection<MediaItemModel> _items = new();

    public override void Initialize(object? args = null)
    {
        if (args is not string query)
            return;
        SearchCommand.Execute(query);
    }

    [RelayCommand]
    private async Task Search(string query)
    {
        Items.Clear();
        var result = await _externalService.AniClient.SearchMediaAsync(query);
        foreach (var item in result.Data)
            Items.Add(MediaItemModel.Map(item));
    }

}