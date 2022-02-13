using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Common.Collections;
using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core.AniList;
using Otakulore.Models;
using Otakulore.Views.Pages;

namespace Otakulore.Views.Panels;

public sealed partial class SchedulePanel
{

    public IncrementalLoadingCollection<Source, MediaItemModel> Items { get; private set; }

    public SchedulePanel()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is KeyValuePair<MediaSeason, int>(var season, var year))
            Items = new IncrementalLoadingCollection<Source, MediaItemModel>(new Source(season, year));
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel item)
            App.NavigateFrame(typeof(DetailsPage), item.Media);
    }

    public class Source : IIncrementalSource<MediaItemModel>
    {

        private readonly MediaSeason _season;
        private readonly int _year;

        public Source(MediaSeason season, int year)
        {
            _season = season;
            _year = year;
        }

        public async Task<IEnumerable<MediaItemModel>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = new())
        {
            return (await App.Client.GetMediaBySeason(_season, _year, new AniPaginationOptions(pageIndex + 1, pageSize))).Data.Select(media => new MediaItemModel(media));
        }

    }

}