using CommunityToolkit.Mvvm.Input;
using Otakulore.Pages;
using Otakulore.Services;

namespace Otakulore.Models;

public partial class SourceItemModel
{

    public Uri ImageUrl { get; }
    public string Title { get; }
    public MediaSource Data { get; }
    public IProvider Provider { get; }

    public SourceItemModel(MediaSource data, IProvider provider)
    {
        ImageUrl = data.ImageUrl;
        Title = data.Title;
        Data = data;
        Provider = provider;
    }

    [ICommand]
    private async Task Open()
    {
        await Shell.Current.GoToAsync(nameof(SourceViewerPage), new Dictionary<string, object>
        {
            { "data", Data },
            { "provider", Provider}
        });
    }

}