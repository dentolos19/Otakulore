using AniListNet.Objects;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Otakulore.Models;
using Otakulore.Views.Pages;

namespace Otakulore.Views.Panels;

public sealed partial class DetailsRelationsPanel
{

    public DetailsRelationsPanel()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not Media media)
            return;
        var relations = await App.Client.GetMediaRelationsAsync(media.Id);
        ProgressIndicator.IsActive = false;
        foreach (var edge in relations)
            RelationList.Items.Add(new MediaItemModel(edge));
    }

    private void OnItemClicked(object sender, ItemClickEventArgs args)
    {
        if (args.ClickedItem is MediaItemModel { Data: MediaEdge media })
            App.NavigateFrame(typeof(DetailsPage), media.Media.Id);
    }

}