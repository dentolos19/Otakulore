#if ANDROID
using Android.Content.PM;
using Android.Views;
#endif
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otakulore.Core;

namespace Otakulore.Models;

public partial class ContentViewerViewModel : ObservableObject,  IQueryAttributable
{

    [ObservableProperty] private Uri? _url;
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
            Url = hasExtracted ? url : content.Url;
            #if ANDROID
            Platform.CurrentActivity.Window.AddFlags(WindowManagerFlags.Fullscreen);
            Platform.CurrentActivity.RequestedOrientation = ScreenOrientation.Landscape;
            #endif
        }
        else
        {
            Url = content.Url;
        }
        IsLoading = false;
    }

    [ICommand]
    private Task Back()
    {
        #if ANDROID
        Platform.CurrentActivity.Window.ClearFlags(WindowManagerFlags.Fullscreen);
        Platform.CurrentActivity.RequestedOrientation = ScreenOrientation.Portrait;
        #endif
        Url = new Uri("https://dentolos19.github.io");
        return MauiHelper.NavigateBack();
    }

}