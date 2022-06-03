using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Services;

namespace Otakulore.Models;

public partial class SourceViewerViewModel : ObservableObject, IQueryAttributable
{

    private bool _alreadyAppliedQuery;

    [ObservableProperty] private string _title;
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private ObservableCollection<ContentItemModel> _items = new();

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!(query.ContainsKey("data") && query.ContainsKey("provider")) || _alreadyAppliedQuery)
            return;
        _alreadyAppliedQuery = true;
        var data = (MediaSource)query["data"];
        var provider = (IProvider)query["provider"];
        Title = data.Title;
        Task.Run(async () =>
        {
            IsLoading = true;
            var contents = await provider.GetContentAsync(data);
            IsLoading = false;
            foreach (var item in contents)
                Items.Add(new ContentItemModel(item, provider));
        });
    }

}