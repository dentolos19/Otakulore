using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Core;
using Otakulore.Core.Providers;

namespace Otakulore.Models;

public partial class SourceViewerViewModel : ObservableObject, IQueryAttributable
{

    [ObservableProperty] private bool _isLoading = true;
    [ObservableProperty] private ObservableCollection<ContentItemModel> _items = new();

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!(query.ContainsKey("source") && query.ContainsKey("provider")))
            return;
        if (query["source"] is not MediaSource source || query["provider"] is not IProvider provider)
            return;
        var contents = await provider.GetContents(source);
        foreach (var item in contents)
            Items.Add(new ContentItemModel(item, provider));
        IsLoading = false;
    }

}