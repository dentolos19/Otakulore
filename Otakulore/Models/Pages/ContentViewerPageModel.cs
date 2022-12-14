using CommunityToolkit.Mvvm.ComponentModel;
using Otakulore.Content;
using Otakulore.Content.Objects;
using Otakulore.Helpers;
using Otakulore.Utilities.Attributes;

namespace Otakulore.Models;

[TransientService]
public partial class ContentViewerPageModel : BasePageModel
{

    [ObservableProperty] private Uri _url;

    protected override async void Initialize(object? args = null)
    {
        if (args is not (IProvider provider, MediaContent content))
            return;
        try
        {
            Url = provider switch
            {
                IAnimeProvider animeProvider => await animeProvider.ExtractAnimePlayerUrl(content),
                IMangaProvider mangaProvider => await mangaProvider.ExtractMangaReaderUrl(content),
                _ => new Uri("about:blank")
            };
        }
        catch
        {
            await ParentPage.DisplayAlert(
                "Otakulore",
                "An error occurred.",
                "Back"
            );
            await MauiHelper.NavigateBack();
        }
    }

    public override void OnNavigatedFrom()
    {
        Url = new Uri("about:blank");
    }

}