using System.Linq;
using AniListNet;
using AniListNet.Objects;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Models;

namespace Otakulore.Views.Panels;

public sealed partial class DetailsCharactersPanel
{

    public IncrementalLoadingCollection<IncrementalSource<MediaItemModel>, MediaItemModel> Items { get; private set; }

    public DetailsCharactersPanel()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not Media media)
            return;
        var source = new IncrementalSource<MediaItemModel>(async (index, size) => (await App.Client.GetMediaCharactersAsync(media.Id, new AniPaginationOptions(index + 1, size))).Data.Select(character => new MediaItemModel(character)));
        Items = new IncrementalLoadingCollection<IncrementalSource<MediaItemModel>, MediaItemModel>(source, 100);
    }

}