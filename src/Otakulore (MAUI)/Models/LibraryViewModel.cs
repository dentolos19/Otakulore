using System.Collections.ObjectModel;
using AniListNet;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Otakulore.Models;

public partial class LibraryViewModel : ObservableObject
{

    private int _id;

    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private ObservableCollection<MediaEntryItemModel> _items = new();

    public async void OnAfterAuthentication()
    {
        var user = await App.Client.GetAuthenticatedUserAsync();
        _id = user.Id;
        var results = await App.Client.GetUserEntriesAsync(_id, new AniPaginationOptions(1, 100));
        foreach (var entry in results.Data)
            Items.Add(new MediaEntryItemModel(entry));
    }

}