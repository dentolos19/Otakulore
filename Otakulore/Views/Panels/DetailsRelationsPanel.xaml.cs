using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core;
using Otakulore.Core.AniList;
using Otakulore.Models;
using Otakulore.Views.Pages;

namespace Otakulore.Views.Panels;

public sealed partial class DetailsRelationsPanel
{

    public DetailsRelationsPanel()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not MediaExtra media)
            return;
        foreach (var edge in media.Relations)
            RelationList.Items.Add(new MediaItemModel(edge.Details, edge.Relation.ToEnumDescription(true)));
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel item)
            App.NavigateFrame(typeof(DetailsPage), item.Media.Id);
    }

}