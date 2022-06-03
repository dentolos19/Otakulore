using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Services;

namespace Otakulore.Models;

public partial class SourceViewerViewModel : ObservableObject, IQueryAttributable
{

    [ObservableProperty] private string _currentAddress;
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private ContentItemModel? _selectedItem;
    [ObservableProperty] private ObservableCollection<ContentItemModel> _items = new();

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!(query.ContainsKey("data") && query.ContainsKey("provider")))
            return;
        var data = (MediaSource)query["data"];
        var provider = (IProvider)query["provider"];
        Task.Run(async () =>
        {
            IsLoading = true;
            var contents = await provider.GetContentAsync(data);
            IsLoading = false;
            foreach (var item in contents)
                Items.Add(new ContentItemModel(item));
            SelectedItem = Items.First();
        });
    }

    [ICommand]
    private async Task Navigate(ContentItemModel item)
    {
        CurrentAddress = item.Data.ToString();
    }

}