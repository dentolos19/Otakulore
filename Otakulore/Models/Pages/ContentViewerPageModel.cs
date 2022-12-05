using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Content;
using Otakulore.Content.Objects;
using Otakulore.Helpers;

namespace Otakulore.Models;

[TransientService]
public partial class ContentViewerPageModel : BasePageModel
{

    [ObservableProperty] private Uri _url;

    protected override async void Initialize(object? args = null)
    {
        if (args is not (IProvider provider, MediaContent content))
            return;
        if (provider is IAnimeProvider animeProvider)
        {
            var url = await animeProvider.ExtractVideoPlayerUrl(content);
            Url = url ?? content.Url;
            /*
            #if ANDROID
            Platform.CurrentActivity.Window.AddFlags(WindowManagerFlags.Fullscreen);
            Platform.CurrentActivity.RequestedOrientation = ScreenOrientation.Landscape;
            #endif
            */
        }
        else
        {
            Url = content.Url;
        }
    }

}