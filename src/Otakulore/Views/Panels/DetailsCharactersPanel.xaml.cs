using Microsoft.UI.Xaml.Navigation;
using Otakulore.Core.AniList;
using Otakulore.Models;

namespace Otakulore.Views.Panels;

public sealed partial class DetailsCharactersPanel
{

    public DetailsCharactersPanel()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs args)
    {
        if (args.Parameter is not MediaExtra media)
            return;
        foreach (var character in media.Characters)
            CharacterList.Items.Add(new MediaItemModel(character));
    }

}