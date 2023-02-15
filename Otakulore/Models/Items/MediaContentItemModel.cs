using CommunityToolkit.Mvvm.Input;
using Otakulore.Content;
using Otakulore.Content.Objects;
using Otakulore.Pages;

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