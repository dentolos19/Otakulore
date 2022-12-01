using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Helpers;
using Otakulore.Services;

namespace Otakulore.Models;

[TransientService]
public partial class MediaRelationsPageModel : ObservableObject, IQueryAttributable
{

    private readonly DataService _data = MauiHelper.GetService<DataService>();

    private bool _queryApplied;

    [ObservableProperty] private bool _isLoading = true;
    [ObservableProperty] private ObservableCollection<MediaRelationItemModel> _items = new();

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (_queryApplied)
            return;
        if (!query.ContainsKey("id"))
            return;
        if (query["id"] is not int id)
            return;
        var results = await _data.Client.GetMediaRelationsAsync(id);
        foreach (var item in results)
            Items.Add(new MediaRelationItemModel(item));
        IsLoading = false;
    }

}