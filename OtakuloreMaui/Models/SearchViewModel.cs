using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Core.AniList;

namespace Otakulore.Models;

public partial class SearchViewModel : ObservableObject, IQueryAttributable
{

    [ObservableProperty] private string _query;
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private ObservableCollection<MediaItemModel> _items = new();

    [ICommand]
    private async void Search(string query)
    {
        IsLoading = true;
        Items.Clear();
        var results = await App.Client.SearchMedia(new AniFilter { Query = _query });
        foreach (var item in results.Data)
            Items.Add(new MediaItemModel(item));
        IsLoading = false;
    }
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.ContainsKey("query"))
            return;
        Query = Uri.UnescapeDataString((string)query["query"]);
        Search(Query);
    }

}