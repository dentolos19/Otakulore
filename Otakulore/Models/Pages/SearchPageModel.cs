using AniListNet;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Helpers;
using Otakulore.Pages;
using Otakulore.Services;

namespace Otakulore.Models;

[TransientService]
public partial class SearchPageModel : BasePageModel
{

    [ObservableProperty] private AccumulableCollection<MediaItemModel> _items = new();

    protected override void Initialize(object? args = null)
    {
        if (args is not string query)
            return;
        if (ParentPage is SearchPage page)
            page.SearchBox.Text = query;
        SearchCommand.Execute(query);
    }

    [RelayCommand]
    private Task Search(string query)
    {
        Items = new AccumulableCollection<MediaItemModel>();
        Items.AccumulationFunc += async index =>
        {
            var result = await DataService.Instance.Client.SearchMediaAsync(query, new AniPaginationOptions(index));
            return (
                result.HasNextPage,
                result.Data.Select(MediaItemModel.Map).ToList()
            );
        };
        Items.AccumulateCommand.Execute(null);
        return Task.CompletedTask;
    }

}