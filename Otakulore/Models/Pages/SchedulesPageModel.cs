using AniListNet;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Services;
using Otakulore.Utilities;
using Otakulore.Utilities.Attributes;

namespace Otakulore.Models;

[SingletonService]
public partial class SchedulesPageModel : BasePageModel
{

    [ObservableProperty] private AccumulableCollection<MediaItemModel> _items = new();

    protected override void Initialize(object? args = null)
    {
        RefreshItemsCommand.Execute(null);
    }

    [RelayCommand]
    private Task RefreshItems()
    {
        Items = new AccumulableCollection<MediaItemModel>();
        Items.AccumulationFunc += async index =>
        {
            var result = await DataService.Instance.Client.GetMediaSchedulesAsync(default, new AniPaginationOptions(index));
            return (
                result.HasNextPage,
                result.Data.Select(MediaItemModel.Map).ToList()
            );
        };
        Items.AccumulateCommand.Execute(null);
        return Task.CompletedTask;
    }

}