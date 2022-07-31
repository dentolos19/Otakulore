using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Core;

namespace Otakulore.Models;

public partial class SourceViewerViewModel : ObservableObject, IQueryAttributable
{

    private bool _queryApplied;

    [ObservableProperty] private string _title;
    [ObservableProperty] private bool _isLoading = true;
    [ObservableProperty] private ObservableCollection<ContentItemModel> _items = new();

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (_queryApplied)
            return;
        _queryApplied = true;
        if (!(query.ContainsKey("source") && query.ContainsKey("provider")))
            return;
        if (query["source"] is not MediaSource source || query["provider"] is not IProvider provider)
            return;
        Title = source.Title;
        var contents = await provider.GetContents(source);
        if (contents is null)
        {
            IsLoading = false;
            return;
        }
        foreach (var item in contents)
            Items.Add(new ContentItemModel(item, provider));
        IsLoading = false;
    }

}