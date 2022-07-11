using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Core;

namespace Otakulore.Models;

public partial class ContentViewerViewModel : ObservableObject,  IQueryAttributable
{

    [ObservableProperty] private string _url;
    [ObservableProperty] private bool _isLoading = true;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!(query.ContainsKey("content") && query.ContainsKey("provider")))
            return;
        if (query["content"] is not MediaContent content || query["provider"] is not IProvider provider)
            return;
        if (provider is IAnimeProvider animeProvider)
        {
            var hasExtracted = await animeProvider.TryExtractVideoPlayerUrl(content, out var url);
            Url = hasExtracted ? url.ToString() : content.Data.ToString();
        }
        else
        {
            Url = content.Data.ToString();
        }
        IsLoading = false;
    }

}