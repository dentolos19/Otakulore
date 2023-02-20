using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Providers;
using Otakulore.Providers.Objects;
using Otakulore.Utilities.Attributes;

namespace Otakulore.Models;

[TransientService]
public partial class SourceViewerPageModel : BasePageModel
{
    [ObservableProperty] private ObservableCollection<MediaContentItemModel> _items = new();

    private IProvider _provider;
    private MediaSource _source;
    [ObservableProperty] private string _subtitle;

    [ObservableProperty] private string _title;

    protected override void Initialize(object? args = null)
    {
        if (args is not (IProvider provider, MediaSource source))
            return;
        _provider = provider;
        _source = source;
        Title = _source.Title;
        Subtitle = _provider.Name;
        RefreshItemsCommand.Execute(null);
    }

    [RelayCommand]
    private async Task RefreshItems()
    {
        Items.Clear();
        var results = await _provider.GetContents(_source);
        foreach (var item in results)
            Items.Add(MediaContentItemModel.Map(_provider, item));
    }
}