using CommunityToolkit.Mvvm.Input;
using Otakulore.Pages;
using Otakulore.Providers;
using Otakulore.Providers.Objects;

namespace Otakulore.Models;

public partial class MediaContentItemModel
{
    public required string Name { get; init; }
    public required (IProvider, MediaContent) Data { get; init; }

    [RelayCommand]
    private Task Interact()
    {
        return MauiHelper.Navigate(typeof(ContentViewerPage), Data);
    }

    public static MediaContentItemModel Map(IProvider provider, MediaContent content)
    {
        return new MediaContentItemModel
        {
            Name = content.Name,
            Data = (provider, content)
        };
    }
}