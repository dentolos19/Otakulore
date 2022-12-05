using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Content;
using Otakulore.Content.Objects;
using Otakulore.Helpers;

namespace Otakulore.Models;

[TransientService]
public partial class SourceViewerPageModel : BasePageModel
{

    private IProvider _provider;
    private MediaSource _source;

    [ObservableProperty] private ObservableCollection<MediaContentItemModel> _items = new();

    protected override void Initialize(object? args = null)
    {
        if (args is not (IProvider provider, MediaSource source))
            return;
        _provider = provider;
        _source = source;
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