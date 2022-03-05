using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Core.AniList;
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
        var source = new IncrementalSource<MediaItemModel>(async (index, size) => (await App.Client.GetMediaBySeason(season, year, new AniPaginationOptions(index + 1, size))).Data.Select(media => new MediaItemModel(media)));
        Items = new IncrementalLoadingCollection<IncrementalSource<MediaItemModel>, MediaItemModel>(source);
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel { Media: Media media })
            App.NavigateFrame(typeof(DetailsPage), media.Id);
    }

}