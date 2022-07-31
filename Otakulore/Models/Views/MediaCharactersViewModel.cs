using System.Collections.ObjectModel;
using AniListNet;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Services;

namespace Otakulore.Models;

public partial class MediaCharactersViewModel : ObservableObject, IQueryAttributable
{

    private readonly DataService _data = MauiHelper.GetService<DataService>();

    private int? _id;
    private int _currentPageIndex;
    private bool _hasNextPage = true;

    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private ObservableCollection<CharacterItemModel> _items = new();

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.ContainsKey("id"))
            return;
        if (query["id"] is not int id)
            return;
        _id = id;
        await Accumulate();
    }

    [ICommand]
    private async Task Accumulate()
    {
        if (!_id.HasValue)
            return;
        if (IsLoading || !_hasNextPage)
            return;
        IsLoading = true;
        var results = await _data.Client.GetMediaCharactersAsync(_id.Value, new AniPaginationOptions(++_currentPageIndex));
        if (results.Data is not { Length: > 0 })
        {
            _hasNextPage = false;
            IsLoading = false;
            return;
        }
        _hasNextPage = results.HasNextPage;
        foreach (var item in results.Data)
            Items.Add(new CharacterItemModel(item));
        IsLoading = false;
    }

}