using CommunityToolkit.Mvvm.Input;
using Otakulore.Core;
using Otakulore.Pages;

namespace Otakulore.Models;

public partial class SourceItemModel
{

    public string Name { get; }
    public MediaSource Source { get; }
    public IProvider Provider { get; }

    public SourceItemModel(MediaSource source, IProvider provider)
    {
        Name = source.Title;
        Source = source;
        Provider = provider;
    }

    [ICommand]
    private Task Open()
    {
        return MauiHelper.NavigateTo(
            typeof(SourceViewerPage),
            new Dictionary<string, object>
            {
                { "source", Source },
                { "provider", Provider }
            }
        );
    }

}