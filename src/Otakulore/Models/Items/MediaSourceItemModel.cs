using CommunityToolkit.Mvvm.Input;
using Otakulore.Pages;
using Otakulore.Providers;
using Otakulore.Providers.Objects;

namespace Otakulore.Models;

public partial class MediaSourceItemModel
{
    public required Uri ImageUrl { get; init; }
    public required string Title { get; init; }
    public required (IProvider, MediaSource) Data { get; init; }

    [RelayCommand]
    private Task Interact()
    {
        return MauiHelper.Navigate(typeof(SourceViewerPage), Data);
    }

    public static MediaSourceItemModel Map(IProvider provider, MediaSource source)
    {
        return new MediaSourceItemModel
        {
            ImageUrl = source.ImageUrl,
            Title = source.Title,
            Data = (provider, source)
        };
    }
}