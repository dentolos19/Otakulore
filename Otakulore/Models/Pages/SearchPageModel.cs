using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Helpers;
using Otakulore.Pages;
using Otakulore.Services;

namespace Otakulore.Models;

[TransientService]
public partial class SearchPageModel : BasePageModel
{

    [ObservableProperty] private ObservableCollection<MediaItemModel> _items = new();

    protected override void Initialize(object? args = null)
    {
        if (args is not string query)
            return;
        if (ParentPage is SearchPage page)
            page.SearchBox.Text = query;
        SearchCommand.Execute(query);
    }

    [RelayCommand]
    private async Task Search(string query)
    {
        Items.Clear();
        var result = await DataService.Instance.Client.SearchMediaAsync(query);
        foreach (var item in result.Data)
            Items.Add(MediaItemModel.Map(item));
    }

}