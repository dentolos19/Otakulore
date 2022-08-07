using CommunityToolkit.Mvvm.Input;
using Otakulore.Core;
using Otakulore.Pages;

namespace Otakulore.Models;

public partial class ContentItemModel
{

    public string Name { get; }
    public MediaContent Content { get; }
    public IProvider Provider { get; }

    public ContentItemModel(MediaContent content, IProvider provider)
    {
        Name = content.Name;
        Content = content;
        Provider = provider;
    }

    [RelayCommand]
    private Task Open()
    {
        return MauiHelper.NavigateTo(
            typeof(ContentViewerPage),
            new Dictionary<string, object>
            {
                { "content", Content },
                { "provider", Provider }
            }
        );
    }

}