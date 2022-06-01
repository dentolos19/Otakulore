using System.Collections.Generic;
using System.Linq;
using AniListNet;
using AniListNet.Models;
using AniListNet.Objects;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Models;
using Otakulore.Views.Pages;

namespace Otakulore.Views.Panels;

public sealed partial class SchedulePanel
{

    public IncrementalLoadingCollection<IncrementalSource<MediaItemModel>, MediaItemModel> Items { get; private set; }

    public SchedulePanel()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not KeyValuePair<MediaSeason, int>(var season, var year))
            return;
        var source = new IncrementalSource<MediaItemModel>(async (index, size) => (await App.Client.SearchMediaAsync(new SearchMediaFilter { Season = season, SeasonYear = year }, new AniPaginationOptions(index + 1, size))).Data.Select(media => new MediaItemModel(media)));
        Items = new IncrementalLoadingCollection<IncrementalSource<MediaItemModel>, MediaItemModel>(source, 100);
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel { Data: Media media })
            App.NavigateFrame(typeof(DetailsPage), media.Id);
    }

}