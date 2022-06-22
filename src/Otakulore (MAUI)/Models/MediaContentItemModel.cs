using CommunityToolkit.Mvvm.Input;
using Otakulore.Pages;
using Otakulore.Services;

namespace Otakulore.Models;

public partial class MediaContentItemModel
{

    public string Name { get; }
    public MediaContent Data { get; }
    public IProvider Provider { get; }

    public MediaContentItemModel(MediaContent data, IProvider provider)
    {
        Name = data.Name;
        Data = data;
        Provider = provider;
    }

    [ICommand]
    private async Task Open(MediaContent item)
    {
        await Shell.Current.GoToAsync(nameof(ContentViewerPage), new Dictionary<string, object>
        {
            { "data", Data },
            { "provider", Provider}
        });
    }

}